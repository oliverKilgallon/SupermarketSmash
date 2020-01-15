using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPowerUp5 : Powerup
{
    public override IEnumerator execEffect(float duration, int playerNumber)
    {
        manager.CR_running = true;
        yield return new WaitForSeconds(duration);
        manager.CR_running = false;
    }
}
