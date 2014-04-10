using UnityEngine;
using System.Collections;

public enum TroopType {
	LEFT_TROOP,
	RIGHT_TROOP,
}

public class Troop : MonoBehaviour {
	
	[SerializeField] private PathManager pm = null;
	public PathManager TroopPathManager
	{
		get
		{
			return pm;
		}
		set
		{
			pm = value;
				pm.ManageTroop(this);
		}
	}
	[HideInInspector] public int nextPathNodeID = 0;

	public float troopSpeed = 2;
	public TroopType type = TroopType.LEFT_TROOP;

	private GameObject combatTarget;

	private TroopStats unitStats;

	// Use this for initialization
	void Awake()
	{
		unitStats = GetComponent<TroopStats>();
		if(!unitStats)
			Debug.LogWarning("Stats Script not found in " + name);
	}
	void Start () 
	{
			if(pm)pm.ManageTroop (this);

	}	
	// Update is called once per frame
	public Vector3 Move (Transform nextNode) 
	{
		Vector3 direction = nextNode.position - transform.position;
		transform.position += direction.normalized * Time.deltaTime * troopSpeed;
		return direction;
	}

	public bool LookForEnemy() 
	{
		if(!combatTarget)
		{
			Collider[] targets = Physics.OverlapSphere(transform.position, unitStats.RangeOfSight); // read all enemies around
				
			foreach(Collider c in targets)
			{
				Troop enemyScript = c.GetComponent<Troop>();
			
				if(c.gameObject.layer == LayerMask.NameToLayer("Troop") && CheckIsEnemy(enemyScript.type))
				{	
					combatTarget = c.gameObject; // assign the target
					return true;
				}
			}
			return false;
		}
		else return true;
	}

	private bool CheckIsEnemy(TroopType unitType)
	{
		if(unitType != this.type )
		{
			return true;
		}
		return false;
	}


	public void Attack() 
	{
		Debug.Log("Attack");
	}

}
