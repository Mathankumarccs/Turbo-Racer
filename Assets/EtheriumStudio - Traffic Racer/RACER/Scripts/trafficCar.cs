using UnityEngine;
using System.Collections;

public class trafficCar : MonoBehaviour {

	private bool canVariateSpeed;
	public bool frontClear;
	public GameObject[] TrafficCars;
	private bool playedSpecialSound;
	private bool playedCrashSound;
	private float afterCrashForwardPosition;
	public static float farthestZCar;
	private bool changedLanes;
	private bool changeLanesSignal;

	private bool incrementedOvertakesCounter;
	public int TrafficLimit;
	Transform thisTrans;
	public float speed;
	public float OriginalSpeed;
	public Transform[] wheelObjs;
	public float wheelTurnSpeed;
	// Use this for initialization	
	public GameObject TrafficMaster;
	
	//used to seed the car with a chance for a lane change
	public int RandomizerForLaneChange;
	
	public Vector3 InitialPosition;
	
	public LightChange CarLaneLights;
	
	public float LaneChangeDelay ;
	public float LCDTimer ;
	public int R;
	
	public GameObject[] TrafficSpawns;
//	public float[] Lanes = new float[]{(-10.5f/2.0f)+270.0f,(-3.6f/2.0f)+270.0f,(2.8f/2.0f)+270.0f,(9.6f/2.0f)+270.0f};
	private float[] Lanes;
	public float PickedLaneX;
	public bool executed;
	private float overtakesOffset;
	public float CustomDelimiterInterval;
	private bool CloseCars;
	
	public bool Moving=true;
	public bool ModifiedSpeed;

	private bool crashed;


	private TextMesh currentText;
	private TextMesh outlineCurrentText;
	private GameObject overtakeParticles;
	private GameObject overtakeParticles2;


	private string trafficCarType;
	private float trafficCarOvertakeOffset; 

	private float actualTrafficCarSpeed;
	//private float overtakesScoreBonus;

	private float lane0Speed, lane1Speed, lane2Speed, lane3Speed;


	
	void Start () {
		afterCrashForwardPosition = 30;
		Lanes = GamePlayController.LanesForLaneChange;
		crashed = false;
		changeLanesSignal = false;
		//speed = 20f;   //pt 80 km/h
	
		speed = 10f;
		actualTrafficCarSpeed = Mathf.Clamp (CarController.actualSpeed * 0.6f, 12, 22);
		//InvokeRepeating ("GetPlayerCarSpeed", 2f, 2f);
		//pt 150
//		lane0Speed = speed * 0.38f;
//		lane1Speed = speed * 0.57f;
//		lane2Speed = speed * 0.66f;
//		lane3Speed = speed * 0.475f;


		//pt 100
		lane0Speed = speed * 0.15f;
		lane1Speed = speed * 0.7f;
		lane2Speed = speed * 1.2f;
		lane3Speed = speed * 0.3f;



		int randomNumber = Random.Range(0,5);
		if(randomNumber < 3)
		{
			canVariateSpeed = true;
		}
		else
		{
			canVariateSpeed = false;
		}


		//if player car is a truck, we need to increment trafficCarOvertakeOffset by 1, because the truck is wider
		trafficCarOvertakeOffset = 1.55f;


		incrementedOvertakesCounter = false;
		thisTrans = transform;
		R = Random.Range (0, 2);
		InitialPosition = transform.position;
		
		CarLaneLights = thisTrans.GetComponent<LightChange> ();
		
		RandomizerForLaneChange = Random.Range (1, 100);
		
		TrafficMaster = GameObject.Find ("GameController");

		//speedcontrol for each lane
		float PickedLane = thisTrans.position.x;

	
		//speed = speed / 2;

		OriginalSpeed = speed;

		if(PickedLane==Lanes[0])
		{
			speed = lane0Speed;
		}
		if(PickedLane==Lanes[1])
		{
			speed = lane1Speed;
		}
		if(PickedLane==Lanes[2])
		{
			speed = lane2Speed;
		}
		if(PickedLane==Lanes[3])
		{
			speed = lane3Speed;
		}




		#if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8
		//		foreach(Transform t in transform.GetComponentsInChildren<Transform>() )
		//		{ if(t.name.Contains("Shadow") ) {
		//				Debug.Log(t.name);
		//				Destroy(t.gameObject);
		//			}
		//		}
		#endif
		
		gameObject.AddComponent<Rigidbody> ();
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		CheckCarType ();
//		Debug.Log (CustomDelimiterInterval);
	}



