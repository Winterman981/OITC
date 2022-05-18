using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject pauseUI;
    public MouseController moc;
    public Shoot sh;

	// Update is called once per frame
	private void Start()
	{
        moc = FindObjectOfType<MouseController>();
        sh = FindObjectOfType<Shoot>();

        GamePaused = false;
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            if (GamePaused == false)
            {
                Pause(); 
            }
		}
    }

    public void Resume()
	{
        Cursor.visible = false;
        moc.enabled = true;
        sh.enabled = true;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        GamePaused = false;
    }

    public void Pause()
	{
        Cursor.visible = true;
        moc.enabled = false;
        sh.enabled = false;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        GamePaused = true;
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
	{
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Scenes()
    {
        SceneManager.LoadScene(6);
    }
}
