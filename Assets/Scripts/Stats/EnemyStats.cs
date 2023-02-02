using UnityEngine;

/*
    EnemyStats Class: A custom class that handles enemy health stats

    Necessary Components: SplatterEffect/DeathEffect (GameObject) => particles that play when dead
                          Drops[] (GameObject) => holds all of the drops
                          HealthBar (HealthBar class) => handles the UI health change of enemy
*/
public class EnemyStats : CharacterStats
{
    [Header("Health and State Settings")]
    [SerializeField]
    private float maxHealth = 50; 
    public bool enemyIsDead;
      
    [Header("Particle Effects")]
    [SerializeField]
    private GameObject splatterEffect;
    [SerializeField]
    private GameObject deathEffect;

    [Header("UI")]
    [SerializeField]
    private Healthbar healthbar;

    [Header("Drops")]
    [SerializeField]
    private GameObject[] drops;

    [Header("Shaking effect")]
    private CinemachineShake shake;
    private float shakeTimer = 1;   


    /*
        Awake Function: - Initializes currentHealth, UI health bar, and shake camera
    */
    private void Awake() 
    {
        InitializeHealth(maxHealth);
        healthbar.UpdateHealthBar(maxHealth,currentHealth);
        shake = GameObject.Find("Shake").GetComponent<CinemachineShake>();
    }


    /*
        Input: float damage => The arbritary damage which will be deducted 
    
        Functionality: - Overwritten function from CharacterStats class that subtracts currentHealth 
                         with damage via base keyword
                       - Plays a camera shake effect when enemy is hurt and particle effects
                       - Updates healthbar when hurt

        Called In: Overwritten in another class
 
    */
    public override void TakeDamage(float damage)
    {
        // shakes camera
        shake.Shake(2f,0);

        if(splatterEffect != null)
        {
            PlayParticleEffects(splatterEffect);
        }

        // calls the og function from CharacterStats
        base.TakeDamage(damage);        

        healthbar.UpdateHealthBar(maxHealth,currentHealth);
    }


    /*
        Input: n/A
    
        Functionality: - Overwritten function from CharacterStats class that allows enemy to die via
                         base keyword
                       - Plays audio from AudioManager singleton and particle effects
                       - Picks a random index from available drops and instantiates it

        Called In: Overwritten in another class
 
    */
    public override void Die()
    {
        AudioManager.Instance.PlayEffect("GateDestroy");

        // calls the og function in CharacterStats to die
        enemyIsDead = true;
        base.Die();

        // play particles and drop random item
        PlayParticleEffects(deathEffect);
        int randomIndex = Random.Range(0,drops.Length);
        Instantiate(drops[randomIndex], new Vector3(transform.position.x, 1f, 
                                                    transform.position.z), Quaternion.identity);
    }
}
