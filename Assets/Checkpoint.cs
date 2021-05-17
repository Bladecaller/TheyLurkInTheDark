using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
        {
        other.gameObject.transform.position = respawnPoint.transform.position;
        other.gameObject.GetComponent<PlayerCombat>().takeDMG(10);
        FindObjectOfType<AudioManager>().Play("Checkpoint");
        }
        if(other.tag == "Crab" || other.tag == "Octopus" || other.tag == "Jumper"){
            other.gameObject.GetComponent<Enemy>().dead = true;
        }
    }
}
