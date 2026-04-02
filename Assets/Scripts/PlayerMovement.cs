using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Header("Movement Settings")]
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float turnSmoothTime = 0.1f;

    [Header("Health Settings")]
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;

    [Header("Input Actions")]
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction jumpAction;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    void Awake()
    {
        if (inputActions != null)
        {
            var playerMap = inputActions.FindActionMap("Player");
            moveAction = playerMap.FindAction("Move");
            jumpAction = playerMap.FindAction("Jump");
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            // Opcional: Reiniciar posición o escena
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    void OnEnable()
    {
        moveAction?.Enable();
        jumpAction?.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();
        jumpAction?.Disable();
    }

    void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Input handling (New Input System)
        Vector2 moveInput = Vector2.zero;
        if (moveAction != null)
        {
            moveInput = moveAction.ReadValue<Vector2>();
        }

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Jump handling
        if (jumpAction != null && jumpAction.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
