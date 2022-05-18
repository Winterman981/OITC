using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KamikazeAI : MonoBehaviour
{
    public Transform target;
    public bool seenPlayer;
    public bool inChaseRange;
    public float chaseDistance;
    public float attackRange;
    public bool attacking;

    public LayerMask whatIsPlayer;

    NavMeshAgent nav;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        seenPlayer = Physics.CheckSphere(transform.position, chaseDistance, whatIsPlayer);
        attacking = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //If in chase and attack range, shoot at player.
        if (seenPlayer && !attacking)
        {
            transform.LookAt(target);
            nav.SetDestination(target.position);
        }

        else if (seenPlayer && attacking)
		{
            Explode();
		}
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Explode();
        }
    }

    void Explode()
	{
		Destroy(gameObject);

		var hitColliders = Physics.OverlapSphere(transform.position, attackRange);
		foreach (var hitCollider in hitColliders)
		{
			var Enemy = hitCollider.GetComponent<EnemyHP>();
			if (Enemy)
			{
				Enemy.TakeDamage(1);
			}

            var Player = hitCollider.GetComponent<PlayerHP>();
            if (Player)
            {
                Player.TakeDamage(3);
            }
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
