using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyStats : CharacterStats
{
    [Header("Health")]
    [SerializeField]
    private float maxHealth = 50; 
      
    [Header("Particle Effects")]
    [SerializeField]
    private GameObject splatterEffect;
    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private Healthbar healthbar;

    [SerializeField]
    private GameObject[] drops;

    public CinemachineShake shake;
    float shakeTimer = 1;   

    public bool enemyIsDead;

    private void Awake() {
        InitializeHealth(maxHealth);
        healthbar.UpdateHealthBar(maxHealth,currentHealth);
        shake = GameObject.Find("Shake").GetComponent<CinemachineShake>();
    }

    public override void TakeDamage(float damage)
    {
        shake.Shake(2f,0);
        if(splatterEffect != null){
            PlayParticleEffects(splatterEffect);
        }
        base.TakeDamage(damage);        

        healthbar.UpdateHealthBar(maxHealth,currentHealth);

        Debug.Log(transform.name + "has" + currentHealth + "health");
    }

    public override void Die()
    {
        int randomIndex = Random.Range(0,drops.Length);
        enemyIsDead = true;
        base.Die();
        PlayParticleEffects(deathEffect);
        Instantiate(drops[randomIndex], new Vector3(transform.position.x, 1f, transform.position.z), Quaternion.identity);
    }


}
