using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PlayerInput))]
public class PlayerSelectionUI : MonoBehaviour
{
    [Header("Player State")]
    public int playerIndex;
    public TeamSide team = TeamSide.Left;
    public bool ready;

    public string PlayerName => $"Player {playerIndex + 1}";

    [Header("Cursor Sprites")]
    public Sprite P1Cursor;
    public Sprite P2Cursor;
    public Sprite P3Cursor;
    public Sprite P4Cursor;

    [Header("Colors")]
    public Color readyColor = Color.green;
    public Color unreadyColor = Color.red;

    private SpriteRenderer spriteRenderer;
    private TMP_Text label;
    private PlayerInput input;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerIndex = input.playerIndex;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        label = GetComponentInChildren<TMP_Text>();

        AssignVisuals();
        TeamSelectionManager.Instance.Register(this);
    }

    void Start()
    {
        SetSpawnPosition();
    }

    void AssignVisuals()
    {
        if (spriteRenderer == null) return;

        switch (playerIndex)
        {
            case 0: spriteRenderer.sprite = P1Cursor; break;
            case 1: spriteRenderer.sprite = P2Cursor; break;
            case 2: spriteRenderer.sprite = P3Cursor; break;
            case 3: spriteRenderer.sprite = P4Cursor; break;
        }

        if (label != null)
        {
            label.text = $"P{playerIndex + 1}";
            label.color = unreadyColor;
        }
    }

    void SetSpawnPosition()
    {
        // P1 & P3 Display 1
        if (playerIndex == 0 || playerIndex == 2)
        {
            transform.position = new Vector3(200f, 540f, 0f);
            team = TeamSide.Left;
        }
        // P2 & P4 Display 2
        else
        {
            transform.position = new Vector3(2120f, 540f, 0f);
            team = TeamSide.Right;
        }
    }

    public void ToggleReady()
    {
        ready = !ready;

        if (label != null)
            label.color = ready ? readyColor : unreadyColor;

        TeamSelectionManager.Instance.CheckReady();
    }

    public void SwapTeam()
    {
        team = team == TeamSide.Left ? TeamSide.Right : TeamSide.Left;

        Vector3 pos = transform.position;
        pos.x = team == TeamSide.Left ? 200f : 2120f;
        transform.position = pos;
    }
}
