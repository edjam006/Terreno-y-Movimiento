using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public float speed = 5f;            // Velocidad de movimiento
    public float mouseSensitivity = 2f; // Sensibilidad del mouse
    public float gravity = -9.81f;      // Gravedad
    public float jumpHeight = 1.5f;     // Altura de salto

    private CharacterController controller;
    private Transform playerCamera;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;

        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Movimiento con teclado ---
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        // --- Saltar ---
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- Rotación con el mouse ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotación vertical (cámara)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limitar a -90/90
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (jugador)
        transform.Rotate(Vector3.up * mouseX);
    }
}