	private void CheckCarType()
	{
		/*
		if (thisTrans.name.Contains ("Coupe")) {
			
			CustomDelimiterInterval = 20.0f;
			overtakesOffset = 1f;
			//Debug.Log("Coupe delimiter");		
		}
		if (thisTrans.name.Contains ("Pickup")) {
			
			CustomDelimiterInterval = 21.0f;
			overtakesOffset = 1f;
			//Debug.Log("Pickup delimiter");		
		}
		if (thisTrans.name.Contains ("Saloon")) {
			
			CustomDelimiterInterval = 21.0f;
			overtakesOffset = 1f;
			//Debug.Log("Saloon delimiter");		
		}
		if (thisTrans.name.Contains ("SUV")) {
			
			CustomDelimiterInterval = 21.0f;
			overtakesOffset = 1f;
			//Debug.Log("SUV delimiter");		
		}
		if (thisTrans.name.Contains ("Truck")) {
			
			CustomDelimiterInterval = 22f;
			overtakesOffset = 1.6f;
			//Debug.Log("SUV delimiter");		
		}
		if (thisTrans.name.Contains ("Limo")) {
			
			CustomDelimiterInterval = 24f;
			overtakesOffset = 1.25f;
			//Debug.Log("SchoolBus delimiter");		
		}
		if (thisTrans.name.Contains ("SchoolBus")) {
			
			CustomDelimiterInterval = 27f;
			overtakesOffset = 2f;
			//Debug.Log("SchoolBus delimiter");		
		}
		if (thisTrans.name == ("Bus(Clone)")) {
			
			CustomDelimiterInterval = 27f;
			overtakesOffset = 2f;
			//Debug.Log("SchoolBus delimiter");		
		}
		if (thisTrans.name == ("CopCar2(Clone)")) {
			
			CustomDelimiterInterval = 19f;
			overtakesOffset = 1f;
			//Debug.Log("SchoolBus delimiter");		
		}
		if (thisTrans.name == ("IceCreamTruck1(Clone)")) {
			
			CustomDelimiterInterval = 22f;
			overtakesOffset = 1.3f;
			//Debug.Log("SchoolBus delimiter");		
		}
		if (thisTrans.name.Contains ("mini_truck")) {
			CustomDelimiterInterval = 32.0f;
			overtakesOffset = 1.5f;
			//Debug.Log("mini truck delimiter");	
		}
		if (thisTrans.name == "Truck1(Clone)") {
			CustomDelimiterInterval = 32.0f;
			overtakesOffset = 2f;
			//Debug.Log("mini truck delimiter");	
		}*/


		//traffic - set 2 

		if (thisTrans.name.Contains ("TrailerAttach")) {
			CustomDelimiterInterval = 15.0f;
		}
		if (thisTrans.name.Contains ("SmallCar")) {
			CustomDelimiterInterval = 12.0f;
		}
		if (thisTrans.name.Contains ("SchoolBus")) {
			CustomDelimiterInterval = 13.0f;
		}
		if (thisTrans.name.Contains ("Police")) {
			CustomDelimiterInterval = 11.0f;
		}
		if (thisTrans.name.Contains ("Pickup")) {
			CustomDelimiterInterval = 11.5f;
		}
		if (thisTrans.name.Contains ("IceCream")) {
			CustomDelimiterInterval = 12.0f;
		}
		if (thisTrans.name.Contains ("DeliveryVan")) {
			CustomDelimiterInterval = 12.0f;
		}
		if (thisTrans.name.Contains ("DeliverySemi")) {
			CustomDelimiterInterval = 12.5f;
		}
		if (thisTrans.name.Contains ("Bus")) {
			CustomDelimiterInterval = 15.0f;	
		}
		if (thisTrans.name.Contains ("Armored")) {
			CustomDelimiterInterval = 11.0f;	
		}
		if (thisTrans.name.Contains ("Ambulance")) {
			CustomDelimiterInterval = 12.0f;	
		}


	}
	
