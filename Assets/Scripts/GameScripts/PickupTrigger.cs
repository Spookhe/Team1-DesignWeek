using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerThrow playerThrow = other.GetComponent<PlayerThrow>();
        if (playerThrow == null) return;

        if (!playerThrow.HasOrb())
        {
            playerThrow.PickupOrb(gameObject); // Pass the orb itself
        }
    }
}
