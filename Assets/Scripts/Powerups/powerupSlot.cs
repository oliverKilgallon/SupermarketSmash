using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class powerupSlot : MonoBehaviour
{
    public RawImage slot;
    public Powerup current;

    private void Start()
    {
        removeItem();
    }


    public void updateCurrentItem(Powerup newItem)
    {
        current = newItem;
        slot.texture = newItem.icon.texture;
        if (slot.texture == null) { this.slot.color = new Color(1, 1, 1, 0); }
        else { this.slot.color = new Color(1, 1, 1, 1); }
        if (newItem.throwable)
        {
            StartCoroutine(newItem.execEffect(1, GetComponentInChildren<MovementTest>().playerNumber)); 
        }
    }

    public void removeItem()
    {
        this.current = null;
        this.slot.color = new Color(1,1,1,0);
    }




}
