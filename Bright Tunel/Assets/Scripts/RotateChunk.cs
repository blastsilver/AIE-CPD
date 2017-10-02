using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChunk : MonoBehaviour
{
    //the amout of times to rotate the chunk 90degrees
    public int      timesToRotate;
    //the time needed to rotate the chunk completely
    private float   m_timeToRotate = 0;
    //the amount of time currently spent rotating
    private float   m_timeSpentRotating;

    //the original rotation value of the chunk on the y axis
    float m_beginingRotation;
    
	void Start ()
    {
        //sets up all the values
        timesToRotate = Random.Range(1, 5);
        m_timeSpentRotating = m_timeToRotate = 1 * timesToRotate;
        m_timeSpentRotating += 1;
	}
	
	void Update ()
    {
        //function to rotate the chunk
        if (m_timeSpentRotating < m_timeToRotate)
        {
            m_timeSpentRotating += Time.deltaTime;
            
            //the math lerp function to get the value to set the rotation of the chunk
            float holder = Mathf.Lerp(m_beginingRotation, m_beginingRotation + (90 * timesToRotate), m_timeSpentRotating / m_timeToRotate);
            
            //seting the rotation of the chunk with the value created from our lerp
            transform.parent.parent.rotation = Quaternion.AngleAxis(holder,Vector3.up);
        }
    }

    //function to call to start chunk rotation
    public void RotateTheChunk()
    {
        m_timeSpentRotating = 0;
        m_beginingRotation = transform.parent.parent.rotation.eulerAngles.y;
    }
}
