using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAddon : MonoBehaviour
{
    [SerializeField]
    private int damage = 20;
    [SerializeField]
    private GameObject hitEffect;

    SwordAttack swordAttack;

    private void Awake() {
        swordAttack = GetComponent<SwordAttack>();
    }

    // TO DO: Use OverlapCollider() instead of OnCollisionEnter();
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<EnemyStats>() != null)
        {
            collision.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
        }
    }

}
