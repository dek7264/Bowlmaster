using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Ball ball;
    public PinSetter pinSetter;

    private List<int> pinsKnockedDownList = new List<int>();
    private ActionMaster actionMaster = new ActionMaster();
    private ScoreMaster scoreMaster = new ScoreMaster();

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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReportPinsKnockedDown(int pinsKnockedDown)
    {
        Debug.Log("pinsKnockedDown " + pinsKnockedDown);
        //Register number of pins knocked down
        pinsKnockedDownList.Add(pinsKnockedDown);

        //Report pinsKnockedDown to the ActionMaster and get the Action the PinSetter needs to perform
        ReportActionToPinSetter(pinsKnockedDown);

        //Pass pinsKnockedDownList to ScoreMaster so it can generate the final frame scores to report to the ScoreDisplay
        GetFrameScores();

        //Reset the ball
        ResetBall();
    }

    private void ReportActionToPinSetter(int pinsKnockedDown)
    {
        ActionMaster.Action actionToPerform = actionMaster.BowlAndReturnActionToPerform(pinsKnockedDown);
        Debug.Log("ActionToPerform: " + actionToPerform);
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
