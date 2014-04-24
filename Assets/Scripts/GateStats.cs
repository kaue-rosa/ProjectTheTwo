using UnityEngine;
using System.Collections;

public class GateStats : MonoBehaviour 
{

	[SerializeField]private GameElement element = GameElement.NORMAL;

	//HP
	[SerializeField]private int currentHealth = 100;
	[SerializeField]private int maxHealth = 100;

	//XP
	[SerializeField]private int xp = 0;
	[SerializeField]private int level = 0;
	[SerializeField]private int defeatGivenXp = 0;

	private int maxGateXp = 1000;

	[SerializeField]private float deffense = 10f;


	public GameElement MyElement
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

	public float Deffense
	{
		get{ return deffense*.01f; }
		set {deffense = value;}
	}
	
	public int Xp
	{
		get{return xp;}
		set{xp = value;}
	}
	
	public int Level
	{
		get{return (int) Mathf.Floor(Xp*0.01f);}
		set {level = value;}
	}

	public int DefeatGivenXp
	{
		get{ return defeatGivenXp;}
	}

	void Start()
	{
		//currentHealth = maxHealth;
	}
	void FixedUpdate()
	{
		currentHealth = (currentHealth<0) ? 0:currentHealth;
	}

	public void GiveXP (int xpToGive)
	{
		this.xp += xpToGive;
		//TODO: have the max gate xp set somewhere
		if (this.xp > maxGateXp)this.xp = maxGateXp;
	}
}
