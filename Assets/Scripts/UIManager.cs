using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour 
{
	[SerializeField] private GameObject healthBarPreFab = null;
	[SerializeField] private float healthBarHight = 3f;
	private Gate[]gates;

	void Start () 
	{
		gates = FindObjectsOfType<Gate>();

		for(int i=0; i<gates.Length; i++)
		{
			if(healthBarPreFab)
			{
				GameObject healthBarObj = (GameObject) Instantiate(healthBarPreFab, gates[i].transform.position+(Vector3.up * healthBarHight), Quaternion.identity);
				gates[i].GetComponent<Gate>().GateHealthBar = healthBarObj.GetComponent<HealthBar>();
			}
		}
	}

	void OnGUI()
	{
		for(int i=0; i<gates.Length; i++)
		{
			if(gates[i].IsVisible&&gates[i].IsPlayer)
			{
				for(int ii = 0; ii<gates[i].TroopPrefabs.Count; ii++)
				{
					if(GUILayout.Button(gates[i].TroopPrefabs[ii].name))
						gates[i].NextTroopToSpawnName = gates[i].TroopPrefabs[ii].name;
				}
			}
		}
	}
}
