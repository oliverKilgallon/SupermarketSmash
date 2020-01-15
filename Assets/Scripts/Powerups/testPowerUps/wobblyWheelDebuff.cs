using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wobblyWheelDebuff : Powerup
{
    public float timer = 0;
    public override IEnumerator execEffect(float duration, int playerNumber)
    {
        manager.CR_running = true;
        float multiplier = 1;
        while (timer != duration)
        {
            timer += 1;
            multiplier = Random.Range(Random.Range(-1, -.9f), Random.Range(.9f, 1f));
            manager.players[playerNumber-1].GetComponentInChildren<MoveMultiplayer>().turnSpeed *= multiplier;
            Debug.Log("WOBBLY");
            yield return new WaitForSeconds(duration/10);
            manager.players[playerNumber - 1].GetComponentInChildren<MoveMultiplayer>().turnSpeed /= multiplier;
        }
        yield return new WaitForSeconds(duration);
        Debug.Log("effect ended");
        manager.CR_running = false;
    }
}
