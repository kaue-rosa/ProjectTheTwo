using UnityEngine;
using System.Collections;

public class GameMatch : MonoBehaviour {

	[SerializeField] private TroopManager matchTroopManager = null;
	private bool matchIsOver = false;

	void Awake()
	{


		if(PlayerManager.control.SelectedTroops.Count == 0)
		{
			foreach(GameObject troop in PlayerManager.control.TotalTroops)
			{
				PlayerManager.control.SelectedTroops.Add(troop);
			}
		}

		Transform playerGatePosition = GameObject.Find("PlayerGate").transform;

		GameObject path = GameObject.Find("Path1");
		
		GameObject playerGate = (GameObject)Instantiate(Resources.Load("Gates/"+PlayerManager.control.SelectedGate.name), playerGatePosition.position, Quaternion.identity);
		Gate gateScript = playerGate.GetComponent<Gate>();

		gateScript.IsPlayer = true;
		gateScript.GateTeam = Team.TeamA;
		gateScript.Path = path;

		foreach(GameObject troop in PlayerManager.control.SelectedTroops)
		{
			Debug.Log("ADD");
			gateScript.TroopPrefabs.Add(troop);
		}
	}

	void Start ()
	{

		matchIsOver = false;
	}

	void Update()
	{
		if (!matchIsOver)
		{
			matchTroopManager.ManageTroops ();
		}
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
