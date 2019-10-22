using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GreedyGame.Runtime;

public class MainMenu : MonoBehaviour {

	public Camera uiCamera;
	public Renderer[] menuButtonRenders;
	public Texture[] buttonTexture;
	public RaycastHit hit;
	public GameObject storeObject;
	private int carIndex;
	public GameObject[] carMeshObj;
	public string[] reviewUrls,MoreUrls;
	bool isDrag=false;

	public static GameObject selectedCarWheels;
	private string wheelSetName;



	// Use this for initialization
	void Start () {
        //Uncomment this code line to reset the game data to default values, lock all cars/wheels/colors, reset upgrades and purchases.
        //PlayerPrefs.DeleteAll();

        HelperScript.deathCount = 0;

        if (PlayerPrefs.GetInt("InitialCarsSetupComplete", 0) == 0)
		{
			SetupAllCarsForTheFirstTime();
		}
		carIndex = PlayerPrefs.GetInt ("selectedCarIndex", 0);


		// instantiate the appropriate wheels for every car aveilable in menu
		for(int i = 0; i< 6; i++)
		{
			SetUpSelectedWheels(i);
		}

	





		string carBodyColorMat = PlayerPrefs.GetString ("Car" + carIndex + "Color"); //get the path to load the selected car body material, the path was set in menu, in carSelection.cs script
		
		
		Transform carTransform = carMeshObj[carIndex].transform;
		foreach(Transform child in carTransform)
		{
			foreach(Transform child2 in child)
			{
				if(child2.name == "body")
				{
					Renderer rnd = child2.GetComponent<Renderer>();						
					
					//instantiate the new material from resources
					Material newCarBodyMaterial = Resources.Load ("CarColors/" + carMeshObj[carIndex].transform.name + "Colors/" +  carBodyColorMat) as Material;	
					//						Debug.Log (carMeshObj[carIndex].transform.name + "Colors/" +  carBodyColorMat);
					//set the new material for the car body, so it changes color
					Material[] mats = rnd.materials;

					for(int i=0; i<mats.Length; i++)
					{
						//if the material name has the following format: CarName_ + color - it must be updated to change color. 
						//This is our naming convention, since all car body materials are names like this: CarName_ColorName
						//the SUV has 3 materials that need updating, in order to change its color, the rest of the cars, just 1
						if (mats[i].name.Contains(carMeshObj[carIndex].transform.name+ "_"))  
						{
							mats[i] = newCarBodyMaterial;
						}
					}

					//mats[0] = newCarBodyMaterial;
					rnd.materials = mats;
					
				}
				
			}
		}






		carMeshObj[carIndex].SetActive(true);
		Debug.Log( "MainMenu.cs is Attached to " + gameObject.name );

#if UNITY_IPHONE
		// Apple won't allow quit button in their app submission guides 
		//menuButtonRenders[4].transform.parent.gameObject.SetActive(false);
#endif


	}



	void SetUpSelectedWheels(int currentCarIndex)
	{
		wheelSetName = PlayerPrefs.GetString("car" + currentCarIndex + "Wheels");	


		
//		if(selectedCarWheels)
//		{
//			DestroyImmediate(selectedCarWheels);
//		}
		Debug.Log ("CarWheelsMenu/"+ carMeshObj[currentCarIndex].transform.name +"/" +  wheelSetName);
		selectedCarWheels = Instantiate(Resources.Load ("CarWheelsMenu/"+ carMeshObj[currentCarIndex].transform.name +"/" +  wheelSetName)) as GameObject;

		Vector3 prefabWheelsPosition = selectedCarWheels.transform.localPosition;
		Vector3 prefabWheelsScale = selectedCarWheels.transform.localScale;


//		selectedCarWheels.transform.position = carMeshObj[currentCarIndex].transform.position;
//		selectedCarWheels.transform.rotation = carMeshObj[currentCarIndex].transform.rotation;
		
		selectedCarWheels.transform.SetParent(carMeshObj[currentCarIndex].transform);

		//set position / rotation / scale after setting the car as a parent because unity looses these values for some reason
		selectedCarWheels.transform.localPosition = prefabWheelsPosition;
		selectedCarWheels.transform.localScale = prefabWheelsScale;
		//set rotation to 0,0,0 on x,y,z axis
		selectedCarWheels.transform.localRotation = Quaternion.identity;


//		selectedCarWheels.transform.localScale = new Vector3(100,100,100);
		
		
		
		
		//rotate the front wheels to make it look like the wheels are turned a bit(looks better this way)
//		if(selectedCarWheels)
//		{
//			foreach (Transform child in selectedCarWheels.transform)
//			{
//				
//				if(child.name.Contains("Parent"))
//				{
//					
//					child.rotation = Quaternion.Euler(0,-100,0);
//				}
//				
//				
//			}
//		}

		//each car instance with it"s own wheels instance
		menuCarWheels.eachCarCurrentWheels[currentCarIndex] = selectedCarWheels;

	}




