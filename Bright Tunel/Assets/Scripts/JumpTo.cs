using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTo : MonoBehaviour
{
    public AnimationCurve jumpPath;

    public Transform StartPoint;
    public Transform EndPoint;

    float JumpTimer = 0;
    float MaxJumpTime = 1;
    Vector3 TempV;

    bool finishedJump;

    void Start ()
    {
        JumpTimer = MaxJumpTime = jumpPath.keys[jumpPath.length - 1].time;
        finishedJump = true;
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && JumpTimer >= MaxJumpTime)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.tag == "JumpBlock")
                {
                    EndPoint = hit.transform;
                    JumpTimer = 0;
                }
            }
        }

        if (JumpTimer < MaxJumpTime)
        {
            JumpTimer += Time.deltaTime;

            TempV = Vector3.Lerp(StartPoint.position, EndPoint.position, JumpTimer / MaxJumpTime);
            TempV.y += jumpPath.Evaluate(JumpTimer);

            Debug.Log(JumpTimer);

            transform.position = TempV;
            finishedJump = false;
        }
        else
        {
            if (finishedJump == false)
            {
                finishedJump = true;
                StartPoint = EndPoint;
                EndPoint = null;
            }
            gameObject.transform.position = StartPoint.position;
        }
    }

}
