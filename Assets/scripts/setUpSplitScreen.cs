using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class setUpSplitScreen : MonoBehaviour
{
    public int numberOfPlayers;
    public GameObject[] cams;
    public GameObject[] lists;

    public Vector2[] ListPosP2;
    public Vector2[] ListPosP3;
    public Vector2[] ListPosP4;

    private void Awake()
    {
        ListPosP2 = new Vector2[]
        {
            new Vector2( 0, 415 ),
            new Vector2( 0, -415 ),
        };

        ListPosP3 = new Vector2[]
        {
            new Vector2( -400, 415 ),
            new Vector2( 400, 415 ),
            new Vector2( 0, -415 ),
        };

        ListPosP4 = new Vector2[]
        {
            new Vector2( -400, 415 ),
            new Vector2( 400, 415 ),
            new Vector2( -400, -415 ),
            new Vector2( 400, -415 ),
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
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP2[i];
                    }
                }
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
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP3[i];
                    }
                    else if (i < 3)
                    {
                        c.enabled = true;
                        c.rect = new Rect(.25f, 0, .5f, .5f);
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP3[i];
                    }
                }
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
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP4[i];
                    }
                    else
                    {
                        c.enabled = true;
                        c.rect = new Rect(0 + (.5f * (i-2)), 0, .5f, .5f);
                        l.gameObject.SetActive(true);
                        l.GetComponent<RawImage>().rectTransform.anchoredPosition = ListPosP4[i];
                    }
                }
                break;
            default:
                
                break;
        }

    }





}
