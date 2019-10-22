using UnityEngine;
using System.Collections;
using System;
public class carCamera : MonoBehaviour {

	// Use this for initialization

	public static int cameraMode;
	public static bool cameraChanged;



	public Transform targetTrans,thisTransform ;
	public static Transform thisTransformStatic;
	public Vector3 offset;
	public Vector3 offsetLimit;


	//change these to change Camera, there are 3 offset vectors, for the 3 camera modes. Change their values as you see fit if you need to change the camera position.
	public Vector3 offset1;
	public Vector3 offset2;
	public Vector3 offset3;


	public Vector3 offsetLimit1; 
	public Vector3 offsetLimit2;
	public Vector3 offsetLimit3;

	public float accCameraOff = 0.65f;      //how much the car goes forward when accelerating, relative to the camera's position
	public float brakeCameraOff = 0.4f;		//how much the car goes back when braking, relative to the camera's position
		



	// Update is called once per frame
	bool justOnce = false;
	float speed = 0.2f;

	  void Start()
	  {
		thisTransformStatic = gameObject.transform;
		offset = offset1;
		offsetLimit = offsetLimit1;
		cameraMode = 1;
		speed = 0.2f;

		//accCameraOff = 0;
		//brakeCameraOff = 0;





	  }
	void LateUpdate () {


		if (!cameraChanged) 
		{				
			if(cameraMode == 1)
			{

				offset = Vector3.Lerp(offset, offset1, Time.deltaTime * 2f);
				offsetLimit = Vector3.Lerp(offsetLimit, offsetLimit1, Time.deltaTime * 2f);	



				GetComponent<Camera>().transform.rotation = Quaternion.Euler (Mathf.Lerp(GetComponent<Camera>().transform.rotation.eulerAngles.x, 8f, Time.deltaTime * 3f), GetComponent<Camera>().transform.rotation.y, GetComponent<Camera>().transform.rotation.z);


				GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, 40, Time.deltaTime * 2f);


				 

			}
			else if(cameraMode == 2)
			{			

				offset = Vector3.Lerp(offset, offset2, Time.deltaTime * 2f);
				offsetLimit = Vector3.Lerp(offsetLimit, offsetLimit2, Time.deltaTime * 2f);
				GetComponent<Camera>().transform.rotation = Quaternion.Euler (Mathf.Lerp(GetComponent<Camera>().transform.rotation.eulerAngles.x, 19f, Time.deltaTime * 3f), GetComponent<Camera>().transform.rotation.y, GetComponent<Camera>().transform.rotation.z);
				GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, 31, Time.deltaTime * 2f);
			}
			else if(cameraMode == 3)
			{

				offset = Vector3.Lerp(offset, offset3, Time.deltaTime * 2f);
				offsetLimit = Vector3.Lerp(offsetLimit, offsetLimit3, Time.deltaTime * 2f);
				GetComponent<Camera>().transform.rotation = Quaternion.Euler (Mathf.Lerp(GetComponent<Camera>().transform.rotation.eulerAngles.x, 0f, Time.deltaTime * 3f), GetComponent<Camera>().transform.rotation.y, GetComponent<Camera>().transform.rotation.z);
				GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, 50, Time.deltaTime * 2f);
			}
		}


		if (!GamePlayController.isGameEnded && targetTrans != null) 
		{



			if(cameraMode == 1)
			{
				if(AccelerationIndicator.isAccelerating)
				{
					offset.z = Mathf.Lerp(offset.z, offsetLimit.z - accCameraOff, Time.deltaTime * 4f);
				}
				else if(UIControl.isBrakesOn && CarController.actualSpeed > 2.2f)
				{								
					offset.z = Mathf.Lerp(offset.z, offsetLimit.z + brakeCameraOff, Time.deltaTime * 4f);							
				}
				else
				{				
					offset.z = Mathf.Lerp(offset.z, offsetLimit.z, Time.deltaTime * 4f);
				}
				thisTransform.position = new Vector3 (offset.x+ ( (targetTrans.position.x*1)/4), offset.y, targetTrans.position.z + offset.z);

			}
			else if(cameraMode == 2)
			{
				if(AccelerationIndicator.isAccelerating)
				{
					offset.z = Mathf.Lerp(offset.z, offsetLimit.z - accCameraOff, Time.deltaTime * 4f);
				}
				else if(UIControl.isBrakesOn && CarController.actualSpeed > 2.2f)
				{								
					offset.z = Mathf.Lerp(offset.z, offsetLimit.z + brakeCameraOff, Time.deltaTime * 4f);							
				}
				else
				{				
					offset.z = Mathf.Lerp(offset.z, offsetLimit.z, Time.deltaTime * 4f);
				}
				thisTransform.position = new Vector3 (offset.x+ ( (targetTrans.position.x*1)/4), offset.y, targetTrans.position.z + offset.z);
			}
			else if(cameraMode == 3)
			{
				thisTransform.position = new Vector3 (targetTrans.position.x, offset.y, targetTrans.position.z + offset.z);
			}

		} 


		else 
		{			   
				thisTransform.Translate(Vector3.forward*speed  );
				if(!justOnce)
				{
					justOnce=true;
					Invoke("disableScript",0.4f);
				}
		}
	
		}

	void disableScript()
	{
		speed = 0;

	}
}
