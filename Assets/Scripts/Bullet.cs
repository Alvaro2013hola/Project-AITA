using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Ignorar colisión con el jugador para que la bala no desaparezca al disparar
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Verificar si el objeto golpeado es un enemigo
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            // Aplicar 1 de daño al enemigo
            enemy.TakeDamage(1);
        }

        // Destruir la bala tras el impacto con cualquier otra cosa (suelo, paredes, enemigos)
        Destroy(gameObject);
    }
}

