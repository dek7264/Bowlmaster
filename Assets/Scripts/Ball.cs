using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
        
    public Vector3 defaultLaunchVelocity;

    private Rigidbody ballRigidBody;
    private AudioSource audioSource;
    private bool hasLaunched;
    private Vector3 startPosition;

    public bool HasLaunched
    {
        get { return hasLaunched; }
        private set { hasLaunched = value; }
    }

	// Use this for initialization
	void Start () {
        SetComponents();
        startPosition = gameObject.transform.position;
        ResetBall();        
        //Launch(defaultLaunchVelocity);
	}

    // Update is called once per frame
	void Update () {
	
	}

    private void SetComponents()
    {
        ballRigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Launch(Vector3 launchVelocity)
    {
        if (ballRigidBody != null)
        {
            ballRigidBody.useGravity = true;
            ballRigidBody.velocity = launchVelocity;
            hasLaunched = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void ResetBall()
    {
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = Quaternion.identity;
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
        ballRigidBody.useGravity = false;
        hasLaunched = false;
    }
}
