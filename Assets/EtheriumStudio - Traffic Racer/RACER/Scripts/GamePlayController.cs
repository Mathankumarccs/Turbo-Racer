using UnityEngine;
using System.Collections;
using GreedyGame.Runtime;

public class GamePlayController : MonoBehaviour
{


    private bool alreadySetEndScoreStats = false;
    public static double checkpointOffset;      //used when giving rewards from checkpoints
    private double checkpointWallOffset;   //used to know when and where to spawn checkpoints

    private int checkpointReward;           //the value of the next checkpoint reward
    private bool canGiveCheckpoint;

    private TextMesh checkpointText;
    private TextMesh outlineCheckpointText;
    private GameObject checkpointParticles;
    private GameObject outlineCheckpointParticles;
    private GameObject checkpointWall;      //the actual checkpoint wall object



    float lastPickedLane;
    int nextCarSpawnIndex;


    public float TrafficLimit;
    public float SpawnsPerSecond;
    private float initialSpawnsPerSecond;


    public static float[] LanesForLaneChange;

    public float[] Lanes;

    public GameObject[] trafficCars, playerCars;


    public static int checkpointsCount = 0;

    public static double distanceTravelled;
    public static int checkpointsCash;
    public static int distanceCash;
    public static int highSpeedCash;
    public static int overtakesCash;
    public static int totalCash;

    public static double distTraveled;

    public static bool isGameEnded = false;
    public GameObject playerObj;
    public GameObject gameEndMenu;
    public TextMesh distanceText, distanceTextOut, speedText, speedTextOut, gameOverTxt, scoreText, scoreTextOut, overtakesText, highSpeedText, highSpeedTextOut, textKmh, textM;

    public GameObject HighSpeedUi;
    public GameObject OvertakesUi;
    public GameObject inGameUI;
    private float highSpeedBonusCounter;
    public static float finalScore;
    public static float totalHighSpeed;
    public static float totalOvertakes;

    float startPlayerCarPositionZ;
    public carCamera camScript;
    public float trafficStartingPoint;
    public float trafficCarDistance;



    public float ActualCarspeed;

    public float trafficCarY = 0f;


    // Use this for initialization

    void OnEnable()
    {

        LanesForLaneChange = Lanes;
        isGameEnded = false;
        CarController.gameEnded += onGameEnd;




    }
    void OnDisable()
    {
        CarController.gameEnded -= onGameEnd;
    }

    void onGameEnd(System.Object obj, System.EventArgs args)
    {

        //set end game score and stats
        distanceCash = (int)(distTraveled * 100f);
        highSpeedCash = (int)(totalHighSpeed * 5f);
        overtakesCash = (int)(totalOvertakes * 10f);

        totalCash = (int)((distTraveled * 100f) + (totalOvertakes * 10f) + (totalHighSpeed * 5f)) + checkpointsCash;
        if (PlayerPrefs.GetInt("CashDoubler", 0) == 1)
        {
            totalCash = totalCash * 2;
        }
        alreadySetEndScoreStats = true;



        inGameUI.SetActive(false);
        CancelInvoke();
        gameEndMenu.SetActive(true);


    }


    void Start()
    {


        Resources.UnloadUnusedAssets();

        trafficCarDistance = 120;
        Destroy(playerObj);
        checkpointOffset = 2;  //give checkpoint every 2 km
        checkpointWallOffset = 2f;  //give checkpoint every 2 km

        finalScore = 0;
        totalHighSpeed = 0;
        totalOvertakes = 0;
        distTraveled = 0;






        playerObj = GameObject.Instantiate(playerCars[carSelection.carIndex]) as GameObject;

        GreedyGameAgent.Instance.removeFloatUnit("float-2484");

        if (Application.loadedLevelName == "highway")
        {

            CarController.yStart = playerObj.transform.localPosition.y;
            CarController.xLimitLeft = 268.328f;
            CarController.xLimitRight = 274.563f;
            CarController.xStart = 271.5f;
        }
        if (Application.loadedLevelName == "City")
        {

            CarController.yStart = playerObj.transform.localPosition.y;
            CarController.xLimitLeft = -89.683f;
            CarController.xLimitRight = -82.957f;
            CarController.xStart = -86.447f;
        }
        if (Application.loadedLevelName == "Desert2")
        {

            CarController.yStart = playerObj.transform.localPosition.y; //put the player car on the y position from the prefab
            CarController.xLimitLeft = -2.9f;
            CarController.xLimitRight = 3.1f;
            CarController.xStart = 0.21f;
        }
        if (Application.loadedLevelName == "Desert3")
        {


            CarController.yStart = playerObj.transform.localPosition.y;
            CarController.xLimitLeft = -3.0f;
            CarController.xLimitRight = 3.25f;
            CarController.xStart = 0.21f;
        }


        initialSpawnsPerSecond = SpawnsPerSecond;
        totalCash = 0;
        if (camScript == null) camScript = Camera.main.GetComponent<carCamera>();

        startPlayerCarPositionZ = playerObj.transform.position.z;
        camScript.targetTrans = playerObj.transform;

        nextCarSpawnIndex = Random.Range(0, trafficCars.Length);

        //spawn 8 cars on game start
        int i = 9;
        while (i > 0)
        {
            SpawnCarsOnGameStart();
            i--;
        }

        checkpointWall = (GameObject)Instantiate(Resources.Load("checkpointWall"));

    }



