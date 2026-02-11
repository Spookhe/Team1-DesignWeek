using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] leftTeamSpawnPoints;
    public Transform[] rightTeamSpawnPoints;

    private int leftIndex = 0;
    private int rightIndex = 0;

    private void OnEnable()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    /*private void OnDisable()
    {
        PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    }*/

    private void OnPlayerJoined(PlayerInput player)
    {
        int index = player.playerIndex;

        // -------------------------
        // Spawn position
        // -------------------------
        if (index % 2 == 0) // Left Team
        {
            if (leftIndex < leftTeamSpawnPoints.Length)
            {
                player.transform.position = leftTeamSpawnPoints[leftIndex].position;
                leftIndex++;
            }
        }
        else // Right Team
        {
            if (rightIndex < rightTeamSpawnPoints.Length)
            {
                player.transform.position = rightTeamSpawnPoints[rightIndex].position;
                rightIndex++;
            }
        }

        // Rename player in hierarchy
        player.gameObject.name = $"Player {index + 1}";

        // -------------------------
        // Initialize PlayerController
        // -------------------------
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.AssignPlayerInput(player);      // Assign this player's Input
            pc.AssignPlayerNumber(index + 1);  // Optional: numbering starts at 1
        }

        // -------------------------
        // Initialize PlayerThrow
        // -------------------------
        PlayerThrow pt = player.GetComponent<PlayerThrow>();
        if (pt != null)
        {
            // Assign team based on spawn
            pt.team = (index % 2 == 0) ? PlayerThrow.Team.Left : PlayerThrow.Team.Right;

            // Assign input so Attack works
            pt.AssignPlayerInput(player);

            Debug.Log($"Player {index + 1}: Assigned team {(pt.team == PlayerThrow.Team.Left ? "Left" : "Right")}");
        }
    }
}
