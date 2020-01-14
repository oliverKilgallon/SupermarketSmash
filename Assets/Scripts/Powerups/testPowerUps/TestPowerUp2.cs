using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPowerUp2 : Powerup
{
    public override IEnumerator execEffect(float duration, int playerNumber)
    {
        Debug.Log(playerNumber + " blinded everyone with " + name);



        yield return new WaitForSeconds(duration);
        Debug.Log("effect ended");
        manager.players[playerNumber].GetComponent<powerupSlot>().removeItem();
    }
}
