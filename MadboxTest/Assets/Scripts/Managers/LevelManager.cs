using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelFinishedEvent(Player[] players);
    public event LevelFinishedEvent OnLevelFinished;

    public int PointsAmount = 5;
    public Player[] Players;

    private List<Player> finishedPlayers;

    private void Start()
    {
        GameManager.Instance.SetUpNewLevel(this);
    }

    private void OnEnable()
    {
        foreach(Player p in Players)
        {
            p.OnPlayerFinished += SetPointsToPlayer;
        }
    }

    private void SetPointsToPlayer(Player p)
    {

        int multiplier = Players.Length - finishedPlayers.Count;
        p.Points += PointsAmount * multiplier;

        finishedPlayers.Add(p);

        if (Players.Length == finishedPlayers.Count)
            OnLevelFinished?.Invoke(Players);
    }
}
