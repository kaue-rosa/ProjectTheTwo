﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Team
{
	TeamA,
	TeamB,
}

public class Troop : MonoBehaviour {

	private Element element = new Element();

	private Team myTeam;
	public Team TroopTeam
	{
		get
		{
			return myTeam;
		}
		set
		{
			myTeam = value;
		}
	}

	[SerializeField] private TroopManager pm = null;
	public TroopManager TroopPathManager
	{
		get
		{
			return pm;
		}
		set
		{
			pm = value;
			//pm.ManageTroop(this);
		}
	}

	private List<GameObject> myPath = new List<GameObject>();
	public List<GameObject> MyPath
	{
		get 
		{
			return myPath;
		}
	}
	private int nextPathNodeID = 0;
	public int NextPathNodeID
	{
		get
		{
			return nextPathNodeID;
		}
		set 
		{
			nextPathNodeID = value;
		}
	}
	private int totalPathNodes = 0;
	public int TotalPathNodes
	{
		get 
		{
			return totalPathNodes;
		}
	}

	private Troop combatTarget;
	private TroopStats troopStats;

	public int TroopAttackValue
	{
		get{return troopStats.AttackDamage;}
	}

	private float attackTimer = 0;

	// Use this for initialization
	void Start () 
	{
		troopStats = GetComponent<TroopStats>();
		if(!troopStats)Debug.LogWarning("Stats Script not found in " + name);
		if(pm)pm.ManageTroop (this);
		StartCoroutine ("LookForEnemy");
	}

	void LateUpdate()
	{
		if (troopStats.CurrentHealth <= 0) this.Die ();
	}

	public void AssignPath(GameObject path)
	{
		for(int i=0; i<path.transform.childCount; i++)
		{
			GameObject pathNode = path.transform.GetChild(i).gameObject;
			if(pathNode)
				myPath.Add(pathNode);
		}
		totalPathNodes = myPath.Count;
	}

	public Vector3 Move (Transform nextNode) 
	{
		Vector3 direction = nextNode.position - transform.position;
		transform.position += direction.normalized * Time.deltaTime * troopStats.MovementSpeed;
		return direction;
	}

	public IEnumerator LookForEnemy() 
	{
		while(true)
		{
			Collider[] targets = Physics.OverlapSphere(transform.position, troopStats.RangeOfSight); // read all enemies around
			foreach(Collider c in targets) // for each enemy found
			{
				Troop enemyScript = c.GetComponent<Troop>();
				if(c.gameObject.layer == LayerMask.NameToLayer("Troop") && CheckIsEnemy(enemyScript.TroopTeam))
				{	
					if(!combatTarget ) // make sure forget the old target
					{
						combatTarget = c.GetComponent<Troop>(); // assign the target
					}
				}
			}
			yield return null; // every frame
		}
	}

	private bool CheckIsEnemy(Team unitTeam)
	{
		if(unitTeam != this.myTeam )return true;
		return false;
	}

	public bool HasEnemyInRange() 
	{
		if (combatTarget)return true;
		return false;
	}

	public void Attack() 
	{
		if(attackTimer <= 0)
		{
			attackTimer = troopStats.AttackSpeed;
			combatTarget.TakeDamage(troopStats.AttackDamage, troopStats.MyElement);
		}
		else 
		{
			attackTimer -= Time.deltaTime;
		}
	}

	public void TakeDamage (int damage, GameElement attakerElement)
	{

		//health.TakeDamage(damage);
		Instantiate((GameObject)Resources.Load("Particles/Attack Particle"),transform.position,transform.rotation);
		troopStats.CurrentHealth -= (int) Mathf.Round((damage * element.ElementCheck(attakerElement)));

	}

	public void Die()
	{
		TroopPathManager.StopManagingTroop (this);
		this.StopCoroutine("LookForEnemy");
		Destroy (this.gameObject);
	}

	public void EnterGateEffect ()
	{
		//should face first, or do some effect
		troopStats.CurrentHealth = 0;
	}
}
