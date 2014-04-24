using UnityEngine;
using System.Collections;

public class TroopStats : MonoBehaviour 
{
	[SerializeField]private int troopID = 0;
	[SerializeField]private GameElement element = GameElement.NORMAL;

	//HP
	[SerializeField]private int currentHealth = 100;
	[SerializeField]private int maxHealth = 100;

	//Battle
	[SerializeField]private float rangeOfSight = 5f;
	[SerializeField]private float movementSpeed = 2f;
	[SerializeField]private int attackDamage = 50;
	[SerializeField]private float attackSpeed = 1f;
	[SerializeField]private float deffense = 10f;

	[SerializeField]private float spawnTime = 1f;

	public int TroopID
	{
		get{return troopID;}
		set{troopID = value;}
	}

	public GameElement TroopElement
	{
		get{return element;}
		set{element = value;}
	}

	public int CurrentHealth
	{
		get{return currentHealth;}
		set{currentHealth = value;}
	}
	
	public int MaxHealth
	{
		get{return maxHealth;}
		set{maxHealth = value;}
	}

	public float RangeOfSight
	{
		get{return rangeOfSight;}
		set{rangeOfSight = value;}
	}

	public float MovementSpeed
	{
		get{return movementSpeed;}
		set{movementSpeed = value;}
	}

	public int AttackDamage
	{
		get{return attackDamage;}
		set{attackSpeed = value;}
	}

	public float AttackSpeed
	{
		get{return 1/attackSpeed;}
		set{attackSpeed = 1/value;}
	}

	public float Deffense
	{
		get{ return deffense*0.01f; }
		set{deffense = value*0.01f;}
	}

	public float SpawnTime
	{
		get{return spawnTime;}
		set{spawnTime = value;}
	}
}
