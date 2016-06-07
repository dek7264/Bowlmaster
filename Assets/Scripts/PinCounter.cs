using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinCounter : MonoBehaviour {

    public Text standingPinText;
    public GameManager gameManager;

    private const int INITIAL_PIN_COUNT = 10;
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
        maxPinsThisRoll = INITIAL_PIN_COUNT;
        if (gameManager == null)
        {
            GameObject.FindObjectOfType<GameManager>();
        }
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

    private void ReportScoreToGameManager()
    {
        //Debug.Log(maxPinsThisRoll - lastStandingCount);
        gameManager.ReportPinsKnockedDown(maxPinsThisRoll - lastStandingCount);
    }

    private void ResetPinCounter()
    {
        lastStandingCount = -1;
        ballLeftBox = false;
    }
}
