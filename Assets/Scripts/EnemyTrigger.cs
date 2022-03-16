using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

	// Update is called once per frame
	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
            enemy1.gameObject.SetActive(true);
            enemy2.gameObject.SetActive(true);
            enemy3.gameObject.SetActive(true);

            Destroy(this.gameObject);
        }
	}
}
