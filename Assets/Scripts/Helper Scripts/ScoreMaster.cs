using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster {

    //Returns a list of individual frame scores, NOT cumulative
    public List<int> ScoreFrames (List<int> rolls)
    {
        List<int> frameList = new List<int>();

        //Code here - 19 new lines?


        return frameList;
    }

    //Returns a list of cumulative scores, like a normal score card.
    public List<int> ScoreCumulative (List<int> rolls)
    {
        List<int> cumulativeScore = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls))
        {
            runningTotal += frameScore;
            cumulativeScore.Add(runningTotal);
        }

        return cumulativeScore;
    }
}
