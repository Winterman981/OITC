using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
	public GameObject text1;
	public GameObject text2;
	public GameObject text3;

	public int textID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(textID == 1)
			{
				StartCoroutine(Text1());
			}

			if (textID == 2)
			{
				StartCoroutine(Text2());
			}

			if (textID == 3)
			{
				StartCoroutine(Text3());
			}
		}
	}

	IEnumerator Text1()
	{
		text1.gameObject.SetActive(true);
		yield return new WaitForSeconds(5);
		text1.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}

	IEnumerator Text2()
	{
		text2.gameObject.SetActive(true);
		yield return new WaitForSeconds(5);
		text2.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}

	IEnumerator Text3()
	{
		text3.gameObject.SetActive(true);
		yield return new WaitForSeconds(5);
		text3.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}
}
