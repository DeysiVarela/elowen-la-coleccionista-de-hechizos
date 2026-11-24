using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 1.6f, 0f);
    public float distance = 4f;
    public float sensitivity = 0.15f;
    public float minPitch = -20f;
    public float maxPitch = 60f;

    float yaw;
    float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Mouse.current != null)
        {
            Vector2 d = Mouse.current.delta.ReadValue();
            yaw += d.x * sensitivity;
            pitch -= d.y * sensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 focus = target.position + offset;
        transform.position = focus - rot * Vector3.forward * distance;
        transform.rotation = rot;

        // Esc libera el mouse
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}