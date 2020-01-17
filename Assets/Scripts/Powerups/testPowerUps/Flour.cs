using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flour : Powerup
{



    public override IEnumerator execEffect(float duration, int playerNumber)
    {

        manager.CR_running = true;


        manager.players[playerNumber - 1].GetComponentInChildren<projectiles>().currentWeapon = "flour";
        yield return new WaitForSeconds(duration);
        manager.CR_running = false;
    }
}
