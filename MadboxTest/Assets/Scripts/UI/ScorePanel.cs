using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] PlayerCard[] cards;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetUpPanel(List<Player> players)
   {
        players.Sort((p1, p2) => p1.Points.CompareTo(p2.Points));

        for(int i = 0; i < players.Count; i++)
        {
            cards[i].SetupCard(i + 1, players[i].NumberPlayer.ToString(), (int)players[i].Points);
            cards[i].gameObject.SetActive(true);
        }
    }
}
