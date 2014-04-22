using UnityEngine;
using System.Collections;

public class PreMatchMenu : MonoBehaviour
{
	PlayerManager playerManager;

	void Start()
	{
		playerManager = GameObject.FindObjectOfType<PlayerManager>();
	}

	void OnGUI()
	{
		//Show troop choices
		for (int t=0; t<playerManager.TotalTroops.Count; t++)
		{
			if(!playerManager.SelectedTroops.Contains(playerManager.TotalTroops[t]))
			{						
				if(GUI.Button(new Rect (10, 40*t, 100, 30), PlayerManager.GetTextureFromSprite(playerManager.TotalTroops[t])) && playerManager.SelectedTroops.Count<playerManager.MaxNumberOfTroops)
				{

					playerManager.SelectedTroops.Add(playerManager.TotalTroops[t]);
				}
			}
		}

		//Show gate choices
		GUILayout.BeginArea(new Rect(Screen.width-150, 10, 250, Screen.height));

		for (int g=0; g<playerManager.TotalGates.Count; g++)
		{
			if(playerManager.SelectedGate == playerManager.TotalGates[g])
				continue;

			if(GUI.Button(new Rect (10, 40*g, 100, 30),PlayerManager.GetTextureFromSprite(playerManager.TotalGates[g])) && !playerManager.SelectedGate)
			{
				playerManager.SelectedGate = playerManager.TotalGates[g];
			}
		}

		GUILayout.EndArea();

		//Show Selection

		GUILayout.BeginArea(new Rect((Screen.width/2) - 100,10, Screen.width, Screen.height));

		if(playerManager.SelectedGate)
		{
			if(GUI.Button(new Rect(10, 0, 100, 30), PlayerManager.GetTextureFromSprite(playerManager.SelectedGate)))
			{
				playerManager.SelectedGate = null;
			}
		}


		for(int ii=0; ii<playerManager.SelectedTroops.Count; ii++)
		{

			if(GUI.Button(new Rect(10,30*(ii+1), 100, 30), PlayerManager.GetTextureFromSprite(playerManager.SelectedTroops[ii])))
			{
				playerManager.SelectedTroops.RemoveAt(ii);
			}
		}



		if(playerManager.SelectedTroops.Count == playerManager.MaxNumberOfTroops && playerManager.SelectedGate)
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
