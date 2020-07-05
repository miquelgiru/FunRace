using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    #endregion

    public int PlayerCount;
    public string[] SceneNames;

    private int indexLevel = 0;
    private Player[] players;
    private LevelManager currentLevel;
    [SerializeField] ScorePanel scorePanel;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void LoadNextLevel(int index)
    {
        SceneManager.LoadScene(SceneNames[index], LoadSceneMode.Single);
    }

    public void StartNewGame(int playerCount)
    {
        if (players != null)
            players = null;

        PlayerCount = playerCount;
        indexLevel = 0;


        LoadNextLevel(indexLevel);
    }

    public void SetUpNewLevel(LevelManager level)
    {
        currentLevel = level;
        currentLevel.OnLevelFinished += CurrentLevelFinished;
    }

    private void CurrentLevelFinished(Player[] players)
    {
        if (this.players == null)
        {
            this.players = new Player[players.Length];
            players.CopyTo(this.players, 0);
        }

        foreach(Player p in players)
        {
            foreach(Player globalP in this.players)
            {
                if (p.NumberPlayer == globalP.NumberPlayer)
                    globalP.Points += p.Points;
            }
        }

        StartCoroutine(ShowScore(players));
    }

    private IEnumerator ShowScore(Player[] players)
    {
        scorePanel.SetUpPanel(new List<Player>(players));
        scorePanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        scorePanel.gameObject.SetActive(false);

        if (indexLevel + 1 < SceneNames.Length)
            LoadNextLevel(++indexLevel);
        else
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
