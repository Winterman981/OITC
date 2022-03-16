using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [SerializeField] private bool hasBullet;
    [SerializeField] private float cooldown;

    [SerializeField] private GameObject firePoint;

    public LineRenderer laser;

    public TextMeshProUGUI statusUi;

    private string ready = "Ready!";
    private string charging = "Reloading...";

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
        }
    }

    private void Fire()
	{
        hasBullet = false;
            RaycastHit hit;
		if (UnityEngine.Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, 4000))
	    {
            Debug.Log(hit.transform.name);

            //If the object hit by the Raycast has this script, deal damage.
            EnemyHP enemy = hit.transform.GetComponent<EnemyHP>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
                hasBullet = true;
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
            StartCoroutine(StartCooldown());

            LineRenderer LR = Instantiate(laser, firePoint.transform.position, Quaternion.identity) as LineRenderer;
            Vector3[] LinePos = new Vector3[2];
            LinePos[0] = firePoint.transform.position;
            LinePos[1] = firePoint.transform.forward  * 100f;
            LR.positionCount = 2;
            LR.SetPositions(LinePos);
        }
	}

	IEnumerator StartCooldown()
	{
		statusUi.text = charging.ToString();
		yield return new WaitForSeconds(cooldown);
		statusUi.text = ready.ToString();
		hasBullet = true;
	}
}
