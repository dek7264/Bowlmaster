using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Ball ball;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
	    if (ball == null)
        {
            ball = GameObject.FindObjectOfType<Ball>();
        }

        //offset = new Vector3(0, 50, -100);
        offset = transform.position - ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (ball.transform.position.z < 1829f)
        {
            gameObject.transform.position = ball.transform.position + offset;
        }
	}
}
