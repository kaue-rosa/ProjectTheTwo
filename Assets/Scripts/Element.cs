using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameElement
{
	WATER, 
	FIRE, 
	ROCK,
	AIR,
	ELECTRIC,
	NORMAL,

}

public class Element
{
	public Dictionary<GameElement, float> baseElementsMultiplyer = new Dictionary<GameElement, float>();


	public Element()
	{
	}

	public virtual void AssignDictionary()
	{
		baseElementsMultiplyer.Add (GameElement.WATER, 1f);
		baseElementsMultiplyer.Add (GameElement.FIRE, 1f);
		baseElementsMultiplyer.Add (GameElement.ROCK, 1f);
		baseElementsMultiplyer.Add (GameElement.AIR, 1f);
		baseElementsMultiplyer.Add (GameElement.ELECTRIC, 1f);
		baseElementsMultiplyer.Add (GameElement.NORMAL, 1f);
	}

	public virtual float ElementCheck(GameElement comparedElement)
	{
		return baseElementsMultiplyer[comparedElement];
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
		baseElementsMultiplyer[GameElement.ROCK] = 2f;
		baseElementsMultiplyer[GameElement.AIR] = .5f;
		baseElementsMultiplyer[GameElement.ELECTRIC] = .5f;
	}

	public override float ElementCheck(GameElement comparedElement)
	{
		return baseElementsMultiplyer[comparedElement];
	}
}

public class ElementFire:Element
{
	public ElementFire()
	{
		AssignDictionary();
		
	}
	
	public override void AssignDictionary()
	{
		base.AssignDictionary();
		
		baseElementsMultiplyer[GameElement.WATER] = .5f;
		baseElementsMultiplyer[GameElement.ROCK] = .5f;
		baseElementsMultiplyer[GameElement.AIR] = 2f;
		baseElementsMultiplyer[GameElement.ELECTRIC] = 2f;
	}
	
	public override float ElementCheck(GameElement comparedElement)
	{
		return baseElementsMultiplyer[comparedElement];
	}
}
public class ElementRock:Element
{
	public ElementRock()
	{
		AssignDictionary();
		
	}
	
	public override void AssignDictionary()
	{
		base.AssignDictionary();
		
		baseElementsMultiplyer[GameElement.WATER] = .5f;
		baseElementsMultiplyer[GameElement.FIRE] = 2f;
		baseElementsMultiplyer[GameElement.AIR] = 2f;
		baseElementsMultiplyer[GameElement.ELECTRIC] = .5f;
	}
	
	public override float ElementCheck(GameElement comparedElement)
	{
		return baseElementsMultiplyer[comparedElement];
	}
}
public class ElementAir:Element
{
	public ElementAir()
	{
		AssignDictionary();
		
	}
	
	public override void AssignDictionary()
	{
		base.AssignDictionary();
		
		baseElementsMultiplyer[GameElement.WATER] = 2f;
		baseElementsMultiplyer[GameElement.FIRE] = .5f;
		baseElementsMultiplyer[GameElement.ROCK] = 2f;
		baseElementsMultiplyer[GameElement.ELECTRIC] = .5f;
	}
	
	public override float ElementCheck(GameElement comparedElement)
	{
		return baseElementsMultiplyer[comparedElement];
	}
}
public class ElementElectric:Element
{
	public ElementElectric()
	{
		AssignDictionary();
		
	}
	
	public override void AssignDictionary()
	{
		base.AssignDictionary();
		
		baseElementsMultiplyer[GameElement.WATER] = 2f;
		baseElementsMultiplyer[GameElement.FIRE] = .5f;
		baseElementsMultiplyer[GameElement.ROCK] = .5f;
		baseElementsMultiplyer[GameElement.AIR] = 2f;
	}
	
	public override float ElementCheck(GameElement comparedElement)
	{
		return baseElementsMultiplyer[comparedElement];
	}
}

