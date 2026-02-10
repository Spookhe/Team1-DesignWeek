using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamSelectionManager : MonoBehaviour
{
    public static TeamSelectionManager Instance;

    public Button startButton;
    public string gameSceneName;

    private List<PlayerSelectionUI> players = new();
    public IReadOnlyList<PlayerSelectionUI> Players => players;

    void Awake()
    {
        Instance = this;
        startButton.interactable = false;
    }

    public void Register(PlayerSelectionUI player)
    {
        if (!players.Contains(player))
            players.Add(player);
    }

    public void CheckReady()
    {
        if (players.Count < 2)
        {
            startButton.interactable = false;
            return;
        }

        foreach (var p in players)
        {
            if (!p.ready)
            {
                startButton.interactable = false;
                return;
            }
        }

        startButton.interactable = true;
    }

    public void StartGame()
    {
        GameSession.Teams[TeamSide.Left].Clear();
        GameSession.Teams[TeamSide.Right].Clear();

        foreach (var p in players)
        {
            if (!p.ready) continue;
            GameSession.Teams[p.team].Add(p.playerIndex);
        }

        SceneManager.LoadScene(gameSceneName);
    }
}
