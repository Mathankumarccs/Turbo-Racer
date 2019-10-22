using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class carSelection : MonoBehaviour {

	public GameObject carPlatform;
    public GameObject showroomObj;
    public GameObject LoadingProgress;
	public GameObject upgradeSpeedButton, upgradeHandlingButton, upgradeBrakesButton;
	// Use this for initialization
	public static carSelection Static;
	public static bool canRenderGUI;

	public GameObject[] colorButtons;
	public GameObject paintPriceTagBlue,paintPriceTagRed,paintPriceTagYellow,paintPriceTagWhite,paintPriceTagBlack,paintPriceTagPurple,paintPriceTagPink,paintPriceTagGreen;
	public GameObject[] wheelButtons;
	public GameObject wheelPriceTag_WS_2,wheelPriceTag_WS_3,wheelPriceTag_WS_4,wheelPriceTag_WS_5,wheelPriceTag_WS_6;
	public Camera uiCamera;
    public Camera mainCamera;
	public Renderer[] buttonRenders;
	public Renderer paintParentRenderer;
	public Renderer wheelParentRenderer;

	public Texture[] buttonTexture;
	public RaycastHit hit;
	public GameObject buyButton,playButton;
	public GameObject buyPopUpObj,InAPPMenu;
	public GameObject loadingLevelObj;
	public GameObject priceTag;
	public GameObject nextBtn;
	public GameObject previousBtn;
	public GameObject paintParent;
	public GameObject wheelsParent;
	public GameObject paintButton;
	public GameObject wheelsButton;
	public GameObject[] upgradeCarButtons;

	private Material carBodyMaterial;

	public static int carIndex=0;
	public static string carTransformName;
	public GameObject[] carMeshObjs;
	public TextMesh carSpeedDisplayText,carSpeedDisplayTextOutline,carSpeedDisplayTextBevel,  carHandlingDisplayText,carHandlingDisplayTextOutline,carHandlingDisplayTextBevel, carBrakesDisplayText,   carBrakesDisplayTextOutline,   carBrakesDisplayTextBevel,carPriceDisplayText,headingText, wheelsParentText, paintParentText;


	private GameObject carBody;
	private GameObject body;

	public float widthFactor;
	public float heightFactor;

	public float widthScaleFactor;
	public float heightScaleFactor;

	//DISPLAY BAR
	float speedBarDisplay; //current progress
	float handlingBarDisplay;
	float brakesBarDisplay;
	public Vector2 pos = new Vector2(280,360);
	public Vector2 size = new Vector2(180,20);
	public Texture emptyTex;
	public Texture fullTex;
	private GameObject selectedCarWheels;
	public static string wheelSetName;

    void OnGUI() {


		if(canRenderGUI)
		{

			//the position of the 3 bars corresponding to Speed, Handling and Brakes, based on the screen size
			pos = new Vector2(Screen.width/widthFactor, Screen.height/heightFactor);

			//the size of the 3 bars corresponding to Speed, Handling and Brakes, based on the screen size
			size = new Vector2(Screen.width/widthScaleFactor, Screen.height/heightScaleFactor);

			//Speed bar 


			//draw the background:
			GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));

			GUI.DrawTexture(new Rect(0,0, size.x, size.y), emptyTex);
			
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, size.x * speedBarDisplay, size.y));
			GUI.DrawTexture(new Rect(0,0, size.x, size.y), fullTex);

			GUI.EndGroup();
			GUI.EndGroup();


			//Handling bar
			//draw the background:
			GUI.BeginGroup(new Rect(pos.x, pos.y + Screen.height/33f * 2.2f, size.x, size.y));
			GUI.DrawTexture(new Rect(0,0, size.x, size.y), emptyTex);


			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, size.x * handlingBarDisplay, size.y));
			GUI.DrawTexture(new Rect(0,0, size.x, size.y), fullTex);

			GUI.EndGroup();
			GUI.EndGroup();


			//brakes bar
			//draw the background:
			GUI.BeginGroup(new Rect(pos.x, pos.y + Screen.height/33f * 4.4f, size.x, size.y));
			GUI.DrawTexture(new Rect(0,0, size.x, size.y), emptyTex);

			
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, size.x * brakesBarDisplay, size.y));
			GUI.DrawTexture(new Rect(0,0, size.x, size.y), fullTex);

			GUI.EndGroup();
			GUI.EndGroup();
		}
	}


	void Start () 
	{

		wheelSetName = null;

		canRenderGUI = true;
		Static = this;
	
		//PlayerPrefs.DeleteAll();
		//carMeshObjs [0].SetActive (false);

		carIndex = PlayerPrefs.GetInt ("selectedCarIndex", 0);

		carMeshObjs [carIndex].SetActive (true);
		carTransformName = carMeshObjs[carIndex].transform.name;

		selectedCarWheels =	menuCarWheels.eachCarCurrentWheels[carIndex];





		if (carIndex == carMeshObjs.Length) 
		{
			nextBtn.SetActive(false);			
		}
		if(carIndex == 0)
		{
			previousBtn.SetActive(false);		
		}

        //LAKSHYA
        int flag = PlayerPrefs.GetInt("isFirstLaunch", 0);
       if ( flag == 0)
        {
            PlayerPrefs.SetInt("isFirstLaunch", 1);
           TotalCoins.staticInstance.AddCoins(500);
            
        }
        //TotalCoins.staticInstance.AddCoins(999999);
        //SetupAllCarsForTheFirstTime ();
        //		Debug.Log( "carSelection.cs is Attached to " + gameObject.name );

#if UNITY_EDITOR || UNITY_WEBPLAYER
        //TotalCoins.staticInstance.AddCoins(999999); //allot some coins to test it 
#endif


        showcarINFO ();

	}








	/// <summary>
	/// Updates the car wheels.
	/// </summary>
	/// <param name="wheelSetName">Wheel set name.</param>
	/// <param name="wheelButtonName">Wheel button name, for the corresponding wheel set that is being equipped.</param>
	public void UpdateCarWheels(string wheelSetName, string wheelButtonName)
	{

		//this is just to scale up the wheels button of the selected wheel
		for(int i=0; i< wheelButtons.Length; i++)
		{
			if(wheelButtons[i].transform.name == wheelButtonName)
			{
				SpriteRenderer x = wheelButtons[i].GetComponent<SpriteRenderer>();
				//x.color = new Color(0,0,0);
				
				//x.sprite =		//set new sprite for the selected color
				wheelButtons[i].transform.localScale = new Vector3(3f,3f,3f);
				
			}
			else
			{
				wheelButtons[i].transform.localScale = new Vector3(2.5f,2.5f,2.5f);
			}
		}


		selectedCarWheels = menuCarWheels.eachCarCurrentWheels[carIndex];
		if(selectedCarWheels)
		{
			DestroyImmediate(selectedCarWheels);
		}
		
		
		selectedCarWheels = Instantiate(Resources.Load ("CarWheelsMenu/"+ carMeshObjs[carIndex].transform.name +"/" +  wheelSetName)) as GameObject;

		//get the scale and position values from the instantiated prefab
		Vector3 prefabWheelsPosition = selectedCarWheels.transform.localPosition;
		Vector3 prefabWheelsScale = selectedCarWheels.transform.localScale;


		//selectedCarWheels.transform.position = carMeshObjs[carIndex].transform.position;



		//selectedCarWheels.transform.rotation = carMeshObjs[carIndex].transform.rotation;

		//set the car as parent for the wheels set
		selectedCarWheels.transform.SetParent(carMeshObjs[carIndex].transform);
		

		
		//set position / rotation / scale after setting the car as a parent because unity looses these values for some reason
		selectedCarWheels.transform.localPosition = prefabWheelsPosition;
		selectedCarWheels.transform.localScale = prefabWheelsScale;
		//set rotation to 0,0,0 on x,y,z axis
		selectedCarWheels.transform.localRotation = Quaternion.identity;



		
		
		
		
		//rotate the front wheels to make it look like the wheels are turned a bit(looks better this way)
//		if(carMeshObjs[carIndex].transform.name == "playerSedan4")
//		{
//			if(selectedCarWheels)
//			{
//				foreach (Transform child in selectedCarWheels.transform)
//				{
//					
//					if(child.name.Contains("Parent"))
//					{
//						
//						child.rotation = Quaternion.Euler(0,-50,0);
//					}
//					
//					
//				}
//			}
//		}
//		else
		//{
			if(selectedCarWheels)
			{
				foreach (Transform child in selectedCarWheels.transform)
				{
					
					if(child.name.Contains("Parent"))
					{
						
						child.rotation = Quaternion.Euler(0,-100,0);
					}
					
					
				}
			}
		//}
		//set the selected wheels value in player pref
		PlayerPrefs.SetString("car" + carIndex + "Wheels", wheelSetName);
		menuCarWheels.eachCarCurrentWheels[carIndex] = selectedCarWheels;
		//pass the car wheels object to the script which holds the wheels references of the corresponding car


		ShowHideWheelsPriceTags();
		showcarINFO ();
	}


	/// <summary>
	/// Changes the color of the car.
	/// </summary>
	/// <param name="colorName">Color name.</param>
	public void UpdateCarColor(string colorName)
	{
	
		for(int i=0; i< colorButtons.Length; i++)
		{
			if(colorButtons[i].transform.name == colorName)
			{
				SpriteRenderer x = colorButtons[i].GetComponent<SpriteRenderer>();
				//x.color = new Color(0,0,0);

				//x.sprite =		//set new sprite for the selected color
				colorButtons[i].transform.localScale = new Vector2(16,8.5f);

			}
			else
			{
				colorButtons[i].transform.localScale = new Vector2(15,7.5f);
			}
		}

		PlayerPrefs.SetString("Car" + carIndex + "Color", carMeshObjs[carIndex].transform.name + "_" + colorName);
		ShowHidePaintPriceTags();
		showcarINFO ();
	}


	void FixedUpdate()
	{
		//if admob banner is not null, and it is visible, hide it
//		if(ad_mob_banner.banner1 != null && HelperScript.isAdMobBannerVisible)
//		{
//			ad_mob_banner.banner1.Hide();
//			HelperScript.isAdMobBannerVisible = false;
//		}

		//lerp the values of the Speed, Handling and Brakes bar, every time the car is changed in main menu
		float speedVariable = PlayerPrefs.GetInt("car" + carIndex + "Speed", 25);
		float handlingVariable = PlayerPrefs.GetInt("car" + carIndex + "Handling", 15);
		float brakesVariable = PlayerPrefs.GetInt("car" + carIndex + "Brakes", 15);
		
		
		
		speedBarDisplay = Mathf.Lerp(speedBarDisplay, speedVariable/100, Time.deltaTime * 3f);
		handlingBarDisplay = Mathf.Lerp(handlingBarDisplay, handlingVariable/100, Time.deltaTime * 3f);
		brakesBarDisplay = Mathf.Lerp(brakesBarDisplay, brakesVariable/100, Time.deltaTime * 3f);
//		Debug.Log (buyPopUpObj.activeSelf);
		if(ShowroomPlatformRotate.ComeUp || ShowroomPlatformRotate.goDown)
		{
			wheelsButton.SetActive(false);
			paintButton.SetActive(false);
			wheelsParent.SetActive(false);
			paintParent.SetActive(false);
		}
		else if(!wheelsButton.activeSelf && !paintButton.activeSelf && !buyPopUpObj.activeSelf) 
		{
			ShowPaintAndWheelsButtons_AfterCarSelect();
		}

	}

	void ShowPaintAndWheelsButtons_AfterCarSelect()
	{
		if(PlayerPrefs.GetInt("iscar"+ carIndex +"Purchased",0) == 1)
		{
			wheelsButton.SetActive(true);
			paintButton.SetActive(true);
		}
	}

	void Update () {





//		Debug.Log (TotalCoins.staticInstance.totalCoins + "   - COINS");

		if( Input.GetKeyDown(KeyCode.Mouse0) )
		{		
			if(!MouseDrag.isDrag)
			{
				MouseDown(Input.mousePosition );
			}
		}
		if( Input.GetKeyUp(KeyCode.Mouse0) )
		{
			if(!MouseDrag.isDrag)
			{
				MouseUp(Input.mousePosition );
			}
		}



		if( Input.GetKeyUp(KeyCode.P) )
		{
			TotalCoins.staticInstance.AddCoins(999999);
		}
		if( Input.GetKeyUp(KeyCode.Q) )
		{
			TotalCoins.staticInstance.ClearCoins();
		}
		if(!MouseDrag.isMousePressed)
		{
			foreach(Renderer r in buttonRenders )
			{
				if(r)
				{
					r.material.mainTexture = buttonTexture[0];
				}
			}
		}
	}
	
	
	
	void MouseUp(Vector3 a )
	{
		foreach(Renderer r in buttonRenders )
		{
			if(r)
			{
			r.material.mainTexture = buttonTexture[0];
			}
		}
		Ray ray = uiCamera.ScreenPointToRay(a);
		
		if (Physics.Raycast(ray, out hit, 500))
		{

			switch(hit.collider.name)
			{
			case "next":
				if(carPlatform.transform.position.y > 11f)
				{				
					ShowroomPlatformRotate.NextOrPrevious = "Next";

					wheelsParentText.color = new Color32(0,0,0,255);
					paintParentText.color = new Color32(0,0,0,255);


					ShowroomPlatformRotate.goDown = true; //immerse the platform with the car
					//showNextcar();
					previousBtn.SetActive(true);
				
				}
				break;
			case "previous":
				if(carPlatform.transform.position.y > 11f)
				{
					ShowroomPlatformRotate.NextOrPrevious = "Previous";

					wheelsParentText.color = new Color32(0,0,0,255);
					paintParentText.color = new Color32(0,0,0,255);

					ShowroomPlatformRotate.goDown = true;
					//showPreviouscar();
					nextBtn.SetActive(true);				
				}
				break;
				
			case "play":
                LoadingProgress.SetActive(true);
                    mainCamera.enabled = false;
                    //loadingLevelObj.SetActive(true);
                gameObject.SetActive(false);
                showroomObj.SetActive(false);

                    break;
			case "buycar" :

				purchasecars();
				break;

			case "back" :

                Application.LoadLevel("NewMainMenu");
				gameObject.SetActive(false);
				break;
			case "reset" :
				//PlayerPrefs.DeleteAll();
//				SetupAllCarsForTheFirstTime();
				showcarINFO();
				break;
			case "upgradeSpeed" :



				int speedValue = PlayerPrefs.GetInt("car"+ carSelection.carIndex +"Speed", 15);
				speedValue = speedValue * speedValue/25;

				int speedPrice = (int)(500 * Mathf.Ceil(speedValue * 60 / 500));

				if(TotalCoins.staticInstance.totalCoins >= speedPrice)
				{
					buyPopUP.transactionType = buyPopUP.UpgradeTransaction;
					buyPopUP.upgradeName = "Speed";
					buyPopUP.transactionCost = speedPrice;
					buyPopUpObj.SetActive(true);

				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}

				break;
			case "upgradeHandling" :


				int handlingValue = PlayerPrefs.GetInt("car"+ carSelection.carIndex +"Handling", 15);
				if(handlingValue > 20)
				{
					handlingValue = handlingValue * handlingValue/25;
				}

				int handlingPrice = (int)(500 * Mathf.Ceil(handlingValue * 60 / 500));

				if(TotalCoins.staticInstance.totalCoins >= handlingPrice)
				{
					buyPopUP.transactionType = buyPopUP.UpgradeTransaction;
					buyPopUP.upgradeName = "Handling";


					buyPopUP.transactionCost = handlingPrice;
					buyPopUpObj.SetActive(true);
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "upgradeBrakes" :

				int brakesValue = PlayerPrefs.GetInt("car"+ carSelection.carIndex +"Brakes", 15);
				if(brakesValue > 20)
				{
					brakesValue = brakesValue * brakesValue/25;
				}
				int brakesPrice = (int)(500 * Mathf.Ceil(brakesValue * 60 / 500));
				
				if(TotalCoins.staticInstance.totalCoins >= brakesPrice)
				{

				buyPopUP.transactionType = buyPopUP.UpgradeTransaction;
				buyPopUP.upgradeName = "Brakes";												
				buyPopUP.transactionCost = brakesPrice;
				buyPopUpObj.SetActive(true);
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "paint" :
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				TogglePaintParent();
				showcarINFO();
				break;
			case "colorBlue" :

				//the value stired in Player Pref which tells us if the color requested is unlocked or not
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Blue", 0) == 1)
				{
					UpdateCarColor("Blue");
					showcarINFO();
				}
				//if the color is locked, but the player has more than enough cash, prompt him to buy it
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					//set the transaction type, so in buyPopUP class we know what the player wants to buy
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Blue";
					buyPopUpObj.SetActive(true);

				}
				//if the color is locked, and the player has no cash for it
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "colorRed" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Red", 0) == 1)
				{
					UpdateCarColor("Red");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Red";
					buyPopUpObj.SetActive(true);

					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "colorYellow" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Yellow", 0) == 1)
				{
					UpdateCarColor("Yellow");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Yellow";
					buyPopUpObj.SetActive(true);

					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "colorPurple" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Purple", 0) == 1)
				{
					UpdateCarColor("Purple");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{

					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Purple";
					buyPopUpObj.SetActive(true);
				
					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}

				break;
			case "colorGreen" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Green", 0) == 1)
				{
					UpdateCarColor("Green");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Green";
					buyPopUpObj.SetActive(true);
				
					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "colorBlack" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Black", 0) == 1)
				{
					UpdateCarColor("Black");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Black";
					buyPopUpObj.SetActive(true);

					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "colorWhite" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_White", 0) == 1)
				{
					UpdateCarColor("White");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "White";
					buyPopUpObj.SetActive(true);
				
					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "colorOrange" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Orange", 0) == 1)
				{
					UpdateCarColor("Orange");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Orange";
					buyPopUpObj.SetActive(true);

					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;
			case "colorPink" :
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Pink", 0) == 1)
				{
					UpdateCarColor("Pink");
					showcarINFO();
				}
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{   
					buyPopUP.transactionType = buyPopUP.PaintTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.paintName = "Pink";
					buyPopUpObj.SetActive(true);
				
					
				}
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				break;

			case "wheels" :
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				ToggleWheelParent();
				showcarINFO();
				break;

			case "button_WS_1" :




				wheelSetName = carMeshObjs[carIndex].transform.name + "_WS_1";
				UpdateCarWheels(wheelSetName,"button_WS_1");






				showcarINFO();
				break;
			
			case "button_WS_2" :
				//if the wheels are unlocked, select them
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_2", 0) == 1)
				{
					wheelSetName = carMeshObjs[carIndex].transform.name + "_WS_2";
					UpdateCarWheels(wheelSetName,"button_WS_2");			

					
					showcarINFO();
				}
				//else, prompt for purchase if player has enough cash
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.WheelsTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.wheelsName = "_WS_2";
					buyPopUP.wheelButtonName = "button_WS_2";
					buyPopUpObj.SetActive(true);					
					
				}
				//else, offer cash
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
			


				break;

			case "button_WS_3" :


				//if the wheels are unlocked, select them
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_3", 0) == 1)
				{
					wheelSetName = carMeshObjs[carIndex].transform.name + "_WS_3";
					UpdateCarWheels(wheelSetName,"button_WS_3");			
					
					
					showcarINFO();
				}
				//else, prompt for purchase if player has enough cash
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.WheelsTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.wheelsName = "_WS_3";
					buyPopUP.wheelButtonName = "button_WS_3";
					buyPopUpObj.SetActive(true);					
					
				}
				//else, offer cash
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}

				break;

			case "button_WS_4" :


				//if the wheels are unlocked, select them
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_4", 0) == 1)
				{
					wheelSetName = carMeshObjs[carIndex].transform.name + "_WS_4";
					UpdateCarWheels(wheelSetName,"button_WS_4");			
					
					
					showcarINFO();
				}
				//else, prompt for purchase if player has enough cash
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.WheelsTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.wheelsName = "_WS_4";
					buyPopUP.wheelButtonName = "button_WS_4";
					buyPopUpObj.SetActive(true);					
					
				}
				//else, offer cash
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}

				break;

			case "button_WS_5" :


				//if the wheels are unlocked, select them
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_5", 0) == 1)
				{
					wheelSetName = carMeshObjs[carIndex].transform.name + "_WS_5";
					UpdateCarWheels(wheelSetName,"button_WS_5");			
					
					
					showcarINFO();
				}
				//else, prompt for purchase if player has enough cash
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.WheelsTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.wheelsName = "_WS_5";
					buyPopUP.wheelButtonName = "button_WS_5";
					buyPopUpObj.SetActive(true);					
					
				}
				//else, offer cash
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}

				break;


			case "button_WS_6" :
				
				
				//if the wheels are unlocked, select them
				if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_6", 0) == 1)
				{
					wheelSetName = carMeshObjs[carIndex].transform.name + "_WS_6";
					UpdateCarWheels(wheelSetName,"button_WS_6");			
					
					
					showcarINFO();
				}
				//else, prompt for purchase if player has enough cash
				else if(TotalCoins.staticInstance.totalCoins >= 2000)
				{
					buyPopUP.transactionType = buyPopUP.WheelsTransaction;
					buyPopUP.transactionCost = 2000;
					buyPopUP.wheelsName = "_WS_6";
					buyPopUP.wheelButtonName = "button_WS_6";
					buyPopUpObj.SetActive(true);					
					
				}
				//else, offer cash
				else
				{
					InAPPMenu.SetActive(true);
					gameObject.SetActive(false);
				}
				
				break;

			

				
			}

		}
		
	}
	void MouseDown(Vector3 a )
	{



		Ray ray = uiCamera.ScreenPointToRay(a);
		
		if (Physics.Raycast(ray, out hit, 500))
		{
			SoundController.Static.PlayButtonClickSound();
//			Debug.Log("mouse hit on "+ hit.collider.name);

			switch(hit.collider.name)
			{

			case "next":
				//buttonRenders[0].material.mainTexture  = buttonTexture[1];
				break;
			case "previous":
				//buttonRenders[1].material.mainTexture  = buttonTexture[1];
				break;
				
			case "play":
				buttonRenders[2].material.mainTexture  = buttonTexture[1];
				break;
			case "buycar" :
				buttonRenders[3].material.mainTexture  = buttonTexture[1];
				break;
//			case "wwq":
//				buttonRenders[4].material.mainTexture  = buttonTexture[1];
//				break;
			case "back" :
				buttonRenders[4].material.mainTexture  = buttonTexture[1];
				break;
			case "reset" :
				buttonRenders[5].material.mainTexture  = buttonTexture[1];
				break;
			case "upgradeSpeed" :
				//buttonRenders[6].material.mainTexture  = buttonTexture[1];
				break;
			case "upgradeHandling" :
				//buttonRenders[7].material.mainTexture  = buttonTexture[1];
				break;
			case "upgradeBrakes" :
				//buttonRenders[8].material.mainTexture  = buttonTexture[1];
				break;
			

			}
			
			
		}
		
	}

	//check all paints, see which one is locked, and hide price tags for the unlocked ones
	void ShowHidePaintPriceTags()
	{
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Red", 0) == 1)
		{
			paintPriceTagRed.SetActive(false); 
		}
		else
		{
			paintPriceTagRed.SetActive(true); 
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Yellow", 0) == 1)
		{
			paintPriceTagYellow.SetActive(false); 
		}
		else
		{
			paintPriceTagYellow.SetActive(true); 
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Purple", 0) == 1)
		{
			paintPriceTagPurple.SetActive(false); 
		}
		else
		{
			paintPriceTagPurple.SetActive(true); 
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Green", 0) == 1)
		{
			paintPriceTagGreen.SetActive(false); 
		}
		else
		{
			paintPriceTagGreen.SetActive(true); 
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Black", 0) == 1)
		{
			paintPriceTagBlack.SetActive(false); 
		}
		else
		{
			paintPriceTagBlack.SetActive(true); 
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_White", 0) == 1)
		{
			paintPriceTagWhite.SetActive(false); 
		}
		else
		{
			paintPriceTagWhite.SetActive(true); 
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Pink", 0) == 1)
		{
			paintPriceTagPink.SetActive(false); 
		}
		else
		{
			paintPriceTagPink.SetActive(true); 
		}

		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "Colors/" +  carMeshObjs[carIndex].transform.name + "_Blue", 0) == 1)
		{
			paintPriceTagBlue.SetActive(false); 
		}
		else
		{
			paintPriceTagBlue.SetActive(true); 
		}

	}


    
	//show/hide the paint menu
	void TogglePaintParent()
	{
		if(paintParent.activeSelf)
		{
			paintParentText.color = new Color32(0,0,0,255);
			paintParent.SetActive(false);
			paintParentRenderer.material.mainTexture  = buttonTexture[0];
		}
		else
		{
			//check all paints, see which one is locked, and hide price tags for the unlocked ones
			ShowHidePaintPriceTags();
			wheelsParentText.color = new Color32(0,0,0,255);
			paintParentText.color = new Color32(115,201,0,255);
			paintParent.SetActive(true);
			paintParentRenderer.material.mainTexture  = buttonTexture[1];
		}
	}



	void ShowHideWheelsPriceTags()
	{

		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_2", 0) == 1)
		{
			wheelPriceTag_WS_2.SetActive(false);
		}
		else
		{
			wheelPriceTag_WS_2.SetActive(true);
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_3", 0) == 1)
		{
			wheelPriceTag_WS_3.SetActive(false);
		}
		else
		{
			wheelPriceTag_WS_3.SetActive(true);
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_4", 0) == 1)
		{
			wheelPriceTag_WS_4.SetActive(false);
		}
		else
		{
			wheelPriceTag_WS_4.SetActive(true);
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_5", 0) == 1)
		{
			wheelPriceTag_WS_5.SetActive(false);
		}
		else
		{
			wheelPriceTag_WS_5.SetActive(true);
		}
		if(PlayerPrefs.GetInt(carMeshObjs[carIndex].transform.name + "_WS_6", 0) == 1)
		{
			wheelPriceTag_WS_6.SetActive(false);
		}
		else
		{
			wheelPriceTag_WS_6.SetActive(true);
		}


	}




	//show/hide the wheels menu
	public void ToggleWheelParent()
	{
		if(wheelsParent.activeSelf)
		{
			wheelsParentText.color = new Color32(0,0,0,255);
			wheelsParent.SetActive(false);
			wheelParentRenderer.material.mainTexture  = buttonTexture[0];
		}
		else
		{
			paintParentText.color = new Color32(0,0,0,255);
			wheelsParentText.color = new Color32(115,201,0,255);
			wheelsParent.SetActive(true);
			wheelParentRenderer.material.mainTexture  = buttonTexture[1];
			ShowHideWheelsPriceTags();
		}
	}

	//show the next car
	public void showNextcar()
	{

		if(carPlatform.transform.position.y < 10.3)	
		{
		
		//carPlatform.transform.rotation = 

			carIndex++;
			if( carIndex > carMeshObjs.Length-1 ) carIndex=0;
			for( int carCount=0 ; carCount<= carMeshObjs.Length-1; carCount ++ )
			{
				carMeshObjs[carCount].SetActive(false);
				
			}
			carMeshObjs[carIndex].SetActive(true);
			showcarINFO();
			ShowroomPlatformRotate.goDown = false;
			ShowroomPlatformRotate.ComeUp = true;
		}
	}

	//show the previous car
	public void showPreviouscar()
	{			

		if(carPlatform.transform.position.y < 10.3)	
		{
			carIndex--;
			if( carIndex < 0 ) carIndex=carMeshObjs.Length-1;
			for( int carCount=0 ; carCount<= carMeshObjs.Length-1; carCount ++ )
			{
				carMeshObjs[carCount].SetActive(false);
				
			}
			carMeshObjs[carIndex].SetActive(true);
			showcarINFO();
			ShowroomPlatformRotate.goDown = false;
			ShowroomPlatformRotate.ComeUp = true;
		}
	}
	void OnEnable()
	{

		if(carIndex ==0 ) return;
		if( PlayerPrefs.GetInt("iscar"+carIndex+"Purchased",0) == 1 )
		{
			playButton.SetActive(true);
			buyButton.SetActive(false);
		}
		else{
			buyButton.SetActive(true);
			playButton.SetActive(false);
		}
		 
		 
	}

	//display info about the current car
	public void showcarINFO()
	{

		wheelSetName = PlayerPrefs.GetString("car" + carIndex + "Wheels");
		carTransformName = carMeshObjs[carIndex].transform.name;
		//Debug.Log ("selectedCarIndex" + carIndex);
		PlayerPrefs.SetInt ("selectedCarIndex", carIndex);

		string carBodyColorMat = PlayerPrefs.GetString ("Car" + carIndex + "Color"); //get the path to load the selected car body material, the path was set in menu, in carSelection.cs script

			
			Transform carTransform = carMeshObjs[carIndex].transform;
			foreach(Transform child in carTransform)
			{
				foreach(Transform child2 in child)
				{
					if(child2.name == "body")
					{
						Renderer rnd = child2.GetComponent<Renderer>();						

						//instantiate the new material from resources
					Material newCarBodyMaterial = Resources.Load ("CarColors/" + carMeshObjs[carIndex].transform.name + "Colors/" +  carBodyColorMat) as Material;	
//						Debug.Log (carMeshObjs[carIndex].transform.name + "Colors/" +  carBodyColorMat);
						//set the new material for the car body, so it changes color
						//rnd.material = newCarBodyMaterial;

					Material[] mats = rnd.materials;

					//if the material name has the following format: CarName_ + color - it must be updated to change color. 
					//This is our naming convention, since all car body materials are names like this: CarName_ColorName
					//the SUV has 3 materials that need updating, in order to change its color, the rest of the cars, just 1
					for(int i=0; i < mats.Length; i++)
					{
						if (mats[i].name.Contains(carMeshObjs[carIndex].transform.name + "_")) //if the material name has the following format: CarName_ + color - it must be updated to change color
						{
							mats[i] = newCarBodyMaterial;
						}
					}
					rnd.materials = mats;
						
					}
					
				}
			}


			






	
	
		//hide the Next button if the current car is the last on the list, and hide the previous button if the current car is the first on the list
		if (carIndex == carMeshObjs.Length - 1) 
		{
			nextBtn.SetActive(false);			
		}
		if(carIndex == 0)
		{
			previousBtn.SetActive(false);		
		}








		switch(carIndex)
		{
		case 0:
			//headingText.text="MODEL : Hunter ";

			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;

			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;

			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;



			//carPriceDisplayText.text = " OWNED ";

			//if the car is owned, show everything that needs showing, like buttons to upgrade, etc
			if(PlayerPrefs.GetInt("iscar0Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}

			//if the car is not owned, hide everything that needs hiding, and show the price tag and buy button
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "Price : 100$ ";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];

				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}


//
//			priceTag.SetActive(false);
//			playButton.SetActive(true);
//			buyButton.SetActive(false);
			break;
		case 1:
			//headingText.text="MODEL : Eagle";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;

			priceTag.SetActive(true);

			if(PlayerPrefs.GetInt("iscar1Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "10 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;
		case 2:
			//headingText.text="MODEL : Racer ";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar2Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "15 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;
		case 3:
			//headingText.text="MODEL : Racer ";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;

			
			if(PlayerPrefs.GetInt("iscar3Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "22 500$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;
		case 4:
			//headingText.text="MODEL : F1-Car";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar4Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "33 500$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;	

		case 5:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar5Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "45 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 6:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar6Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "60 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 7:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar7Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "75 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 8:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar8Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "100 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 9:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar9Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "125 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 10:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar10Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "135 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 11:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar11Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "150 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 12:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;

			
			if(PlayerPrefs.GetInt("iscar12Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "185 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

		case 13:
			//headingText.text="MODEL : Truck";
			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
			carSpeedDisplayTextOutline.text = carSpeedDisplayText.text;
			carSpeedDisplayTextBevel.text = carSpeedDisplayText.text;
			
			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";	
			carHandlingDisplayTextOutline.text = carHandlingDisplayText.text;	
			carHandlingDisplayTextBevel.text = carHandlingDisplayText.text;
			
			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
			carBrakesDisplayTextOutline.text = carBrakesDisplayText.text;
			carBrakesDisplayTextBevel.text = carBrakesDisplayText.text;


			
			if(PlayerPrefs.GetInt("iscar13Purchased",0) == 1 )
			{
				priceTag.SetActive(false);
				playButton.SetActive(true);
				buyButton.SetActive(false);
				paintButton.SetActive(true);
				wheelsButton.SetActive(true);
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(true);
				}
			}
			else{
				buyButton.SetActive(true);
				playButton.SetActive(false);
				priceTag.SetActive(true);
				paintButton.SetActive(false);
				wheelsButton.SetActive(false);
				carPriceDisplayText.text = "225 000$";
				paintParent.SetActive(false);
				paintParentRenderer.material.mainTexture  = buttonTexture[0];
				wheelsParent.SetActive(false);
				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
				foreach (GameObject upgradeButton in upgradeCarButtons)
				{
					upgradeButton.SetActive(false);
				}
			}
			break;

//		case 14:
//			//headingText.text="MODEL : Truck";
//			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
//			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";			
//			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
//
//			
//			if(PlayerPrefs.GetInt("iscar14Purchased",0) == 1 )
//			{
//				priceTag.SetActive(false);
//				playButton.SetActive(true);
//				buyButton.SetActive(false);
//				paintButton.SetActive(true);
//				wheelsButton.SetActive(true);
//				foreach (GameObject upgradeButton in upgradeCarButtons)
//				{
//					upgradeButton.SetActive(true);
//				}
//			}
//			else{
//				buyButton.SetActive(true);
//				playButton.SetActive(false);
//				priceTag.SetActive(true);
//				paintButton.SetActive(false);
//				wheelsButton.SetActive(false);
//				carPriceDisplayText.text = "Price  :     100$ ";
//				paintParent.SetActive(false);
//				paintParentRenderer.material.mainTexture  = buttonTexture[0];
//				wheelsParent.SetActive(false);
//				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
//				foreach (GameObject upgradeButton in upgradeCarButtons)
//				{
//					upgradeButton.SetActive(false);
//				}
//			}
//			break;
//
//		case 15:
//			//headingText.text="MODEL : Truck";
//			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
//			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";			
//			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
//			
//			
//			if(PlayerPrefs.GetInt("iscar15Purchased",0) == 1 )
//			{
//				priceTag.SetActive(false);
//				playButton.SetActive(true);
//				buyButton.SetActive(false);
//				paintButton.SetActive(true);
//				wheelsButton.SetActive(true);
//				foreach (GameObject upgradeButton in upgradeCarButtons)
//				{
//					upgradeButton.SetActive(true);
//				}
//			}
//			else{
//				buyButton.SetActive(true);
//				playButton.SetActive(false);
//				priceTag.SetActive(true);
//				paintButton.SetActive(false);
//				wheelsButton.SetActive(false);
//				carPriceDisplayText.text = "Price  :     100$ ";
//				paintParent.SetActive(false);
//				paintParentRenderer.material.mainTexture  = buttonTexture[0];
//				wheelsParent.SetActive(false);
//				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
//				foreach (GameObject upgradeButton in upgradeCarButtons)
//				{
//					upgradeButton.SetActive(false);
//				}
//			}
//			break;
//		case 16:
//			//headingText.text="MODEL : Truck";
//			carSpeedDisplayText.text ="Speed: "+ PlayerPrefs.GetInt ("car" + carIndex +"Speed", 0) + "%";
//			carHandlingDisplayText.text = "Handling: "+ PlayerPrefs.GetInt ("car"+carIndex+"Handling", 0) +"%";			
//			carBrakesDisplayText.text = "Brakes: "+ PlayerPrefs.GetInt ("car" + carIndex + "Brakes", 0) + "%";
//			
//			
//			if(PlayerPrefs.GetInt("iscar16Purchased",0) == 1 )
//			{
//				priceTag.SetActive(false);
//				playButton.SetActive(true);
//				buyButton.SetActive(false);
//				paintButton.SetActive(true);
//				wheelsButton.SetActive(true);
//				foreach (GameObject upgradeButton in upgradeCarButtons)
//				{
//					upgradeButton.SetActive(true);
//				}
//			}
//			else{
//				buyButton.SetActive(true);
//				playButton.SetActive(false);
//				priceTag.SetActive(true);
//				paintButton.SetActive(false);
//				wheelsButton.SetActive(false);
//				carPriceDisplayText.text = "Price  :     100$ ";
//				paintParent.SetActive(false);
//				paintParentRenderer.material.mainTexture  = buttonTexture[0];
//				wheelsParent.SetActive(false);
//				wheelParentRenderer.material.mainTexture  = buttonTexture[0];
//				foreach (GameObject upgradeButton in upgradeCarButtons)
//				{
//					upgradeButton.SetActive(false);
//				}
//			}
//			break;
		}

		if(PlayerPrefs.GetInt("car" + carIndex + "UpgradedSpeed", 0) >= 3)
		{
			Debug.Log ("UpgradedSpeed");
			upgradeSpeedButton.SetActive(false);
		}
		
		if(PlayerPrefs.GetInt("car" + carIndex + "UpgradedHandling", 0) >= 3)
		{
			
			upgradeHandlingButton.SetActive(false);
		}
		
		if(PlayerPrefs.GetInt("car" + carIndex + "UpgradedBrakes", 0) >= 3)
		{
			upgradeBrakesButton.SetActive(false);	
		}

	}


	//buy current car
	void purchasecars()
	{

		switch(carIndex)
		{
		case 1:

			if( TotalCoins.staticInstance.totalCoins >= 10000 )
			{
				//to set the cost in buyPopUpObjScript
				buyPopUP.transactionCost = 10000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			 
			break;
		case 2:
			if( TotalCoins.staticInstance.totalCoins >= 15000 )
			{
				buyPopUP.transactionCost = 15000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 3:
			if( TotalCoins.staticInstance.totalCoins >= 22500 )
			{
				buyPopUP.transactionCost = 22500;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 4:
			if( TotalCoins.staticInstance.totalCoins >= 33500 )
			{
				buyPopUP.transactionCost = 33500;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 5:
			if( TotalCoins.staticInstance.totalCoins >= 45000 )
			{
				buyPopUP.transactionCost = 45000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 6:
			if( TotalCoins.staticInstance.totalCoins >= 60000 )
			{
				buyPopUP.transactionCost = 60000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 7:
			if( TotalCoins.staticInstance.totalCoins >= 75000 )
			{
				buyPopUP.transactionCost = 75000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 8:
			if( TotalCoins.staticInstance.totalCoins >= 100000 )
			{
				buyPopUP.transactionCost = 100000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 9:
			if( TotalCoins.staticInstance.totalCoins >= 125000 )
			{
				buyPopUP.transactionCost = 125000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 10:
			if( TotalCoins.staticInstance.totalCoins >= 135000 )
			{
				buyPopUP.transactionCost = 135000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 11:
			if( TotalCoins.staticInstance.totalCoins >= 150000 )
			{
				buyPopUP.transactionCost = 150000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 12:
			if( TotalCoins.staticInstance.totalCoins >= 185000 )
			{
				buyPopUP.transactionCost = 185000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;
		case 13:
			if( TotalCoins.staticInstance.totalCoins >= 225000 )
			{
				buyPopUP.transactionCost = 225000;
				buyPopUP.transactionType = buyPopUP.CarTransaction;
				buyPopUpObj.SetActive(true);
				gameObject.SetActive(false);
			}
			else {
				InAPPMenu.SetActive(true);
				gameObject.SetActive(false);
			}
			
			break;

//		case 14:
//			if( TotalCoins.staticInstance.totalCoins >= 100 )
//			{
//				buyPopUP.transactionCost = 100;
//				buyPopUP.transactionType = buyPopUP.CarTransaction;
//				buyPopUpObj.SetActive(true);
//				gameObject.SetActive(false);
//			}
//			else {
//				InAPPMenu.SetActive(true);
//				gameObject.SetActive(false);
//			}
//			
//			break;
//		case 15:
//			if( TotalCoins.staticInstance.totalCoins >= 100 )
//			{
//				buyPopUP.transactionCost=100;
//				buyPopUpObj.SetActive(true);
//				gameObject.SetActive(false);
//			}
//			else {
//				InAPPMenu.SetActive(true);
//				gameObject.SetActive(false);
//			}
//			
//			break;
//		
		}

	}
}
