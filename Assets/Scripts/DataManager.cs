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
	 List<GateStats> allGatesStats = new List<GateStats>();

	 List<GameObject> allTroops = new List<GameObject>();
	 List<TroopStats> allTroopsStats = new List<TroopStats>();

	private DataManager()
	{
		foreach(GameObject obj in Resources.LoadAll("Gates/"))
		{
			allGates.Add(obj);
			allGatesStats.Add(obj.GetComponent<GateStats>());
		}
		foreach(GameObject obj in Resources.LoadAll("Troops/"))
		{
			allTroops.Add(obj);
			allTroopsStats.Add(obj.GetComponent<TroopStats>());
		}
	}

	public void SaveData()
	{
		Debug.Log("Saving");

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+"/GameInfo.dat");

		GameData data = new GameData();
		foreach(GateStats gate in PlayerManager.control.TotalGates)
		{	
			GatesData dataG = new GatesData();

			dataG.element = gate.MyElement;
			dataG.currentHealth = gate.CurrentHealth;
			dataG.maxHealth = gate.MaxHealth;
			dataG.xp = gate.Xp;
			dataG.level = gate.Level;
			dataG.deffense = gate.Deffense;

			if(!data.gatesOwned.Contains(dataG))
				data.gatesOwned.Add(dataG);

		}

		foreach(GameObject troop in PlayerManager.control.TotalTroops)
		{
			if(!data.troopsOwned.Contains(troop.name))
				data.troopsOwned.Add(troop.name);			
		}

		bf.Serialize(file, data);
		file.Close();

	}

	public void LoadData()
	{
		Debug.Log("Loading");

		if(File.Exists(Application.persistentDataPath+"/GameInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath+"/GameInfo.dat", FileMode.Open);

			GameData data = (GameData)bf.Deserialize(file);

			file.Close();

			PlayerManager.control.TotalGates.Clear();
			PlayerManager.control.TotalTroops.Clear();

			foreach(GatesData gate in data.gatesOwned)
			{
				for(int g = 0; g<allGatesStats.Count; g++)
				{
					if(allGatesStats[g].MyElement == gate.element)
					{
						allGatesStats[g].CurrentHealth = gate.currentHealth;
						allGatesStats[g].MaxHealth = gate.maxHealth;
						allGatesStats[g].Xp = gate.xp;
						allGatesStats[g].Level = gate.level;
						allGatesStats[g].Deffense = gate.deffense;

						PlayerManager.control.TotalGates.Add(allGatesStats[g]);
					}
				}
			}
			foreach(string troop in data.troopsOwned)
			{
				for(int t = 0; t<allTroopsStats.Count; t++)
				{
					if(allTroopsStats[t].name == troop)
					{						
						PlayerManager.control.TotalTroops.Add(allTroops[t]);
					}
				}
			}
		}
	}

	public void SetGateStats (GateStats stats)
	{
		foreach(GateStats gate in PlayerManager.control.TotalGates)
		{
			if(gate.MyElement == stats.MyElement)
			{
				gate.MaxHealth = stats.MaxHealth;
				gate.CurrentHealth = stats.CurrentHealth;
				gate.Xp = stats.Xp;
				gate.Level = stats.Level;
				gate.Deffense = stats.Deffense;
			}
		}
	}
	
	public GateStats GetGateByElement (GameElement element)
	{
		foreach(GateStats gate in allGatesStats)
		{
			if(gate.MyElement == element)
			{
				return gate;
			}
		}

		return null;

	}
}
[Serializable]
class GatesData
{
	public GameElement element;
	
	//HP
	public int currentHealth;
	public int maxHealth;
	
	//XP
	public int xp;
	public int level;
		
	public float deffense;

}
[Serializable]
class GameData
{
	public List<GatesData> gatesOwned = new List<GatesData>();
	public List<string> troopsOwned = new List<string>();
}