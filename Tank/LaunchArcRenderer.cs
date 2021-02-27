using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArcRenderer : MonoBehaviour
{
    public GameObject m_LineRenderer;

    [HideInInspector] public float velocity;
    [HideInInspector] public float angle;
    [HideInInspector] public float xConstant;
    public int resolution;
    [HideInInspector] public float height;
    [HideInInspector] public Vector3 tankPosition;
    public LayerMask m_Collider;

    float g; //force of gravity on the y axis
    float radianAngle;

    LineRenderer lr;

    private void Awake()
    {
        lr = m_LineRenderer.GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void OnValidate()
    {
        if(lr!=null && Application.isPlaying){
            RenderArc();
        }
    }

    // Start is called before the first frame update
    void LateUpdate()
    {
        RenderArc();
    }

    //initialization
    void RenderArc()
    {
        // obsolete: lr.SetVertexCount(resolution + 1);
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());
    }
    //Create an array of Vector 3 positions for the arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;

        float vx = velocity*Mathf.Cos(radianAngle);
        float vy = velocity*Mathf.Sin(radianAngle);
        float sqrt = Mathf.Sqrt(((vy*vy)/(g*g)) + ((2*height)/g));
        float maxDistance = vx*((vy/g) + sqrt);
        

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);            

            RaycastHit hit;
            // int layer = (1 << 8);
            if (i > 1)
            {
                // Debug.Log(hit.point);
                float thetad = m_LineRenderer.transform.eulerAngles.y;
                float theta  = Mathf.Deg2Rad * thetad;
                float checka = Mathf.Deg2Rad * 8f;

                float checkptyii = arcArray[i-1].z * Mathf.Tan(checka) - ((g * arcArray[i-1].z * arcArray[i-1].z) / (2 * velocity * velocity * Mathf.Cos(checka) * Mathf.Cos(checka)));
                float checkptyi = arcArray[i].z * Mathf.Tan(checka) - ((g * arcArray[i].z * arcArray[i].z) / (2 * velocity * velocity * Mathf.Cos(checka) * Mathf.Cos(checka)));

                Vector3 checkIIrespect = new Vector3((arcArray[i-1].z)*Mathf.Sin(theta),checkptyii,(arcArray[i-1].z)*Mathf.Cos(theta));
                Vector3 checkIrespect = new Vector3((arcArray[i].z)*Mathf.Sin(theta),checkptyi,(arcArray[i].z)*Mathf.Cos(theta));
                Vector3 checkII = checkIIrespect + m_LineRenderer.transform.position;
                Vector3 checkI = checkIrespect + m_LineRenderer.transform.position;

                if (Physics.Linecast(checkII,checkI,out hit,m_Collider.value))
                {
                    // Debug.Log("IamHere");
                    for (int j = i+1; j <= resolution; j++)
                    {
                        arcArray[j] = arcArray[i];
                    }
                    i = resolution +1;
                    
                }
                
            }

        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        // float z = x + xConstant;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(0,y,x);
    }
}