using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Godmode : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHP>().God();
            FindObjectOfType<AudioManager>().Play("God");
            Destroy(this.gameObject);
        }
    }
}
