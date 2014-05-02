using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class HealthBar : MonoBehaviour {

	[SerializeField] private Transform healthBar = null;
	[SerializeField] private Transform healthBarShadow = null;

	public void SetHealthBarByPercentage(float per)
	{
		if (per <= 0)per = 0;
		HOTween.To (healthBar, 0.2f, "localScale", new Vector3 (per, 1, 1));
		HOTween.To (healthBarShadow, 0.5f, "localScale", new Vector3 (per, 1, 1));
	}
}
