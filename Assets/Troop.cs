using UnityEngine;
using System.Collections;

public enum TroopType {
	LEFT_TROOP,
	RIGHT_TROOP,
}

public class Troop : MonoBehaviour {
	
	[SerializeField] private PathManager pm;
	[HideInInspector] public int nextPathNodeID = 0;

	public float troopSpeed = 2;
	public TroopType type = TroopType.LEFT_TROOP;


	// Use this for initialization
	void Start () {
		pm.ManageTroop (this);
	}
	
	// Update is called once per frame
	public Vector3 Move (Transform nextNode) {
		Vector3 direction = nextNode.position - transform.position;
		transform.position += direction.normalized * Time.deltaTime * troopSpeed;
		return direction;
	}

	public void LookForEnemy() {
	}

	public bool HasEnemyInRange() {
		return false;
	}

	public void Attack() 
	{
	
	}

}
