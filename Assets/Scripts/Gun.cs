using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;

    public float shotForce = 1000;
    public float shotRate = 0.2f;

    private float shotRateTime = 0;

    public float range = 20f;
    public float rotationSpeed = 10f;

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private Transform target;

    void Update()
    {
        FindClosestEnemy();

        if (target != null)
        {
            AimAtTarget();
        }

        if (Input.GetButton("Fire1") || Input.GetMouseButton(0))
        {
            if (Time.time > shotRateTime)
            {
                Shoot();
            }
        }
    }

    void FindClosestEnemy()
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= range)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        target = closestEnemy;
    }

    void AimAtTarget()
    {
        // Aim at the center of the enemy's body (approx +1 meter up from pivot)
        Vector3 targetPoint = target.position + Vector3.up;
        Vector3 direction = targetPoint - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
        shotRateTime = Time.time + shotRate;
        Destroy(newBullet, 2);

        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
