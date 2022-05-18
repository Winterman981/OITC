using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurstAI : MonoBehaviour
{
    private static BurstAI _instance;
    public Transform target;
    public bool seenPlayer;
    public bool inChaseRange;
    public float chaseDistance;
    public float attackRange;
    public bool attacking;

    public LayerMask whatIsPlayer;

    public Transform firePoint;
    public Bullet bulletPrefab;
    public float FireRate;
    public float shootForce;
    float nextShot = 0;


    public bool burstActive;
    public int maxBurst;
    private int burstNumber;
    public float burstDelay;

    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = new GameObject("Navigation");
        //go.transform.SetParent(this.transform, false);

        //go.AddComponent<NavMeshAgent>();
        //nav = go.GetComponent<NavMeshAgent>();

        nav = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        seenPlayer = Physics.CheckSphere(transform.position, chaseDistance, whatIsPlayer);
        attacking = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //If in chase and attack range, shoot at player.
        if (seenPlayer && attacking)
		{
            LookAt(target.position);
            firePoint.transform.LookAt(target);
            if (Time.time > nextShot)
            {
                if (burstActive == false)
				{
                    nextShot = Time.time + 1 / FireRate;
                    Shoot();
                    Debug.Log(burstNumber);

                    if (burstNumber == maxBurst)
                    {
                        StartCoroutine(BurstReload());
                    }
                }

            }
        }

        //If outside of attack range but in chase range, pursue player
        else if (seenPlayer && !attacking)
		{
            nav.SetDestination(target.position);
        }

    }

    void Shoot()
    {
        firePoint.transform.LookAt(target);
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(target.position - transform.position, Vector3.up));

        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * shootForce, ForceMode.Impulse);

        //FindObjectOfType<AudioManager>().Play("BurstShoot");

        burstNumber = burstNumber + 1;
    }

    IEnumerator BurstReload()
	{
        burstActive = true;
        yield return new WaitForSeconds(burstDelay);
        burstNumber = 1;
        Debug.Log(burstNumber);
        burstActive = false;
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(pos - transform.position, Vector3.up));
    }
}