    public void OnGameStart()
    {

        alreadySetEndScoreStats = false;
        inGameUI.SetActive(true);


        float TrafficFrequency = 1.0f / SpawnsPerSecond;
        InvokeRepeating("generateTrafficCars", 0.8f, 0.8f);
        distanceTravelled = 0;
    }

    void Update()
    {

    }


    void FixedUpdate()

    {

        if (isGameEnded)
        {

            distTraveled = 0;
            speedText.text = "";
            speedTextOut.text = "";
            distanceText.text = "";
            gameOverTxt.text = "GAME OVER :(";

        }
        else
        {

            distTraveled = (double)((CarController.thisPosition.z - CarController.distanceToZeroOnZ) / 1000f);
            distanceTravelled = distTraveled;
        }















        if (!CarController.hasCrashed)
        {


            if (distTraveled - checkpointOffset >= 0)
            {
                if (CarController.actualSpeed > 10f)
                {
                    checkpointReward = (int)(CarController.actualSpeed * 20);
                    checkpointOffset += 2;

                    checkpointsCash += checkpointReward;
                }
                else if (CarController.actualSpeed <= 10f)
                {

                    checkpointReward = 100;
                    checkpointOffset += 2;

                    checkpointsCash += checkpointReward;
                }
                canGiveCheckpoint = true;
                checkpointsCount++;
                SoundController.Static.PlayCheckpointSound();

            }



            if (distTraveled - checkpointWallOffset >= -2)
            {
                checkpointWallOffset += 2f;

                float checkpointTextXCoord = CarController.xStart;

                checkpointWall.transform.position = new Vector3(checkpointTextXCoord, CarController.yStart + 2, CarController.thisPosition.z + 2000f);

            }





            if (canGiveCheckpoint)
            {
                float checkpointTextXCoord = CarController.xStart;

                checkpointParticles = (GameObject)Instantiate(Resources.Load("CheckpointTextMesh"));
                checkpointParticles.transform.position = new Vector3(checkpointTextXCoord, CarController.thisPosition.y + 0.1f, CarController.thisPosition.z + 15f);

                checkpointText = checkpointParticles.GetComponent<TextMesh>();


                checkpointText.text = "Speed Bonus: +" + checkpointReward.ToString() + " $!";

                Destroy(checkpointParticles, 1.5f);
                canGiveCheckpoint = false;


            }

            if (checkpointParticles != null)
            {
                checkpointText.color = new Color(checkpointText.color.r, checkpointText.color.g, checkpointText.color.b, Mathf.Lerp(checkpointText.color.a, 0f, Time.deltaTime / 4f));
                checkpointParticles.GetComponent<Rigidbody>().velocity = new Vector3(0f, Mathf.Lerp(checkpointParticles.GetComponent<Rigidbody>().velocity.y, 1f, Time.deltaTime * 14f), CarController.playerVelocity.z);

            }

        }










        ActualCarspeed = CarController.actualSpeed;

        if (ActualCarspeed < 10f)
        {
            SpawnsPerSecond = 1f;
        }
        if (ActualCarspeed > 10f && ActualCarspeed < 14f)
        {
            SpawnsPerSecond = 2f;
        }
        if (ActualCarspeed > 14f)
        {
            SpawnsPerSecond = 3f;
        }



        //1.1f represents the distance the car goes every frame with 100 km/h

        if (ActualCarspeed > 5f)
        {
            if (highSpeedBonusCounter > 0)
            {
                finalScore += (CarController.actualSpeed / 15f) * 2;
            }
            else
            {
                finalScore += (CarController.actualSpeed / 15f);
            }
        }




        speedText.text = "" + CarController.testDisplay.ToString();
        speedTextOut.text = "" + CarController.testDisplay.ToString();

        distanceText.text = "" + (distTraveled.ToString("F2"));
        distanceTextOut.text = "" + (distTraveled.ToString("F2"));
        scoreText.text = ((int)finalScore).ToString();
        scoreTextOut.text = ((int)finalScore).ToString();




        if (CarController.overtakesCounter > 1f)
        {

            OvertakesUi.SetActive(true);
            overtakesText.text = CarController.overtakesCounter.ToString();
        }
        else
        {
            OvertakesUi.SetActive(false);
            overtakesText.text = "";

        }


        //High Speed Bonus
        if (CarController.actualSpeed > 10f)
        {
            speedText.color = new Color32(24, 200, 24, 255);
            textKmh.color = new Color32(24, 200, 24, 255);
            scoreText.color = new Color32(24, 200, 24, 255);
            highSpeedBonusCounter += Time.deltaTime;
            totalHighSpeed += Time.deltaTime;
            HighSpeedUi.SetActive(true);
            highSpeedText.text = "" + (highSpeedBonusCounter).ToString("F2");
            highSpeedTextOut.text = "" + (highSpeedBonusCounter).ToString("F2");

        }
        else
        {
            scoreText.color = new Color32(218, 184, 42, 255);
            speedText.color = new Color32(218, 184, 42, 255);
            textKmh.color = new Color32(218, 184, 42, 255);
            HighSpeedUi.SetActive(false);
            highSpeedText.text = "";
            highSpeedTextOut.text = "";
            highSpeedBonusCounter = 0f;
            CarController.resetOvertakesCounter = 0f;

        }


    }


