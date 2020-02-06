using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoxSpawn : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject[] spawnPoints;
    public GameObject[] powerups;
    public GameObject boxParent;
    public int boxAmount;

    public int currentCubes;
    // Start is called before the first frame update
    void Start()
    {
        currentCubes = 0;
        //boxParent = this.gameObject;
        //SpawnCube();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCubes < boxAmount) { SpawnCube(); }
    }
    void SpawnCube()
    {
        currentCubes++;
        int rand;
        int randAngle;
        rand = Random.Range(0, spawnPoints.Length - 1);
        randAngle = Random.Range(0, 360);
        GameObject box;
        Vector3 pos = new Vector3(spawnPoints[rand].transform.position.x+Random.Range(-4,4), spawnPoints[rand].transform.position.y, spawnPoints[rand].transform.position.z + Random.Range(-4, 4));
        box = Instantiate(boxPrefab, pos, Quaternion.Euler(new Vector3(0, randAngle, 0)), boxParent.transform);
        //box.GetComponent<powerUpBox>().Powerups[]
        int i = 0;
        foreach (GameObject PU in powerups)
        {
            box.GetComponent<powerUpBox>().Powerups[i] = PU;
            i++;
        }

    }
}
