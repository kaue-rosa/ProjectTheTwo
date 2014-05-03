using UnityEngine;
using System.Collections;

public class GameMatch : MonoBehaviour {

	[SerializeField] private TroopManager matchTroopManager = null;
	[SerializeField] private MatchEndScreen matchEndScreen = null;

	private bool matchIsOver = false;

	GameObject playerGate;
	Gate playerGateScript;

	void Awake()
	{
		if(PlayerManager.control.SelectedTroopsIds.Count == 0)
		{
			foreach(int troop in PlayerManager.control.TotalOwnedTroopsIds)
			{
				PlayerManager.control.SelectedTroopsIds.Add(troop);
			}
		}

		playerGate = GameObject.Find("PlayerGate");

		GameObject path = GameObject.Find("Path1");

		playerGateScript = playerGate.GetComponent<Gate>();

		playerGateScript.gateSprite.sprite = DataManager.Control.GetSpriteFromGateId (PlayerManager.control.SelectedGateId);

		playerGateScript.Stats.ID = PlayerManager.control.SelectedGateId;
		playerGateScript.IsPlayer = true;
		playerGateScript.GateTeam = Team.TeamA;
		playerGateScript.Path = path;

		//print (PlayerManager.control.SelectedGateId);

		DataManager.Control.AssignGateDataToGateStats (playerGateScript.Stats);

		playerGateScript.TroopPrefabs.Clear();
		foreach(int troopID in PlayerManager.control.SelectedTroopsIds)
		{
			playerGateScript.TroopPrefabs.Add(DataManager.Control.GetTroopPrefabByID(troopID));
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

		if(Input.GetKey(KeyCode.LeftArrow))
			playerGateScript.TakeDamage(10,playerGateScript.Stats.GateElement);
		
		if(Input.GetKey(KeyCode.RightArrow))
			playerGateScript.Heal(10);
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
				g.CelebrateVictory();
			}
		}

		//bring match end screen
		GameObject matchEndScreenGmo = (GameObject) Instantiate(matchEndScreen.gameObject);
		matchEndScreen = matchEndScreenGmo.GetComponent<MatchEndScreen> ();
		matchEndScreenGmo.transform.parent = Camera.main.transform;
		matchEndScreen.transform.localPosition = new Vector3 (0,0,0.5f);

		DataManager.Control.SetGateStats(playerGate.GetComponent<GateStats>());
		DataManager.Control.SaveData();
	}
}
