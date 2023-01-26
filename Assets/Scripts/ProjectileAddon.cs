using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public bool destroyOnHit;

    [Header("Effects")]
    public GameObject muzzleEffect;
    public GameObject hitEffect;

    [Header("Explosive Projectile")]
    public bool isExplosive;
    public float explosionRadius;
    public float explosionForce;
    public int explosionDamage = 50;
    public GameObject explosionEffect;

    [Header("Lighter")]
    public bool isLight;
    public float lightRadius;

    [Header("Healing")]
    public bool isHeal;
    public GameObject healEffect;
    public float healRadius = 5f;

    private Rigidbody rb;

    private bool hitTarget;


    private void Start()
    {
        // get rigidbody component
        rb = GetComponent<Rigidbody>();
        

        // spawn muzzleEffect (if assigned)
        if(muzzleEffect != null)
            Instantiate(muzzleEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (hitTarget)
            return;
        else
            hitTarget = true;

        // enemy hit
        if (collision.gameObject.GetComponent<EnemyStats>() != null)
        {
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();

            // deal damage to the enemy
            enemy.TakeDamage(damage);

            // spawn hit effect (if assigned)
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            
            // destroy projectile
            if (!isExplosive && destroyOnHit)
                Invoke(nameof(DestroyProjectile), 0.1f);
        }

        // explode projectile if it's explosive
        if (isExplosive)
        {
            Explode();
            return;
        }

        else if (isLight) {
            LightenUp();
            return;
        }

        else if (isHeal) {
            Heal();
            return;
        }

        // make sure projectile sticks to surface
        rb.isKinematic = true;

        // make sure projectile moves with target
        transform.SetParent(collision.transform);
    }

    private void Explode()
    {
        // spawn explosion effect (if assigned)
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            
        AudioManager.Instance.PlayEffect("BombThrow");

        // find all the objects that are inside the explosion range
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

        // loop through all of the found objects and apply damage and explosion force
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            if (objectsInRange[i].gameObject == gameObject)
            {
                // don't break or return please, thanks
            }
            else
            {
                // check if object is enemy, if so deal explosionDamage
                if (objectsInRange[i].GetComponent<CharacterStats>() != null)
                    objectsInRange[i].GetComponent<CharacterStats>().TakeDamage(explosionDamage);

                // check if object has a rigidbody
                if (objectsInRange[i].GetComponent<Rigidbody>() != null)
                {
                    // custom explosionForce
                    Vector3 objectPos = objectsInRange[i].transform.position;

                    // calculate force direction
                    Vector3 forceDirection = (objectPos - transform.position).normalized;

                    // apply force to object in range
                    objectsInRange[i].GetComponent<Rigidbody>().AddForceAtPosition(forceDirection * explosionForce + Vector3.up * explosionForce, transform.position + new Vector3(0,-0.5f,0), ForceMode.Impulse);
                }
            }
        }

        // destroy projectile with 0.1 seconds delay
        wait(0.5f);
        Destroy(gameObject);
    }

    private void LightenUp() {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position,lightRadius);

        for(int i=0; i<objectsInRange.Length; i++) {
            if(objectsInRange[i].gameObject.tag == "Enemy" ) {
                objectsInRange[i].GetComponent<Outline>().enabled = true;
            }
        }
        wait(0.5f);
        Destroy(gameObject);
    }

    private void Heal() {
        if (healEffect != null)
            Instantiate(healEffect, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayEffect("HealImpact");
            
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position,healRadius);

        for(int i=0; i<objectsInRange.Length; i++) {
            int playerMask = LayerMask.NameToLayer("whatIsPlayer");
            if(objectsInRange[i].gameObject.layer == playerMask) {
                objectsInRange[i].GetComponent<CharacterStats>().currentHealth+=10;
                if(objectsInRange[i].GetComponent<CharacterStats>().currentHealth >= objectsInRange[i].GetComponent<PlayerStats>().maxHealth) {
                   objectsInRange[i].GetComponent<CharacterStats>().currentHealth = objectsInRange[i].GetComponent<PlayerStats>().maxHealth;
                }
                Debug.Log(objectsInRange[i].name + objectsInRange[i].GetComponent<CharacterStats>().currentHealth);
            }
        }
        wait(0.1f);
        Destroy(gameObject); 
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    IEnumerator wait(float num) { yield return new WaitForSeconds(num); }

}
