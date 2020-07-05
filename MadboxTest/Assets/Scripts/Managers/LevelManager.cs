using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelFinishedEvent(Player[] players);
    public event LevelFinishedEvent OnLevelFinished;

    public int PointsAmount = 5;
    public Player[] Players;

    private List<Player> finishedPlayers = new List<Player>();

    private void Start()
    {
        GameManager.Instance.SetUpNewLevel(this);


        Player[] newPlayers = new Player[GameManager.Instance.PlayerCount];
        for(int i = 0; i < GameManager.Instance.PlayerCount; i++)
        {
            Players[i].gameObject.SetActive(true);
            newPlayers[i] = Players[i];
        }

        Players = new Player[newPlayers.Length];
        newPlayers.CopyTo(Players, 0);

        if (Players.Length == 1)
        {
            Players[0].GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 1);
        }
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
