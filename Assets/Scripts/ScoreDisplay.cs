using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollScores = new Text[21];
    public Text[] frameScores = new Text[10];

	public void FillRolls(List<int> rolls)
    {
        string formattedRollScores = FormatRolls(rolls);
        for (int i = 0; i < formattedRollScores.Length; i++)
        {
            rollScores[i].text = formattedRollScores[i].ToString();
        }
    }

    public void FillFrames(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameScores[i].text = frames[i].ToString();
        }
    }

    public static string FormatRolls(List<int> rolls)
    {
        string output = "";
        bool canRollStrike = true;

        string debugOutput = "";
        foreach (int pinFalls in rolls)
        {
            debugOutput += "," + pinFalls.ToString();
        }
        Debug.Log(debugOutput);

        for (int i = 0; i < rolls.Count; i++)
        {
            if (canRollStrike == true && rolls[i] == 10)
            {
                //Strike
                output += "X";
                if (output.Length < 18)
                {
                    output += " ";
                }
                canRollStrike = true;
            }
            else if (canRollStrike == false && rolls[i-1] + rolls[i] == 10)
            {
                //Spare
                output += "/";
                canRollStrike = true;
            }
            else
            {
                //Normal roll
                if (rolls[i] == 0)
                {
                    output += "-";
                }
                else
                {
                    output += rolls[i].ToString();
                }

                if (output.Length % 2 == 0 && output.Length != 20)
                {
                    canRollStrike = true;
                }
                else
                {
                    canRollStrike = false;
                }
            }
        }

        return output;
    }
}
