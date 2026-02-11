using System;
using UnityEditor.Tilemaps;
using UnityEngine;

using Random=UnityEngine.Random;

public class Powerup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5.0f; 
    void Start()
    {
        transform.position = new Vector2(Random.Range(1,2), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down*speed*Time.deltaTime); // * Time.deltaTime?
        Console.WriteLine(speed*Time.deltaTime);

    }
}