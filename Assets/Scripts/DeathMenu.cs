using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public MouseController moc;
    public Shoot sh;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = true;
        moc = FindObjectOfType<MouseController>();
        sh = FindObjectOfType<Shoot>();

        moc.enabled = false;
        sh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
