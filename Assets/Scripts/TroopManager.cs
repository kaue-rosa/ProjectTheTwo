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
				//find the gate to damage
				Gate gateToDamage = null;
				for (int i = 0; i < gates.Count; i++){if(gates[i].GateTeam != _troop.TroopTeam)gateToDamage = gates[i];}
				DamageGate(_troop.TroopAttackValue,_troop.TroopElement , _troop, gateToDamage);
				continue;
			}

			float _troopDistanceToNode = _troop.Move (_troop.MyPath[_troop.NextPathNodeID].transform).magnitude;
			if (_troopDistanceToNode < 0.1f)
			{
				_troop.NextPathNodeID += 1;
			}
		}
	}

	void DamageGate (int troopAttackValue, GameElement attackElement, Troop _troop, Gate gateToDamage)
	{
		if(gateToDamage.CanDamageGate)
		{   
			gateToDamage.TakeDamage(troopAttackValue, attackElement);
			KillTroop(_troop);
		}
	}

	void KillTroop (Troop _troop)
	{
		_troop.EnterGateEffect();
	}

	public void OnGateDestroyed(Gate gateDestroyed)
	{
		int matchXP = gateDestroyed.Stats.DefeatGivenXp;
		gameMatch.SetMatchOver(matchXP);
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
