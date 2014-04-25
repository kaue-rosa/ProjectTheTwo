using UnityEngine;
using System.Collections;

public class ButtonEvent : MonoBehaviour {
	
	[SerializeField] private Color colorOver = new Color(1f,0.88f,0.55f); 
	[SerializeField] private Color colorPushed  = new Color(0.66f,0.66f,0.48f);
	[SerializeField] private MonoBehaviour target  = null;
	[SerializeField] private string targetMessage  = "";

	private Color originalColor;
		
	void Start() {
			originalColor = gameObject.renderer.material.color;     
	}
		
	void OnMouseEnter() {
		gameObject.renderer.material.color= colorOver;		
	}   

	void OnMouseExit() {
		gameObject.renderer.material.color= originalColor;
	}

	void OnMouseDown() {
		gameObject.renderer.material.color= colorPushed;
	}
	
	void OnMouseUpAsButton() {       
		gameObject.renderer.material.color= originalColor;
		if(target!=null)
			target.SendMessage (targetMessage);
	}	
}