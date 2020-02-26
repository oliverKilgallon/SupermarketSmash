using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpBox : MonoBehaviour
{
    public GameObject[] Powerups; 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Powerup randomItem = Powerups[Random.Range(0, Powerups.Length)].GetComponent<Powerup>();
            Debug.Log(randomItem.GetComponent<Powerup>());
            if (!GameObject.Find("PowerUpManager").GetComponent<powerUpManager>().CR_running)
            {
                collision.gameObject.GetComponentInParent<powerupSlot>().updateCurrentItem(randomItem);
                GameObject.Find("Power Ups").GetComponent<PowerBoxSpawn>().currentCubes--;
                Destroy(gameObject);
            }
            
        }
        
    }
}
