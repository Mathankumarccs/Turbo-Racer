using UnityEngine;
using System.Collections;

public class LightChange : MonoBehaviour {


	public bool LightActivator;
	public GameObject LightLeft;
	public GameObject LightRight;
	GameObject Target;
	GameObject Target2;

	public bool Red_Activator;
	public bool Green_Activator;
	public bool Blue_Activator;

	public float BaseRed;
	public float BaseGreen;
	public float BaseBlue;

	public float OscilationFrequency;

	public float RedOscilator;
	public float GreenOscilator;
	public float BlueOscilator;

	public float PrimitiveRed;
	public float PrimitiveGreen;
	public float PrimitiveBlue;

	public float Red;
	public float Green;
	public float Blue;

	public float RedWave;
	public float GreenWave;
	public float BlueWave;

	public Color ObjectColor;

	public bool LeftLane;
	public bool RightLane;

	//the original object scale is retained
	public Vector3 OriginalScale;
	public float MultiplierPace;

	// Use this for initialization
	void Start () {


		if (LightLeft) {
			OriginalScale = LightLeft.gameObject.transform.localScale;

			if(Application.loadedLevelName != "CarSelectionMenu")
			{
				LightLeft.gameObject.SetActive (false);
			}
		}
		if (LightRight) {
			LightRight.gameObject.SetActive (false);			
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (LeftLane) {
			LightLeft.gameObject.SetActive (true);
						Target = LightLeft;
						LightChange T = Target.GetComponent<LightChange> ();
						
				}
	
		if (RightLane) {
						Target = LightRight;
			LightRight.gameObject.SetActive (true);
						LightChange T = Target.GetComponent<LightChange> ();						
				}


		if (LeftLane && RightLane) {
			LightLeft.gameObject.SetActive (true);
			LightRight.gameObject.SetActive (true);
			Target = LightLeft;
			Target2 = LightRight;
		}
		

				// A color can have in RGB a value from 0 to 255; this renders one color unit as 1/255
				float ColorUnit = 1.0f / 255.0f;
				//the speed with which the sine is done
				
					
				//if a color has been activated we will take the values of the based colors and convert them to color units
				//then using a Sine function we will oscilate that value between the interval give by ColoOscilator
				//using the OscilationFrequency in reference with Time (from -1.0f to 1.0f) 
				//if there is no activator we will use the value of the PrimitiveRed which is by default 0 but can be changed

		if (LightActivator) {
			if (Target) {
				if (Red_Activator) {						
					float ProcessedRed = (BaseRed + 60.0f) * ColorUnit;
					RedOscilator = 0.0f;
					RedWave = Mathf.Sin (Time.time * OscilationFrequency) * RedOscilator * ColorUnit;
					Red = ProcessedRed + RedWave;
				} else {
					Red = (PrimitiveRed - 50.0f) * ColorUnit;
				}
				if (Green_Activator) {		
					float ProcessedGreen = (BaseGreen + 60.0f) * ColorUnit;						
					GreenWave = Mathf.Sin (Time.time * OscilationFrequency) * GreenOscilator * ColorUnit;
					Green = ProcessedGreen + GreenWave;
				} else {
					Green = (PrimitiveGreen - 50.0f) * ColorUnit;		
				}
				if (Blue_Activator) {						
					float ProcessedBlue = (BaseBlue + 60.0f) * ColorUnit;						
					BlueWave = Mathf.Sin (Time.time * OscilationFrequency) * BlueOscilator * ColorUnit;
					Blue = ProcessedBlue + BlueWave;
				} else {
					Blue = (PrimitiveBlue - 50.0f) * ColorUnit;		
				}

				//set oscillation according to color oscilation / wave dampener
				float scaleOscillation = Mathf.Sin (Time.time * OscilationFrequency) /100.0f;
				MultiplierPace += Time.deltaTime * 0.25f;
				//scale oscilation is reduced in time considering the player moving towards object
				float scaleMultiplier = Mathf.Lerp(3.0f,1.5f,MultiplierPace);

				//create oscilatiion vector
				Vector3 ScaleOscilator = new Vector3 (Mathf.Clamp (Target.transform.localScale.x + scaleOscillation, OriginalScale.x, OriginalScale.x * scaleMultiplier ), 
				                                      Mathf.Clamp (Target.transform.localScale.y + scaleOscillation, OriginalScale.y, OriginalScale.y * scaleMultiplier),
				                                      Mathf.Clamp (Target.transform.localScale.z + scaleOscillation, OriginalScale.z, OriginalScale.z * scaleMultiplier));

				//use vector for target
				Target.transform.localScale = ScaleOscilator;
				Target.GetComponent<Renderer> ().material.color = new Color (Red, Green, Blue);

				if (Target2) {
					Target2.transform.localScale = ScaleOscilator;
					Target2.GetComponent<Renderer> ().material.color = new Color (Red, Green, Blue);
				}
			}
		} else {
			//reset colors to initial primitives
			Red = (PrimitiveRed - 50.0f) * ColorUnit;
			Green = (PrimitiveGreen - 50.0f) * ColorUnit;	
			Blue = (PrimitiveBlue - 50.0f) * ColorUnit;	
			//apply colors
			LightLeft.GetComponent<Renderer> ().material.color = new Color (Red, Green, Blue);
			LightRight.GetComponent<Renderer> ().material.color = new Color (Red, Green, Blue);
			//reset object scale to original
			LightLeft.transform.localScale = OriginalScale;
			LightRight.transform.localScale = OriginalScale;
			//deactivate object
			if(Application.loadedLevelName != "CarSelectionMenu")
			{
				LightLeft.gameObject.SetActive (false);
			}
			LightRight.gameObject.SetActive (false);
		}
	}
}
	

		
	








