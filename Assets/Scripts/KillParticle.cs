using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour 
{
	 private ParticleSystem particle;

	void Start () 
	{
		particle = GetComponent <ParticleSystem>();
	}

	void Update () 
	{
		if(!particle.IsAlive())
			Destroy(this.gameObject);
	}
}
