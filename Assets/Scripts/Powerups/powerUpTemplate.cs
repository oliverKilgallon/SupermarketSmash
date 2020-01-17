using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpTemplate : Powerup
{
    //variables assigned in inspector

    public override IEnumerator execEffect(float duration, int playerNumber)
    {
        manager.CR_running = true;
        //effect code here
        //////////////////
        yield return new WaitForSeconds(duration);
        //reset effect here
        ///////////////////
        manager.CR_running = false;
    }
}
