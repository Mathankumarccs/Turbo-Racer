using UnityEngine;
using System.Collections;
using System;

public class AccelerationIndicator : MonoBehaviour
{

		
		

		public static AccelerationIndicator Static;
		public bool isAccelerationPressed = false ;
		public static bool isAccelerating;
		
	void OnEnable ()
		{				
			Static = this;	
		}


	
		
		
 
		public void Update ()
		{
			if (isAccelerationPressed) 
			{
				isAccelerating = true;
			}
		    else
			{
				if(CarController.actualSpeed > 4f)
					CarController.isDoubleSpeed -= Time.deltaTime /20;
					isAccelerating = false;
			}

		}


}