	void SetupAllCarsForTheFirstTime()
	{

		PlayerPrefs.SetString("RatePopupDateTime",null);
		PlayerPrefs.SetInt("CanShowRatePopup",1);

		PlayerPrefs.SetInt("CashDoubler", 0);
		PlayerPrefs.SetInt("RemoveAds", 0);
		PlayerPrefs.SetInt("CityHighScore",0);
		PlayerPrefs.SetInt("Desert2HighScore",0);
		PlayerPrefs.SetInt("Desert3HighScore",0);
		PlayerPrefs.SetInt("highwayHighScore",0);

		//		PlayerPrefs.DeleteAll();


		//for all the cars
		for(int i = 0; i< carMeshObj.Length; i++)
		{




			//setup the first set of wheels for all cars
			PlayerPrefs.SetString("car" + i + "Wheels", carMeshObj[i].transform.name + "_WS_2");

			PlayerPrefs.SetInt("car" + i + "UpgradedSpeed", 0);
			PlayerPrefs.SetInt("car" + i + "UpgradedHandling", 0);
			PlayerPrefs.SetInt("car" + i + "UpgradedBrakes", 0);
			
			
			//set the default color for all the cars, the color that will be also unlocked by default
			PlayerPrefs.SetString("Car" + i + "Color", carMeshObj[i].transform.name + "_Orange");
			
			
			//unlock the default color and lock the rest of them (0 means LOCKED, and 1 means UNLOCKED) - in player pref the colors are named by their path in resources folder
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Red", 0);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Yellow", 0);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Purple", 0);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Green", 0);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Black", 0);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_White", 0);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Orange", 1);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Pink", 0);
			PlayerPrefs.SetInt("CarColors/" + carMeshObj[i].transform.name + "Colors/" +  carMeshObj[i].transform.name + "_Blue", 0);

			//unlock the default Wheels and lock the rest of them (0 means LOCKED, and 1 means UNLOCKED) - in player pref the Wheels are named by their exact name in the resources folder
			PlayerPrefs.SetInt(carMeshObj[i].transform.name + "_WS_2", 1);
			PlayerPrefs.SetInt(carMeshObj[i].transform.name + "_WS_3", 0);
			PlayerPrefs.SetInt(carMeshObj[i].transform.name + "_WS_4", 0);
			PlayerPrefs.SetInt(carMeshObj[i].transform.name + "_WS_5", 0);
			PlayerPrefs.SetInt(carMeshObj[i].transform.name + "_WS_6", 0);

		}		

		//set default attributes for all cars
		if(!PlayerPrefs.HasKey("car0Speed"))
		{
			PlayerPrefs.SetInt("iscar0Purchased",1);
			PlayerPrefs.SetInt ("car0Speed", 25);
			PlayerPrefs.SetInt ("car0Handling", 20);
			PlayerPrefs.SetInt ("car0Brakes", 15);
		}
		if(!PlayerPrefs.HasKey("car1Speed"))
		{
			PlayerPrefs.SetInt("iscar1Purchased",0);
			PlayerPrefs.SetInt ("car1Speed", 30);
			PlayerPrefs.SetInt ("car1Handling", 35);
			PlayerPrefs.SetInt ("car1Brakes", 20);	
		}
		if(!PlayerPrefs.HasKey("car2Speed"))
		{
			PlayerPrefs.SetInt("iscar2Purchased",0);
			PlayerPrefs.SetInt ("car2Speed", 55);
			PlayerPrefs.SetInt ("car2Handling", 40);
			PlayerPrefs.SetInt ("car2Brakes", 40);
		}
		if(!PlayerPrefs.HasKey("car3Speed"))
		{
			PlayerPrefs.SetInt("iscar3Purchased",0);
			PlayerPrefs.SetInt ("car3Speed", 50);
			PlayerPrefs.SetInt ("car3Handling", 30);
			PlayerPrefs.SetInt ("car3Brakes", 35);
		}
		if(!PlayerPrefs.HasKey("car4Speed"))
		{
			PlayerPrefs.SetInt("iscar4Purchased",0);
			PlayerPrefs.SetInt ("car4Speed", 70);
			PlayerPrefs.SetInt ("car4Handling", 70);
			PlayerPrefs.SetInt ("car4Brakes", 65);
		}
		if(!PlayerPrefs.HasKey("car5Speed"))
		{
			PlayerPrefs.SetInt("iscar5Purchased",0);		
			PlayerPrefs.SetInt ("car5Speed", 85);
			PlayerPrefs.SetInt ("car5Handling", 75);
			PlayerPrefs.SetInt ("car5Brakes", 80);	
		}
		PlayerPrefs.SetInt("InitialCarsSetupComplete", 1); //the cars have been set up successfully
		
	}












    void Update()
    {

        //show admob banner if it is not null, and it is not visible already
        //		if(ad_mob_banner.banner1 != null && !HelperScript.isAdMobBannerVisible)
        //		{
        //			ad_mob_banner.banner1.Show();
        //			HelperScript.isAdMobBannerVisible = true;
        //		}


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!MouseDrag.isDrag)
            {
                //				MouseDown(Input.mousePosition );
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!MouseDrag.isDrag)
            {
                MouseUp(Input.mousePosition);
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            //Application.Quit();
        }
        if (!MouseDrag.isMousePressed)
        {
            foreach (Renderer r in menuButtonRenders)
            {
                r.material.mainTexture = buttonTexture[0];
            }
        }

    }




    void MouseUp(Vector3 a)
    {

        foreach (Renderer r in menuButtonRenders)
        {
            r.material.mainTexture = buttonTexture[0];
        }
        Ray ray = uiCamera.ScreenPointToRay(a);

        if (Physics.Raycast(ray, out hit, 500))
        {

            switch (hit.collider.name)
            {
                case "Play":
                    //carSelection.SetActive(true);
                    gameObject.SetActive(false);
                    break;


                case "more":

                    string url = "https://play.google.com/store/apps/developer?id=GreedyGame+Media";
                    Application.OpenURL(url);
                    break;
                case "RateUs":

                    string rateurl = "https://play.google.com/store/apps/details?id=com.etheriumstudio.game.trafficracer";
                    Application.OpenURL(rateurl);
                    break;
                case "Quit":
                    Application.Quit();
                    break;

            }
        }

    }
    //	void MouseDown(Vector3 a )
    //	{
    //		
    //		Ray ray = uiCamera.ScreenPointToRay(a);
    //		
    //		if (Physics.Raycast(ray, out hit, 500))
    //		{
    //			SoundController.Static.PlayButtonClickSound();
    //
    //			switch(hit.collider.name)
    //			{
    //			case "Play":
    //				menuButtonRenders[0].material.mainTexture  = buttonTexture[1];
    //				break;
    //			case "Store":
    //				menuButtonRenders[1].material.mainTexture  = buttonTexture[1];
    //				break;
    //
    //			case "more":
    //				menuButtonRenders[2].material.mainTexture  = buttonTexture[1];
    //				break;
    //			case "RateUs":
    //				menuButtonRenders[3].material.mainTexture  = buttonTexture[1];
    //				break;
    //			case "Quit":
    //				menuButtonRenders[4].material.mainTexture  = buttonTexture[1];
    //				break;
    //				
    //			}
    //
    //			 
    //		}
    //		
    //	}
}
