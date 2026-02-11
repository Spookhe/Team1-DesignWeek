using System;
using UnityEditor.Tilemaps;
using UnityEngine;

using Random=UnityEngine.Random;

public class Powerup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5.0f; 
    public GameObject player;
    public PlayerController playerScript;

    public float speedBoost = 35f;

    void Start()
    {
        transform.position = new Vector2(Random.Range(-10,-48), 10);
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down*speed*Time.deltaTime); // * Time.deltaTime?
        Console.WriteLine(speed*Time.deltaTime);
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Powerup");
            //playerScript.moveSpeed = speedBoost;
            Destroy(gameObject);
        }
    }
}