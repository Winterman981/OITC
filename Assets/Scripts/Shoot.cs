using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [SerializeField] private bool hasBullet;
    [SerializeField] private float cooldown;
    public bool infiniteAmmo;

    [SerializeField] private GameObject firePoint;

    public LineRenderer laser;

    public TextMeshProUGUI statusUi;
    public GameObject green;

    private string ready = "Ready!";
    private string charging = "Reloading...";

    public LayerMask ignoreLayer;


    // Start is called before the first frame update
    void Start()
    {
        hasBullet = true;

        statusUi.text = ready.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hasBullet == true)
		{
            Fire();
            FindObjectOfType<AudioManager>().Play("PlayerShoot");
        }
    }

    private void Fire()
	{
        RaycastHit hit;

		if (UnityEngine.Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, 4000, ~ignoreLayer))
	    {
            Debug.Log(hit.transform.name);

            //If the object hit by the Raycast has this script, deal damage.
            EnemyHP enemy = hit.transform.GetComponent<EnemyHP>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            //If not, perform reload.
            else
            {
                StartCoroutine(StartCooldown());
            }

            LineRenderer LR = Instantiate(laser, firePoint.transform.position, Quaternion.identity) as LineRenderer;
            Vector3[] LinePos = new Vector3[2];
            LinePos[0] = firePoint.transform.position;
            LinePos[1] = hit.point;
            LR.positionCount = 2;
            LR.SetPositions(LinePos);
        }

        //Reload if the Raycast hit nothing.

		else

		{
            LineRenderer LR = Instantiate(laser, firePoint.transform.position, Quaternion.identity) as LineRenderer;
            Vector3[] LinePos = new Vector3[2];
            LinePos[0] = firePoint.transform.position;
            LinePos[1] = firePoint.transform.forward * 100f;
            LR.positionCount = 2;
            LR.SetPositions(LinePos);

            StartCoroutine(StartCooldown());
        }
	}

    public void Unlimited()
	{
        StartCoroutine(Infinite());
	}

	IEnumerator StartCooldown()
	{
        if(infiniteAmmo == false)
		{
            hasBullet = false;
            statusUi.text = charging.ToString();
            yield return new WaitForSeconds(cooldown);
            statusUi.text = ready.ToString();
            hasBullet = true;
            FindObjectOfType<AudioManager>().Play("Recharged");
        }
	}

    IEnumerator Infinite()
    {
        green.gameObject.SetActive(true);
        infiniteAmmo = true;
        yield return new WaitForSeconds(5f);
        infiniteAmmo = false;
        green.gameObject.SetActive(false);
    }
}
