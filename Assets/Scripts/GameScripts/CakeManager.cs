using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CakeManager : MonoBehaviour
{
    [Header("Cakes")]
    [SerializeField] private Cake cake1;
    [SerializeField] private Cake cake2;

    [Header("Timer Settings")]
    [SerializeField] private float matchTimeSeconds = 180f;

    [Header("Win UI")]
    [SerializeField] private TMP_Text winTextTeam1;
    [SerializeField] private TMP_Text winTextTeam2;

    [Header("Timer UI")]
    [SerializeField] private TMP_Text timerTextTeam1;
    [SerializeField] private TMP_Text timerTextTeam2;

    private bool gameEnded = false;
    private float timer;

    private void Start()
    {
        timer = matchTimeSeconds;

        // Hide win texts initially
        if (winTextTeam1 != null) winTextTeam1.gameObject.SetActive(false);
        if (winTextTeam2 != null) winTextTeam2.gameObject.SetActive(false);

        cake1.OnCakeDestroyed += Cake1Destroyed;
        cake2.OnCakeDestroyed += Cake2Destroyed;
    }

    private void Update()
    {
        if (gameEnded)
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("ReadyScene");
            }
            return;
        }



        // Countdown timer
        timer -= Time.unscaledDeltaTime;
        if (timer < 0f) timer = 0f;

        UpdateTimerUI(timer);

        if (timer <= 0f)
        {
            TimerEnded();
        }
    }

    private void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string formatted = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timerTextTeam1 != null) timerTextTeam1.text = formatted;
        if (timerTextTeam2 != null) timerTextTeam2.text = formatted;
    }

    private void Cake1Destroyed(Cake cake)
    {
        EndGame("Team 2 Wins!");
    }

    private void Cake2Destroyed(Cake cake)
    {
        EndGame("Team 1 Wins!");
    }

    private void TimerEnded()
    {
        if (gameEnded) return;

        int health1 = cake1 != null ? cake1.CurrentHealth : 0;
        int health2 = cake2 != null ? cake2.CurrentHealth : 0;

        if (health1 > health2)
            EndGame("Team 1 Wins!");
        else if (health2 > health1)
            EndGame("Team 2 Wins!");
        else
            EndGame("Draw!");
    }

    private void EndGame(string message)
    {
        if (gameEnded) return;

        gameEnded = true;

        Debug.Log(message);

        // Show win texts
        ShowWinText(winTextTeam1, message);
        ShowWinText(winTextTeam2, message);

        // Hide timer texts
        if (timerTextTeam1 != null) timerTextTeam1.gameObject.SetActive(false);
        if (timerTextTeam2 != null) timerTextTeam2.gameObject.SetActive(false);

        Time.timeScale = 0f;
    }

    private void ShowWinText(TMP_Text text, string message)
    {
        if (text == null) return;

        text.text = message;
        text.gameObject.SetActive(true);
    }
}
