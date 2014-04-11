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
			//pm.ManageTroop(this);
		}
	}
	[HideInInspector] public int nextPathNodeID = 0;
	public TroopType type = TroopType.LEFT_TROOP;

	private Troop combatTarget;
	private TroopStats unitStats;
	private Health health;

	private float attackTimer = 0;

	// Use this for initialization
	void Start () 
	{
		health = GetComponent<Health>();
		if(!health)Debug.LogWarning("Health Script not found in " + name);

		unitStats = GetComponent<TroopStats>();
		if(!unitStats)Debug.LogWarning("Stats Script not found in " + name);

		if(pm)pm.ManageTroop (this);

		StartCoroutine ("LookForEnemy");
	}
	
	// Update is called once per frame
	public Vector3 Move (Transform nextNode) 
	{
		Vector3 direction = nextNode.position - transform.position;
		transform.position += direction.normalized * Time.deltaTime * unitStats.MovementSpeed;
		return direction;
	}

	public IEnumerator LookForEnemy() 
	{
		while(true)
		{
			Collider[] targets = Physics.OverlapSphere(transform.position, unitStats.RangeOfSight); // read all enemies around
			foreach(Collider c in targets) // for each enemy found
			{
				Troop enemyScript = c.GetComponent<Troop>();
				if(c.gameObject.layer == LayerMask.NameToLayer("Troop") && CheckIsEnemy(enemyScript.type))
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

	private bool CheckIsEnemy(TroopType unitType)
	{
		if(unitType != this.type )return true;
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
			attackTimer = unitStats.AttackSpeed;
			//Debug.Log("Attack");
			combatTarget.TakeDamage(unitStats.AttackDamage);
		}
		else 
		{
			attackTimer -= Time.deltaTime;
		}
	}

	public void TakeDamage (float damage)
	{
		health.TakeDamage(damage);
	}

	public void Die()
	{
		TroopPathManager.StopManageTroop (this);
		Destroy (this.gameObject);
	}

}
