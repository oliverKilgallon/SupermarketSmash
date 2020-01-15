using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindPlayers : Powerup
{

    public override IEnumerator execEffect(float duration, int playerNumber)
    {
        manager.CR_running = true;

        Debug.Log(playerNumber + " blinded everyone with " + name);

        for (int i = 1; i <= 4; i++)
        {
            if (i == playerNumber){}
            else
            {
                manager.playerPowerUpCanvas[i-1].gameObject.SetActive(true);
            } 
        }
        yield return new WaitForSeconds(duration);
        Debug.Log("effect ended");
        for (int i = 0; i < 4; i++)
        {
            manager.playerPowerUpCanvas[i].gameObject.SetActive(false);
        }
        manager.CR_running = false;
    }
}
