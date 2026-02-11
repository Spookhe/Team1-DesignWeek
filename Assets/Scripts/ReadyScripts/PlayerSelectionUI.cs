using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerSelectionUI : MonoBehaviour
{
    public int playerIndex;
    public bool ready;

    [Header("Cursor Visuals")]
    public Sprite P1Cursor, P2Cursor, P3Cursor, P4Cursor;

    private SpriteRenderer spriteRenderer;
    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        playerIndex = input.playerIndex;
        AssignVisual();

        if (TeamSelectionManager.Instance != null)
            TeamSelectionManager.Instance.Register(this);
    }

    private void AssignVisual()
    {
        if (spriteRenderer == null) return;

        switch (playerIndex)
        {
            case 0: spriteRenderer.sprite = P1Cursor; break;
            case 1: spriteRenderer.sprite = P2Cursor; break;
            case 2: spriteRenderer.sprite = P3Cursor; break;
            case 3: spriteRenderer.sprite = P4Cursor; break;
        }
    }

    public void SetReady(bool isReady)
    {
        ready = isReady;
    }

    public string PlayerName => $"Player {playerIndex + 1}";
}
