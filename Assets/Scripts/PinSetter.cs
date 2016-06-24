using UnityEngine;

using System.Collections;

public class PinSetter : MonoBehaviour {
    
    public GameObject PinSet; //Pin Layout Prefab
    
    //private Ball ball;
    private Animator animator;
    //private ActionMaster actionMaster; //Must only have a single instance of ActionMaster
    
	// Use this for initialization
	void Start () {
        //ball = GameObject.FindObjectOfType<Ball>();
        animator = gameObject.GetComponent<Animator>();
        //actionMaster = new ActionMaster();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void PerformAction(ActionMaster.Action actionToPerform)
    {
        if (actionToPerform == ActionMaster.Action.Tidy)
        {
            Debug.Log("PinSetter performing Tidy");
            animator.SetTrigger("tidyTrigger");
        }
        else if (actionToPerform == ActionMaster.Action.Reset || actionToPerform == ActionMaster.Action.EndTurn)
        {
            Debug.Log("PinSetter performing Reset");
            animator.SetTrigger("resetTrigger");
        }
        else if (actionToPerform == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle EndGame!");
        }
    }

    public void RaisePins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Raise();
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    public void RenewPins()
    {
        GameObject newPins = Instantiate(PinSet);
        newPins.transform.position = new Vector3(0, 0, 0);
    }
}
