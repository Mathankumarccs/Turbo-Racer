using UnityEngine;
using System.Collections;

public class freeCoinsADpromotion : MonoBehaviour {
 
	// Use this for initialization
	public GameObject InAppPurchaseMenu,carSelectionMenu;
	public Camera uiCamera;
	void OnEnable () {
	

 
 		
 

	}
	
	void Update () {
		if( Input.GetKeyUp(KeyCode.Mouse0) )
		{
			if(!MouseDrag.isDrag)
			{
				MouseUp(Input.mousePosition );
			}
		}
		
	}
	//this static bool will give coins only once  per game session.
	public static bool alreadyGiveFreeCoins=false;
	void MouseUp(Vector3 a )
	{
		Ray ray = uiCamera.ScreenPointToRay(a);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 500))
		{
			SoundController.Static.PlayButtonClickSound();
			Debug.Log(gameObject.name + "    " + hit.collider.name);
			switch(hit.collider.name)
			{
			case "GETMORE":
				InAppPurchaseMenu.SetActive(true);
				gameObject.SetActive(false);

 //				//show in app purchases menu
				break;
			case "BACK":
				carSelectionMenu.SetActive(true);
				gameObject.SetActive(false);
				 
				break;
				
				
			}
			
		}
		
	}
	 
}
