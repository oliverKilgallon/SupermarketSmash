using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class playerModelChanger : MonoBehaviour
{
    //public Mesh playerMesh;
    public GameObject characterModel;

    public GameObject newCharModel;

    public AnimatorController anim;

    Vector3 pos;
    Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        pos = characterModel.transform.position;
        rot = characterModel.transform.rotation;

        Destroy(characterModel);


        playerModelExport pme = GameObject.FindGameObjectWithTag("playerModelExport").GetComponent<playerModelExport>();
        GameObject[] playerModelInfo = pme.playerPanels;
        GameObject player = playerModelInfo[GetComponentInChildren<MovementTest>().playerNumber - 1];

        newCharModel = Instantiate(player.GetComponent<playerPanel>().playerModel, pos, rot, this.gameObject.transform.GetChild(1));
        Animator charAnim = newCharModel.GetComponent<Animator>();
        charAnim.runtimeAnimatorController = anim;
        GetComponentInChildren<MovementTest>().animator = charAnim;

        Destroy(player.gameObject);

        

        //characterModel.GetComponent<MeshFilter>().mesh = playerMesh;
        //characterModel.GetComponent<SkinnedMeshRenderer>().sharedMesh = playerMesh;
    }
}
