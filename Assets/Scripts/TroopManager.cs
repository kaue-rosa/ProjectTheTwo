using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TroopManager : MonoBehaviour {
	[SerializeField] private List<Transform> leftToRightNodes = new List<Transform>();
					 private List<Transform> rightToLeftNodes = new List<Transform>();

	private int totalNodes = 0;

	public List<Troop> troops = new List<Troop>();

	void Start () {
		totalNodes = leftToRightNodes.Count;
		foreach (Transform node in leftToRightNodes)rightToLeftNodes.Add (node);
		rightToLeftNodes.Reverse ();
	}
	
	void Update () {
		ManageTroops ();
	}

	void ManageTroops ()
	{
		foreach (Troop _troop in troops) {
		
			//First make this troop look for an enemy which is nearby
			if(_troop.HasEnemyInRange()) {
				_troop.Attack();
				continue;
			}

			if(_troop.nextPathNodeID >= totalNodes -1)continue;

			float _troopDistanceToNode = _troop.Move (GetNodeForTroop(_troop.type, _troop.nextPathNodeID)).magnitude;
			if (_troopDistanceToNode < 0.1f) {
				_troop.nextPathNodeID += 1;
			}
		}
	}

	public void ManageTroop(Troop _troop) {
		troops.Add (_troop);
	}

	public void StopManagingTroop(Troop _troop) {
		troops.Remove (_troop);
	}

	public Transform GetNodeForTroop(TroopType _tt, int _nextPathID) {
		if (_tt == TroopType.LEFT_TROOP)
			return leftToRightNodes [_nextPathID];
		else
			return rightToLeftNodes [_nextPathID];
	}
}
