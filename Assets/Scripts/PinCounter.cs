using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinCounter : MonoBehaviour {

    public Text standingPinText;
    
    private GameManager gameManager;
    private bool ballLeftBox = false;
    private int lastStandingCount = -1;
    private float lastChangeTime;
    private int maxPinsThisRoll;

	// Use this for initialization
	void Start () {
        if (standingPinText == null)
        {
            GameObject.Find("Pin Count");
        }
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        standingPinText.text = CountStanding().ToString(); //TODO Remove this. Don't have this update constantly. Set initially in Start and then only in PinsHaveSettled

        if (ballLeftBox == true)
        {
            standingPinText.color = Color.red;
            UpdateStandingCountAndSettle();
        }
	}

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Ball>() != null)
        {
            ballLeftBox = true;
        }
    }

    int CountStanding()
    {
        int standingPinCount = 0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding() == true)
            {
                standingPinCount++;
            }
        }
        return standingPinCount;
    }

    void UpdateStandingCountAndSettle()
    {
        //Update the lastStandingCount
        int standingCount = CountStanding();

        if (lastStandingCount != standingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = standingCount;
        }
        else
        {
            if ((Time.time - lastChangeTime) >= 3f)
            {
                PinsHaveSettled();
            }
        }
    }

    void PinsHaveSettled()
    {
        //Report to the ActionMaster and perform the necessary action
        ReportScoreToGameManager();
        //Reset the pinsetter
        ResetPinCounter();
        standingPinText.color = Color.green;
    }

    public void SetBallLeftBox()
    {
        ballLeftBox = true;
    }

    public void SetMaxPinsForCurrentRoll(int maxPins)
    {
        maxPinsThisRoll = maxPins;
    }

    private void ReportScoreToGameManager()
    {
        gameManager.Bowl(maxPinsThisRoll - lastStandingCount);
    }

    private void ResetPinCounter()
    {
        lastStandingCount = -1;
        ballLeftBox = false;
    }
}
