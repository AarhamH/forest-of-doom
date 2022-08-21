using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollToggle : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public CapsuleCollider capsuleCollider;
    public EnemyController enemyController;

    public Collider[] ChildrenCollider;
    public Rigidbody[] ChildrenRigidBody;

    // Start is called before the first frame update
    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyController = GetComponent<EnemyController>();
    
        ChildrenCollider = GetComponentsInChildren<Collider>();
        ChildrenRigidBody = GetComponentsInChildren<Rigidbody>();
    }

    private void Start() {
        RagDollActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyStats.enemyIsDead){
            RagDollActive(true);
        }
    }

    private void RagDollActive(bool active){

        //children
        foreach(var collider in ChildrenCollider){
            collider.enabled = active;
        }
        foreach(var rigidbody in ChildrenRigidBody){
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }


        animator.enabled = !active;
        rb.detectCollisions = !active;
        rb.isKinematic = active;
        capsuleCollider.enabled = !active;
        enemyController.enabled = !active;
    }
}
