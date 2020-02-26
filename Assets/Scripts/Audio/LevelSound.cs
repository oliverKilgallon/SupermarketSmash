using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSound : MonoBehaviour
{
    public string MainLevelName;
    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded -= PlayLevelMusic;
        SceneManager.sceneLoaded += PlayLevelMusic;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= PlayLevelMusic;
    }

    private void PlayLevelMusic(Scene scene, LoadSceneMode sceneMode)
    {
        
        if (scene.name.Equals(MainLevelName))
        {
            SoundManager.instance.PlaySound("Main Theme", true);
        }
    }
}
