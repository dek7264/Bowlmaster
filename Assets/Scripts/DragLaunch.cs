using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private GameObject lane;
    private float dragStartTime;
    private Vector3 dragStartPos;
    //private float dragEndTime;
    //private Vector3 dragEndPos;

	// Use this for initialization
	void Start () {
        ball = gameObject.GetComponent<Ball>();
        lane = GameObject.FindGameObjectWithTag("Floor");
	}

    public void DragStart()
    {
        if (ball.HasLaunched == false)
        {
            //Capture time and position of drag start/mouse click
            dragStartTime = GetCurrentTime();
            dragStartPos = GetCurrentMousePosition();
        }
    }

    public void DragEnd()
    {
        if (ball.HasLaunched == false)
        {
            //Launch the ball
            float dragEndTime = GetCurrentTime();
            Vector3 dragEndPos = GetCurrentMousePosition();

            float deltaX = dragEndPos.x - dragStartPos.x;
            float deltaZ = dragEndPos.y - dragStartPos.y;
            float deltaTime = dragEndTime - dragStartTime;

            Vector3 launchVelocity = new Vector3(deltaX / deltaTime, 0f, deltaZ / deltaTime);
            ball.Launch(launchVelocity);
        }
    }

    private float GetCurrentTime()
    {
        return Time.time;
    }

    private Vector3 GetCurrentMousePosition()
    {
        return Input.mousePosition;
    }

    public void MoveStart(float xNudge)
    {
        if (ball.HasLaunched == false)
        {
            //Debug.Log("Moving " + xNudge);
            //ball.transform.position += new Vector3(xNudge, 0f, 0f);
            //ball.transform.Translate(new Vector3(xNudge, 0f, 0f));
            float newX = Mathf.Clamp(ball.transform.position.x + xNudge, lane.transform.lossyScale.x / -2, lane.transform.lossyScale.x / 2);
            ball.transform.position = new Vector3(newX, ball.transform.position.y, ball.transform.position.z);
        }
    }
}
