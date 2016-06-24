using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};
    private int roll = 1;

    //private int[] scoreEachRoll = new int[21];
    //private bool playerGetsThirdRollOnLastFrame = false;

	public Action GetNextAction (List<int> pinList)
    {
        //TODO Account for second roll in a frame to be 0 if player rolled a strike on the first roll. For Loop?
        Action actionToReturn = new Action();
        bool isFinalFrame = false;
        foreach (int pinFall in pinList)
        {
            //Get the pins knocked down on the most recent roll
            //int pinsHitLastRoll = pinList[roll - 1];

            //Validate that we received a valid number of pins hit
            if (pinFall < 0 || pinFall > 10)
            {
                throw new UnityException("Invalid pin amount sent to ActionMaster.Bowl!");
            }

            //Check if this is the final frame
            if (isFinalFrame == false)
            {
                isFinalFrame = IsFinalFrame();
            }

            //Special logic for the last frame
            if (isFinalFrame == true)
            {
                actionToReturn = ProcessFinalFrame(pinList, pinFall);
            }
            else
            {
                //Frames 1 through 9
                //Check if this is the first roll of the frame
                bool isFirstRoll = IsFirstRollOfFrame();

                //If first bowl of frame is less than 10, return tidy else return end turn
                if (isFirstRoll == true)
                {
                    //First bowl in a frame (or last frame)
                    if (pinFall == 10)
                    {
                        //Strike was bowled. Increment the bowl counter by 2 and end the turn.
                        roll += 2;
                        actionToReturn = Action.EndTurn;
                    }
                    else
                    {
                        roll += 1;
                        actionToReturn = Action.Tidy;
                    }
                }
                else
                {
                    //Second bowl in a frame
                    roll += 1;
                    actionToReturn = Action.EndTurn;
                }
            }
        }
        //throw new UnityException("Not sure what action to return!");
        return actionToReturn;
    }

    private Action ProcessFinalFrame(List<int> pinList, int pinsHitLastRoll)
    {
        switch (roll)
        {
            case 19:
                //Player always gets to roll ball 20 whether they just rolled a strike or not
                roll += 1;
                if (pinsHitLastRoll == 10)
                {
                    return Action.Reset;
                }
                else
                {
                    return Action.Tidy;
                }
            case 20:
                roll += 1;
                int scoreForRoll19And20 = pinsHitLastRoll + pinList[pinList.Count - 2];
                if (scoreForRoll19And20 >= 10)
                {
                    //Player gets to roll a third ball
                    //Determine if we need to Tidy or Reset
                    if (pinsHitLastRoll == 10)
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
                //roll += 1;
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
