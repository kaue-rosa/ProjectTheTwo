using UnityEngine;
using System.Collections;

public class GameMatch : MonoBehaviour {

	public static bool machIsOver = false;

	// Use this for initialization
	void Start () {
		machIsOver = false;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (machIsOver)
		{
			if (GUI.Button (new Rect(Screen.width/2 - 50, Screen.height/2 - 50, 100 ,100),"Restart")) {
					Application.LoadLevel (0);
			}
		}
	}
}
