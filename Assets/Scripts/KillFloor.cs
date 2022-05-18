using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    public void OnCollisionEnter(Collision col)
    {
        PlayerHP player = col.gameObject.GetComponent<PlayerHP>();
        if (player != null)
        {
            player.TakeDamage(1000);
        }
    }
}
