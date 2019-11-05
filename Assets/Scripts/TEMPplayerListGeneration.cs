using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TEMPplayerListGeneration : MonoBehaviour
{
    public float listLength;
    public ArrayList items = new ArrayList();
    public ArrayList playerList = new ArrayList();

    public Text[] list;




    void Start()
    {
        int counter = 0;
        string line;

        System.IO.StreamReader file =
            new System.IO.StreamReader("Assets/test.txt");
        while ((line = file.ReadLine()) != null)
        {
            Debug.Log(line);
            items.Add(line);
            counter++;
        }

        file.Close();
        Debug.Log("There were " + counter + " lines.");
        generateList();
    }
    void Update()
    {
        
    }
    void generateList()
    {
        for (int i = 0; i < listLength; i++)
        {
            playerList.Add(items[Random.Range(0, items.Count - 1)]);
            list[i].text = (string)items[Random.Range(0, items.Count - 1)];
        }
    }





}
