using UnityEngine;
using System.Collections;

/// <summary>
/// Buttons_ br_ acc : set the two touch areas for Brakes and Acceleration to the lower left and roght quarters of the screen. We did not use colliders for this.
/// </summary>
public class Buttons_Br_Acc : MonoBehaviour {


	public Texture[] brakeButtonTex,nitrousButton;
	public Renderer brakeRenderer,nitrousButtonRenderer;

	private Rect bottomLeft = new Rect ();
	private Rect bottomRight = new Rect ();
	// Use this for initialization
	void Start () {
		bottomLeft = new  Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2);
		bottomRight = new Rect	(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2);
	}
	
	// Update is called once per frame
	void Update () {

		if (Application.isMobilePlatform)
		{
		//handle all the touches
		for (int i = 0; i < Input.touchCount; i++) 
		{

			//and the 3 touch states that we care about
			if (Input.GetTouch (i).phase == TouchPhase.Began || Input.GetTouch (i).phase == TouchPhase.Moved || Input.GetTouch (i).phase == TouchPhase.Stationary)
			{
				//get the touch position
				var touchPos = Input.GetTouch(i).position;

					
				//var touchPos = Input.mousePosition;

				//invert the y touch position, otherwise the screen won't be split correctly (GUI / Event screen space is flipped vertically compared to normal screen space.)
				touchPos = new Vector2 (touchPos.x, Screen.height - touchPos.y);

				//brakes
				if (bottomLeft.Contains(touchPos))
				{
					//Debug.Log("bottomLeft touched");
					AccelerationIndicator.Static.isAccelerationPressed = false; // is accelerating
					nitrousButtonRenderer.material.mainTexture = nitrousButton [0];
						if(CarController.actualSpeed > 2.01f)
						{
							UIControl.isBrakesOn = true ;
						}
						else
						{
							UIControl.isBrakesOn = false;
						}
					brakeRenderer.material.mainTexture = brakeButtonTex [1];	
					//if the user is touching the brake area, ignore the rest of the touches
					break;

				}
				//accelerate
				if (bottomRight.Contains(touchPos))
				{
					//Debug.Log("bottomRight touched");
					AccelerationIndicator.Static.isAccelerationPressed = true; // is accelerating
					nitrousButtonRenderer.material.mainTexture = nitrousButton [1];
					UIControl.isBrakesOn = false ;
					brakeRenderer.material.mainTexture = brakeButtonTex [0];


				}
			}

		}

		//reset the button textures and accelerate/brakes variables when there are no touches
		if (Input.touchCount == 0) 
		{
			AccelerationIndicator.Static.isAccelerationPressed = false; 
			nitrousButtonRenderer.material.mainTexture = nitrousButton [0];

			UIControl.isBrakesOn = false ;
			brakeRenderer.material.mainTexture = brakeButtonTex [0];
		}
			
		}

	}







}
