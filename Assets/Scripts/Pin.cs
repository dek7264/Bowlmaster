using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;
    public float distanceToRaise = 40f;

    private Rigidbody rigidBody;
    
	// Use this for initialization
	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(name + " " + IsStanding().ToString());
	}

    public bool IsStanding()
    {
        Vector3 rotationInEuler = gameObject.transform.rotation.eulerAngles;
        float tiltInX = Mathf.Abs(rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        if ((tiltInX < standingThreshold || (360f - tiltInX) < standingThreshold) && (tiltInZ < standingThreshold || (360f - tiltInZ) < standingThreshold))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Raise()
    {
        if (IsStanding() == true)
        {
            rigidBody.useGravity = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.Translate(new Vector3(0f, distanceToRaise, 0f), Space.World);
        }
    }

    public void Lower()
    {
        gameObject.transform.Translate(new Vector3(0f, -distanceToRaise, 0f), Space.World);
        rigidBody.useGravity = true;
    }
}
