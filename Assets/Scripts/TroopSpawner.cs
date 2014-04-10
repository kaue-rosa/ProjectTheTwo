using UnityEngine;
using System.Collections;

public class TroopSpawner : MonoBehaviour
{
	[SerializeField] private PathManager pm = null;
	[SerializeField] private string prefabName = "";
	[SerializeField] private float timeToSpawn = 1;
	private float currentTimer = 0;
	
	void Update()
	{
		currentTimer += Time.deltaTime;
		if (currentTimer >= timeToSpawn)
		{
			GameObject _troopGm = (GameObject)Instantiate((GameObject)Resources.Load(prefabName),transform.position,transform.rotation);
			Troop _troop = _troopGm.GetComponent<Troop>();
			_troop.TroopPathManager = this.pm;
			currentTimer = 0;
			print ("hey");
		}
	}

}
