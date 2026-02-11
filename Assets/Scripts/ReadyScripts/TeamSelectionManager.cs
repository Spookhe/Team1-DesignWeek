using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TeamSelectionManager : MonoBehaviour
{
    public static TeamSelectionManager Instance;

    [Header("UI References - Left Monitor")]
    public TMP_Text readyCounterTextLeft;
    public TMP_Text countdownTextLeft;

    [Header("UI References - Right Monitor")]
    public TMP_Text readyCounterTextRight;
    public TMP_Text countdownTextRight;

    [Header("Game Settings")]
    public string gameSceneName;

    private List<PlayerSelectionUI> players = new List<PlayerSelectionUI>();
    private Coroutine countdownRoutine;

    private void Awake()
    {
        Instance = this;

        if (countdownTextLeft != null) countdownTextLeft.gameObject.SetActive(false);
        if (countdownTextRight != null) countdownTextRight.gameObject.SetActive(false);
    }

    // Register players when they spawn
    public void Register(PlayerSelectionUI player)
    {
        if (!players.Contains(player))
            players.Add(player);

        UpdateReadyCounter();
    }

    public void PlayerSetReady(PlayerSelectionUI player, bool isReady)
    {
        player.ready = isReady;
        UpdateReadyCounter();
        CheckReadyState();
    }

    private void UpdateReadyCounter()
    {
        int readyCount = 0;

        foreach (var p in players)
            if (p != null && p.ready) readyCount++;

        string text = $"{readyCount}/{players.Count} Players Ready";

        if (readyCounterTextLeft != null) readyCounterTextLeft.text = text;
        if (readyCounterTextRight != null) readyCounterTextRight.text = text;
    }

    private bool AllPlayersReady()
    {
        if (players.Count < 2)
            return false;

        foreach (var p in players)
            if (p == null || !p.ready)
                return false;

        return true;
    }

    private void CheckReadyState()
    {
        if (AllPlayersReady())
        {
            if (countdownRoutine == null)
                countdownRoutine = StartCoroutine(StartCountdown());
        }
        else
        {
            if (countdownRoutine != null)
            {
                StopCoroutine(countdownRoutine);
                countdownRoutine = null;
            }

            if (countdownTextLeft != null) countdownTextLeft.gameObject.SetActive(false);
            if (countdownTextRight != null) countdownTextRight.gameObject.SetActive(false);
        }
    }

    private IEnumerator StartCountdown()
    {
        int timer = 5;

        if (countdownTextLeft != null) countdownTextLeft.gameObject.SetActive(true);
        if (countdownTextRight != null) countdownTextRight.gameObject.SetActive(true);

        while (timer > 0)
        {
            if (countdownTextLeft != null) countdownTextLeft.text = $"Game Starting {timer}";
            if (countdownTextRight != null) countdownTextRight.text = $"Game Starting {timer}";

            yield return new WaitForSeconds(1f);
            timer--;

            if (!AllPlayersReady())
            {
                countdownRoutine = null;
                yield break;
            }
        }

        SceneManager.LoadScene(gameSceneName);
    }
}
