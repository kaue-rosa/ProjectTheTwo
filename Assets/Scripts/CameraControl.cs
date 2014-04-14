using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	private bool isHolding = false; 
	private Camera myCamera;

	private Vector3 initialPositionOnScreen = Vector3.zero;
	private Vector3 currentPositionOnScreen = Vector3.zero;

	[SerializeField] private float correctionFactor = 0.025f;

	void Start () 
	{
		myCamera = Camera.main;
	}

	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			isHolding = true;
			initialPositionOnScreen = Input.mousePosition;
		}
		if(Input.GetMouseButtonUp(0))
			isHolding = false;

		if(isHolding)
		{

			if(Input.mousePosition != currentPositionOnScreen)
			{
				currentPositionOnScreen = Input.mousePosition;

				myCamera.transform.position += MoveCamera(initialPositionOnScreen, currentPositionOnScreen);

				initialPositionOnScreen = currentPositionOnScreen;
			}
		}

	}
	 private Vector3 MoveCamera(Vector3 initPos, Vector3 currPos)
	{
		Vector3 cameraMovement = Vector3.zero;

		cameraMovement = initPos - currPos;

		cameraMovement *= correctionFactor;

		return cameraMovement;
	}
}
