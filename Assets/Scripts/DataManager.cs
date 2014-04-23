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

		Debug.Log(allGates.Count);
	}

	public void SaveData()
	{
		Debug.Log("Saving");

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+"/GameInfo.dat");

		GameData data = new GameData();
		foreach(GameObject gate in PlayerManager.control.TotalGates)
		{	
			GatesData dataG = new GatesData();
			GateStats stats = gate.GetComponent<GateStats>();

			dataG.element = stats.MyElement;
			dataG.currentHealth = stats.CurrentHealth;
			dataG.maxHealth = stats.MaxHealth;
			dataG.xp = stats.Xp;
			dataG.level = stats.Level;
			dataG.deffense = stats.Deffense;

			if(!data.gatesOwned.Contains(dataG))
				data.gatesOwned.Add(dataG);

		}
		foreach(GameObject troop in PlayerManager.control.TotalTroops)
		{
			TroopsData dataT = new TroopsData();
			TroopStats stats = troop.GetComponent<TroopStats>();

			dataT.element = stats.MyElement;
			dataT.currentHealth = stats.CurrentHealth;
			dataT.maxHealth = stats.MaxHealth;
			dataT.rangeOfSight = stats.RangeOfSight;
			dataT.movementSpeed = stats.MovementSpeed;
			dataT.attackDamage = stats.AttackDamage;
			dataT.attackSpeed = stats.AttackSpeed;
			dataT.deffense = stats.Deffense;
			dataT.spawnTime = stats.SpawnTime;

			if(!data.troopsOwned.Contains(dataT))
				data.troopsOwned.Add(dataT);			
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

						PlayerManager.control.TotalGates.Add(allGates[g]);
					}
				}
			}
			foreach(TroopsData troop in data.troopsOwned)
			{
				for(int t = 0; t<allTroopsStats.Count; t++)
				{
					if(allTroopsStats[t].MyElement == troop.element)
					{						
						PlayerManager.control.TotalTroops.Add(allTroops[t]);
					}
				}
			}
		}
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
class TroopsData
{
	//HP
	public GameElement element;
	
	public int currentHealth;
	public int maxHealth;
	
	//Battle
	public float rangeOfSight;
	public float movementSpeed;
	public int attackDamage;
	public float attackSpeed;
	public float deffense;
	
	public float spawnTime;
}

[Serializable]
class GameData
{
	public List<GatesData> gatesOwned = new List<GatesData>();
	public List<TroopsData> troopsOwned = new List<TroopsData>();
}