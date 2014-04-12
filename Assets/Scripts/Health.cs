using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	private Troop troop;

	[SerializeField] private string damagePrefabName = "";

	private float maxHealth = 100;
	private float currentHealth = 100;

	public float MaxHealth
	{
		get{return maxHealth;}
		set{maxHealth = value;}
	}

	public float CurrentHealth
	{
		get{return currentHealth;}
		set{currentHealth = value;}
	}

	void Start ()
	{
		troop = GetComponent<Troop>();
		if(!troop) Debug.LogWarning("Troop component missing in " + name);
		
		currentHealth = MaxHealth;
	}

	//TODO: Get Rid of this!!!!!!!!!!!!!!!
	void Update () 
	{
		if(currentHealth <= 0)
			troop.Die();
	}

	//TODO: And This!!!!!!!!!!!!!!!!!!!
	public void TakeDamage(float damage)
	{
		currentHealth -= damage;

		if(damagePrefabName != "")
			Instantiate((GameObject)Resources.Load(damagePrefabName),transform.position,transform.rotation);
	}
}
