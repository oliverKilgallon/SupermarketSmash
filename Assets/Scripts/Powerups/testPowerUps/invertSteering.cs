using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invertSteering : Powerup
{
    public override IEnumerator execEffect(float duration, int playerNumber)
    {
        manager.CR_running = true;

        Debug.Log(playerNumber + " confused everyone with " + name);

        for (int i = 1; i <= 4; i++)
        {
            if (i == playerNumber){}
            else
            {
                manager.players[i-1].GetComponentInChildren<MoveMultiplayer>().turnSpeed *= -1;
            }
        }

        yield return new WaitForSeconds(duration);

        Debug.Log("effect ended");
        for (int i = 1; i <= 4; i++)
        {
            if (i == playerNumber){}
            else
            {
                manager.players[i-1].GetComponentInChildren<MoveMultiplayer>().turnSpeed *= -1;
            }
        }
        manager.CR_running = false;
    }
}
