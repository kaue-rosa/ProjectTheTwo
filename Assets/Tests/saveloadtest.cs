using UnityEngine;
using System.Collections;

public class saveloadtest : MonoBehaviour 
{

	void OnGUI()
	{
		if (DataManager.Control.AllOwnedGatesData.Count > 0)
			GUILayout.Label (DataManager.Control.AllOwnedGatesData[0].currentHealth.ToString());//PlayerManager.control.TotalOwnedGatesIds[0].CurrentHealth.ToString());

		if(GUILayout.Button("Save"))
		{
			DataManager.Control.SaveData();
		}
		if(GUILayout.Button("Load"))
		{
			DataManager.Control.LoadData();

		}

		if(GUILayout.Button("+life"))
		{
			if(PlayerManager.control.TotalOwnedGatesIds.Count>0)
			{
				if (DataManager.Control.AllOwnedGatesData.Count > 0)
					DataManager.Control.AllOwnedGatesData[0].currentHealth++;
			}
		}
		if(GUILayout.Button("-Life"))
		{
			if(PlayerManager.control.TotalOwnedGatesIds.Count>0)
			{
				if (DataManager.Control.AllOwnedGatesData.Count > 0)
					DataManager.Control.AllOwnedGatesData[0].currentHealth--;
			}
			
		}

	}

}
