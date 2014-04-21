using UnityEngine;
using System.Collections;

public class TroopStats : MonoBehaviour 
{
	//HP
	[SerializeField]private GameElement element = GameElement.NORMAL;

	[SerializeField]private int currentHealth = 100;
	[SerializeField]private int maxHealth = 100;

	//Battle
	[SerializeField]private float rangeOfSight = 5f;
	[SerializeField]private float movementSpeed = 2f;
	[SerializeField]private int attackDamage = 50;
	[SerializeField]private float attackSpeed = 1f;
	[SerializeField]private float deffense = 10f;

	[SerializeField]private float spawnTime = 1f;

	public GameElement MyElement
	{
		get{return element;}
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
	}

	public float MovementSpeed
	{
		get{return movementSpeed;}
	}

	public int AttackDamage
	{
		get{return attackDamage;}
	}

	public float AttackSpeed
	{
		get{return 1/attackSpeed;}
	}

	public float Deffense
	{
		get{ return deffense*.01f; }
	}

	public float SpawnTime
	{
		get{return spawnTime;}
	}
}
