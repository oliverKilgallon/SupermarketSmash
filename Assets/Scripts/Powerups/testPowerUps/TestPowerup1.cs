using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPowerup1 : Powerup
{
    public override IEnumerator execEffect(float duration, int playerNumber)
    {
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
        manager.players[playerNumber].GetComponent<powerupSlot>().removeItem();
    }
}
