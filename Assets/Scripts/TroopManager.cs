using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TroopManager : MonoBehaviour 
{
	public List<Troop> troops = new List<Troop>();

	void Start () 
	{
	}
	
	void Update () {
		ManageTroops ();
	}

	void ManageTroops ()
	{
		foreach (Troop _troop in troops) 
		{
		
			//First make this troop look for an enemy which is nearby
			if(_troop.HasEnemyInRange())
			{
				_troop.Attack();
				continue;
			}

			if(_troop.NextPathNodeID >= _troop.TotalPathNodes)continue;

			float _troopDistanceToNode = _troop.Move (_troop.MyPath[_troop.NextPathNodeID].transform).magnitude;
			if (_troopDistanceToNode < 0.1f)
			{
				_troop.NextPathNodeID += 1;
			}
		}
	}

	public void ManageTroop(Troop _troop)
	{
		troops.Add (_troop);
	}

	public void StopManagingTroop(Troop _troop) 
	{
		troops.Remove (_troop);
	}
}
