using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager control;

	[SerializeField] private List<GameObject> totalTroops = new List<GameObject>();
	private List<GameObject> selectedTroops = new List<GameObject>();

	[SerializeField] private List<GameObject> totalGates;
	[SerializeField] private GameObject selectedGate = null;

	public List<GameObject> TotalTroops
	{
		get {return totalTroops;}
	}
	public List<GameObject> SelectedTroops
	{
		get {return selectedTroops;}
	}
	public List<GameObject> TotalGates
	{
		get {return totalGates;}
	}
	public GameObject SelectedGate
	{
		get {return selectedGate;}
		set {selectedGate = value;}
	}

	[SerializeField]private int maxNumberOfTroops = 3;
	public int MaxNumberOfTroops
	{
		get {return maxNumberOfTroops;}
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if(control == null)
		{
			control = this;
		}
		else if (control != this)
		{
			Destroy(gameObject);
		}
	}
	public static Texture2D GetTextureFromSprite(GameObject objWithTheSprite)
	{
		Texture2D img = objWithTheSprite.GetComponent<SpriteRenderer>().sprite.texture;
		Rect rect = objWithTheSprite.GetComponent<SpriteRenderer>().sprite.rect;
		
		Color[] pixels = img.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
		
		Texture2D finalImg = new Texture2D((int)rect.width, (int)rect.height);
		finalImg.SetPixels(pixels);
		
		finalImg.Apply();

		return(finalImg);
	}
}
