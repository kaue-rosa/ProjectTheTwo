using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager
{
	private static DataManager instance;

	public static DataManager Control
	{
		get 
		{
			if (instance == null)
			{
				instance = new DataManager();
			}
			return instance;
		}
	}

	List<GameObject> allGates = new List<GameObject>();
	List<GatesData> allOwnedGatesData = new List<GatesData>();
	public List<GatesData> AllOwnedGatesData
	{
		get { return allOwnedGatesData;}
		set { allOwnedGatesData = value;}
	}

	List<GameObject> allTroops = new List<GameObject>();


	private DataManager()
	{
		foreach(GameObject obj in Resources.LoadAll("Gates/"))
		{
			allGates.Add(obj);
		}

		foreach (GameObject obj in Resources.LoadAll("Troops/"))
		{
			allTroops.Add(obj);
		}
	}

	public void SaveData()
	{
		Debug.Log("Saving");

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+"/GameInfo.dat");

		GameData data = new GameData();

		foreach(int gateId in PlayerManager.control.TotalOwnedGatesIds)
		{	
			GatesData dataG = null;

			if(!GateStatsContainsID(gateId))
			{
				//saving after buying a new gate
				dataG = new GatesData();

				foreach(GameObject gmoGate in allGates)
				{
					GateStats _gStats = gmoGate.GetComponent<GateStats>();

					if(_gStats.ID == gateId)
					{
						AssignGateStatsToGateData(_gStats, dataG);
						allOwnedGatesData.Add (dataG);
					}
				}
			}
			else
			{
				//saving after a match, this data has already been modified.

				dataG = GetGateDataByGateID(gateId);
			}

			if(!data.gatesOwned.Contains(dataG))
				data.gatesOwned.Add(dataG);

		}

		foreach(int troopId in PlayerManager.control.TotalOwnedTroopsIds)
		{
			if(!data.troopsOwned.Contains(troopId))
				data.troopsOwned.Add(troopId);			
		}

		bf.Serialize(file, data);
		file.Close();

	}

	public void LoadData()
	{
		if(File.Exists(Application.persistentDataPath+"/GameInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath+"/GameInfo.dat", FileMode.Open);

			GameData data = (GameData)bf.Deserialize(file);

			file.Close();

			PlayerManager.control.TotalOwnedGatesIds.Clear();
			PlayerManager.control.TotalOwnedTroopsIds.Clear();
			allOwnedGatesData.Clear();

			foreach(GatesData gate in data.gatesOwned)
			{
				allOwnedGatesData.Add (gate);
				PlayerManager.control.TotalOwnedGatesIds.Add(gate.id);
			}

			foreach(int troop in data.troopsOwned)
			{					
				PlayerManager.control.TotalOwnedTroopsIds.Add(troop);
			}
		}
	}

	public void SetGateStats (GateStats _gateStats)
	{
		foreach(GatesData _gateData in allOwnedGatesData)
		{
			if(_gateData.id == _gateStats.ID)
			{
				AssignGateStatsToGateData(_gateStats, _gateData);
			}
		}
	}

	public GatesData GetGateDataByGateID(int _id)
	{
		foreach(GatesData gData in allOwnedGatesData)
		{
			if(gData.id == _id)return gData;
		}
		return null;
	}

	public bool GateStatsContainsID(int _id)
	{
		foreach(GatesData gData in allOwnedGatesData)
		{
			if(gData.id == _id)return true;
		}
		return false;
	}

	public void AssignGateDataToGateStats(GateStats gateStatsToBeAssigned)
	{
		foreach (GatesData _gatesData in allOwnedGatesData)
		{
			if(_gatesData.id == gateStatsToBeAssigned.ID)
				AssignGateDataToGateStats(_gatesData,gateStatsToBeAssigned);
		}
	}

	public void AssignGateDataToGateStats(GatesData newGateData, GateStats gateStatsToBeAssigned)
	{
		gateStatsToBeAssigned.ID = newGateData.id;
		gateStatsToBeAssigned.GateElement = newGateData.gateElement;
		gateStatsToBeAssigned.CurrentHealth = newGateData.currentHealth;
		gateStatsToBeAssigned.MaxHealth = newGateData.maxHealth;
		gateStatsToBeAssigned.Xp = newGateData.xp;
		gateStatsToBeAssigned.Deffense = newGateData.deffense;
	}

	public void AssignGateStatsToGateData(GateStats newGateStats, GatesData gateDataToBeAssigned)
	{
		gateDataToBeAssigned.id = newGateStats.ID;
		gateDataToBeAssigned.gateElement = newGateStats.GateElement;
		gateDataToBeAssigned.currentHealth = newGateStats.CurrentHealth;
		gateDataToBeAssigned.maxHealth = newGateStats.MaxHealth;
		gateDataToBeAssigned.xp = newGateStats.Xp;
		gateDataToBeAssigned.deffense = newGateStats.Deffense;
	}

	public GameObject GetTroopPrefabByID (int troopID)
	{
		foreach(GameObject troopObj in allTroops)
		{
			if(troopObj.GetComponent<TroopStats>().TroopID == troopID)
			{
				return troopObj;
			}
		}

		return null;
	}

	public Sprite GetSpriteFromGateId(int _id)
	{
		foreach (GameObject obj in allGates)
		{
			if(obj.GetComponent<GateStats>().ID == _id)
				return obj.GetComponent<Gate>().gateSprite.sprite;
		}
		return null;
	}
}

[Serializable]
public class GatesData
{
	public int id;
	public GameElement gateElement;
	
	//HP
	public int currentHealth;
	public int maxHealth;
	
	//XP
	public int xp;

	public float deffense;

}

[Serializable]
class GameData
{
	public List<GatesData> gatesOwned = new List<GatesData>();
	public List<int> troopsOwned = new List<int>();
}