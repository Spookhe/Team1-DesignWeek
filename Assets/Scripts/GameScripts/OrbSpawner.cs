using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;
    public float spawnInterval = 4f;

    public Vector2 leftSpawn = new Vector2(-8f, 1f);
    public Vector2 rightSpawn = new Vector2(8f, 1f);

    private void Start()
    {
        InvokeRepeating(nameof(SpawnLeft), 1f, spawnInterval);
        InvokeRepeating(nameof(SpawnRight), 1f, spawnInterval);
    }

    private void SpawnLeft()
    {
        Instantiate(orbPrefab, leftSpawn, Quaternion.identity);
    }

    private void SpawnRight()
    {
        Instantiate(orbPrefab, rightSpawn, Quaternion.identity);
    }
}
