using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIescenas : MonoBehaviour
{
    public KeyCode triggerKey = KeyCode.Return; // Change this to the key you want to use

    void Update()
    {
        // Check if the specified key is pressed
        if (Input.GetKeyDown(triggerKey))
        {
            LoadNext();
        }
    }

    void LoadNext()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene in the build index, wrapping around if necessary
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}