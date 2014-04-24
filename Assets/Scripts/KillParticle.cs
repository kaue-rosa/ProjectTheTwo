using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour 
{
	 private ParticleSystem particle;

	void Start () 
	{
		if(GameObject.Find("Particles"))
			transform.parent = GameObject.Find("Particles").transform;
		else
		{
			GameObject parent = new GameObject("Particles");
			transform.parent = parent.transform;
		}


		particle = GetComponent <ParticleSystem>();
	}

	void Update () 
	{
		if(!particle.IsAlive())
			Destroy(this.gameObject);
	}
}
