using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollScores = new Text[21];
    public Text[] frameScores = new Text[10];

	public void FillScoreCard(List<int> rolls)
    {
        for (int i = 0; i < rolls.Count - 1; i++)
        {
            rollScores[i].text = rolls[i].ToString();
        }
    }
}
