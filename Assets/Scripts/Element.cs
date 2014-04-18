using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameElement
{
	NORMAL,
	WATER, 
	FIRE, 
	ROCK,
	AIR,
	ELECTRIC,
}

public class Element : MonoBehaviour
{
	private static Element newElementClass= null;
	private static string[,] elementsMultiplier;
	[SerializeField] private TextAsset csv = null;
	
	void Start () {
		if (!newElementClass){newElementClass = this;}
		else{return;}
		
		elementsMultiplier = CSVReader.SplitCsvGrid (csv.text);
		GetMultiplayerForAttackerElement (GameElement.NORMAL,GameElement.NORMAL);
	}

	public static float GetMultiplayerForAttackerElement (GameElement attacker, GameElement defender) {
		string result = elementsMultiplier [(int)defender+1,(int)attacker+1];
		return float.Parse (result);
	}
}