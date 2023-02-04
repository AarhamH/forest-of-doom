using UnityEngine;
using UnityEngine.AI;

/*
    EnemyController class: Attached to the Enemy to handle their movement
    Necessary Components: Enemy (GameObject) => reference to the enemy this is attached
                          Agent (NavMeshAgent) => reference to the navmesh component in enemy
*/
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

    /*
        Awake Function: enables navmesh, playerset and stats, animations and disables outline
    */
    private void Awake()
    {
        // intialize Navmesh agent, player, player stats
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Transform>();
        playerStats = GetComponent<PlayerStats>();

        // animation components enabled
        animationController = GetComponent<AnimationController>();
        animationController.AnimationPlayerInstance();
        walkAnimation = Animator.StringToHash("Walk");
        runAnimation = Animator.StringToHash("Run");
        attackAnimation1 = Animator.StringToHash("Attack");

        // outline disab;ed
        this.GetComponent<Outline>().enabled = false;
    }

    /*
        Update Function: - For every frame, a logical sphere checks the presence of a player game
                           object and modifies state
                         - If player is in outermost range, enemy will engage and chase
                         - If Player is in inner most ranger, enemy will attack
                         - The attack radius can be increased (supports ranged enemies)
    */
    private void Update()
    {

        // operate loop to check for players via layer
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

    /*
        Input: n/A

        Output: n/A

        Functionality: - Method to handle enemy passive state
                       - Calls SearchWalkPoint() to create random destination vector
                       - if walkpoint reached, walkPointSet = false and new walkPoint is made
                       - handles walk animations
        
        Called In: Update()

        Notable Functions Docs:
        - SetDestination: https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.SetDestination.html
    */
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

    /*
        Input: n/A

        Output: n/A

        Functionality: - Generates random Vector3 based on x and z coordinates
                       - Used to create a walkPoint
        
        Called In: Patroling()
    */
    private void SearchWalkPoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    /*
        Input: n/A

        Output: n/A

        Functionality: - Method to handle enemy aggro state
                       - Sets destination to player; enemy now follows player
        
        Called In: Update()

        Notable Functions Docs:
        - SetDestination: https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.SetDestination.html
    */
    private void ChasePlayer()
    {
        // enemy follows player here
        animationController.animator.CrossFade(runAnimation, 0f);
        agent.SetDestination(player.position);
          
    }

    /*
        Input: n/A

        Output: n/A

        Functionality: - Method to handle enemy attack state
                       - While the player is not dead, enemy will swing and deal damage to the respective
                         character, attacks one at a time
                       - Calls reset attack with a delay to avoid spamming
        
        Called In: Update()

        Notable Functions Docs:
        - SetDestination: https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.SetDestination.html
    */
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


    // helper function to reset the enemy attack so enemy can attack again
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    // debug function to draw out aggro and attack spheres
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
