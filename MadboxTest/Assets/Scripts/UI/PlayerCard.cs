using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public Text PositionNumberText;
    public Text NameText;
    public Text ScoreText;


    public void SetupCard(int PositionNumber, string Name, int Score)
    {
        PositionNumberText.text = PositionNumber.ToString() + ".";
        NameText.text = Name;
        ScoreText.text = Score.ToString();
    }

}
