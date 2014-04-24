using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager control;

	[SerializeField] private List<int> totalOwnedTroopsIds = new List<int>();
					 private List<int> selectedTroopsIds = new List<int>();

	[SerializeField] private List<int> totalOwnedGatesIds = new List<int>();
	[SerializeField] private int selectedGateId = -1;

	[SerializeField]private int maxNumberOfTroops = 3;

	public List<int> TotalOwnedTroopsIds
	{
		get {return totalOwnedTroopsIds;}
		set {totalOwnedTroopsIds = value;}
	}

	public List<int> SelectedTroopsIds
	{
		get {return selectedTroopsIds;}
	}

	public List<int> TotalOwnedGatesIds
	{
		get {return totalOwnedGatesIds;}
		set {totalOwnedGatesIds = value;}
	}

	public int SelectedGateId
	{
		get {return selectedGateId;}
		set {selectedGateId = value;}
	}

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

	//TODO: Remove if not in use!
	public static Texture2D GetTextureFromSprite(Sprite sprite)
	{
		Texture2D img = sprite.texture;
		Rect rect = sprite.rect;
		
		Color[] pixels = img.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
		
		Texture2D finalImg = new Texture2D((int)rect.width, (int)rect.height);
		finalImg.SetPixels(pixels);
		
		finalImg.Apply();

		return(finalImg);
	}
}
