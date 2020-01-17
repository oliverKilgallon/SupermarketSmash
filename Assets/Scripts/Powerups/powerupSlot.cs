﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class powerupSlot : MonoBehaviour
{
    public RawImage slot;
    public Powerup current;

    private void Start()
    {
        current = null;
    }


    public void updateCurrentItem(Powerup newItem)
    {
        current = newItem;
        slot.texture = newItem.icon.texture;
    }

    public void removeItem()
    {
        this.current = null;
        this.slot.texture = null;
        Debug.Log("removing item");
    }




}