using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto golpeado es un enemigo
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            // Aplicar 1 de daño al enemigo
            enemy.TakeDamage(1);
        }

        // Destruir la bala tras el impacto (opcional, si quieres que desaparezca al chocar)
        Destroy(gameObject);
    }
}
