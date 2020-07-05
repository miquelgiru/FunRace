using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public int PlayerCount = 1;

    public void Play()
    {
        GameManager.Instance.StartNewGame(PlayerCount);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    public void AddPlayer(Text t)
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        if (PlayerCount == 2)
            return;

        PlayerCount++;
        t.text = PlayerCount.ToString();
    }

    public void RemovePlayer(Text t)
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        if (PlayerCount == 1)
            return;

        PlayerCount--;
        t.text = PlayerCount.ToString();
    }

}
