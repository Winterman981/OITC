using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    public int health;
    private int maxHP = 3;

    public TextMeshProUGUI healthUI;

    public GameObject red;
    public GameObject deathScreen;

    public MovementController mc;
    public MouseController moc;

    void Start()
    {
        healthUI.text = health.ToString();

        mc = FindObjectOfType<MovementController>();
        moc = FindObjectOfType<MouseController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(int healAmount)
	{
        health += healAmount;
        healthUI.text = health.ToString();

        if (health > maxHP)
		{
            health = maxHP;
            healthUI.text = health.ToString();
        }
    }

    public void TakeDamage(int damage)
	{
        if(health > 0)
		{
            StartCoroutine(FlashRed());
            health -= damage;
            healthUI.text = health.ToString();
        }

        if (health <= 0)
		{
            health = 0;
            healthUI.text = health.ToString();
            Die();
		}
	}

    void Die()
	{
        mc.enabled = false;
        moc.enabled = false;
        deathScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Player is dead");
	}

    IEnumerator FlashRed()
	{
        red.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        red.gameObject.SetActive(false);
    }
}
