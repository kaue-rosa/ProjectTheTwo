using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Gate : MonoBehaviour
{
	[SerializeField]private bool player = false;
	public bool IsPlayer
	{
		get {return player;}
	}

	private GameElement gateElement = GameElement.NORMAL;

	[SerializeField] private Team gateTeam = Team.TeamA;
	[SerializeField] private Transform troopsFolder = null;
	[SerializeField] private TroopManager tm = null;
	[SerializeField] private List<string> prefabName = new List<string>();
	public List<string>PrefabName
	{
		get{return prefabName;}
	}
	[SerializeField] private Troop troopToSpawn = null;
	[SerializeField] private GameObject path = null;

	private bool visible = false;
	public bool IsVisible
	{
		get {return visible;}
	}

	private string nextTroopName = "";
	public string NextTroopName
	{
		get{return nextTroopName;}
		set{nextTroopName = value;}
	}
	private float currentTimer = 0;
	private float timeToSpawn = 0;
	private GateStats stats = null;

	public Team GateTeam
	{
		get{return gateTeam;}
	}


	void Start()
	{
		//only if we didn't assign, try to do it automatically
		if (!tm)tm = FindObjectOfType<TroopManager> ();
		tm.ManageGate (this);
		nextTroopName = prefabName[0];
		if(troopToSpawn)timeToSpawn = troopToSpawn.GetComponent<TroopStats>().SpawnTime;
		stats = GetComponent<GateStats> ();
		if (!stats)Debug.LogError ("@Gate.Start(). No reference to stats");
	}

	void Update()
	{
		currentTimer += Time.deltaTime;
		if (currentTimer >= timeToSpawn)
		{
			SpawnTroop();
		}
	}

	void SpawnTroop ()
	{
		//make sure we spawn the troop under a game object, for organization porposes.
		if (!troopsFolder)troopsFolder = new GameObject("Troops " + gateTeam).transform;
		GameObject _troopGm = (GameObject)Instantiate ((GameObject)Resources.Load (nextTroopName), transform.position, transform.rotation);
		_troopGm.transform.parent = troopsFolder;
		Troop _troop = _troopGm.GetComponent<Troop> ();
		_troop.TroopPathManager = this.tm;
		_troop.TroopTeam = gateTeam;
		_troop.AssignPath (path);
		currentTimer = 0;
		timeToSpawn = _troop.gameObject.GetComponent<TroopStats> ().SpawnTime;
	}

	public void TakeDamage (int damage)
	{
		stats.CurrentHealth -= damage;
		if (stats.CurrentHealth <= 0) {
			this.Die();
		}
	}

	void Die ()
	{
		Debug.LogError ("I'm DEAD :D", this);
	}

	void OnBecameVisible()
	{
		visible = true;
	}
	void OnBecameInvisible()
	{
		visible = false;
	}

//	void OnGUI()
//	{
//		for(int i = 0; i<prefabName.Count; i++)
//		{
//			if(GUILayout.Button("Unit " + i))
//				nextTroopName = prefabName[i];
//		}
//	}

}
