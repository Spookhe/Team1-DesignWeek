using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Cursor : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;

    [Header("Camera References (Assign on Prefab)")]
    [SerializeField] private Camera leftCamera;
    [SerializeField] private Camera rightCamera;

    private Camera targetCamera;
    private InputAction moveAction;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Awake()
    {
        // Validate PlayerInput
        PlayerInput input = GetComponent<PlayerInput>();
        if (input == null || input.actions == null)
        {
            Debug.LogError($"{name}: PlayerInput or InputActions missing.");
            enabled = false;
            return;
        }

        // Find Move action
        moveAction = input.actions.FindAction("Move");
        if (moveAction == null)
        {
            Debug.LogError($"{name}: Move action not found in Input Actions.");
            enabled = false;
            return;
        }

        // Editor camera references
        if (leftCamera == null || rightCamera == null)
        {
            Debug.LogError($"{name}: Left and/or Right Camera not assigned on prefab.");
            enabled = false;
            return;
        }

        // Assign camera based on player index
        AssignCamera(input.playerIndex);
    }

    private void Start()
    {
        // Activate second display
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }

        CalculateCameraBounds();
    }

    private void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 delta = new Vector3(input.x, input.y, 0f) * speed * Time.deltaTime;

        Vector3 newPos = transform.position + delta;

        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        transform.position = newPos;
    }

    private void AssignCamera(int playerIndex)
    {
        // Even players index (0,2) Left Camera
        // Odd players index (1,3) Right Camera
        targetCamera = (playerIndex % 2 == 0) ? leftCamera : rightCamera;
    }

    private void CalculateCameraBounds()
    {
        Vector3 min = targetCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, targetCamera.nearClipPlane)
        );

        Vector3 max = targetCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, targetCamera.nearClipPlane)
        );

        minBounds = min;
        maxBounds = max;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (targetCamera == null) return;

        Gizmos.color = Color.green;

        Vector3 bl = targetCamera.ViewportToWorldPoint(new Vector3(0, 0, targetCamera.nearClipPlane));
        Vector3 tr = targetCamera.ViewportToWorldPoint(new Vector3(1, 1, targetCamera.nearClipPlane));

        Gizmos.DrawWireCube((bl + tr) * 0.5f, tr - bl);
    }
#endif
}
