using UnityEngine;
using System.Collections;

public class TroopStats : MonoBehaviour 
{
	[SerializeField]private float maxHealth = 10f;
	public float MaxHealth
	{
		get
		{ 
			return maxHealth;
		}
	}

	[SerializeField]private float rangeOfSight = 5f;
	public float RangeOfSight
	{
		get
		{
			return rangeOfSight;
		}
	}

	[SerializeField]private float movementSpeed = 2f;
	public float MovementSpeed
	{
		get
		{
			return movementSpeed;
		}
	}


	[SerializeField]private float attackDamage = 50f;
	public float AttackDamage
	{
		get
		{
			return attackDamage;
		}
	}

	
	[SerializeField]private float attackSpeed = 1f;
	public float AttackSpeed
	{
		get
		{
			return attackSpeed;
		}
	}
}
