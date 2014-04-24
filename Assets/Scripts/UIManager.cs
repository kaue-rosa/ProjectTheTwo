using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour 
{
	[SerializeField] private GameObject healthBarPreFab = null;
	[SerializeField] private float healthBarHight = 3f;
	[SerializeField]private float healthBarFactor = 3f;
	private float healthBarSize = 100f;
	private Gate[]gates;
	private List<GameObject> healthBars = new List<GameObject>();

	void Start () 
	{
		gates = FindObjectsOfType<Gate>();

		for(int i=0; i<gates.Length; i++)
		{
			//Debug.Log(gates[i]);
			if(healthBarPreFab)
			{
				GameObject healthBarObj = (GameObject) Instantiate(healthBarPreFab, gates[i].transform.position+(Vector3.up * healthBarHight), Quaternion.identity);

				GameObject healthBar = healthBarObj.transform.GetChild(0).gameObject;
				GameObject healthBarBg = healthBarObj.transform.GetChild(1).gameObject;

				healthBars.Add(healthBar);

				healthBarSize = gates[i].GetComponent<GateStats>().MaxHealth;
				healthBarSize *= 1/healthBarFactor;

				if(healthBarBg)
					healthBarBg.transform.localScale = new Vector3(gates[i].GetComponent<GateStats>().MaxHealth/healthBarSize, healthBarBg.transform.localScale.y, healthBarBg.transform.localScale.z);

				healthBarObj.transform.position = new Vector3((healthBarObj.transform.position.x - (healthBarBg.transform.localScale.x/2)), healthBarObj.transform.position.y, healthBarObj.transform.position.z);
			}

		}
	}

	void Update () 
	{
	}

	void LateUpdate () 
	{
		for(int i=0; i<gates.Length; i++)
		{
			healthBarSize = gates[i].GetComponent<GateStats>().MaxHealth;
			healthBarSize *= 1/healthBarFactor;
			healthBars[i].transform.localScale = new Vector3(gates[i].GetComponent<GateStats>().CurrentHealth/healthBarSize, healthBars[i].transform.localScale.y, healthBars[i].transform.localScale.z);
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
					if(GUILayout.Button("he"))
						gates[i].NextTroopToSpawnName = gates[i].TroopPrefabs[ii].name;
				}
			}
		}
	}
}
