using UnityEngine;
using Cinemachine;

/*
    EnemyStats Class: A custom class that handles enemy health stats

    Necessary Components: SplatterEffect/DeathEffect (GameObject) => particles that play when dead
*/
public class PlayerStats : CharacterStats
{
    [Header("Health and State Stats")]
    [SerializeField]
    public float maxHealth = 100; 
    [HideInInspector]
    static public bool playerIsDead;
    [HideInInspector]
    public bool dead;
      
    [Header("Particle Effects")]
    [SerializeField]
    private GameObject splatterEffect;
    [SerializeField]
    private GameObject deathEffect;

    [Header("UI")]
    MainHealth mainHealth;

    [Header("Animations")]
    private AnimationController animationController;

    [Header("Cinemachine Characters")]
    private CinemachineVirtualCamera deathCamera;
    private CinemachineVirtualCamera thirdPersonCamera;
    private CinemachineVirtualCamera aimCamera;


    /*
        Awake Function: - Initializes currentHealth, state bools, UI health bar, and animation controls
    */
    private void Awake() 
    {
        InitializeHealth(maxHealth);
        playerIsDead = false;
        dead = false;
        animationController = GetComponent<AnimationController>();
        mainHealth = GetComponent<MainHealth>();
    }


    /*
        Update Function: - Updates health bar every frame
    */
    private void Update() 
    {
        mainHealth.UpdateHealth(maxHealth,currentHealth);
    }

    /*
        Input: float damage => The arbritary damage which will be deducted 
    
        Functionality: - Overwritten function from CharacterStats class that subtracts currentHealth 
                         with damage via base keyword
                       - Plays death audio from AudioManager singleton and particle effects

        Called In: Overwritten in another class
    */
    public override void TakeDamage(float damage)
    {
        if(splatterEffect != null)
        {
            PlayParticleEffects(splatterEffect);
        }
        
        base.TakeDamage(damage);        
        AudioManager.Instance.PlayEffect("PlayerDamage");
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
        AudioManager.Instance.PlayEffect("PlayerDie");

        // sets death conditions to true
        playerIsDead = true;
        dead = true;

        // calls og class from CharacterStats
        base.Die();

        animationController.ExecuteAnimation("Die");
        if(deathEffect != null){
            PlayParticleEffects(deathEffect);
        }
    }
}
