using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager control;

	[SerializeField]private List<GameObject> totalTroops = new List<GameObject>();
	public List<GameObject> selectedTroops = new List<GameObject>();

	[SerializeField]private int maxNumberOfTroops = 3;

	void Awake()
	{
		if(!control)
		{
			control = this;
		}
		else if (control != this)
		{
			Destroy(this.gameObject);
		}
	}

	void OnGUI()
	{

		if(Application.loadedLevel <= 0)
		{
			for (int i=0; i<totalTroops.Count; i++)
			{
				if(!selectedTroops.Contains(totalTroops[i]))
				{						
					if(GUILayout.Button(GetTextureFromSprite(totalTroops[i])) && selectedTroops.Count<maxNumberOfTroops)
					{

						selectedTroops.Add(totalTroops[i]);
					}
				}
			}
			GUILayout.BeginArea(new Rect(Screen.width/2,10, Screen.width/2, Screen.height));
			for(int ii=0; ii<selectedTroops.Count; ii++)
			{

				GUILayout.Button(GetTextureFromSprite(selectedTroops[ii]));
			}

			if(selectedTroops.Count == maxNumberOfTroops)
			{
				if(GUILayout.Button("Play"))
				{
					Application.LoadLevel(1);
				}
			}

			GUILayout.EndArea();
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
