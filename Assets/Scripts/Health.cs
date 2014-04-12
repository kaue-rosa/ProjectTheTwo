using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	private Troop troop;

	//[SerializeField] private string damagePrefabName = "";

	private float _health = 100;
	private float _maxHealth = 100;
	private float _currentHealth = 100;

	public float health
	{
		get{return _health;}
		set{_health = value;}
	}

	public float maxHealth
	{
		get{return _maxHealth;}
		set{_maxHealth = value;}
	}

	public float currentHealth
	{
		get{return _currentHealth;}
		set{_currentHealth = value;}
	}

	void Start ()
	{
		currentHealth = maxHealth;
	}
	/*
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
	}*/
}
