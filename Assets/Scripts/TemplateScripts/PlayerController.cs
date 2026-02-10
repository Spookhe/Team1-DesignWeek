using UnityEngine;
using UnityEngine.InputSystem;

public enum ScreenSide { Left, Right }

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform throwOrigin;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject trajectoryDotPrefab;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxDragDistance = 4f;
    [SerializeField] private float throwForceMultiplier = 40f;
    [SerializeField] private int dotCount = 5;
    [SerializeField] private float dotSpacing = 0.25f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private bool doJump;
    private bool isDragging;
    private Vector2 dragStart;

    private GameObject[] dots;

    private ScreenSide screenSide;
    private int playerNumber;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        // Create trajectory dots
        dots = new GameObject[dotCount];
        for (int i = 0; i < dotCount; i++)
        {
            dots[i] = Instantiate(trajectoryDotPrefab);
            dots[i].SetActive(false);
        }
    }

    private void Update()
    {
        HandleJumpInput();
        HandleThrowInput();

        if (!isDragging) ClearDots();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        if (doJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            doJump = false;
        }
    }

    // Movement Actions
    public void AssignPlayerInputDevice(PlayerInput input)
    {
        playerInput = input;
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
        attackAction = input.actions["Attack"];
    }

    public void AssignPlayerNumber(int number) => playerNumber = number;
    public void AssignScreenSide(ScreenSide side) => screenSide = side;

    public void AssignColor(Color color)
    {
        if (spriteRenderer != null) spriteRenderer.color = color;
        if (dots != null)
        {
            foreach (var d in dots)
            {
                if (d.TryGetComponent(out SpriteRenderer sr))
                    sr.color = color;
            }
        }
    }

    // Input Handling
    private void HandleMovement()
    {
        float move = 0f;
        if (moveAction != null) move = moveAction.ReadValue<Vector2>().x;
        else if (playerNumber == 1)
        {
            if (Keyboard.current.aKey.isPressed) move = -1f;
            if (Keyboard.current.dKey.isPressed) move = 1f;
        }

        rb.AddForce(Vector2.right * move * moveSpeed, ForceMode2D.Force);
    }

    private void HandleJumpInput()
    {
        if (jumpAction != null && jumpAction.WasPressedThisFrame()) doJump = true;
        else if (playerNumber == 1 && Keyboard.current.spaceKey.wasPressedThisFrame) doJump = true;
    }

    private void HandleThrowInput()
    {
        bool pressed = false, released = false;

        if (attackAction != null)
        {
            pressed = attackAction.WasPressedThisFrame();
            released = attackAction.WasReleasedThisFrame();
        }
        else if (playerNumber == 1)
        {
            pressed = Keyboard.current.enterKey.wasPressedThisFrame;
            released = Keyboard.current.enterKey.wasReleasedThisFrame;
        }

        if (pressed)
        {
            dragStart = GetAimPosition();
            isDragging = true;
        }

        if (isDragging) UpdateDrag();

        if (released && isDragging)
        {
            FireProjectile();
            ClearDots();
            isDragging = false;
        }
    }

    private Vector2 GetAimPosition()
    {
        if (Mouse.current != null) return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return throwOrigin.position;
    }

    // Throw projectiles
    private void UpdateDrag()
    {
        Vector2 current = GetAimPosition();
        Vector2 dragVector = dragStart - current;
        float distance = Mathf.Clamp(dragVector.magnitude, 0f, maxDragDistance);
        Vector2 direction = dragVector.normalized;
        float force = distance * throwForceMultiplier;
        DrawDots(direction * force);
    }

    private void FireProjectile()
    {
        Vector2 current = GetAimPosition();
        Vector2 dragVector = dragStart - current;
        float distance = Mathf.Clamp(dragVector.magnitude, 0f, maxDragDistance);
        Vector2 direction = dragVector.normalized;
        float force = distance * throwForceMultiplier;

        GameObject proj = Instantiate(projectilePrefab, throwOrigin.position, Quaternion.identity);
        Rigidbody2D rbProj = proj.GetComponent<Rigidbody2D>();
        if (rbProj != null) rbProj.AddForce(direction * force, ForceMode2D.Impulse);
    }

    // Trajectory Dots
    private void DrawDots(Vector2 velocity)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            float t = (i + 1) * dotSpacing;
            Vector2 point = (Vector2)throwOrigin.position + velocity * t + 0.5f * Physics2D.gravity * t * t;

            // Clamp to screen
            float dividerX = 0f;
            if (screenSide == ScreenSide.Left) point.x = Mathf.Min(point.x, dividerX);
            else point.x = Mathf.Max(point.x, dividerX);

            dots[i].transform.position = point;
            dots[i].SetActive(true);
        }
    }

    private void ClearDots()
    {
        foreach (var d in dots) d.SetActive(false);
    }
}
