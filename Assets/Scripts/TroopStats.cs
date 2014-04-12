using UnityEngine;
using System.Collections;

public class TroopStats : MonoBehaviour 
{
	//HP
	[SerializeField]private float currentHealth = 100;
	[SerializeField]private float maxHealth = 100;

	//Battle
	[SerializeField]private float rangeOfSight = 5f;
	[SerializeField]private float movementSpeed = 2f;
	[SerializeField]private float attackDamage = 50f;
	[SerializeField]private float attackSpeed = 1f;

	public float CurrentHealth
	{
		get{return currentHealth;}
		set{currentHealth = value;}
	}
	
	public float MaxHealth
	{
		get{return maxHealth;}
		set{maxHealth = value;}
	}

	public float RangeOfSight
	{
		get{return rangeOfSight;}
	}

	public float MovementSpeed
	{
		get{return movementSpeed;}
	}

	public float AttackDamage
	{
		get{return attackDamage;}
	}

	
	public float AttackSpeed
	{
		get{return attackSpeed;}
	}
}
