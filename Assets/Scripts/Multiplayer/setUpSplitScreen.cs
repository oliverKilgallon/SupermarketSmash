﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class setUpSplitScreen : MonoBehaviour
{
    public int numberOfPlayers;
    public GameObject[] cams;
    public GameObject[] lists;
    public GameObject[] players;

    public Vector2[] ListPosP2;
    public Vector2[] ListPosP3;
    public Vector2[] ListPosP4;

    public Text[] list1Text;
    public Text[] list2Text;
    public Text[] list3Text;
    public Text[] list4Text;

    public RawImage[] powerupSlots;

    public float Xpos, Ypos, PowerupSlotPosAbove, PowerupSlotPosBelow;

    public float FourPlayersCouponX, FourPlayersCouponY;
    public RawImage Coupon1, Coupon2, Coupon3, Coupon4;
    
    

    private void Awake()
    {
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].SetActive(true);
        }
        for (int i = 0; i < list1Text.Length; i++)
        {
            list1Text[i].rectTransform.anchoredPosition = new Vector3(0f, (53f - (12.5f * i)), 0f);
        }
        for (int i = 0; i < list2Text.Length; i++)
        {
            list2Text[i].rectTransform.anchoredPosition = new Vector3(0f,(53f - (12.5f * i)),0f);
        }
        for (int i = 0; i < list3Text.Length; i++)
        {
            list3Text[i].rectTransform.anchoredPosition = new Vector3(0f, (53f - (12.5f * i)), 0f);
        }
        for (int i = 0; i < list4Text.Length; i++)
        {
            list4Text[i].rectTransform.anchoredPosition = new Vector3(0f, (53f - (12.5f * i)), 0f);
        }
        //Player list UI position arrays
        //Each vector corresponds to one list

        ListPosP2 = new Vector2[]
        {
            new Vector2( -Xpos, Ypos ),
            new Vector2(-Xpos, -Ypos),
        };

        ListPosP3 = new Vector2[]
        {
            new Vector2( -Xpos, Ypos ),
            new Vector2( Xpos, Ypos ),
            new Vector2( -Xpos, -Ypos ),
        };

        ListPosP4 = new Vector2[]
        {
            new Vector2( -Xpos, Ypos ),
            new Vector2( Xpos, Ypos ),
            new Vector2( -Xpos, -Ypos ),
            new Vector2( Xpos, -Ypos ),
        };

        switch (numberOfPlayers)
        {
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    Camera c = cams[i].GetComponent<Camera>();
                    GameObject l = lists[i];
                    if (i < 2)
                    {
                        c.enabled = true;
                        c.rect = new Rect(0, .5f - (.5f * i), 1, .5f);
                        c.fieldOfView = 120;
                        c.fieldOfView = 60;
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP2[i];
                        
                    }
                }
                powerupSlots[0].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosBelow);
                powerupSlots[1].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosAbove);

                Coupon1.rectTransform.anchoredPosition = new Vector3(0, FourPlayersCouponY,0);
                Coupon2.rectTransform.anchoredPosition = new Vector3(0, -FourPlayersCouponY,0);

                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    Camera c = cams[i].GetComponent<Camera>();
                    GameObject l = lists[i];
                    if (i < 2)
                    {
                        c.enabled = true;
                        c.rect = new Rect(0+(.5f * i), .5f, .5f, .5f);
                        c.fieldOfView = 120;
                        c.fieldOfView = 60;
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP3[i];       
                    }
                    else if (i < 3)
                    {
                        c.enabled = true;
                        c.rect = new Rect(.25f, 0, .5f, .5f);
                        c.fieldOfView = 120;
                        c.fieldOfView = 60;
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP3[i];
                    }
                }
                powerupSlots[0].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosBelow);
                powerupSlots[1].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosBelow);
                powerupSlots[2].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosAbove);

                Coupon1.rectTransform.anchoredPosition = new Vector3(-FourPlayersCouponX, FourPlayersCouponY, 0);
                Coupon2.rectTransform.anchoredPosition = new Vector3(FourPlayersCouponX, FourPlayersCouponY, 0);
                Coupon3.rectTransform.anchoredPosition = new Vector3(0, -FourPlayersCouponY, 0);
                
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    Camera c = cams[i].GetComponent<Camera>();
                    GameObject l = lists[i];
                    if (i < 2)
                    {
                        c.enabled = true;
                        c.rect = new Rect(0 + (.5f * i), .5f, .5f, .5f);
                        c.fieldOfView = 120;
                        c.fieldOfView = 60;
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP4[i];

                    }
                    else
                    {
                        c.enabled = true;
                        c.rect = new Rect(0 + (.5f * (i-2)), 0, .5f, .5f);
                        c.fieldOfView = 120;
                        c.fieldOfView = 60;
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP4[i];

                    }
                }
                powerupSlots[0].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosBelow);
                powerupSlots[1].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosBelow);
                powerupSlots[2].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosAbove);
                powerupSlots[3].rectTransform.anchoredPosition = new Vector2(0, PowerupSlotPosAbove);

                Coupon1.rectTransform.anchoredPosition = new Vector3(-FourPlayersCouponX, FourPlayersCouponY, 0);
                Coupon2.rectTransform.anchoredPosition = new Vector3(FourPlayersCouponX, FourPlayersCouponY, 0);
                Coupon3.rectTransform.anchoredPosition = new Vector3(-FourPlayersCouponX, -FourPlayersCouponY, 0);
                Coupon4.rectTransform.anchoredPosition = new Vector3(FourPlayersCouponX, -FourPlayersCouponY, 0);

                break;
            default:
                
                break;
        }

    }





}
