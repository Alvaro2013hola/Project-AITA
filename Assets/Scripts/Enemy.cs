using UnityEngine;

public class Enemy : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public int maxHealth = 2;
    public int health;
    public HealthBar healthBar;

    [Header("Attack Settings")]
    public int attackDamage = 1;
    public float attackRange = 2.0f;
    public float attackRate = 1f;
    private float nextAttackTime;


    private void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    private void Update()
    {
        if (player != null)
        {
            agent.destination = player.position;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    private void AttackPlayer()
    {
        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        if (playerScript != null)
        {
            Debug.Log("Enemigo Atacando Jugador!");
            playerScript.TakeDamage(attackDamage);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
