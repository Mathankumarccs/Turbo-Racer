




// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class ShowroomPlatformRotate : MonoBehaviour {

	public static bool goDown;
	public static bool ComeUp;

	private bool changedCar;
	public static string NextOrPrevious;

	public Transform target;
	public float distance= 10.0f;
	
	public float xSpeed= 250.0f;
	public float ySpeed= 120.0f;
	
	public float yMinLimit= -20;
	public float yMaxLimit= 80;

	public float rotationSpeed;
	
	private float x= 0.0f;
	private float y= 0.0f;

	private Quaternion xx = new Quaternion(0,0,0,0);
	private Quaternion yy = new Quaternion(0,720,0,0);

	private Vector3 initialPosition = new Vector3();
	private Vector3 fullyImmersedPosition = new Vector3();
	Quaternion rotation;
	
	//@script AddComponentMenu("Camera-Control/Mouse Orbit")
		
	void  Start (){
		initialPosition = transform.position;
		fullyImmersedPosition = new Vector3(transform.position.x, -1.8f, transform.position.z);
		
		// Make the rigid body not change rotation
		goDown = false;
		ComeUp = false;

	}



	void  FixedUpdate ()
	{
		//Debug.Log (target.position);

		if(goDown)
		{
			ComeUp= false;


			float intCoord = Mathf.Lerp(transform.position.y, 10.15f, Time.deltaTime * 1f);

			if(intCoord < 11.1)
			{
				intCoord = 10.2f;
			}

			float yRotation = Mathf.Lerp(transform.rotation.eulerAngles.y, 130, Time.deltaTime);

			if(target.position.y < 10.3 && !changedCar)	
			{
				Debug.Log ("GOING DOWN");
				if(NextOrPrevious == "Next")
				{
					carSelection.Static.showNextcar();
				}
				else if(NextOrPrevious == "Previous")
				{
					carSelection.Static.showPreviouscar();
				}
			}

			target.position = new Vector3(transform.position.x, intCoord, transform.position.z);
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);

		}


		if(ComeUp)
		{


			changedCar = false;
			float intCoord = Mathf.Lerp(transform.position.y, 14.2603f, Time.deltaTime * 3f);



			float yRotation = Mathf.Lerp(transform.rotation.eulerAngles.y, 4.8f, Time.deltaTime);

			if(intCoord > 14.26)
			{
				ComeUp = false;
			}
		


			target.position = new Vector3(transform.position.x, intCoord, transform.position.z);
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
		}
			


	}
	
	static float  ClampAngle ( float angle ,   float min ,   float max  ){
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
