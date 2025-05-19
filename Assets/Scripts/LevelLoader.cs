using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1VICTORY()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel1Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevel2VICTORY()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevel2Restart()
    {
        SceneManager.LoadScene(5);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
