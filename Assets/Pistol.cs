using UnityEngine;

public class pistol : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;

    public float shotForce = 1800;
    public float shotRate = 0.2f;

    private float shotRateTime = 0;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (Time.time > shotRateTime)
            {
                GameObject newBullet;
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);

                shotRateTime = Time.time + shotRate;

                Destroy(newBullet, 2);


            }



        }





    }
}
