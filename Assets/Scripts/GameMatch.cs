using UnityEngine;
using System.Collections;

public class GameMatch : MonoBehaviour {

	[SerializeField] private TroopManager matchTroopManager = null;
	private bool matchIsOver = false;

	GameObject playerGate;

	void Awake()
	{


		if(PlayerManager.control.SelectedTroops.Count == 0)
		{
			foreach(GameObject troop in PlayerManager.control.TotalTroops)
			{
				PlayerManager.control.SelectedTroops.Add(troop);
			}
		}

		playerGate = GameObject.Find("PlayerGate");

		GameObject path = GameObject.Find("Path1");

		Gate gateScript = playerGate.GetComponent<Gate>();

		gateScript.IsPlayer = true;
		gateScript.GateTeam = Team.TeamA;
		gateScript.Path = path;

		Debug.Log(gateScript.Stats);

		GateStats savedStats = DataManager.Control.GetGateByElement(gateScript.Stats.MyElement);

		gateScript.Stats.CurrentHealth = savedStats.CurrentHealth;
		gateScript.Stats.MaxHealth = savedStats.MaxHealth;
		gateScript.Stats.Xp = savedStats.Xp;
		gateScript.Stats.Level = savedStats.Level;
		gateScript.Stats.Deffense = savedStats.Deffense;

		gateScript.TroopPrefabs.Clear();
		foreach(GameObject troop in PlayerManager.control.SelectedTroops)
		{
			gateScript.TroopPrefabs.Add(troop);
		}
	}

	void Start ()
	{
		matchIsOver = false;
	}
	void OnDisable()
	{
		//DataManager.Control.SetGateStats(playerGate.GetComponent<GateStats>());
		//DataManager.Control.SaveData();
	}

	void Update()
	{
		if (!matchIsOver)
		{
			matchTroopManager.ManageTroops ();
		}

		if(Input.GetKeyDown(KeyCode.UpArrow))
			Time.timeScale++;
		
		if(Input.GetKeyDown(KeyCode.DownArrow))
			Time.timeScale--;
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			playerGate.GetComponent<GateStats>().CurrentHealth-= 10;
		
		if(Input.GetKeyDown(KeyCode.RightArrow))
			playerGate.GetComponent<GateStats>().CurrentHealth+= 10;
	}

	void OnGUI ()
	{
		if (matchIsOver)
		{
			if (GUI.Button (new Rect(Screen.width/2 - 50, Screen.height/2 - 50, 100 ,100),"Restart")) {
					Application.LoadLevel (0);
			}
		}
	}

	public void SetMatchOver(int totalGameXP)
	{
		DataManager.Control.SetGateStats(playerGate.GetComponent<GateStats>());
		DataManager.Control.SaveData();

		matchIsOver = true;
		foreach (Gate g in matchTroopManager.gates)
		{
			g.CanSpawn = false;
			if(g.IsAlive && g.IsPlayer)
			{
				g.Stats.GiveXP(totalGameXP);
			}
		}
	}
}
