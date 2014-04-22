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
	[SerializeField]private List<GameObject> gates = new List<GameObject>();

	private List <bool> troopsAvailable = new List<bool>();
	private List<bool> gatesAvailable  = new List<bool>();

	public List<GameObject> Troops
	{
		get {return troops;}
		set {troops = value;}
	}
	public List<GameObject> Gates
	{
		get {return gates;}
		set {gates = value;}
	}


	void Start () 
	{
		foreach(GameObject troop in listOfTroops)
		{
			troops.Add(troop);
			troopsAvailable.Add(true);
			listOfTroopsSprite.Add(PlayerManager.GetTextureFromSprite(troop));
		}
		foreach(GameObject gate in listOfGates)
		{
			gates.Add(gate);
			gatesAvailable.Add(true);
			listOfGatesSprite.Add(PlayerManager.GetTextureFromSprite(gate));
		}
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0,0,Screen.width/2, Screen.height));

		for(int t = 0; t<troops.Count; t++ )
		{
			//GUILayout.Button(listOfTroopsSprite[t]);
			if(troopsAvailable[t] && !PlayerManager.control.TotalTroops.Contains(troops[t]))
			{
				if(GUI.Button(new Rect (10, 40*t, 100, 30),listOfTroopsSprite[t]))
				{
					//if(!PlayerManager.control.TotalTroops.Contains(troops[t]))
						PlayerManager.control.TotalTroops.Add(troops[t]);
				}
			}
		}

		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(Screen.width/2, 0, Screen.width, Screen.height));

		for(int g = 0; g<gates.Count; g++ )
		{
			//GUILayout.Button(listOfTroopsSprite[t]);
			if(gatesAvailable[g] && !PlayerManager.control.TotalGates.Contains(gates[g]))
			{
				if(GUI.Button(new Rect (10, 40*g, 100, 30), listOfGatesSprite[g]))
				{
					PlayerManager.control.TotalGates.Add(gates[g]);
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
