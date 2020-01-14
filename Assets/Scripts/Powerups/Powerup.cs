using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    new public string name;
    public Sprite icon;
    public float effectDuration;
    public bool throwable;
    public powerUpManager manager;

    private void Start()
    {
        manager = GameObject.Find("PowerUpManager").GetComponent<powerUpManager>();
    }

    public abstract IEnumerator execEffect(float duration,int playerNumber);
}
