﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameElement
{
	WATER, 
	FIRE, 
	ROCK,
	NORMAL,

}

public class Element
{
	public Dictionary<GameElement, float> baseElementsMultiplyer = new Dictionary<GameElement, float>();


	public Element()
	{
		AssignDictionary();
	}

	public virtual void AssignDictionary()
	{
		baseElementsMultiplyer.Add (GameElement.WATER, 1f);
		baseElementsMultiplyer.Add (GameElement.FIRE, 1f);
		baseElementsMultiplyer.Add (GameElement.ROCK, 1f);
		baseElementsMultiplyer.Add (GameElement.NORMAL, 1f);
	}

	public virtual float ElementCheck(GameElement attackElement)
	{
		return baseElementsMultiplyer[attackElement];
	}
}



public class ElementWater:Element
{
	public ElementWater()
	{
		AssignDictionary();

	}

	public override void AssignDictionary()
	{
		base.AssignDictionary();

		baseElementsMultiplyer[GameElement.FIRE] = 2f;
		baseElementsMultiplyer[GameElement.ROCK] = .5f;
	}

	public override float ElementCheck(GameElement attackElement)
	{
		return baseElementsMultiplyer[attackElement];
	}
}
