using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChunk : MonoBehaviour
{
    private float m_amountToRotate;
    private float m_timeToRotate;
    private float m_timeSpentRotating;

    private float m_startAngle;

    private Transform m_parentObject;

    private float m_toTurn;

	void Start ()
    {
        m_amountToRotate = Random.Range(1,5);
        m_timeSpentRotating = m_timeToRotate = 1 * m_amountToRotate;
        m_parentObject = transform.parent.parent;
	}
	
	void Update ()
    {
        if (m_timeSpentRotating < m_timeToRotate)
        {
            m_timeSpentRotating += Time.deltaTime;

            m_toTurn = Mathf.Lerp(m_startAngle, m_startAngle + (90 * m_amountToRotate), m_timeSpentRotating / m_timeToRotate);

            m_parentObject.rotation = Quaternion.AngleAxis(m_toTurn, Vector3.up);
        }
	}

    public void RotateTheChunk()
    {
        m_startAngle = m_parentObject.rotation.eulerAngles.y;
        m_timeSpentRotating = 0;
    }
}
