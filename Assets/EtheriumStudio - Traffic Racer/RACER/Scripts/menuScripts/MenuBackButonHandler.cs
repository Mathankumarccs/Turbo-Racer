using UnityEngine;

public class MenuBackButonHandler : MonoBehaviour {
	public GameObject buyPopUpObj, menuObj, levelSelectObj, carSelectObj;
	// Use this for initialization
	void Start () {
		Debug.Log( "BackButtonHandler.cs is Attached to " + gameObject.name );
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyUp(KeyCode.Escape) )
		{
			if(!buyPopUpObj.activeSelf)
			{
				if(carSelectObj.activeSelf)
				{
					menuObj.SetActive(true);
					carSelectObj.SetActive(false);
				}
				else if(levelSelectObj.activeSelf)
				{
					carSelectObj.SetActive(true);
					carSelection.canRenderGUI = true;
					levelSelectObj.SetActive(false);
				}


			}
		}
	}
}
