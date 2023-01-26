using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy/Player Initializations")]
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform player;
    private Collider[] potentialPlayers;

    [Header("Navmesh Agent")]
    [SerializeField]
    private NavMeshAgent agent;

    [Header("LayerMasks")]
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsPlayer;

    [Header("Enemy Damage Stats")]
    [SerializeField]
    private int damage = 20;

    [Header("Patrolling")]
    [SerializeField]
    private float walkPointRange = 5f;
    private Vector3 walkPoint;
    private bool walkPointSet;

    [Header("Attacking")]
    [SerializeField]
    private float timeBetweenAttacks = 0.833f;
    private bool alreadyAttacked;

    [Header("Enemy States")]
    [SerializeField]
    private float sightRange = 15f;
    [SerializeField]
    private float attackRange = 1.37f;
    private bool playerInSightRange, playerInAttackRange;

    [Header("Player Stats Reference")]
    PlayerStats playerStats;

    [Header("Animation References")]
    AnimationController animationController;
    int walkAnimation;
    int runAnimation;
    int attackAnimation1;

    private void Awake()
    {
        // intialize Navmesh agent, player, player stats, and animation controls
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Transform>();

        playerStats = GetComponent<PlayerStats>();

        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();

        walkAnimation = Animator.StringToHash("Walk");
        runAnimation = Animator.StringToHash("Run");
        attackAnimation1 = Animator.StringToHash("Attack");

        this.GetComponent<Outline>().enabled = false;
    }

    private void Update()
    {
        // create an overlap sphere to create dynamic player targets
        // if a character enters the sphere, the enemy will set that character as the target
        // break after condition met to avoid enemy from switching targets prematurely
        potentialPlayers = Physics.OverlapSphere(transform.position,20f);
        foreach(var collider in potentialPlayers){
            int playerMask = LayerMask.NameToLayer("whatIsPlayer");

            if(collider.gameObject.layer == playerMask && !PlayerStats.playerIsDead){
                player = collider.gameObject.transform;
                break;
            }
        }
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //handle enemy states given conditions
        if (!playerInSightRange && !playerInAttackRange && player != null) Patroling();
        if (playerInSightRange && !playerInAttackRange && player != null)  ChasePlayer(); 
        if (playerInAttackRange && playerInSightRange && player != null) AttackPlayer();
    }


    private void Patroling()
    {

        animationController.animator.CrossFade(walkAnimation, 0f);   
 
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // enemy follows player here
        animationController.animator.CrossFade(runAnimation, 0f);
        agent.SetDestination(player.position);
          
    }

    // attack code can be a different class, that way I can have unique attacks for enemies
    // leave for now cuz whatever
    private void AttackPlayer()
    {
        // make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked && !PlayerStats.playerIsDead)
        {
            ///Attack code here
            animationController.animator.CrossFade(attackAnimation1, animationController.animationPlayTransition);
            player.GetComponent<PlayerStats>().TakeDamage(damage);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
