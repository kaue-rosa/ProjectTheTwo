using UnityEngine;
using System.Collections;

public class GateStats : MonoBehaviour {

	//HP
	[SerializeField]private int currentHealth = 100;
	[SerializeField]private int maxHealth = 100;

	//XP
	[SerializeField]private int xp = 0;
	[SerializeField]private int level = 0;
	[SerializeField]private int defeatGivenXp = 0;

	private int maxGateXp = 1000;
	
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
	
	public int Xp
	{
		get{return xp;}
		set{xp = value;}
	}
	
	public int Level
	{
		get{return (int) Mathf.Floor(Xp*0.01f);}
	}

	public int DefeatGivenXp
	{
		get{ return defeatGivenXp;}
	}

	void Start()
	{
		currentHealth = maxHealth;
	}

	void Update()
	{
		print (Level);
	}

	public void GiveXP (int xpToGive)
	{
		this.xp += xpToGive;
		//TODO: have the max gate xp set somewhere
		if (this.xp > maxGateXp)this.xp = maxGateXp;
	}
}
