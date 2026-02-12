using UnityEngine;

public class ProjectileBarrier : MonoBehaviour
{
    private Collider2D barrierCollider;

    private void Awake()
    {
        barrierCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the object is NOT a player, ignore collision
        if (!collision.collider.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(
                collision.collider,
                barrierCollider,
                true
            );
        }
    }
}
