using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TroopManager : MonoBehaviour 
{
	[SerializeField] private GameMatch gameMatch = null;
	public List<Troop> troops = new List<Troop>();
	public List<Gate> gates = new List<Gate>();

	public void ManageTroops ()
	{
		foreach (Troop _troop in troops) 
		{
			//First make this troop look for an enemy which is nearby
			if(_troop.HasEnemyInRange())
			{
				_troop.Attack();
				continue;
			}

			if(_troop.NextPathNodeID >= _troop.TotalPathNodes)
			{
				DamageGate(_troop.TroopAttackValue, _troop.TroopTeam);
				KillTroop(_troop);
				continue;
			}

			float _troopDistanceToNode = _troop.Move (_troop.MyPath[_troop.NextPathNodeID].transform).magnitude;
			if (_troopDistanceToNode < 0.1f)
			{
				_troop.NextPathNodeID += 1;
			}
		}
	}

	void DamageGate (int troopAttackValue, Team troopTeam)
	{
		//find the gate to damage
		for (int i = 0; i < gates.Count; i++) 
		{
			if(gates[i].GateTeam != troopTeam)
				gates[i].TakeDamage(troopAttackValue);
		}
	}

	void KillTroop (Troop _troop)
	{
		_troop.EnterGateEffect();
	}

	public void OnGateDestroyed()
	{
		gameMatch.SetMatchOver();
	}

	public void ManageTroop(Troop _troop)
	{
		troops.Add (_troop);
	}
	
	public void StopManagingTroop(Troop _troop) 
	{
		troops.Remove (_troop);
	}
	
	public void ManageGate (Gate _gate)
	{
		gates.Add (_gate);
	}
}
