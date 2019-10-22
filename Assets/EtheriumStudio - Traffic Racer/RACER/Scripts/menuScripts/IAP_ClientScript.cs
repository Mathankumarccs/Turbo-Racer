using UnityEngine;
using System.Collections;

public class IAP_ClientScript : MonoBehaviour {

	public Camera uiCamera;
	public RaycastHit hit;
	public GameObject inAppPurchasesObj, carSelectObj, FreeCoinsObj, LevelSelectObj,MenuObj,MessagePopupObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Mouse0) )
		{
			if(!MouseDrag.isDrag)
			{
				MouseDown(Input.mousePosition);
			}
		}
	}



	void MouseDown(Vector3 a)
	{
		
		
		
		Ray ray = uiCamera.ScreenPointToRay(a);
		
		if (Physics.Raycast(ray, out hit, 500))
		{
			
			
			
			switch(hit.collider.name)
			{
				
			case "totalCoins":

				break;
			}
		}
	}
}
