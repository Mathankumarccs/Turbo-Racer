using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {
	public static bool isDrag;
	public static bool isMousePressed;
	private float counter;
	private Vector3 mouseUpPosition;
	private Vector3 mouseDownPosition;
	private float xMouseMoved;
	private float yMouseMoved;

	// Use this for initialization
	void Start () {
		isDrag = false;
	}



	// Update is called once per frame
	void OnGUI () {


		if(Application.isMobilePlatform)
		{
			if(Input.touchCount > 1)
			{
				isDrag = false;
			}
		}
		//Debug.Log (isDrag);
		if (Event.current.type == EventType.MouseDown) 
		{


			if(Application.isMobilePlatform)
			{			
				mouseDownPosition = Input.touches[0].position;
			}
			else
			{
				mouseDownPosition = Input.mousePosition;
			}
			isMousePressed = true;

		}
		else if(Event.current.type == EventType.MouseDrag)
		{


			CancelInvoke("StartCounting");
			counter = 0f;
			isDrag=true;
		}
		else if(Event.current.type == EventType.MouseUp)
		{


			isMousePressed = false;
			if(Application.isMobilePlatform)
			{				
				mouseUpPosition = Input.touches[0].position;
			}
			else
			{
				mouseUpPosition = Input.mousePosition;
			}


			xMouseMoved = Mathf.Abs(mouseUpPosition.x - mouseDownPosition.x);
			yMouseMoved = Mathf.Abs(mouseUpPosition.y - mouseDownPosition.y);
			//if the makes a small drag, consider it click
			if((xMouseMoved < 5f) || (yMouseMoved < 5f))
			{
				isDrag = false;
			}
			else
			{
				InvokeRepeating("StartCounting", 0.02f, 0.02f);
			}

			//isDrag = false;

//			Debug.Log (xMouseMoved + "  //  " + yMouseMoved);


		}
		


	}
	//we need this method so that Raycast won't be activated if we release the mouse button or the swipe, only on touches, not on drag
	void StartCounting()
	{

		counter += Time.deltaTime;
		if(counter > 0.01f)
		{
			isDrag = false;
		}
	}
}
