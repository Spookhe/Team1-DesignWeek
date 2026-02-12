using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float jumpCooldown = 1f;

    static bool canJump = true;
    

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    public int playerNumber;

    [Header("Throwing")]
    public PlayerThrow playerThrow; // assign in Inspector

    private bool doJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (playerInput == null) return;

        // Jump input
        if (jumpAction != null && jumpAction.WasPressedThisFrame() && canJump == true)
        {
            doJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (playerInput != null)
        {
            float move = moveAction.ReadValue<Vector2>().x;
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

            if (doJump)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doJump = false;
                StartCoroutine(CoolDownFunction());
            }
        }
    }

    public void AssignPlayerInput(PlayerInput input)
    {
        playerInput = input;

        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];

        // Assign input to PlayerThrow
        if (playerThrow != null)
        {
            playerThrow.AssignPlayerInput(input);
        }
    }

    public void AssignPlayerNumber(int num)
    {
        playerNumber = num;
    }

    IEnumerator CoolDownFunction()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
}
