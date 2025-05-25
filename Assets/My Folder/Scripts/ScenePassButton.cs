using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePassButton : MonoBehaviour
{
    public int sceneIndexToLoad = 2;                // Current Scene index = 1 (0 is for Game Menu)

    public void LoadNextScene()
    {
        Debug.Log("Button pressed. Loading scene...");
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
