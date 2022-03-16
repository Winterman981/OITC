using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;

    public int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);

        PlayerHP player = collision.gameObject.GetComponent<PlayerHP>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
