using UnityEngine;
using System.Collections;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};
    public int roll = 1;

    private int[] scoreEachRoll = new int[21];
    //private bool playerGetsThirdRollOnLastFrame = false;

	public Action BowlAndReturnActionToPerform (int pins)
    {
        //Validate that we received a valid number of pins hit
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("Invalid pin amount sent to ActionMaster.Bowl!");
        }

        //Check if this is the first roll of the frame
        bool isFirstRoll = IsFirstRollOfFrame();
        //Check if this is the final frame
        bool isFinalFrame = IsFinalFrame();

        //Record the score for the roll in the scoreEachRoll array
        scoreEachRoll[roll - 1] = pins;

        //Special logic for the last frame
        if (isFinalFrame == true)
        {
            return ProcessFinalFrame(pins);
        }
        else
        {
            //Frames 1 through 9
            //If first bowl of frame is less than 10, return tidy else return end turn
            if (isFirstRoll == true)
            {
                //First bowl in a frame (or last frame)
                if (pins == 10)
                {
                    //Strike was bowled. Increment the bowl counter by 2 and end the turn.
                    roll += 2;
                    return Action.EndTurn;
                }
                else
                {
                    roll += 1;
                    return Action.Tidy;
                }
            }
            else
            {
                //Second bowl in a frame
                roll += 1;
                return Action.EndTurn;
            }
        }

        //throw new UnityException("Not sure what action to return!");
    }

    private Action ProcessFinalFrame(int pins)
    {
        switch (roll)
        {
            case 19:
                //Player always gets to roll ball 20 whether they just rolled a strike or not
                roll += 1;
                if (pins == 10)
                {
                    return Action.Reset;
                }
                else
                {
                    return Action.Tidy;
                }
            case 20:
                roll += 1;
                int scoreForRoll19And20 = pins + scoreEachRoll[roll - 2];
                if (scoreForRoll19And20 >= 10)
                {
                    //Player gets to roll a third ball
                    //Determine if we need to Tidy or Reset
                    if (pins == 10)
                    {
                        //Player got a strike or spare on this roll. Reset the lane
                        return Action.Reset;
                    }
                    else
                    {
                        //Player got a strike on roll 19 but did not clear the lane on roll 20. Tidy the lane
                        return Action.Tidy;
                    }
                }
                else
                {
                    //Player does not get to roll a third ball
                    //return Action.EndTurn;
                    return Action.EndGame;
                }
            case 21:
                //Player rolled the final ball of their game
                roll += 1;
                //return Action.EndTurn;
                return Action.EndGame;
            default:
                throw new UnityException("Roll must be 19, 20, or 21 to get here. How did this happen?!");
        }
    }

    private bool IsFirstRollOfFrame()
    {
        if (roll % 2 == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool IsFinalFrame()
    {
        if (roll >= 19)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
