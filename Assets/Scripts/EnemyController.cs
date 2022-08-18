using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    bool alreadyAttacked;

    public float timeBetweenAttacks;

    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;


     private void Awake() {
        agent = GetComponent<NavMeshAgent>();
     }
     
     private void Update() {
        ChasePlayer();
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();

     }

     private void Patrolling(){
        if (!walkPointSet){
            SearchWalkPoint();
        }

        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }

     }

     private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomZ, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }
     }

     private void ChasePlayer(){
        agent.SetDestination(player.position);
     }

     private void AttackPlayer(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){
            //Attack Code Here
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
     }

    private void ResetAttack(){
        alreadyAttacked = false;
    }
}
