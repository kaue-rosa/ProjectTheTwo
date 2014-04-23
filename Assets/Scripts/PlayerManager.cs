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
		set {totalTroops = value;}
	}
	public List<GameObject> SelectedTroops
	{
		get {return selectedTroops;}
	}
	public List<GameObject> TotalGates
	{
		get {return totalGates;}
		set {totalGates = value;}
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
		SpriteRenderer sr = objWithTheSprite.GetComponent<SpriteRenderer> ();
		if(sr == null) 
		{
			foreach (Transform child in objWithTheSprite.transform)
			{
				sr = child.GetComponent<SpriteRenderer> ();
				if(sr!=null) break;
			}
		}
		Texture2D img = sr.sprite.texture;
		Rect rect = sr.sprite.rect;
		
		Color[] pixels = img.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
		
		Texture2D finalImg = new Texture2D((int)rect.width, (int)rect.height);
		finalImg.SetPixels(pixels);
		
		finalImg.Apply();

		return(finalImg);
	}
}
