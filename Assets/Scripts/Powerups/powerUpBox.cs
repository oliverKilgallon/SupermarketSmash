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
            Powerup randomItem = Powerups[Random.Range(0, Powerups.Length - 1)].GetComponent<Powerup>();
            if (collision.gameObject.GetComponentInParent<powerupSlot>().current == null)
            {
                collision.gameObject.GetComponentInParent<powerupSlot>().updateCurrentItem(randomItem);
            }
            Destroy(gameObject);
        }
        
    }
}
