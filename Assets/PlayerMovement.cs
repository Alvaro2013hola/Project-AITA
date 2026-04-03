using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cameraTransform;
    public float speed = 12f;

    public Vector3 velocity;

    public Transform groundCheck;
    public float sphereRadius = 0.9f;
    public LayerMask groundMask;
    public bool isGrounded;

    public Transform crouchCam;
    public Transform normalCam;

    private void Update()
    {
        Movement();
        Jump();
        Crouch();
    }
    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        float x = Input.GetAxisRaw("Horizontal"); //Obtener ejes
        float z = Input.GetAxisRaw("Vertical"); //Obtener ejes

        Vector3 forward = cameraTransform.forward; //Obtener la dirección hacia donde mira el jugador
        Vector3 right = cameraTransform.right; // Dirección paralela al jugador

        forward.y = 0; //
        right.y = 0; //

        Vector3 move = (right * x + forward * z).normalized; //Direcciones totales
        characterController.Move(move * speed * Time.deltaTime); //Moverse en la dirección que quiere el jugador + velocidad
    }
    private void Jump()
    {
        if(isGrounded && velocity.y < 0f ) { velocity.y = (jumpHeight*-1); } //
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        { 
            velocity.y = Mathf.Sqrt(jumpHeight * (jumpHeight*-1) * gravity); //Saltar
            isGrounded = false; 
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            cameraTransform = crouchCam;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
}
