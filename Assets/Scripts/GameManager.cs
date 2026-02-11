using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public bool gameOver { get; private set; }

    public string loss;

    public string UITeam;

    public UnityEvent OnGameOver;

    public CakeScript1 cakeScript1;

    public CakeScript2 cakeScript2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loss = "";
        SetGameOver(false, loss);
    }

    // Update is called once per frame
    void Update()
    {
        CheckEndGame();
    }
    void CheckEndGame()
    {
        if (cakeScript1.foodRating == 0)
        {
            if (cakeScript1.Team1 == true)
            {
                loss = "Team 1";
                SetGameOver(true, loss);
            }
            
        }

        if (cakeScript2.foodRating == 0)
        {
            if (cakeScript2.Team1 == false)
            {
                loss = "Team 2";
                SetGameOver(true, loss);
            }

        }

    }
    public void SetGameOver(bool gameOver, string team)
    {
        UITeam = team;
        this.gameOver = gameOver;
        OnGameOver.Invoke();
    }

}
