using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasket : MonoBehaviour
{
    public bool fixedMode;
    public GameObject[] basketItems;
    Playerscript playerScript;
    MoveMultiplayer moveScript;
    public bool[] basketBools;

    public GameObject[] basketPoints;
    public GameObject[] basket;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = this.GetComponent<Playerscript>();
        moveScript = this.GetComponent<MoveMultiplayer>();
        //Debug.Log(basketItems[3].name);
    }

    // Update is called once per frame
    void Update()
    {
        if (fixedMode == true)
        {
            ResetBools();
            for (int i = 0; i < playerScript.currentHeld.Count; i++)
            {
                for (int j = 0; j < basketItems.Length; j++)
                {
                    if (basketItems[j].name == playerScript.currentHeld[i])
                    {
                        basketBools[j] = true;
                    }
                }
            }
            for (int i = 0; i < basketItems.Length; i++)
            {
                if (basketBools[i] == true)
                {
                    basketItems[i].SetActive(true);
                }
                else
                {
                    basketItems[i].SetActive(false);
                }
            }
        }
       
        
    }
    void ResetBools()
    {
        for(int i = 0;i<basketBools.Length; i++)
        {
            basketBools[i] = false;
        }


    }
}
