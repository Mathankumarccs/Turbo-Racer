




// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class DragMenuScript : MonoBehaviour {
	public Transform target;
	public float distance= 10.0f;
	
	public float xSpeed= 250.0f;
	public float ySpeed= 120.0f;
	
	public float yMinLimit= -20;
	public float yMaxLimit= 80;

	public float rotationSpeed;
	
	private float x= 0.0f;
	private float y= 0.0f;

	private float xx;
	private float yy;
	float xAxis = 0;
	private Vector3 position = new Vector3();

	Quaternion rotation;
	
	//@script AddComponentMenu("Camera-Control/Mouse Orbit")
		
	void  Start (){
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		rotation = Quaternion.Euler(y, x, 0);
		position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

		transform.rotation = rotation;
		transform.position = position;
	}



	void Update()
	{
		if(Application.isMobilePlatform)
		{
			if(Input.touchCount > 1)
			{
				MouseDrag.isDrag = false;
			}
		}
	}

	void  LateUpdate ()
	{
		if(Application.isMobilePlatform)
		{
			if (target && MouseDrag.isDrag) 
			{
				//rotationSpeed = 40;
				
				
				x += Input.touches[0].deltaPosition.x * xSpeed * 0.008f;
				y -= Input.touches[0].deltaPosition.y * ySpeed * 0.008f;
				
				y = ClampAngle(y, yMinLimit, yMaxLimit);
				
				rotation = Quaternion.Euler(y, x, 0);
				position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
				
				transform.rotation = rotation;
				transform.position = position;
				
				
			}
			else if(target)
			{
				
				//rotationSpeed = Mathf.Lerp(rotationSpeed, 1, Time.deltaTime);
				
				x += Time.deltaTime * xSpeed * 0.02f * rotationSpeed;
				y = y;
				
				y = ClampAngle(y, yMinLimit, yMaxLimit);
				
				
				
				rotation = Quaternion.Euler(y, x, 0);
				position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
				
				transform.rotation = rotation;
				transform.position = position;		
			}
		}
		else
		{

			if (target && MouseDrag.isDrag) 
			{
				//rotationSpeed = 40;

				//xAxis = Mathf.Lerp(xAxis, Input.GetAxis("Mouse X"), Time.deltaTime *2);
			
				x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
				
				y = ClampAngle(y, yMinLimit, yMaxLimit);
				
				rotation = Quaternion.Euler(y, x, 0);
				position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
				
				transform.rotation = rotation;
				transform.position = position;


			}
			else if(target)
			{

				//rotationSpeed = Mathf.Lerp(rotationSpeed, 1, Time.deltaTime);

				x += Time.deltaTime * xSpeed * 0.02f * rotationSpeed;
				y = y;

				y = ClampAngle(y, yMinLimit, yMaxLimit);



				rotation = Quaternion.Euler(y, x, 0);
				position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
				
				transform.rotation = rotation;
				transform.position = position;		
			}
		}
//		else 
//		{
//			x += Time.deltaTime * xSpeed * 0.02f * rotationSpeed;
//			y = y;// -= Time.deltaTime * ySpeed * 0.02f;
//			
//			y =ClampAngle(y, yMinLimit, yMaxLimit);
//			
//			rotation = Quaternion.Euler(y, x, 0);
//			position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
//			
//			transform.rotation = rotation;
//			transform.position = position;		
//		}
	
	}
	
	static float  ClampAngle ( float angle ,   float min ,   float max  ){
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
