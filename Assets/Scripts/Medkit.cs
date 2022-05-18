using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public int healAmount;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(FindObjectOfType<PlayerHP>().health < 3)
			{
                FindObjectOfType<PlayerHP>().Heal(healAmount);
                FindObjectOfType<AudioManager>().Play("Medkit");
                Destroy(this.gameObject);
            }
        }
    }
}
