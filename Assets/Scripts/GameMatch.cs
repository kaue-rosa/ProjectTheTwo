using UnityEngine;
using System.Collections;

public class GameMatch : MonoBehaviour {

	[SerializeField] private TroopManager matchTroopManager = null;

	private bool matchIsOver = false;

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

	// Update is called once per frame
	void OnGUI ()
	{
		if (matchIsOver)
		{
			if (GUI.Button (new Rect(Screen.width/2 - 50, Screen.height/2 - 50, 100 ,100),"Restart")) {
					Application.LoadLevel (0);
			}
		}
	}

	public void SetMatchOver()
	{
		matchIsOver = true;
		foreach (Gate g in matchTroopManager.gates)
		{
			g.CanSpawn = false;
		}
	}
}
