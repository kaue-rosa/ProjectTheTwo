using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Team
{
	TeamA,
	TeamB,
}

public class Troop : MonoBehaviour 
{
	[SerializeField] private TroopManager pm = null;


	private Team myTeam;
	private List<GameObject> myPath = new List<GameObject>();
	private int nextPathNodeID = 0;
	private int totalPathNodes = 0;
	private float attackTimer = 0;
	private float lastXPosition = 0;
	private bool hit = false;
	private Troop combatTarget;
	private TroopStats troopStats;
	private TroopAnimation troopAnimation;
	private Gate troopGate;

	public Team TroopTeam
	{
		get{return myTeam;}
		set{myTeam = value;}
	}

	public TroopManager TroopPathManager
	{
		get{return pm;}
		set{pm = value;}
	}

	public List<GameObject> MyPath
	{
		get{return myPath;}
	}

	public int NextPathNodeID
	{
		get{return nextPathNodeID;}
		set{nextPathNodeID = value;}
	}

	public int TotalPathNodes
	{
		get {return totalPathNodes;}
	}

	public int TroopAttackValue
	{
		get{return troopStats.AttackDamage;}
	}

	public GameElement TroopElement
	{
		get{return troopStats.TroopElement;}
	}

	public Gate TroopGate {
		get{return troopGate;}
		set{troopGate = value;}
	}

	void Start () 
	{
		lastXPosition = transform.position.x;

		troopStats = GetComponent<TroopStats>();
		troopAnimation = GetComponent<TroopAnimation>();

		if(!troopStats)Debug.LogWarning("Stats Script not found in " + name);
		if(!troopAnimation)Debug.LogWarning("Animation Script not found in " + name);

		if(pm)pm.ManageTroop (this);

		StartCoroutine ("LookForEnemy");
	}

	void Update()
	{
		//face the troop in the correct direction
		if (transform.position.x < lastXPosition)transform.localScale = new Vector3(-1,1,1);
		else if (transform.position.x > lastXPosition)transform.localScale = new Vector3(1,1,1);
		lastXPosition = transform.position.x;

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
		if (!hit) {
			troopAnimation.StartWalking ();
			transform.position += direction.normalized * Time.deltaTime * troopStats.MovementSpeed;
		}
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
			if(!hit)
			{
				attackTimer = troopStats.AttackSpeed;
				troopAnimation.StartAttacking(()=>{
					if(this.combatTarget)
					this.combatTarget.TakeDamage(this.troopStats.AttackDamage, this.troopStats.TroopElement);
				});
			}
		}
		else 
		{
			attackTimer -= Time.deltaTime;
		}
	}

	public void TakeDamage (int damage, GameElement attakerElement)
	{
		hit = true;
		Instantiate((GameObject)Resources.Load("Particles/Attack Particle"),transform.position,transform.rotation);
		int trueDamage = (int) Mathf.Round(damage * Element.GetMultiplayerForAttackerElement(attakerElement,this.troopStats.TroopElement));
		troopStats.CurrentHealth -= (int)(trueDamage - (trueDamage*troopStats.Deffense));
		troopAnimation.Hit (()=>{
			this.hit = false;
			if(this.troopStats.CurrentHealth <= 0){
				Destroy(this.gameObject);
				troopGate.TroopRemoved (this);
			}
		});
	}

	public void Die()
	{
		TroopPathManager.StopManagingTroop (this);
		this.StopCoroutine("LookForEnemy");
		collider.enabled = false;
	}

	public void Celebrate()
	{
		if(this.troopStats.CurrentHealth > 0)
		this.troopAnimation.StartCelebrating ();
	}

	public void EnterGateEffect ()
	{
		//should fade first, or do some effect
		troopStats.CurrentHealth = 0;
		Destroy (this.gameObject,0.1f);
	}
}
