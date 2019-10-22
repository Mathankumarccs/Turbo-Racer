using UnityEngine;
using System.Collections;

public class VegetationCorrector : MonoBehaviour {


	public float ScaleMultiplier;
	public float RotationCorrector;
	public float ObjectCount;

	public float SmallCorrector;



	public bool AllowMerge;

	// Use this for initialization
	public bool ExecuteCorrection () {	

//		Debug.Log ("TEST");
				foreach (Transform child in transform) {
						if (child.name.Contains ("Tree") || child.name.Contains ("V")) {
								float Corrector;
								if (child.name.Contains ("V")) {							
										Corrector = ScaleMultiplier / SmallCorrector;
								} else {
										Corrector = ScaleMultiplier;
								}
								//get current transform position and scale
								Vector3 Scale = child.localScale;
								Vector3 Position = child.position;

								//set new scale in reference to initial
								float S_x = Scale.x * Corrector;
								float S_y = Scale.y * Corrector;
								float S_z = Scale.z * Corrector;

								//set new position in reference to initial it is calculated using the scale ?PROCENTUAL ?
								float P_x = child.position.x + 0.0f;
								float P_y = child.position.y + (S_y / 2.0f - S_y / 8.0f); 
								float P_z = child.position.z + 0.0f;

								//set new rotation in reference to initial
								float R_y = Input.GetAxis ("Vertical") * RotationCorrector;
								Quaternion target = Quaternion.Euler (0, R_y, 0);


								//apply changes to the transform component
								child.localScale = new Vector3 (S_x, S_y, S_z);
								child.position = new Vector3 (P_x, P_y, P_z);
								child.rotation = Quaternion.Lerp (child.rotation, target, 0.0f);	
						}
						ObjectCount++;
				}
				AllowMerge = DoMerge ();
				return AllowMerge;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool DoMerge()
	{
		return true;
	}
}
