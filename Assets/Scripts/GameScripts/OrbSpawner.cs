using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] orbPrefabs;

    public float spawnInterval = 4f;
    public int deadzone = 55;

    public Vector2 leftSpawn = new Vector2(-8f, 1f);
    public Vector2 rightSpawn = new Vector2(8f, 1f);

    private void Start()
    {
        InvokeRepeating(nameof(SpawnLeft), 1f, spawnInterval);
        InvokeRepeating(nameof(SpawnRight), 1f, spawnInterval);
    }

    private void SpawnLeft()
    {
        SpawnOrb(leftSpawn);
    }

    private void SpawnRight()
    {
        SpawnOrb(rightSpawn);
    }

    private void SpawnOrb(Vector2 position)
    {
        if (orbPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, orbPrefabs.Length);
        Instantiate(orbPrefabs[randomIndex], position, Quaternion.identity);
    }
    /*void Update()
    {
        foreach(GameObject i in orbPrefabs)
        {
            if (i.transform.position.x < (-1*deadzone) || i.transform.position.x > deadzone)
            {
                Destroy(i);
            }
        }
    }*/
}
