using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Store : MonoBehaviour
{
	[SerializeField]private List<GameObject> listOfTroops = new List<GameObject>();
	[SerializeField]private List<GameObject> listOfGates = new List<GameObject>();

	private List<Texture2D> listOfTroopsSprite = new List<Texture2D>();
	private List<Texture2D> listOfGatesSprite = new List<Texture2D>();

	[SerializeField]private List <GameObject> troops = new List<GameObject>();
	[SerializeField]private List<GateStats> gates = new List<GateStats>();

	private List <bool> troopsAvailable = new List<bool>();
	private List<bool> gatesAvailable  = new List<bool>();

	public List<GameObject> Troops
	{
		get {return troops;}
		set {troops = value;}
	}
	public List<GateStats> Gates
	{
		get {return gates;}
		set {gates = value;}
	}

	void Start () 
	{

		foreach(GameObject obj in Resources.LoadAll("Gates/"))
		{
			listOfGates.Add(obj);
		}
		foreach(GameObject obj in Resources.LoadAll("Troops/"))
		{
			listOfTroops.Add(obj);
		}

		foreach(GameObject troop in listOfTroops)
		{
			troops.Add(troop);
			troopsAvailable.Add(true);
		}
		foreach(GameObject gate in listOfGates)
		{
			gates.Add(gate.GetComponent<GateStats>());
			gatesAvailable.Add(true);
		}
	}

	void OnDisable()
	{
		DataManager.Control.SaveData();
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0,0,Screen.width/2, Screen.height));

		for(int t = 0; t<troops.Count; t++ )
		{
			//GUILayout.Button(listOfTroopsSprite[t]);
			if(troopsAvailable[t] && !PlayerManager.control.TotalOwnedTroopsIds.Contains(troops[t].GetComponent<TroopStats>().TroopID))
			{
				if(GUI.Button(new Rect (10, 40*t, 100, 30),"bla"))
				{
					//if(!PlayerManager.control.TotalTroops.Contains(troops[t]))
					PlayerManager.control.TotalOwnedTroopsIds.Add(troops[t].GetComponent<TroopStats>().TroopID);
				}
			}
		}

		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(Screen.width/2, 0, Screen.width, Screen.height));

		for(int g = 0; g<gates.Count; g++ )
		{
			//GUILayout.Button(listOfTroopsSprite[t]);
			if(gatesAvailable[g] && !PlayerManager.control.TotalOwnedGatesIds.Contains(gates[g].GetComponent<GateStats>().ID))
			{
				if(GUI.Button(new Rect (10, 40*g, 100, 30), "bla"))
				{
					PlayerManager.control.TotalOwnedGatesIds.Add(gates[g].GetComponent<GateStats>().ID);
				}
			}
		}
		GUILayout.EndArea();

		if(GUI.Button(new Rect(10,240, 100, 30),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
	}
}
