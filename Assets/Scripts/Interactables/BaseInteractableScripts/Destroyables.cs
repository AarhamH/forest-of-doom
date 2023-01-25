using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyables : Interactable
{
    public GameObject explosion;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "BombThrow" && this.tag == "Gate-Breakable") {
            Instantiate(explosion,new Vector3(transform.position.x, 2.5f,transform.position.z),Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public GameObject characterBrain;
    public PlayerChangeBrain playerChangeBrain;
    
    [SerializeField]
    private int index;
    private void Start() {

    }

    public override string GetDescription()
    {
        return "Needs some form of <i><b><u>Explosive</u></b></i> to break through...";
    }

    public override void Interact() {}

}
