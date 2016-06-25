using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Ball ball;
    public PinSetter pinSetter;

    private const int INITIAL_PIN_COUNT = 10;
    private List<int> pinsKnockedDownList = new List<int>();
    private PinCounter pinCounter;
    private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
        if (ball == null)
        {
            ball = GameObject.FindObjectOfType<Ball>();
        }
        if (pinSetter == null)
        {
            pinSetter = GameObject.FindObjectOfType<PinSetter>();
        }
        if (scoreDisplay == null)
        {
            scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        }
        InitializePinCounter();
	}

    private void InitializePinCounter()
    {
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        pinCounter.SetMaxPinsForCurrentRoll(INITIAL_PIN_COUNT);
    }

    public void Bowl(int pinsKnockedDown)
    {
        try
        {
            //Register number of pins knocked down
            pinsKnockedDownList.Add(pinsKnockedDown);

            //Reset the ball
            ResetBall();

            //Get the Action for the pinSetter to Perform
            ActionMaster.Action nextAction = GetNextPinSetterAction();

            //Inform the PinCounter of it's maximum number of pins for the next roll
            SetPinCounterMaxPinsForCurrentRollBasedOnNextAction(nextAction, pinsKnockedDown);
         
            //Report pinsKnockedDown to the ActionMaster and get the Action the PinSetter needs to perform
            ReportActionToPinSetter(nextAction);
        }
        catch
        {
            Debug.LogWarning("GameManager failed when reporting action to PinSetter");
        }

        //Pass roll scores to ScoreDisplay
        try
        {
            scoreDisplay.FillRolls(pinsKnockedDownList);
        }
        catch
        {
            Debug.LogWarning("ScoreDisplay failed to FillScoreCard");
        }

        //Pass cumulative frame scores to ScoreDisplay
        scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(pinsKnockedDownList));
        
    }

    private ActionMaster.Action GetNextPinSetterAction()
    {
        ActionMaster.Action actionToPerform = ActionMaster.NextAction(pinsKnockedDownList);
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

    private void ResetBall()
    {
        ball.ResetBall();
    }

    //TODO Need ability to disable user interactivity until lane has been tidy/reset and everything has been set back up.
}
