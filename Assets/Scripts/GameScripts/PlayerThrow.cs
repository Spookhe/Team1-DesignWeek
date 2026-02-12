using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : MonoBehaviour
{
    public enum Team { Left, Right }

    [Header("Throw Settings")]
    public float throwForce = 10f;      // Horizontal power
    public float throwArcForce = 8f;    // Vertical arc strength

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

        // Disable floating motion
        var mover = orb.GetComponent<ZigZagOrbMover>();
        if (mover != null) mover.enabled = false;

        // Attach to player
        orb.transform.parent = transform;
        orb.transform.localPosition = new Vector3(0f, 1f, 0f);

        // Disable physics while held
        heldRb.bodyType = RigidbodyType2D.Kinematic;
        heldRb.gravityScale = 0f;
        heldRb.linearVelocity = Vector2.zero;
        heldRb.angularVelocity = 0f;

        Debug.Log($"{gameObject.name}: Orb picked up!");
    }


    public void ThrowOrb()
    {
        if (!HasOrb()) return;

        heldOrb.transform.parent = null;

        heldRb.bodyType = RigidbodyType2D.Dynamic;
        heldRb.gravityScale = 1f;

        // IMPORTANT: Team-based direction
        Vector2 horizontalDir = (team == Team.Left) ? Vector2.right : Vector2.left;

        float angle = 25f;
        float power = throwForce * 0.9f;

        float rad = angle * Mathf.Deg2Rad;

        Vector2 launchVelocity = new Vector2(
            Mathf.Cos(rad) * power * horizontalDir.x,
            Mathf.Sin(rad) * power
        );

        heldRb.linearVelocity = launchVelocity;

        Debug.Log($"{gameObject.name}: ARC THROW 55� (reduced power)");

        heldOrb = null;
        heldRb = null;
    }


}
