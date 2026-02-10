using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] leftTeamSpawnPoints;
    public Transform[] rightTeamSpawnPoints;
    public GameObject playerPrefab;

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        int leftIndex = 0;
        int rightIndex = 0;

        foreach (int playerIndex in GameSession.Teams[TeamSide.Left])
        {
            Transform spawn = leftTeamSpawnPoints[leftIndex++];
            GameObject go = Instantiate(playerPrefab, spawn.position, spawn.rotation);
            go.name = $"Player {playerIndex + 1}";
        }

        foreach (int playerIndex in GameSession.Teams[TeamSide.Right])
        {
            Transform spawn = rightTeamSpawnPoints[rightIndex++];
            GameObject go = Instantiate(playerPrefab, spawn.position, spawn.rotation);
            go.name = $"Player {playerIndex + 1}";
        }
    }
}
