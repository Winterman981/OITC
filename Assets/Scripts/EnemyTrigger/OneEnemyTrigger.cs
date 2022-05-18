using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneEnemyTrigger : MonoBehaviour
{
    public GameObject enemy1;

	// Update is called once per frame
	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			enemy1.gameObject.SetActive(true);

            Destroy(this.gameObject);
        }
	}
}
