using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Ball ball;
    public PinSetter pinSetter;

    private const int INITIAL_PIN_COUNT = 10;
    private List<int> pinsKnockedDownList = new List<int>();
    private ActionMaster actionMaster = new ActionMaster();
    private ScoreMaster scoreMaster = new ScoreMaster();
    private PinCounter pinCounter;

	// Use this for initialization
	void Start () {
        if (ball == null)
        {
            GameObject.FindObjectOfType<Ball>();
        }
        if (pinSetter == null)
        {
            GameObject.FindObjectOfType<PinSetter>();
        }
        InitializePinCounter();
	}

    private void InitializePinCounter()
    {
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        pinCounter.SetMaxPinsForCurrentRoll(INITIAL_PIN_COUNT);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Bowl(int pinsKnockedDown)
    {
        Debug.Log("pinsKnockedDown " + pinsKnockedDown);
        //Register number of pins knocked down
        pinsKnockedDownList.Add(pinsKnockedDown);

        //Get the Action for the pinSetter to Perform
        ActionMaster.Action nextAction = GetNextPinSetterAction();

        //Inform the PinCounter of it's maximum number of pins for the next roll
        SetPinCounterMaxPinsForCurrentRollBasedOnNextAction(nextAction, pinsKnockedDown);

        //Report pinsKnockedDown to the ActionMaster and get the Action the PinSetter needs to perform
        ReportActionToPinSetter(nextAction);

        //Pass pinsKnockedDownList to ScoreMaster so it can generate the final frame scores to report to the ScoreDisplay
        GetFrameScores();

        //Reset the ball
        ResetBall();
    }

    private ActionMaster.Action GetNextPinSetterAction()
    {
        ActionMaster.Action actionToPerform = actionMaster.GetNextAction(pinsKnockedDownList);
        Debug.Log("ActionToPerform: " + actionToPerform);
        return actionToPerform;
    }

    private void SetPinCounterMaxPinsForCurrentRollBasedOnNextAction(ActionMaster.Action nextAction, int pinsKnockedDown)
    {
        if (nextAction == ActionMaster.Action.Tidy)
        {
            pinCounter.SetMaxPinsForCurrentRoll(INITIAL_PIN_COUNT - pinsKnockedDown);
        }
        else if (nextAction == ActionMaster.Action.Reset || nextAction == ActionMaster.Action.EndTurn)
        {
            pinCounter.SetMaxPinsForCurrentRoll(INITIAL_PIN_COUNT);
        }
        else if (nextAction == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle EndGame!");
        }
    }
    private void ReportActionToPinSetter(ActionMaster.Action actionToPerform)
    {
        pinSetter.PerformAction(actionToPerform);
    }

    private void GetFrameScores()
    {
        List<int> frameScores = scoreMaster.ScoreFrames(pinsKnockedDownList);
        string frameScoreOutput = "";
        for (int i = 0; i < frameScores.Count; i++)
        {
            frameScoreOutput += "Frame " + (i + 1).ToString() + ": " + frameScores[i];
        }
        Debug.Log(frameScoreOutput);
    }

    private void ResetBall()
    {
        ball.ResetBall();
    }

    //TODO Need ability to disable user interactivity until lane has been tidy/reset and everything has been set back up.
}
