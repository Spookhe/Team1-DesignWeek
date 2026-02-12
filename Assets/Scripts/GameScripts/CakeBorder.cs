using UnityEngine;

public class CakeBorder : MonoBehaviour
{
    private Collider2D barrierCollider;

    private void Awake()
    {
        barrierCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the object is a projectile, dont ignore collision
        if (!collision.collider.CompareTag("Projectile"))
        {
            Physics2D.IgnoreCollision(
                collision.collider,
                barrierCollider,
                true
            );
        }
    }
}
