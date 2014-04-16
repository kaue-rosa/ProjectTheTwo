using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GateAI : MonoBehaviour {

	[SerializeField] int troopChangeChancePercentage = 50;
	[SerializeField] float changeTimer = 5f;

	private Gate gate = null;
	private float currentChangeTimer = 0f;


	// Use this for initialization
	void Start () {
		gate = GetComponent<Gate> ();
		if (!gate)Debug.LogError (gameObject.name + " is not a gate. Please make sure there is a Gate.cs script.", this);
		currentChangeTimer = changeTimer;
	}
	
	// Update is called once per frame
	void Update () {
		currentChangeTimer -= Time.deltaTime;
		if (currentChangeTimer <= 0)
		{
			currentChangeTimer = changeTimer;
			if(Random.value * 100 <= troopChangeChancePercentage)
			{
				gate.NextTroopToSpawnName = gate.TroopPrefabs[(int)(Random.value * gate.TroopPrefabs.Count)].name;
			}
		}
	}
}
