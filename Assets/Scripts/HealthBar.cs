using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class HealthBar : MonoBehaviour {

	[SerializeField] private Transform healthBar = null;
	[SerializeField] private Transform healthBarShadow = null;

	public void SetHealthBarByPercentage(float per, bool animate)
	{
		if (per <= 0)per = 0;
		Vector3 newScale = new Vector3 (per, 1, 1);
		if(animate)
		{
			HOTween.To (healthBar, 0.2f, "localScale", newScale);
			HOTween.To (healthBarShadow, 0.5f, "localScale", newScale);
		}
		else
		{
			healthBar.transform.localScale = newScale;
			healthBarShadow.transform.localScale = newScale;
		}
	}
}
