using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfAmmo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Shoot>().Unlimited();
            FindObjectOfType<AudioManager>().Play("Infinite");
            Destroy(this.gameObject);
        }
    }
}
