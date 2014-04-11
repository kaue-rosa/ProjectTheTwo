using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	private Troop troop;

	[SerializeField] private string damagePrefabName = "";

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
		troop = GetComponent<Troop>();
		if(!troop) Debug.LogWarning("Troop component missing in "+name);

		GetMaxHealth();
		currentHealth = maxHealth;
	}


	void Update () 
	{
		if(currentHealth <= 0)
			troop.Die();

	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

		if(damagePrefabName != "")
			Instantiate((GameObject)Resources.Load(damagePrefabName),transform.position,transform.rotation);
	}
}
