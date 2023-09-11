using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    // Navigate between scenes.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPracticeMode() {

        SceneManager.LoadScene(1);
    }

    public void LoadPlayWithComputerMode()
    {

        SceneManager.LoadScene(3);
    }

    public void LoadLandingScene()
    {
        SceneManager.LoadScene(0);
    }
}
