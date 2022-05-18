using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShotgunAI : MonoBehaviour
{
    private static AIController _instance;
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
                nextShot = Time.time + 1 / FireRate;
                Shoot();
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

        //FindObjectOfType<AudioManager>().Play("ShotgunShoot");

        Vector3 AimPosition = target.position;
        float spreadStat = 10;
        int distance = 1;
        float spread = (spreadStat * distance) / 10;

        for (int i = 0; i < 5; i++)
        {
            Vector3 PelletPos = AimPosition + Random.insideUnitSphere * spread;
            firePoint.transform.LookAt(PelletPos);
            Bullet bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet1.GetComponent<Rigidbody>().AddForce(bullet1.transform.forward * shootForce, ForceMode.Impulse);
        }
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