	//--------------------------------------------------------------------------------------------------
	// Update is called once per frame
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag.Contains ("trafficCar")) {

			transform.Translate(0,0,0);

			if (CarLaneLights) {
				CarLaneLights.LightActivator=true;
				CarLaneLights.LeftLane=true;
				CarLaneLights.RightLane=true;
			}

			if (transform.position.z > CarController.thisPosition.z + 49f) {
				Destroy (gameObject);
			}
			else
			{
				speed=0.0f;
				wheelTurnSpeed=0.0f;

				if(CarLaneLights!= null)
				{
					CarLaneLights.LightActivator=true;
					CarLaneLights.LeftLane=true;
					CarLaneLights.RightLane=true;
				}
			}

					
			
		}
		//ifa car collides with a player that has less then 60km/h
		//stop the car and turn al lights on
		if (collision.collider.tag.Contains ("Player")) {

			crashed = true;

			if(CarLaneLights!= null)
			{
				CarLaneLights.LightActivator=true;
				CarLaneLights.LeftLane=true;
				CarLaneLights.RightLane=true;
			}
			speed=0.0f;
			wheelTurnSpeed=0.0f;


				if (CarController.actualSpeed < 6.0f )
				{
					if(!playedCrashSound)
					{
						SoundController.Static.PlayAccidentCarHornSound();
						SoundController.Static.PlayTrafficCarBump();
						
						playedCrashSound = true;
					}
				}


		}
	}


	
	private void GetPlayerCarSpeed()
	{

	}
	
	void FixedUpdate () {
		//Debug.Log (speed);
//		Debug.Log ("Lane 0: " + Lanes[0] + "Lane 1: " + Lanes[1] + "Lane 2: " + Lanes[2] + "Lane 3: " + Lanes[3]);


		//when the player car has been instantiated 
		if(CarController.carName != null)
		{
			//if the player car is a truck, and we havent incremented the trafficCarOvertakeOffset yet, do it now, once (second condition is because we are inFixedUpdate())
			if(CarController.carName.Contains("Truck") && trafficCarOvertakeOffset < 1.7f)
			{
				trafficCarOvertakeOffset = trafficCarOvertakeOffset + 0.45f;
			}

		}


		if(!crashed)
		{
			actualTrafficCarSpeed = Mathf.Clamp (CarController.actualSpeed * 0.7f, 8, 15);
			//actualTrafficCarSpeed = Mathf.Clamp (CarController.actualSpeed * 1.5f, 10, 60);
			//Debug.Log (actualTrafficCarSpeed);
	//		Debug.Log (actualTrafficCarSpeed);

//			int rnd = Random.Range(1, 100);
//			if(rnd == 50)
//			{
				
			//}


			if(Time.timeSinceLevelLoad > 10f)
			{
				actualTrafficCarSpeed = Mathf.Clamp (CarController.actualSpeed * 0.8f, 10, 16);

			


					if (AccelerationIndicator.isAccelerating) 
					{
						speed = Mathf.Lerp (speed, actualTrafficCarSpeed, Time.deltaTime);
					}
					else if(UIControl.isBrakesOn)
					{
						speed = Mathf.Lerp (speed, actualTrafficCarSpeed, Time.deltaTime / 20f);
					}
					else 
					{
						speed = Mathf.Lerp (speed, actualTrafficCarSpeed, Time.deltaTime);
					}


			}


	//		lane0Speed = speed * 0.25f;
	//		lane1Speed = speed * 0.380f;
	//		lane2Speed = speed * 0.435f;
	//		lane3Speed = speed * 0.330f;

			lane0Speed = speed * 0.15f;
			lane1Speed = speed * 0.6f;
			lane2Speed = speed * 1f;
			lane3Speed = speed * 0.4f;
			//Debug.Log (frontClear);

				if(thisTrans.position.x==Lanes[0])
				{
					speed = Mathf.Lerp (speed, actualTrafficCarSpeed * 0.3f, Time.deltaTime);
					//speed = speed * 0.25f;
				}
				if(thisTrans.position.x==Lanes[1])
				{
					speed = Mathf.Lerp (speed, actualTrafficCarSpeed* 0.9f, Time.deltaTime);
					//speed = speed * 0.380f;
				}
				if(thisTrans.position.x==Lanes[2])
				{
					speed = Mathf.Lerp (speed, actualTrafficCarSpeed * 1.5f, Time.deltaTime);
					//speed = speed * 0.435f;
				}
				if(thisTrans.position.x==Lanes[3])
				{
					speed = Mathf.Lerp (speed, actualTrafficCarSpeed * 0.5f, Time.deltaTime);
					//speed = speed * 0.330f;
				}


//			Debug.Log (frontClear);
			
			thisTrans.Translate( 0,0,speed / 40f);
			float IntervalOfShift=80.0f;
			//change lanes
			if (CarController.thisPosition.z + IntervalOfShift > thisTrans.position.z) {

					
				//trebuie echilibrat
				//	GameObject[] TrafficSpawns = GameObject.FindGameObjectsWithTag ("trafficCar");

				//~25% chance to change lane
				if(RandomizerForLaneChange % 4 == 0)
				{
					if(!changedLanes)
					{
						ChangeLanes ();	
					}
				}			
	
			}
		
		}
		else if (crashed) 
		{


			afterCrashForwardPosition = Mathf.Lerp(afterCrashForwardPosition, 0, Time.deltaTime/2f);


			if(gameObject.transform.position.x > CarController.thisPosition.x)
			{
				gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(CarController.xLimitRight + 2, gameObject.transform.position.y, gameObject.transform.position.z + afterCrashForwardPosition), Time.deltaTime/3);
			}
			else
			{
				gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(CarController.xLimitLeft - 2, gameObject.transform.position.y, gameObject.transform.position.z + afterCrashForwardPosition), Time.deltaTime/3);
			}
			
			//gameObject.GetComponent<Collider>().isTrigger = true;
			StopCar();
		}

		//if nothing important happens destroy car when out of player range
		DestroyCar ();

	
	}
	
	//-----------------------------------------CHANGE LANES SECTION------------------------------
	
	public bool Debugged;
	public void Update()
	{

		if(CarController.actualSpeed > 10f)
		{

			//overtakes bonus
			if (CarController.thisPosition.z - 1.3f > thisTrans.position.z + 1.3f && !incrementedOvertakesCounter && Mathf.Abs(CarController.thisPosition.x - thisTrans.position.x) < trafficCarOvertakeOffset && (Mathf.Abs (CarController.thisPosition.z - 2 - thisTrans.position.z + 2 ) < 4f))
			{
				
				incrementedOvertakesCounter = true;
				CarController.overtakesCounter ++;
				GamePlayController.totalOvertakes ++;
				CarController.resetOvertakesCounter = 3.5f;
				
				
				if (!playedSpecialSound) 
				{
					if(thisTrans.name == ("IceCreamtruck(Clone)"))
					{
						int random = Random.Range(0, 3);
						if(random == 1)
						{
							SoundController.Static.playIceCreamTruckSound();
						}
						playedSpecialSound = true;
					}
					if(thisTrans.name == ("Police-Car(Clone)"))
					{
						int random = Random.Range(0, 3);
						if(random == 1)
						{
							SoundController.Static.playPoliceSound();
						}
						playedSpecialSound = true;
					}
				}
				
				if(CarController.overtakesCounter * 100 < Mathf.RoundToInt(CarController.actualSpeed) * 130f - ((100 - CarController.accelerationFactor) * 12))
				{
					CarController.overtakesScoreBonus = CarController.overtakesCounter * 100;						
				}
				else
				{
					CarController.overtakesScoreBonus = Mathf.RoundToInt(CarController.actualSpeed) * 130f - ((100 - CarController.accelerationFactor) * 12);
				}
				GamePlayController.finalScore += CarController.overtakesCounter * 100;

				
				if(carCamera.cameraMode == 1)
				{
					//outline
					overtakeParticles = Instantiate(SoundController.Static.overtakesGameObject);//(GameObject)Instantiate(Resources.Load("OvertakesTextMesh"));			
					overtakeParticles.transform.position = new Vector3(CarController.thisPosition.x + (thisTrans.position.x - CarController.thisPosition.x ) ,CarController.thisPosition.y + 0.5f, CarController.thisPosition.z + 2.002f);

					//Instantiate(SoundController.Static.overtakesGameObject);
					//the real text
					overtakeParticles2 = Instantiate(SoundController.Static.overtakesGameObject); //(GameObject)Instantiate(Resources.Load("OvertakesTextMesh"));
					overtakeParticles2.transform.position = new Vector3(CarController.thisPosition.x + (thisTrans.position.x - CarController.thisPosition.x ),CarController.thisPosition.y + 0.5f, CarController.thisPosition.z + 2f);

					currentText = overtakeParticles.GetComponent<TextMesh>();
					outlineCurrentText = overtakeParticles2.GetComponent<TextMesh>();
										
					currentText.color = Color.black;
					outlineCurrentText.transform.position = new Vector3(outlineCurrentText.transform.position.x, outlineCurrentText.transform.position.y + 0.06f, outlineCurrentText.transform.position.z);
										
					outlineCurrentText.text =  "+" + (CarController.overtakesScoreBonus);
					currentText.text = "+" + (CarController.overtakesScoreBonus);
					
					Invoke ("PlayOvertakeSound", 0f);
					Destroy(overtakeParticles,0.8f);
					Destroy(overtakeParticles2,0.8f);

					
				}
				else if(carCamera.cameraMode == 2)
				{
					overtakeParticles = (GameObject)Instantiate(SoundController.Static.overtakesGameObject);//Instantiate(Resources.Load("OvertakesTextMesh"));			
					overtakeParticles.transform.position = new Vector3(CarController.thisPosition.x + (thisTrans.position.x - CarController.thisPosition.x ) ,CarController.thisPosition.y + 0.5f, CarController.thisPosition.z + 2.2f);
					
					//the real text
					overtakeParticles2 = (GameObject)Instantiate(SoundController.Static.overtakesGameObject);//Instantiate(Resources.Load("OvertakesTextMesh"));
					overtakeParticles2.transform.position = new Vector3(CarController.thisPosition.x + (thisTrans.position.x - CarController.thisPosition.x ),CarController.thisPosition.y + 0.5f, CarController.thisPosition.z + 2f);
					
					
					currentText = overtakeParticles.GetComponent<TextMesh>();
					outlineCurrentText = overtakeParticles2.GetComponent<TextMesh>();
					
					
					currentText.color = Color.black;
					outlineCurrentText.transform.position = new Vector3(outlineCurrentText.transform.position.x, outlineCurrentText.transform.position.y + 0.07f, outlineCurrentText.transform.position.z);
					
					
					
					outlineCurrentText.text =  "+" + (CarController.overtakesScoreBonus);
					currentText.text = "+" + (CarController.overtakesScoreBonus);
					
					Invoke ("PlayOvertakeSound", 0f);
					Destroy(overtakeParticles,0.8f);
					Destroy(overtakeParticles2,0.8f);
				}
				else if(carCamera.cameraMode == 3)
				{
					
					
					overtakeParticles2 = (GameObject)Instantiate(SoundController.Static.overtakesGameObject);//Instantiate(Resources.Load("OvertakesTextMesh"));
					overtakeParticles2.transform.position = new Vector3(CarController.thisPosition.x + (thisTrans.position.x - CarController.thisPosition.x ),CarController.thisPosition.y + 0f, CarController.thisPosition.z + 4f);
					
					outlineCurrentText = overtakeParticles2.GetComponent<TextMesh>();
					outlineCurrentText.fontSize = 15;
					outlineCurrentText.text =  "+" + (CarController.overtakesScoreBonus);
					
					
					Invoke ("PlayOvertakeSound", 0f);				
					Destroy(overtakeParticles2,0.8f);
					
					
				}
			}
			//make the lerp for text fade out and movement up here
			if(carCamera.cameraMode == 1 || carCamera.cameraMode == 2)
			{
				if (outlineCurrentText != null && currentText != null) 
				{
					outlineCurrentText.color = new Color(outlineCurrentText.color.r,outlineCurrentText.color.g, outlineCurrentText.color.b, Mathf.Lerp(outlineCurrentText.color.a, 0f, Time.deltaTime));	
					currentText.color = new Color(currentText.color.r,currentText.color.g, currentText.color.b,Mathf.Lerp(currentText.color.a, 0f, Time.deltaTime));	
					
					overtakeParticles.GetComponent<Rigidbody>().velocity = new Vector3(0f,Mathf.Lerp(overtakeParticles.GetComponent<Rigidbody>().velocity.y,1f, Time.deltaTime * 14f), CarController.playerVelocity.z);
					overtakeParticles2.GetComponent<Rigidbody>().velocity = overtakeParticles.GetComponent<Rigidbody>().velocity;
				}
			}
			else if(carCamera.cameraMode == 3)
			{
				if (outlineCurrentText != null) 
				{
					outlineCurrentText.color = new Color(outlineCurrentText.color.r,outlineCurrentText.color.g, outlineCurrentText.color.b, Mathf.Lerp(outlineCurrentText.color.a, 0f, Time.deltaTime));	
					overtakeParticles2.GetComponent<Rigidbody>().velocity = new Vector3(0f,Mathf.Lerp(overtakeParticles2.GetComponent<Rigidbody>().velocity.y,1f, Time.deltaTime * 14f), CarController.playerVelocity.z);
					
					
				}
			}
			

			
		}
		else
		{
			if(carCamera.cameraMode == 1 || carCamera.cameraMode == 2)
			{
				if (outlineCurrentText != null && currentText != null) 
				{
					outlineCurrentText.color = new Color(outlineCurrentText.color.r,outlineCurrentText.color.g, outlineCurrentText.color.b, Mathf.Lerp(outlineCurrentText.color.a, 0f, Time.deltaTime));	
					currentText.color = new Color(currentText.color.r,currentText.color.g, currentText.color.b,Mathf.Lerp(currentText.color.a, 0f, Time.deltaTime));	
					
					overtakeParticles.GetComponent<Rigidbody>().velocity = new Vector3(0f,Mathf.Lerp(overtakeParticles.GetComponent<Rigidbody>().velocity.y,1f, Time.deltaTime * 14f), CarController.playerVelocity.z);
					overtakeParticles2.GetComponent<Rigidbody>().velocity = overtakeParticles.GetComponent<Rigidbody>().velocity;
				}
			}
			else if(carCamera.cameraMode == 3)
			{
				if (outlineCurrentText != null) 
				{
					outlineCurrentText.color = new Color(outlineCurrentText.color.r,outlineCurrentText.color.g, outlineCurrentText.color.b, Mathf.Lerp(outlineCurrentText.color.a, 0f, Time.deltaTime));	
					overtakeParticles2.GetComponent<Rigidbody>().velocity = new Vector3(0f,Mathf.Lerp(overtakeParticles2.GetComponent<Rigidbody>().velocity.y,1f, Time.deltaTime * 14f), CarController.playerVelocity.z);
					
					
				}
			}
		}


	}
	
	private void PlayOvertakeSound()
	{
		SoundController.Static.PlayOvertakeWooshSound(4f);
	}



	private void Accelerate()
	{

		TrafficCars = GameObject.FindGameObjectsWithTag ("trafficCar");


		//frontClear = false;

		for(int i = 0; i < TrafficCars.Length; i++)
		{


			if (thisTrans.position.z + CustomDelimiterInterval +5 > TrafficCars [i].transform.position.z && transform.position.x == TrafficCars [i].transform.position.x) 
			{
				//Debug.Log("CAR FOUND");
				//frontClear = false;
				break;

			}
			else
			{
				//Debug.Log("NO CAR FOUND");
				frontClear = true;
			}
		}	

		//Debug.Log (frontClear);

	}

	private void Deccelerate()
	{
		
		TrafficCars = GameObject.FindGameObjectsWithTag ("trafficCar");	
		
		for(int i = 0; i < TrafficCars.Length; i++)
		{			
			if (thisTrans.position.z + CustomDelimiterInterval + 5 > TrafficCars [i].transform.position.z && transform.position.x == TrafficCars [i].transform.position.x) 
			{
				frontClear = false;				
				break;				
			}
			else
			{
				frontClear = true;
			}

		}	
	}

	public void ChangeLanes()
	{

		TrafficSpawns = GameObject.FindGameObjectsWithTag ("trafficCar");
		//Debug.Log ("CHANGE LANES");
		//Debug.Log ("Car is shifting lanes");		
		//!!! - atentie - masian trebuie sa se asigure inainte sa aleaga banda care vrea sa o schimbe
		// cum ? trebuie sa detexteze daca exista alte clone de masini care au coordonata Z 
		// intre transform.position.z - MarjaEroare si transform.position.z + MarjaEroare
		// pentru ca schimbarea de banda sa fie facuta fara coliziuni
		// putem implementa o marja de eroare standard sau custom pentru fiecare tip de masina
		
		// TimeRange e invers proportional cu durata unde durata e 1
		float TimeRange = 0.5f;	

		// 2 variabile care reprezinta daca masina are vecini in spate SI fata
		//se parcurge toata lista si daca se descopera
		
		//GameObject[] TrafficSpawns;


		  //returns GameObject[]

		
		//for SAU foreach ?
		CloseCars = false;
		for (int i=0; i<TrafficSpawns.Length; i++) {
			//	float Z_Original = TrafficSpawns [i].transform.position.z;
			
			//daca exista masini prea aproape de partea din fata a masini sau prea aproape de partea din spate
			//nu executa miscarea de schimbare a benzii
			
			//daca masina analizata nu e cea care vrea sa schimbe banda
			if (TrafficSpawns[i].transform != thisTrans) {

				bool front = false;
				bool behind = false;	
				
				//masina e prea aproape, fie in fata fie in spate, dar PREA aproape
				
				if (thisTrans.position.x == Lanes [0]) {	
					VerifyExecuteLaneChange (Lanes [1], front, behind, TrafficSpawns, CustomDelimiterInterval, i, false, true);
				}
				
				if (thisTrans.position.x == Lanes [1]) {	
					
					if (R == 0) {																				
						VerifyExecuteLaneChange (Lanes [0], front, behind, TrafficSpawns, CustomDelimiterInterval, i, true, false);
					} else {
						VerifyExecuteLaneChange (Lanes [2], front, behind, TrafficSpawns, CustomDelimiterInterval, i, false, true);
					}
				}
				
				if (thisTrans.position.x == Lanes [2]) {														
					
					if (R == 0) {
						VerifyExecuteLaneChange (Lanes [1], front, behind, TrafficSpawns, CustomDelimiterInterval, i, true, false);
					} else {
						VerifyExecuteLaneChange (Lanes [3], front, behind, TrafficSpawns, CustomDelimiterInterval, i, false, true);
					}																		
				}
				if (thisTrans.position.x == Lanes [3]) {
					VerifyExecuteLaneChange (Lanes [2], front, behind, TrafficSpawns, CustomDelimiterInterval, i, true, false);
				}								
			}
		}
		
		//Debug.Log (PickedLaneX);//delay in execution
		if (CarController.thisPosition.z + 150.0f/2.0f > thisTrans.position.z) {
			//continue to increment light change timer
//			while (LCDTimer < LaneChangeDelay + 2.0f ) {
				// do something
				LCDTimer += Time.deltaTime;
				
//			}
		}
		
		if ((CarController.thisPosition.z + 28.0f > thisTrans.position.z)) 
		{



			LCDTimer = LaneChangeDelay + 1f;
		}
		if (LCDTimer > LaneChangeDelay && !CloseCars) {

			//speed is also modified when changing lanes//collisions may appear


			if(PickedLaneX==Lanes[0])
			{
				speed = Mathf.Lerp(speed, lane0Speed, Time.deltaTime * TimeRange);
			}
			if(PickedLaneX==Lanes[1])
			{
				speed = Mathf.Lerp(speed, lane1Speed, Time.deltaTime * TimeRange);
			}
			if(PickedLaneX==Lanes[2])
			{
				speed = Mathf.Lerp(speed, lane2Speed, Time.deltaTime * TimeRange);
			}
			if(PickedLaneX==Lanes[3])
			{
				speed = Mathf.Lerp(speed, lane3Speed, Time.deltaTime * TimeRange);
			}
			
			Vector3 TargetPosition = new Vector3 (PickedLaneX, thisTrans.position.y, thisTrans.position.z);
			thisTrans.position = Vector3.Lerp (thisTrans.position, TargetPosition, Time.deltaTime * TimeRange);



			//Debug.Log (thisTrans.position + " ///  " + TargetPosition);
			if( Mathf.Abs(thisTrans.position.x - PickedLaneX) < 0.01)
			{
				thisTrans.position = new Vector3(PickedLaneX, thisTrans.position.y, thisTrans.position.z);
				changedLanes = true;
				//Debug.Log ("TRUE");
			}
		}


		//light change still active for 2 seconds after shift is executed	 || (CarController.thisPosition.z + 15.0f > thisTrans.position.z)
		if ((LCDTimer > LaneChangeDelay + 3f)) {
						if (CarLaneLights) {
								if (Moving) {
										CarLaneLights.LightActivator = false;
										Moving = false;
								}
						}
				}

	}	
	
	public void VerifyExecuteLaneChange(float LaneValue, bool front, bool behind, GameObject[] TrafficSpawns, float CustomDelimiterInterval, int i, bool LeftL, bool RightL)
	{
		
		//se parcurge toata colectia
		if (TrafficSpawns [i].transform.position.x == LaneValue) {
			
			//Debug.Log("Car found");
			if (transform.position.z + CustomDelimiterInterval > TrafficSpawns [i].transform.position.z) {
				//Debug.Log("Car found at front");
				front = true;
			}
			if (transform.position.z - CustomDelimiterInterval < TrafficSpawns [i].transform.position.z) {
				//Debug.Log("Car found at behind");
				behind = true;
			}
			if ((front == true) && (behind == true)) {
				PickedLaneX = thisTrans.position.x;
				CloseCars = true;
				
			} else {
				PickedLaneX = LaneValue;
				if (CarLaneLights) {
					CarLaneLights.LightActivator = true;
					CarLaneLights.LeftLane = LeftL;	
					CarLaneLights.RightLane = RightL;	
					
				}
			}
			// ? Daca nu sunt masini pe banda, dar sunt masini pe alte benzi ce alegem ?
		} else {
			
			//nu sunt masini pe banda, dar sunt masini pe alte benzi
			if (!CloseCars) {
				PickedLaneX = LaneValue;
				if (CarLaneLights) {
					CarLaneLights.LightActivator = true;
					CarLaneLights.LeftLane = LeftL;	
					CarLaneLights.RightLane = RightL;					
				}				
			}
			else{

				PickedLaneX = thisTrans.position.x;
				if(CarLaneLights != null)
				{
				CarLaneLights.LightActivator = false;
				}
				changedLanes = false;   //couldn't change lanes
			}
		}
	}
	
	
	
	
	
	
	
	//---------------------------------------END CHANGE LANES SECTION------------------------------------
	
	void DestroyCar()
	{

		if (CarController.thisPosition.z - 7f > thisTrans.position.z) 
		{

			Destroy (gameObject);
		}
	}
	
	bool justOnce=false;
	
	public void StopCar()
	{
		Mathf.Lerp (speed, speed/10, Time.deltaTime/10);
		if (speed == 1) 
		{
			//speed = 0;
		}
		//speed=0;
		wheelTurnSpeed=0;
	}
}
