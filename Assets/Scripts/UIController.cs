using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameManager gameManager;
    public TextMeshProUGUI teamText;

    public GameObject endGamePanel;

    public string otherTeam;

    /*
    public void UIUpdateScore()
    {
        scoreText.text = $"SCORE: {tetrisManager.score}";
    }
    */

    public void UpdateGameOver()
    {
        if (gameManager.UITeam == "Team 1")
        {
            otherTeam = "Team 2";
        } else if (gameManager.UITeam == "Team 2")
        {
            otherTeam = "Team 1";
        }
        teamText.text = $"{gameManager.UITeam}'s cake got too dirty. {otherTeam} wins!";
        endGamePanel.SetActive(gameManager.gameOver);
    }
}
