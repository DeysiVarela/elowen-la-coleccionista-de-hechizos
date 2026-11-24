using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1.2f;
    public float gravity = -20f;

    [Header("Referencias")]
    public Animator animator;
    public Transform cameraTransform;

    CharacterController controller;
    Vector3 velocity;
    float currentSpeed;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        // Entrada WASD
        Vector2 input = Vector2.zero;
        if (kb.wKey.isPressed) input.y += 1;
        if (kb.sKey.isPressed) input.y -= 1;
        if (kb.dKey.isPressed) input.x += 1;
        if (kb.aKey.isPressed) input.x -= 1;
        input = Vector2.ClampMagnitude(input, 1f);

        // Caminar o correr (Shift)
        bool running = kb.leftShiftKey.isPressed;
        float targetSpeed = input.magnitude > 0.1f ? (running ? runSpeed : walkSpeed) : 0f;

        // Dirección relativa a la cámara
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();
        Vector3 moveDir = camForward * input.y + camRight * input.x;

        // Girar hacia donde camina
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 10f * Time.deltaTime);

        // Gravedad y salto
        bool grounded = controller.isGrounded;
        if (grounded && velocity.y < 0) velocity.y = -2f;

        if (grounded && kb.spaceKey.wasPressedThisFrame)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 move = moveDir * currentSpeed + Vector3.up * velocity.y;
        controller.Move(move * Time.deltaTime);

        // Avisar al Animator
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("Grounded", controller.isGrounded);
    }
}