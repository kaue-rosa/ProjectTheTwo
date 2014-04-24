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
		for (int t=0; t<PlayerManager.control.TotalOwnedTroopsIds.Count; t++)
		{
			if(!PlayerManager.control.SelectedTroopsIds.Contains(PlayerManager.control.TotalOwnedTroopsIds[t]))
			{						
				if(GUI.Button(new Rect (10, 40*t, 100, 30), PlayerManager.control.TotalOwnedTroopsIds[t].ToString()) && PlayerManager.control.SelectedTroopsIds.Count<PlayerManager.control.MaxNumberOfTroops)
				{

					PlayerManager.control.SelectedTroopsIds.Add(PlayerManager.control.TotalOwnedTroopsIds[t]);
				}
			}
		}

		//Show gate choices
		GUILayout.BeginArea(new Rect(Screen.width-150, 10, 250, Screen.height));

		for (int g=0; g<PlayerManager.control.TotalOwnedGatesIds.Count; g++)
		{
			if(PlayerManager.control.SelectedGateId == PlayerManager.control.TotalOwnedGatesIds[g])
				continue;

			if(GUI.Button(new Rect (10, 40*g, 100, 30), PlayerManager.control.TotalOwnedGatesIds[g].ToString()))
			{
				PlayerManager.control.SelectedGateId = PlayerManager.control.TotalOwnedGatesIds[g];
			}
		}

		GUILayout.EndArea();

		//Show Selection

		GUILayout.BeginArea(new Rect((Screen.width/2) - 100,10, Screen.width, Screen.height));

		if(PlayerManager.control.SelectedGateId >= 0)
		{
			if(GUI.Button(new Rect(10, 0, 100, 30), PlayerManager.control.SelectedGateId.ToString()))
			{
				PlayerManager.control.SelectedGateId = -1;
			}
		}


		for(int ii=0; ii<PlayerManager.control.SelectedTroopsIds.Count; ii++)
		{

			if(GUI.Button(new Rect(10,30*(ii+1), 100, 30), PlayerManager.control.SelectedTroopsIds[ii].ToString()))
			{
				PlayerManager.control.SelectedTroopsIds.RemoveAt(ii);
			}
		}



		if(PlayerManager.control.SelectedTroopsIds.Count == PlayerManager.control.MaxNumberOfTroops && PlayerManager.control.SelectedGateId>=0)
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
