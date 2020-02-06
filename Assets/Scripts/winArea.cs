using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winArea : MonoBehaviour
{
    public GameObject yourWinner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Gameplayloop gc = GameObject.Find("GameController").GetComponent<Gameplayloop>();
            if (gc.roundTime <= 0)
            {
                int max = int.MinValue;
                GameObject winner = null;
                foreach (GameObject player in gc.players)
                {
                    Debug.Log(player.GetComponent<Playerscript>().currentHeld.Count);
                    if (player.GetComponent<Playerscript>().currentHeld.Count > max)
                    {
                        winner = player;
                        Debug.Log("Player "+winner.GetComponent<MoveMultiplayer>().playerNumber);
                    }
                }
                Debug.Log("Winner is: " + winner.GetComponent<MoveMultiplayer>().playerNumber);
            }
            yourWinner.SetActive(true);
        }

    }
}
