using UnityEngine;

public class GunLogic : MonoBehaviour
{
    public GameObject bullet; //Objeto que hará de bala
    public float bulletSpeed = 20f; //Qué velocidad
    public float fireRate = 0.2f; //Cada cuanto?
    private float nextFireTime = 0; //Puedo disparar?

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextFireTime) //Puedo disparar?
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation); //Genera la bala
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>(); //obtenemos el rigidbody
        rb.linearVelocity = transform.forward * bulletSpeed; // con el rigidbody, la enviamos a la mierda
    }
}
