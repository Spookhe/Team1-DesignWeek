using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : MonoBehaviour
{
    public enum Team { Left, Right }

    [Header("Throw Settings")]
    public float throwForce = 20f;
    public float throwArcForce = 5f;

    [Header("Player Info")]
    public Team team; // Assigned during spawn

    private GameObject heldOrb;
    private Rigidbody2D heldRb;

    private PlayerInput playerInput;
    private InputAction attackAction;

    public void AssignPlayerInput(PlayerInput input)
    {
        playerInput = input;
        attackAction = input.actions["Attack"];

        if (attackAction != null)
            Debug.Log($"{gameObject.name}: Attack action assigned!");
        else
            Debug.LogError($"{gameObject.name}: Attack action NOT found!");
    }

    private void Update()
    {
        if (playerInput == null || attackAction == null) return;

        if (attackAction.WasPressedThisFrame())
        {
            Debug.Log($"{gameObject.name}: Attack Pressed!");
            ThrowOrb();
        }
    }

    public bool HasOrb() => heldOrb != null;

    public void PickupOrb(GameObject orb)
    {
        if (HasOrb()) return;

        heldOrb = orb;
        heldRb = orb.GetComponent<Rigidbody2D>();

        var mover = orb.GetComponent<ZigZagOrbMover>();
        if (mover != null) mover.enabled = false;

        orb.transform.parent = transform;
        orb.transform.localPosition = new Vector3(0f, 1f, 0f);

        heldRb.gravityScale = 0f;
        heldRb.linearVelocity = Vector2.zero;
        heldRb.angularVelocity = 0f;
        heldRb.bodyType = RigidbodyType2D.Kinematic;

        Debug.Log($"{gameObject.name}: Orb picked up!");
    }

    public void ThrowOrb()
    {
        if (!HasOrb()) return;

        heldOrb.transform.parent = null;

        heldRb.bodyType = RigidbodyType2D.Dynamic;
        heldRb.gravityScale = 1f;
        heldRb.linearVelocity = Vector2.zero;

        // Determine throw direction based on team
        Vector2 horizontalDir = (team == Team.Left) ? Vector2.right : Vector2.left;

        Vector2 throwDir = horizontalDir + Vector2.up * (throwArcForce / throwForce);

        heldRb.AddForce(throwDir.normalized * throwForce, ForceMode2D.Impulse);

        Debug.Log($"{gameObject.name}: Throwing {(horizontalDir == Vector2.right ? "RIGHT" : "LEFT")}");
        Debug.Log($"{gameObject.name}: Orb thrown!");

        heldOrb = null;
        heldRb = null;
    }
}
