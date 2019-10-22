using UnityEngine;
using System.Collections;

public class levelSelection : MonoBehaviour {

	// Use this for initialization
	public Renderer[] buttonRenders;
	public Texture[] buttonTexture;
	public RaycastHit hit;
	public Camera uiCamera;
	public static string levelName;
	public GameObject carSelection;
	public GameObject LadingSpin;
    public GameObject showroomObj;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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

	}


	void MouseUp(Vector3 a )
	{
		foreach(Renderer r in buttonRenders )
		{
			r.material.mainTexture = buttonTexture[0];
		}
		Ray ray = uiCamera.ScreenPointToRay(a);
		
		if (Physics.Raycast(ray, out hit, 500))
		{
			
			switch(hit.collider.name)
			{
			case "back":
				//iTween.PunchScale(hit.collider.gameObject,new Vector3(1,1,0),0.3f);

				gameObject.SetActive(false);
				carSelection.SetActive(true);
                showroomObj.SetActive(true);
				break;
			
			case "highway":

				levelName = "highway";
				LadingSpin.SetActive(true);
				gameObject.SetActive(false);				 
				break;
			case "city":
				//yield return new WaitForSeconds (1);
				levelName = "City";
				LadingSpin.SetActive(true);
				gameObject.SetActive(false);
				break;

			case "desert":
				//yield return new WaitForSeconds (1);
				levelName = "Desert2";
				LadingSpin.SetActive(true);
				gameObject.SetActive(false);
			
				break;

			case "japan":

				levelName = "Desert3";
				LadingSpin.SetActive(true);
				gameObject.SetActive(false);
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
			 
			switch(hit.collider.name)
			{
			case "back":
				buttonRenders[0].material.mainTexture  = buttonTexture[1];
				 
				break;
			case "dayLight":
				iTween.PunchScale(hit.collider.gameObject,new Vector3(1,1,0),0.3f);
				break;
			case "Sunny":
				iTween.PunchScale(hit.collider.gameObject,new Vector3(1,1,0),0.3f);
				
				break;
			 
				
			}
			
			
		}
		
	}
}
