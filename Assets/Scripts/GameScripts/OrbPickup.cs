using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OrbPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerThrow player = other.GetComponent<PlayerThrow>();
        if (player == null) return;

        if (!player.HasOrb())
        {
            player.PickupOrb(gameObject);
        }
    }
}
