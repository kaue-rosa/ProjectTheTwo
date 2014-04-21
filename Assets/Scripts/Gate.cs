using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Gate : MonoBehaviour
{
	[SerializeField] private bool isPlayer = false;
	[SerializeField] private Team gateTeam = Team.TeamA;
	[SerializeField] private TroopManager tm = null;
	[SerializeField] private Transform troopsFolder = null;
	[SerializeField] private List<GameObject> troopPrefabs = new List<GameObject>();
	[SerializeField] private GameObject path = null;

	private GameElement gateElement = GameElement.NORMAL;
	private string nextTroopToSpawnName = "";
	private float currentTimer = 0;
	private float timeToSpawn = 0;
	private GateStats stats = null;
	private bool visible = false;

	public bool IsPlayer
	{
		get {return isPlayer;}
	}

	public string NextTroopToSpawnName
	{
		get{return nextTroopToSpawnName;}
		set{nextTroopToSpawnName = value;}
	}

	public Team GateTeam
	{
		get{return gateTeam;}
	}

	public bool CanSpawn
	{
		get;
		set;
	}

	public bool IsAlive
	{
		get;
		set;
	}

	public GateStats Stats
	{
		get{return stats;}
		set{ stats = value;}
	}

	public List<GameObject>TroopPrefabs
	{
		get{return troopPrefabs;}
		set{value = TroopPrefabs;}
	}

	public bool IsVisible
	{
		get {return visible;}
	}

	void Awake()
	{
		if(IsPlayer)
		{
			if(PlayerManager.control.SelectedTroops.Count>0)
				troopPrefabs = PlayerManager.control.SelectedTroops;
			else
				troopPrefabs = PlayerManager.control.TotalTroops;
		}
	}

	void Start()
	{
		//only if we didn't assign, try to do it automatically
		if (!tm)tm = FindObjectOfType<TroopManager> ();
		tm.ManageGate (this);
		nextTroopToSpawnName = troopPrefabs[0].name;
		stats = GetComponent<GateStats> ();
		if (!stats)Debug.LogError ("@Gate.Start(). No reference to stats");
		CanSpawn = true;
		IsAlive = true;
	}

	void Update()
	{
		if (IsAlive && CanSpawn)
		{
			currentTimer += Time.deltaTime;
			if (currentTimer >= timeToSpawn)
			{
				SpawnTroop ();
			}
		}
	}

	void SpawnTroop ()
	{
		//make sure we spawn the troop under a game object, for organization porposes.
		if (!troopsFolder)troopsFolder = new GameObject("Troops " + gateTeam).transform;
		GameObject _troopGm = (GameObject)Instantiate ((GameObject)Resources.Load (nextTroopToSpawnName), transform.position, transform.rotation);
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
		int trueDamage = damage;

		stats.CurrentHealth -= (int) Mathf.Round(trueDamage - (trueDamage*stats.Deffense));

		if (stats.CurrentHealth <= 0) {
			this.Die();
		}
	}

	void Die ()
	{
		tm.OnGateDestroyed (this);
		this.IsAlive = false;
	}

	void OnBecameVisible()
	{
		visible = true;
	}
	void OnBecameInvisible()
	{
		visible = false;
	}
}
