using UnityEngine;

public class CakeScript1 : MonoBehaviour
{

    public bool Team1; 

    public int foodRating = 10;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Projectile")
        {
            foodRating -= 1;
        }
    }

    void loseCon()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foodRating <= 0)
        {
            loseCon();
        }
    }
}
