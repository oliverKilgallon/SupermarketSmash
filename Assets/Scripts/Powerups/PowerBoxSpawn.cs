using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoxSpawn : MonoBehaviour
{
    public GameObject boxPrefab;//box prefab to spawn
    public GameObject[] spawnPoints;//where should we spawn the boxes?
    public GameObject[] powerups;//power ups to add because they can't be in the prefab
    public GameObject boxParent;// for organization
    public int boxAmount;//no of boxes we want

    public int currentCubes;//no of boxes we have
    // Start is called before the first frame update
    void Start()
    {
        currentCubes = 0;//we have no cubes
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCubes < boxAmount) { SpawnCube(); }//if we need more cubes, get one
    }
    void SpawnCube()
    {
        currentCubes++;//we have a new cube so count it.
        int rand;
        int randAngle;
        rand = Random.Range(0, spawnPoints.Length - 1);//random spawn point.
        randAngle = Random.Range(0, 360);// random y spin
        GameObject box;
        Vector3 pos = new Vector3(spawnPoints[rand].transform.position.x+Random.Range(-4,4), spawnPoints[rand].transform.position.y, spawnPoints[rand].transform.position.z + Random.Range(-4, 4));//get the position of the chosen spawnpoint and shift it randomly from -4 to 4 in x and z(to stop physics overlap glitches if 2 go to the same place)
        box = Instantiate(boxPrefab, pos, Quaternion.Euler(new Vector3(0, randAngle, 0)), boxParent.transform);//spawn
        //box.GetComponent<powerUpBox>().Powerups[]
        int i = 0;
        foreach (GameObject PU in powerups)//add all the power ups to the box because they're scene objects
        {
            box.GetComponent<powerUpBox>().Powerups[i] = PU;
            i++;
        }

    }
}
