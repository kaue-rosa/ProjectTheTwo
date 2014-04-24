using UnityEngine;
using System.Collections;

public class saveloadtest : MonoBehaviour 
{

	void OnGUI()
	{
		if(PlayerManager.control.TotalGates.Count>0)
			GUILayout.Label(PlayerManager.control.TotalGates[0].CurrentHealth.ToString());

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
			if(PlayerManager.control.TotalGates.Count>0)
			{
				PlayerManager.control.TotalGates[0].GetComponent<GateStats>().CurrentHealth++;
			}
		}
		if(GUILayout.Button("-Life"))
		{
			if(PlayerManager.control.TotalGates.Count>0)
			{
				PlayerManager.control.TotalGates[0].GetComponent<GateStats>().CurrentHealth--;
			}
			
		}

	}

}
