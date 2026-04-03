using UnityEngine;
using UnityEngine.AI;

public class enemigo : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform player;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();


    }


    private void Update()
    {
        agent.destination = player.position;


        

    }
        




    
}




