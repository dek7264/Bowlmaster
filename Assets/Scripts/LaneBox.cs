using UnityEngine;
using System.Collections;

public class LaneBox : MonoBehaviour {

    //private PinSetter pinSetter;
    private PinCounter pinCounter;

	// Use this for initialization
	void Start () {
        //pinSetter = GameObject.FindObjectOfType<PinSetter>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
	}
	
	void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Ball>() != null)
        {
            //pinSetter.SetBallLeftBox(); 
            pinCounter.SetBallLeftBox();
        }
    }
}
