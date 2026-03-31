using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 5f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public Transform cam;

    void Update()
    {
        //Coger Input(viejo)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
           //Girar al Player con un smooth y a donde mira la camara ;)
           float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
           float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
           transform.rotation = Quaternion.Euler(0f, angle, 0f);
           Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

           //Mover con CharacterController
           controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
