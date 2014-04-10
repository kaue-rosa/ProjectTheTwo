using UnityEngine;
using System.Collections;

public class TroopStats : MonoBehaviour 
{
	[SerializeField]private float maxHealth = 10;
	public float MaxHealth
	{
		get
		{ 
			return maxHealth;
		}
	}

	[SerializeField]private float rangeOfSight = 10;
	public float RangeOfSight
	{
		get
		{
			return rangeOfSight;
		}
	}
}
