using UnityEngine;
using System.Collections;

public class TailLight : MonoBehaviour {

	//old version
	/*
	public float BaseRed;
	public float Intensity;
	public float Red;
	

	float ColorUnit = 1.0f / 255.0f;
	float Green;
	float Blue;

	// Use this for initialization
	void Start () {
		Red = BaseRed * ColorUnit;
		Green = 0.0f;
		Blue = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
				// A color can have in RGB a value from 0 to 255; this renders one color unit as 1/255			
			
				float ProcessedRed = BaseRed * ColorUnit;
				float ProcessedIntensity = Intensity * ColorUnit;

				if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.DownArrow)) {					
						Red = ProcessedRed + ProcessedIntensity;	
						Green = Green + 30.0f * ColorUnit;
						Blue = Blue + 20.0f * ColorUnit;
				}
				if (Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp (KeyCode.DownArrow)) {
						Red = BaseRed * ColorUnit;	
						Green = 0.0f;
						Blue = 0.0f;
				}
				GetComponent<Renderer>().material.color = new Color (Red, Green, Blue);
		}*/

	public GameObject Target;

	void Start()
	{
	
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.Space)) {
			Target.GetComponent<Renderer> ().material.SetFloat ("_Metallic", 0.0f);
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.Space)) {
			Target.GetComponent<Renderer> ().material.SetFloat ("_Metallic", 1.0f);
		} 

	}
}


