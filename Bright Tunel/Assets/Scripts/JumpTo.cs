using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTo : MonoBehaviour
{
    //this determins how quickly the player jumps and how high the jump offset is
    public AnimationCurve jumpPath;

    //the start point is where the player starts the jump and is locked to when not moving
    public Transform StartPoint;
    //used for lerping purposes so we dont get weird movment from the player character when jumping
    public Vector3 StartPointPoint;
    //the end point of where the player is going
    public Transform EndPoint;

    //the time spent jumping allready
    float JumpTimer = 0;
    //how long it will take to jump
    float MaxJumpTime = 1;
    //used for lerping between start and end point
    Vector3 TempV;

    //wether we are jumping or not
    bool finishedJump;

    void Start ()
    {
        //set the max jump 
        JumpTimer = MaxJumpTime = jumpPath.keys[jumpPath.length - 1].time;
        finishedJump = true;
    }
	
	void Update ()
    {
        //if not jumping and we left click see if we clicked on a jump point
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
                    StartPointPoint = StartPoint.position;
                }
            }
        }

        //the jump logic
        if (JumpTimer < MaxJumpTime)
        {
            //increase the time spent jumping
            JumpTimer += Time.deltaTime;

            //the lerping between the two points
            TempV = Vector3.Lerp(StartPointPoint, EndPoint.position, JumpTimer / MaxJumpTime);
            //offset our y value with the value recived on the animation curve based on the time passed in
            TempV.y += jumpPath.Evaluate(JumpTimer);
            
            //update the players position with the vector created by the lerp
            transform.position = TempV;
            finishedJump = false;
        }
        else
        {
            //check if we just finished jump and set our new start point to the end and reset values
            if (finishedJump == false)
            {
                finishedJump = true;
                StartPoint = EndPoint;
                EndPoint = null;
                StartPoint.GetComponent<RotateChunk>().RotateTheChunk();
            }

            //lock us to the start point if we are not moving
            gameObject.transform.position = StartPoint.position;
        }
    }

}