    void SpawnATrafficCar()
    {

        float PickedLane;
        GameObject trafficObj;
        PickedLane = Lanes[Random.Range(0, Lanes.Length)];

        trafficObj = GameObject.Instantiate(trafficCars[nextCarSpawnIndex]) as GameObject;
        if (nextCarSpawnIndex < trafficCars.Length - 1)
        {
            nextCarSpawnIndex++;
        }
        else
        {
            nextCarSpawnIndex = 0;
        }

        if (PickedLane == lastPickedLane)
        {
            trafficObj.transform.position = new Vector3(PickedLane, trafficCarY, playerObj.transform.position.z + ((trafficCarDistance + 85)));
        }
        else
        {
            trafficObj.transform.position = new Vector3(PickedLane, trafficCarY, playerObj.transform.position.z + ((trafficCarDistance + Random.Range(60, 80))));
        }

        lastPickedLane = PickedLane;
    }

    void SpawnCarsOnGameStart()
    {

        float PickedLane;
        GameObject trafficObj;
        PickedLane = Lanes[Random.Range(0, Lanes.Length)];
        trafficObj = GameObject.Instantiate(trafficCars[nextCarSpawnIndex]) as GameObject;
        if (nextCarSpawnIndex < trafficCars.Length - 1)
        {
            nextCarSpawnIndex++;
        }
        else
        {
            nextCarSpawnIndex = 0;
        }



        trafficObj.transform.position = new Vector3(PickedLane, trafficCarY, playerObj.transform.position.z + ((Random.Range(50, 120))));   // * randomSalt/2.0f
    }



    void generateTrafficCars()
    {

        GameObject[] TrafficSpawns = GameObject.FindGameObjectsWithTag("trafficCar");
        int TrafficQuantity = TrafficSpawns.Length;
        //spawn cars faster at higher speeds			

        if (ActualCarspeed > 5 && ActualCarspeed < 9)
        {
            TrafficLimit = 8;
        }
        if (ActualCarspeed > 9 && ActualCarspeed < 12)
        {
            TrafficLimit = 14;
        }
        if (ActualCarspeed > 12 && ActualCarspeed < 16)
        {
            TrafficLimit = 18;
        }


        if (TrafficQuantity < TrafficLimit)
        {
            //player speed < 100 km/h
            if (SpawnsPerSecond == 1)
            {
                int randomNumber = Random.Range(1, 100);
                if (randomNumber <= 50)
                {
                    SpawnATrafficCar();
                }
                else if (randomNumber > 50 && randomNumber < 85)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }
                else if (randomNumber >= 85)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }
            }

            //player speed >125 kmh and < 160 kmh
            if (SpawnsPerSecond == 2f)
            {
                int randomNumber = Random.Range(1, 100);
                if (randomNumber <= 30)
                {
                    SpawnATrafficCar();
                }
                if (randomNumber > 30 && randomNumber <= 70)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }
                else if (randomNumber > 70 && randomNumber <= 95)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }
                else if (randomNumber > 95)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }

            }

            //player speed > 160 kmh
            if (SpawnsPerSecond == 3f)
            {
                int randomNumber = Random.Range(1, 100);
                if (randomNumber <= 50)
                {
                    SpawnATrafficCar();
                }
                if (randomNumber > 50 && randomNumber <= 80)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }
                else if (randomNumber > 80 && randomNumber <= 90)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }
                else if (randomNumber > 90)
                {
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                    SpawnATrafficCar();
                }

            }

        }
    }




}
