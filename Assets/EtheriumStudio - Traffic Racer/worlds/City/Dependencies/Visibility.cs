using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Visibility : MonoBehaviour {
	
	public Vector3 Recalculate;
	public GameObject target;
	public Transform Excep;
	public bool TerrainMoved;
	public List<Transform> LevelF;	
	
	//public List<float>Diffs;
	public float TerrainSize;
	public float TerrainTiles;
	public float CameraMultiplier=1.0f;
	
	void Start () {		
		/*
		for (int i=0; i<LevelF.Count-1; i++) {
			float A = LevelF[i].transform.position.z;
			float B = LevelF[i+1].transform.position.z;
			if((A != null )&&(B != null))
			{
				float C = B-A;
				Diffs.Add(C);
			}
		}*/
		/*if (!Excep) {
			gameObject.SetActive (false);
		}*/
		//	InvokeRepeating ("LoadLevelComponent", 0.0f, 0.05f);
		InvokeRepeating ("LoadLevelComponent2", 0.0f, 0.05f);
	}	
	
	/*
	void Update () {
		
	}*/
	/*
	void LoadLevelComponent()	{	
				for (int i=0; i<LevelF.Count; i++) {
						if (LevelF [i].position.z + 514.0f < target.transform.position.z) {	
								if (LevelF [i].GetChild (0).gameObject.name == gameObject.name) {										
										if (i < 5) {
												LevelF [i + 1].GetChild (0).gameObject.SetActive (true);
										} else {
												LevelF [0].GetChild (0).gameObject.SetActive (true);
										}
								} else {
										if (LevelF [i].position.z > target.transform.position.z) {
												Recalculate = LevelF [i].transform.position;										
												Recalculate.z = Recalculate.z + 1542.0f;
												TerrainMoved = true;
										
												LevelF [i].transform.position = Recalculate;													
												LevelF [i].GetChild (0).gameObject.SetActive (false);
												
										}					
								}
						}
				}						
		}*/
	void LoadLevelComponent2()	{	
		for (int i=0; i<LevelF.Count; i++) {
			
			if(LevelF[i].position.z + TerrainSize*CameraMultiplier *2.0f > target.transform.position.z )
			{

				LevelF[i].GetChild(0).gameObject.SetActive(true);				
				
			}
			else{
				
				//new version - get the terrain width and height
				
				
				//NEW FOREST values : 258 and 516
				//old values 514 and 1542
				//800 pt desert


				if (LevelF [i].position.z + TerrainSize*(TerrainTiles-CameraMultiplier) < target.transform.position.z)
				{
					Recalculate = LevelF [i].transform.position;										
					Recalculate.z = Recalculate.z + TerrainSize*TerrainTiles;
					TerrainMoved = true;
					

					LevelF[i].GetChild(0).gameObject.SetActive(false);
					LevelF[i].transform.position = Recalculate;		
					
				}
			
				LevelF[i].GetChild(0).gameObject.SetActive(false);
				
				
			}
		}
	}
}





















