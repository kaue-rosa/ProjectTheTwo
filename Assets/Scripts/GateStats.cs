using UnityEngine;
using System.Collections;

public class GateStats : MonoBehaviour {

	//HP
	[SerializeField]private int currentHealth = 100;
	[SerializeField]private int maxHealth = 100;

	[SerializeField]private float deffense = 10f;

	[SerializeField]private int xp = 100;
	[SerializeField]private int level = 2;
	
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

	public float Deffense
	{
		get{ return deffense*.01f; }
	}
	
	public int Xp
	{
		get{return xp;}
		set{xp = value;}
	}
	
	public int Level
	{
		get{return level;}
		set{level = value;}
	}

	void Start()
	{
		currentHealth = maxHealth;
	}

}
