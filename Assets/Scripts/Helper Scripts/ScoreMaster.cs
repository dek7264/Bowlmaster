using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScoreMaster {

    //Returns a list of individual frame scores, NOT cumulative
    public static  List<int> ScoreFrames (List<int> rollList)
    {
        List<int> frameList = new List<int>();

        int frameScore = 0;
        bool frameIsClosed = false;
        bool isFirstRoll = true;

        for (int rollIndex = 0; rollIndex < rollList.Count; rollIndex++)
        {
            frameScore += rollList[rollIndex];
            if (frameScore >= 10)
            {
                if (isFirstRoll == true && rollList.Count - 1 >= rollIndex + 2)
                {
                    frameScore += rollList[rollIndex + 1];
                    frameScore += rollList[rollIndex + 2];
                    frameIsClosed = true;
                }
                else if (isFirstRoll == false && rollList.Count - 1 >= rollIndex + 1)
                {
                    frameScore += rollList[rollIndex + 1];
                    if (frameList.Count < 10 || rollIndex == 20)
                    {
                        frameIsClosed = true;
                    }
                }
            }
            else if (isFirstRoll == false)
                frameIsClosed = true;
            if (frameIsClosed == true)
            {
                frameList.Add(frameScore);
                if (frameList.Count >= 10)
                    break;
                frameScore = 0;
                isFirstRoll = true;
                frameIsClosed = false;
            }
            else
                isFirstRoll = false;
        }

        return frameList;
    }

    //Returns a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative (List<int> rolls)
    {
        List<int> cumulativeScore = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls))
        {
            runningTotal += frameScore;
            Debug.Log("Adding cumulative frame score to cumulativeScore: " + frameScore.ToString());
            cumulativeScore.Add(runningTotal);
        }
        Debug.Log("Returning cumulativeScore with item count: " + cumulativeScore.Count.ToString());
        return cumulativeScore;
    }
}
