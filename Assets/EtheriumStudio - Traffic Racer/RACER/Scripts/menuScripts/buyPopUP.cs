using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buyPopUP : MonoBehaviour {

	// Use this for initialization
	public TextMesh costText;

	public GameObject carSelectionMenu;
	public GameObject carSelectionPriceTag;	
	public GameObject[] carSelectMenuGameObjects;				//the list of UI elements that will be disabled when showing the purchase popup
		
	public List<bool> carSelectMenuActiveOrNot = new List<bool>();   // a mirror list(for the carSelectMenuGameObjects) of boolean variables to tell us which GUI elements were enabled the moment the BuyPopUp shows, so
																		//we will know what elements to enable when the player closes or confirms the thansaction


	public Camera uiCamera;

	public static int transactionCost;

	public static string transactionType;

	public static string paintName;	
	public static string wheelsName;	
	public static string wheelButtonName; //to know which button was clicked, and to scale it up as we equit the corresponding wheels
	public static string upgradeName;

	public const string CarTransaction = "carPurchase";
	public const string PaintTransaction = "paintPurchase";
	public const string WheelsTransaction = "wheelsPurchase";
	public const string UpgradeTransaction = "upgradePurchase";



	void OnEnable()
	{
		carSelection.canRenderGUI = false;
		costText.text=" "+transactionCost +" $ ?";


		carSelectMenuActiveOrNot.Clear();

		//for all the gameobjects in carSelect menu
		for(int i = 0; i< carSelectMenuGameObjects.Length; i++)
		{
			//if the object is active, store a "true" value in the mirror list
			if(carSelectMenuGameObjects[i].activeSelf)
			{
				carSelectMenuActiveOrNot.Add(true);
			}
			//else store a "false" value
			else
			{
				carSelectMenuActiveOrNot.Add(false);
			}
		}

		//disable all the gameobjects in carSelect menu
		foreach(GameObject child in carSelectMenuGameObjects)
		{
			child.SetActive(false);
		}
   

	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyUp(KeyCode.Mouse0) )
		{
			if(!MouseDrag.isDrag)
			{
				MouseUp(Input.mousePosition);
			}
		}

	}



	void ShowRatePopup()
	{

		string lastRatePopUp_DateTime_String = PlayerPrefs.GetString("RatePopupDateTime");


		if(PlayerPrefs.GetInt("CanShowRatePopup",1) == 1)
		{
			if(lastRatePopUp_DateTime_String.Length > 1)
			{
				System.DateTime lastRatePopupDate_DateTime = new System.DateTime();
				lastRatePopupDate_DateTime = System.Convert.ToDateTime(lastRatePopUp_DateTime_String);

				System.TimeSpan lastPopup_Show = new System.TimeSpan();

				//see how long it passed since last showing the rate-me popup and now
				lastPopup_Show = System.DateTime.Now - lastRatePopupDate_DateTime;


				//how many hours to pass, ultill asking the player to rate the game, again, provided he chose to rate it later, and not Never
				if (lastPopup_Show.Hours > 24)
				{
					//show rate popup - instantiate the rating popup here
					// You need to implement a plugin in order to show a native Android or iOS popup in Unity. In this version, the project is not equipped to show
					//native popups, for Android, or iOS
					
				}
			}
			//we are showing the popup for the first time, so we dont need to know when we showed it before to calculate the timespan
			else
			{
				//show rate popup - instantiate the rating popup here
				// You need to implement a plugin in order to show a native Android or iOS popup in Unity.In this version, the project is not equipped to show
				//native popups, for Android, or iOS
			}
		}




	}

	void MouseUp(Vector3 a )
	{
		 Ray ray = uiCamera.ScreenPointToRay(a);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 500))
		{
			Debug.Log(gameObject.name + "    " + hit.collider.name);
			switch(hit.collider.name)
			{
			case "YES":
				 
				switch(transactionType)
				{
					
					case CarTransaction:					
						PlayerPrefs.SetInt("iscar"+carSelection.carIndex+"Purchased",1); // to save the car lock status

						Debug.Log("PURCHASED CAR");


						Invoke("ShowRatePopup", 2);				

						
					break;

					case PaintTransaction:
						//unlock the chosen color(the player was already verified in carSelection.cs script, has enough cash to purchase and has confimed the transaction)
						PlayerPrefs.SetInt(carSelection.carTransformName + "Colors/" +  carSelection.carTransformName + "_" + paintName, 1);
						carSelection.Static.UpdateCarColor(paintName);	

						Debug.Log("PURCHASED PAINT");
					Invoke("ShowRatePopup", 2);
					break;

					case WheelsTransaction:
						PlayerPrefs.SetInt(carSelection.carTransformName + wheelsName, 1);
						carSelection.Static.UpdateCarWheels(carSelection.carTransformName + wheelsName,wheelButtonName);

						Debug.Log("PURCHASED WHEELS");
					Invoke("ShowRatePopup", 2);
					break;

					case UpgradeTransaction:
					UpdateCarStats(upgradeName);

						Debug.Log("PURCHASED UPGRADE");
					Invoke("ShowRatePopup", 2);
					break;

				
					
				}

					//selectively enable carSelect menu objects, based on the boolean variables stored in the mirror list
					for(int i = 0; i< carSelectMenuActiveOrNot.Count; i++)
					{						
						if(carSelectMenuActiveOrNot[i] == true)
						{
							carSelectMenuGameObjects[i].SetActive(true);
						}
					}

					carSelection.Static.showcarINFO();
					TotalCoins.staticInstance.deductCoins(transactionCost);		
					ClearVariables();
					carSelectionMenu.SetActive(true);
					gameObject.SetActive(false);
					carSelection.canRenderGUI = true;	
					break;
			case "NO":

				//selectively enable carSelect menu objects, based on the boolean variables stored in the mirror list
				for(int i = 0; i< carSelectMenuActiveOrNot.Count; i++)
				{					
					if(carSelectMenuActiveOrNot[i] == true)
					{
						carSelectMenuGameObjects[i].SetActive(true);
					}
				}

				ClearVariables();
				carSelectionMenu.SetActive(true);
				gameObject.SetActive(false);
				carSelection.canRenderGUI = true;
				break;
			 
				
			}
			
		}
		
	}

	void ClearVariables()
	{
		transactionCost = 0;
		transactionType = "";
		paintName = "";
		wheelsName = "";
		upgradeName = "";
	}




	/// <summary>
	/// Updates the car stats.
	/// </summary>
	/// <param name="attributeName">What attribute do we want to upgrade.</param>
	void UpdateCarStats(string attributeName)
	{
		
		if(attributeName == "Speed")
		{
			
		
			//get the current Speed value from player pref
			int value = PlayerPrefs.GetInt("car"+ carSelection.carIndex +"Speed", 25);
			
			
			//add 5 points to acceleration
			PlayerPrefs.SetInt ("car"+ carSelection.carIndex +"Speed", value + 5);

			//increment the number of how many times the speed was upgraded for the current car, so the player wont be able to upgrade it more than 3 times for 1 car
			int timesUpgradedSpeed = PlayerPrefs.GetInt("car" + carSelection.carIndex + "UpgradedSpeed", 0);
			PlayerPrefs.SetInt("car" + carSelection.carIndex + "UpgradedSpeed", timesUpgradedSpeed +1);
		}
		else if(attributeName == "Handling")
		{

			//get the current Handling value from player pref
			int value = PlayerPrefs.GetInt("car"+ carSelection.carIndex +"Handling", 15);
			//add 5 points to handling
			PlayerPrefs.SetInt ("car"+ carSelection.carIndex +"Handling", value + 5);

			int timesUpgradedHandling = PlayerPrefs.GetInt("car" + carSelection.carIndex + "UpgradedHandling", 0);
			PlayerPrefs.SetInt("car" + carSelection.carIndex + "UpgradedHandling", timesUpgradedHandling +1);
		}
		else if(attributeName == "Brakes")
		{

			//get the current Brakes value from player pref
			int value = PlayerPrefs.GetInt("car"+ carSelection.carIndex +"Brakes", 15);
			//add 5 points to brakes
			PlayerPrefs.SetInt ("car"+ carSelection.carIndex +"Brakes", value + 5);

			int timesUpgradedBrakes = PlayerPrefs.GetInt("car" + carSelection.carIndex + "UpgradedBrakes", 0);
			PlayerPrefs.SetInt("car" + carSelection.carIndex + "UpgradedBrakes", timesUpgradedBrakes +1);
		}
		 carSelection.Static.showcarINFO ();
		
	}
}
