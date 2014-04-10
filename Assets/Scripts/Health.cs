using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	private float maxHealth = 100;
	public void GetMaxHealth()
	{
		TroopStats stats = GetComponent<TroopStats>();
		if(stats)
			maxHealth = stats.MaxHealth;
		else
			Debug.LogWarning("This obj Doesnt Have Stats!");
	}
	private float currentHealth;


	void Start ()
	{
		GetMaxHealth();
	}


	void Update () 
	{
		
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
	}
}
