using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TroopSpawner : MonoBehaviour
{
	[SerializeField] private Team team;
	[SerializeField] private TroopManager pm = null;
	[SerializeField] private List<string> prefabName = new List<string>();
	[SerializeField] private float timeToSpawn = 1;
	[SerializeField] private Troop troopToSpawn;
	[SerializeField] private GameObject path;

	private string nextTroopName = "";
	private float currentTimer = 0;


	void Start()
	{
		nextTroopName = prefabName[0];
		if(troopToSpawn)
			timeToSpawn = troopToSpawn.GetComponent<TroopStats>().SpawnTime;
	}

	void Update()
	{
		currentTimer += Time.deltaTime;
		if (currentTimer >= timeToSpawn)
		{
			GameObject _troopGm = (GameObject)Instantiate((GameObject)Resources.Load(nextTroopName),transform.position,transform.rotation);
			Troop _troop = _troopGm.GetComponent<Troop>();
			_troop.TroopPathManager = this.pm;
			_troop.MyTeam = team;
			_troop.AssignPath(path);
			currentTimer = 0;
		}
	}

	void OnGUI()
	{
		if(prefabName.Count>1)
		{
			if(GUILayout.Button("Ball"))
				nextTroopName = prefabName[0];
			if(GUILayout.Button("Square"))
				nextTroopName = prefabName[prefabName.Count-1];
		}
	}

}
