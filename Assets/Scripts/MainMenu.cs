using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Awake()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

    public void Scenes()
    {
        SceneManager.LoadScene(6);
    }
}
