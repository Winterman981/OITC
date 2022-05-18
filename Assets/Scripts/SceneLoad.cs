using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
	public int number;
    public void LoadScene(int sceneNumber)
	{
		number = sceneNumber;
		Cursor.visible = false;
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneNumber);
	}
}
