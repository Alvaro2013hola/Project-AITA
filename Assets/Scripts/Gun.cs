using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;

    [Header("Settings")]
    public float shotForce = 1000;
    public float shotRate = 0.2f;

    [Header("Input Actions")]
    public InputActionAsset inputActions;
    private InputAction shootAction;

    private float shotRateTime = 0;

    void Awake()
    {
        if (inputActions != null)
        {
            var playerMap = inputActions.FindActionMap("Player");
            shootAction = playerMap.FindAction("Shoot");
        }
    }

    void OnEnable()
    {
        shootAction?.Enable();
    }

    void OnDisable()
    {
        shootAction?.Disable();
    }

    void Update()
    {
        RotateTowardsMouse();

        if (shootAction != null && shootAction.triggered)
        {
            if (Time.time > shotRateTime)
            {
                Shoot();
                shotRateTime = Time.time + shotRate;
            }
        }
    }

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, pointToLook, Color.cyan);

            Vector3 lookDirection = (pointToLook - transform.position).normalized;
            // Solo rotar en el eje Y (opcional, dependiendo de si quieres que apunte arriba/abajo)
            lookDirection.y = 0; 
            
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
        Destroy(newBullet, 2);
    }
}
