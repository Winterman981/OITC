using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public MouseController moc;
    public Shoot sh;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        moc = FindObjectOfType<MouseController>();
        sh = FindObjectOfType<Shoot>();

        moc.enabled = false;
        sh.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Next()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
