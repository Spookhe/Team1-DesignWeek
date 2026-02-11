using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ReadyZone : MonoBehaviour
{
    private void Reset()
    {
        // Make sure collider is a trigger
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerSelectionUI player = other.GetComponent<PlayerSelectionUI>();
        if (player == null) return;

        Debug.Log($"{player.PlayerName} entered ReadyZone {name}");
        player.SetReady(true);
        TeamSelectionManager.Instance.PlayerSetReady(player, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerSelectionUI player = other.GetComponent<PlayerSelectionUI>();
        if (player == null) return;

        Debug.Log($"{player.PlayerName} exited ReadyZone {name}");
        player.SetReady(false);
        TeamSelectionManager.Instance.PlayerSetReady(player, false);
    }
}
