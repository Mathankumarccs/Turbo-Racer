using UnityEngine;
using System.Collections;

public class TrafficHeightCorrector : MonoBehaviour {

	public GamePlayController gamePlayController;

	[System.Serializable]
	public class CarCustomOffset {
		public string World;
		public float carOffsetY;
	}

	public CarCustomOffset[] CustomY;

	public bool executed;



	// Use this for initialization
	void Start () {
		//get game controller
		gamePlayController = GameObject.Find("GameController").GetComponent<GamePlayController> ();


	}
	
	// Update is called once per frame
	void Update () {
		//find the level in which the car is loaded
		if (executed == false) {
			if (CustomY.Length > 0) {
				foreach (CarCustomOffset OP in CustomY) {
					string ActiveWorld = "no level";
					if(OP.World != null || OP.World != string.Empty)
					{
					ActiveWorld = OP.World;
					}
					if (Application.loadedLevelName == ActiveWorld) {
						//and add the custom offset
						Vector3 newPosition = new Vector3 (transform.position.x, gamePlayController.trafficCarY + OP.carOffsetY, transform.position.z);
						transform.position = newPosition;
					}
				}
				executed = true;
			}
		}
	}
}
