using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreMatchMenu : MonoBehaviour
{

	[SerializeField] public static List<Texture2D> elementIcons = new List<Texture2D>();

	void Start()
	{
		DataManager.Control.LoadData();
	}

	void OnGUI()
	{
		//Show troop choices
		for (int t=0; t<PlayerManager.control.TotalTroops.Count; t++)
		{
			if(!PlayerManager.control.SelectedTroops.Contains(PlayerManager.control.TotalTroops[t]))
			{						
				if(GUI.Button(new Rect (10, 40*t, 100, 30), "rss") && PlayerManager.control.SelectedTroops.Count<PlayerManager.control.MaxNumberOfTroops)
				{

					PlayerManager.control.SelectedTroops.Add(PlayerManager.control.TotalTroops[t]);
				}
			}
		}

		//Show gate choices
		GUILayout.BeginArea(new Rect(Screen.width-150, 10, 250, Screen.height));

		for (int g=0; g<PlayerManager.control.TotalGates.Count; g++)
		{
			if(PlayerManager.control.SelectedGate == PlayerManager.control.TotalGates[g])
				continue;

			if(GUI.Button(new Rect (10, 40*g, 100, 30), "baaa") && !PlayerManager.control.SelectedGate)
			{
				PlayerManager.control.SelectedGate = PlayerManager.control.TotalGates[g];
			}
		}

		GUILayout.EndArea();

		//Show Selection

		GUILayout.BeginArea(new Rect((Screen.width/2) - 100,10, Screen.width, Screen.height));

		if(PlayerManager.control.SelectedGate)
		{
			if(GUI.Button(new Rect(10, 0, 100, 30), "ba"))
			{
				PlayerManager.control.SelectedGate = null;
			}
		}


		for(int ii=0; ii<PlayerManager.control.SelectedTroops.Count; ii++)
		{

			if(GUI.Button(new Rect(10,30*(ii+1), 100, 30), "hu"))
			{
				PlayerManager.control.SelectedTroops.RemoveAt(ii);
			}
		}



		if(PlayerManager.control.SelectedTroops.Count == PlayerManager.control.MaxNumberOfTroops && PlayerManager.control.SelectedGate)
		{
			if(GUI.Button(new Rect(10,180, 100, 30),"Play"))
			{
				Application.LoadLevel(1);
			}
		}

		


		GUILayout.EndArea();

		if(GUI.Button(new Rect(10,240, 100, 30),"Store"))
		{
			Application.LoadLevel("Store");
		}
	}
}
