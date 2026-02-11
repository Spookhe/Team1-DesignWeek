using UnityEngine;

public class ZigZagOrbMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float zigzagAmplitude = 1.5f;
    public float zigzagFrequency = 2f;

    private float startY;
    private float direction;

    private void Start()
    {
        startY = transform.position.y;
        direction = transform.position.x < 0 ? 1f : -1f;
    }

    private void Update()
    {
        float x = transform.position.x + direction * moveSpeed * Time.deltaTime;
        float y = startY + Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void DisableMotion() { enabled = false; }
}
