using UnityEngine;
using System.Collections;
using System;



public class CarController : MonoBehaviour
{
    public static bool hasCrashed;

    public static string carName;


    private bool reducedSpeed;  //if we have already reduced speed for hitting the side of the road

    public float maxBrakesRotation;
    public float maxAccelerationRotation;

    public float wheelRotation;
    public float rotationFactorZ;
    public float rotationFactorY;

    public static Vector3 playerVelocity;
    public static int overtakesCounter;
    public static float resetOvertakesCounter;
    public static float overtakesScoreBonus;


    private float shiftSkidMarkCounter = 0.0f;

    public static float distanceToZeroOnZ;  //the offset on Z axis, since the car wont spawn at 0 coordinate on Z in every scene, and we need to start counting the distance from 0


    public float handlingFactor;
    public static float accelerationFactor;
    public float brakesFactor;
    public static float myXaccel = 0.0f;
    public static float acceleratia = 0.0f;
    public static string testDisplay = "0";
    float myYaceel = 0.0f;
    float smoothSpeed = 1.0f;

    private bool playedShiftGearSound2 = false;
    private bool playedShiftGearSound3 = false;
    private bool playedShiftGearSound4 = false;
    private bool playedShiftGearSound5 = false;
    private bool playedShiftGearSound6 = false;

    public static float actualSpeed = 2.5f;
    public static float RPM = 0f;
    public static float currentGear = 1f;
    private float maxGear = 0f;


    //these values are set in GameController
    public static float xLimitLeft;
    public static float xLimitRight;
    public static float yStart;
    public static float xStart;

    private float shiftGearPause2 = 0f;
    private float shiftGearPause3 = 0f;
    private float shiftGearPause4 = 0f;
    private float shiftGearPause5 = 0f;
    private float shiftGearPause6 = 0f;


    public float turnSpeed;
    public float carSpeed;
    public float tilt;


    public Transform carBody;
    private Transform[] wheelObjs = new Transform[4];  //0,1 - front wheels, 2,3 - back wheels
    private Transform[] frontWheels = new Transform[2];
    public float wheelSpeed;
    public static event EventHandler gameEnded;
    public static event EventHandler switchOnMagnetPower;
    public static event EventHandler switchOFFMagnetPower;
    public float magnetPowerTime = 3.0f;
    private float nextFire;
    public static float isDoubleSpeed = 1;
    Transform thisTrans;
    public GameObject particleParent;
    public GameObject tailLights;
    public Material tailLightsOffMaterial;
    public Material tailLightsOnMaterial;
    public static Vector3 thisPosition;
    float brakeSpeed = 1;
    carCamera camScript;
    Camera mainCamera;
    //LAKSHYA
    //public float moveHorizontal ;
    public static float moveHorizontal;


    float brakeRotationCounter;
    float accelerateRotationCounter;

    float moveHorizontalBoost;
    float zRotationBoost;

    float xAccelerationInput;
    //LAKSHYA
    //float rotationFromAcc;
    public static float rotationFromAcc;

    public static float maxSpeed;

    float x;
    float timeCounter;
    float timeCounterRight;
    public float velocityLeft;
    public float velocityRight;
    public float lastVelocityLeft;
    public float lastVelocityRight;
    public bool resetedTimeCounter;
    public bool resetedTimeCounterRight;

    private float yVelocity;


    public GameObject leftSkidMark;
    public GameObject rightSkidMark;
    public GameObject leftSkidMark2;    //second skid marks are used in case the car is a truck and needs to leave double skid marks,
    public GameObject rightSkidMark2;   //otherwise, leave null when assiging variables to the script

    private TrailRenderer leftTrailRenderer;
    private TrailRenderer rightTrailRenderer;
    private TrailRenderer leftTrailRenderer2;    //second trail renderers are used in case the car is a truck and needs to leave double skid marks
    private TrailRenderer rightTrailRenderer2;
    private GameObject selectedCarWheels;
    //public static float moveHorizontalNew;



    //currentCarWheelsBack = carWheelsBack;






    void OnEnable()
    {





        tilt = tilt;
        thisTrans = transform;

        mainCamera = Camera.main;
        isDoubleSpeed = 0.5f;
        camScript = Camera.main.GetComponent<carCamera>();
        Debug.Log(camScript);
        camScript.targetTrans = thisTrans;


    }



    void Start()
    {


        carName = gameObject.transform.name;

        //substring to remove the "(Clone)", added by Unity when instantiating a prefab
        carName = carName.Substring(0, (carName.Length - 7));

        selectedCarWheels = Instantiate(Resources.Load("WheelSets/" + carName + "/" + carName + "_WS_1")) as GameObject;

        wheelSetsScript thisWheelSetScript = selectedCarWheels.GetComponent<wheelSetsScript>();

        wheelObjs[0] = thisWheelSetScript.RightFrontWheel;
        wheelObjs[1] = thisWheelSetScript.LeftFrontWheel;

        if (thisWheelSetScript.WheelsBack != null)
        {
            wheelObjs[2] = thisWheelSetScript.WheelsBack;
            wheelObjs[3] = thisWheelSetScript.WheelsBack;
        }
        else
        {
            wheelObjs[2] = thisWheelSetScript.RightBackWheel;
            wheelObjs[3] = thisWheelSetScript.LeftBackWheel;
        }

        frontWheels[0] = thisWheelSetScript.RightFrontWheelParent;
        frontWheels[1] = thisWheelSetScript.LeftFrontWheelParent;

        Debug.Log(selectedCarWheels.transform.localPosition);

        Vector3 instantiatedWheelsPosition = selectedCarWheels.transform.localPosition; //get the local position of the wheels, the one that we have on the prefab
        selectedCarWheels.transform.SetParent(gameObject.transform); //parent the wheels to the car
        selectedCarWheels.transform.localPosition = instantiatedWheelsPosition; //set the wheels local position, the one that was before parenting them to the car,



        if (Application.loadedLevelName == "Desert2" || Application.loadedLevelName == "highway" || Application.loadedLevelName == "Desert3")
        {
            //gameObject.transform.localScale = new Vector3(0.51f,0.51f,0.51f);
        }
        else if (Application.loadedLevelName == "City")
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.9f, gameObject.transform.localScale.y * 0.9f, gameObject.transform.localScale.z * 0.9f);
        }


        string carBodyColorMat = PlayerPrefs.GetString("Car" + carSelection.carIndex + "Color"); //get the path to load the selected car body material, the path was set in menu, in carSelection.cs script

        Debug.Log(carBodyColorMat);

        if (carBodyColorMat != "  ")
        {
            Transform carTransform = gameObject.GetComponent<Transform>();
            foreach (Transform child in carTransform)
            {
                foreach (Transform child2 in child)
                {
                    if (child2.name == "body")
                    {
                        Renderer rnd = child2.GetComponent<Renderer>();

                        //remove the "(Clone)" added by unity when instantiating a prefab, in order to get the real transform name




                        //instantiate the new material from resources
                        Material newCarBodyMaterial = Resources.Load("CarColors/" + carName + "Colors/" + carBodyColorMat) as Material;
                        Debug.Log("CarColors/" + carName + "Colors/" + carBodyColorMat);
                        //set the new material for the car body, so it changes color


                        Material[] mats = rnd.materials;


                        //if the material name has the following format: CarName_ + color - it must be updated to change color. 
                        //This is our naming convention, since all car body materials are names like this: CarName_ColorName
                        //the SUV has 3 materials that need updating, in order to change its color, the rest of the cars, just 1

                        for (int i = 0; i < mats.Length; i++)
                        {
                            if (mats[i].name.Contains(carName + "_"))  //if the material name has the following format: CarName_ + color - it must be updated to change color
                            {

                                mats[i] = newCarBodyMaterial;
                            }
                        }
                        rnd.materials = mats;
                    }

                }
            }

        }


        GetComponent<Rigidbody>().position = new Vector3(xStart, yStart, GetComponent<Rigidbody>().position.z);
        hideNitrousParticle();
        overtakesCounter = 0;
        overtakesScoreBonus = 100f;
        distanceToZeroOnZ = thisTrans.position.z;
        thisPosition = thisTrans.position;

        actualSpeed = 2f;

        //if the car has double wheels on the back ->  it is a truck, we need double skid marks
        if (leftSkidMark2 != null && rightSkidMark2 != null)
        {
            leftTrailRenderer2 = leftSkidMark2.GetComponent<TrailRenderer>();
            rightTrailRenderer2 = rightSkidMark2.GetComponent<TrailRenderer>();
        }

        leftTrailRenderer = leftSkidMark.GetComponent<TrailRenderer>();
        rightTrailRenderer = rightSkidMark.GetComponent<TrailRenderer>();

        leftSkidMark.SetActive(false);
        rightSkidMark.SetActive(false);

        //if the car has double wheels on the back ->  it is a truck, we need double skid marks
        if (leftSkidMark2 != null && rightSkidMark2 != null)
        {
            leftSkidMark2.SetActive(false);
            rightSkidMark2.SetActive(false);
        }

        if (maxAccelerationRotation == 0 && maxBrakesRotation == 0)
        {
            maxAccelerationRotation = 2f;
            maxBrakesRotation = 2.6f;
        }


        if (rotationFactorY == 0 && rotationFactorZ == 0)
        {
            rotationFactorY = 0.8f;
            rotationFactorZ = 0.65f;
        }

        accelerationFactor = PlayerPrefs.GetInt("car" + carSelection.carIndex + "Speed", 25);
        handlingFactor = PlayerPrefs.GetInt("car" + carSelection.carIndex + "Handling", 15);
        brakesFactor = PlayerPrefs.GetInt("car" + carSelection.carIndex + "Brakes", 15);

        Debug.Log("Speed: " + accelerationFactor + " // Handling: " + handlingFactor + " // Brakes: " + brakesFactor);

        wheelSpeed = 650;

        if (maxGear == 5)
        {
            RPM = 2100f;
        }
        if (maxGear == 4)
        {
            RPM = 2000f;
        }
        if (maxGear == 3)
        {
            RPM = 1800f;
        }

        maxSpeed = (70f + accelerationFactor * 1.4f);

        if (maxSpeed < 134f)
        {
            maxGear = 3f;
        }
        else if (maxSpeed > 134f && maxSpeed < 183f)
        {
            maxGear = 4f;
        }
        else if (maxSpeed > 183f)
        {
            maxGear = 5f;
        }

        carSpeed = 2f;



        SoundController.Static.boostAudioControl.enabled = true;
        hasCrashed = false;
    }




    void OnTriggerEnter(Collider c)
    {


    }


    void EndMagnetPower()
    {
        if (switchOFFMagnetPower != null)
            switchOFFMagnetPower(null, null);
    }

    void OnCollisionEnter(Collision incomingCollision)
    {
        GetComponent<Rigidbody>().freezeRotation = true;  //freeze rotation oncollision with traffic so the car doesnt break
        string incTag = incomingCollision.collider.tag;

        if (incTag.Contains("trafficCar"))
        {



            if (actualSpeed > 5f)
            {

                SoundController.Static.boostAudioControl.enabled = false;
                carSpeed = 0;
                wheelSpeed = 0;
                isDoubleSpeed = 0;
                turnSpeed = 0;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                isDoubleSpeed = 1;
                actualSpeed = 0f;
                RPM = 0f;
                currentGear = 1f;
                GamePlayController.isGameEnded = true;
                if (gameEnded != null) gameEnded(null, null);
                iTween.ShakePosition(Camera.main.gameObject, new Vector3(1, 1, 1), 0.6f);


                GameObject trafficCar = incomingCollision.collider.gameObject;
                trafficCar.SendMessage("StopCar", SendMessageOptions.DontRequireReceiver);//to stop the car
                iTween.RotateTo(trafficCar, new Vector3(0, UnityEngine.Random.Range(-1, 2) * 25, 0), 1.0f);




                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                hasCrashed = true;
                HelperScript.deathCount++;
                hideNitrousParticle();
            }
            else
            {
                isDoubleSpeed -= isDoubleSpeed / 4;
                Debug.Log(isDoubleSpeed + "SPEEED");
                GameObject trafficCar = incomingCollision.collider.gameObject;
                trafficCar.SendMessage("StopCar", SendMessageOptions.DontRequireReceiver);//to stop the car
                if (trafficCar)
                    iTween.ShakePosition(Camera.main.gameObject, new Vector3(1f, 1f, 1f), 0.6f);
                if (CarController.thisPosition.x < trafficCar.transform.position.x)
                {
                    iTween.RotateTo(trafficCar, new Vector3(0, 8, 0), 2.0f);
                }
                else
                {
                    iTween.RotateTo(trafficCar, new Vector3(0, -8, 0), 2.0f);
                }

            }






        }


    }


    void FixedUpdate()
    {

        playerVelocity = GetComponent<Rigidbody>().velocity;


        if (resetOvertakesCounter > 0f)
        {
            resetOvertakesCounter -= Time.deltaTime;


        }
        else
        {
            overtakesCounter = 0;
            overtakesScoreBonus = 100f;

        }



        if (AccelerationIndicator.isAccelerating)
        {
            if (isDoubleSpeed > 0.5f)
            {
                isDoubleSpeed -= Time.deltaTime / 15f;
            }
        }
        else
        {
            hideNitrousParticle();
        }



        Accelerate();

        if (AccelerationIndicator.isAccelerating && actualSpeed > 0f)
        {


            if (maxGear == 3)
            {

                if (currentGear == 3f)
                {

                    RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 25) * 30f + actualSpeed * 430f - currentGear * 1100f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                }
                else if (currentGear == 2f)
                {
                    RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 25) * 23f + actualSpeed * 420f - currentGear * 1100f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                }
                else if (currentGear == 1f)
                {
                    RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3000f - (accelerationFactor - 25) * 20f + actualSpeed * 400f - currentGear * 1000f, Time.deltaTime * 6f), 1800, 7000f);
                }



                //1st gear - up to 52 km/h
                if (CarController.actualSpeed < maxSpeed / 18f)
                {
                    //Debug.Log ("1st Gear");
                    currentGear = 1f;
                    CarController.isDoubleSpeed += 0.0025f + accelerationFactor / 9500f;//0.0075f;

                }
                //2nd gear - between 52 and 80 km/h
                else if (CarController.actualSpeed < maxSpeed / 10.9f && CarController.actualSpeed > maxSpeed / 18f)
                {

                    if (shiftGearPause2 < 0.2f && currentGear == 1f)
                    {
                        if (!playedShiftGearSound2)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound2 = true;
                        }
                        //increment the RPM while shifting gear to create the impression that the car is tunned
                        //RPM+= 35;
                        shiftGearPause2 += Time.deltaTime;

                    }
                    else
                    {
                        //Debug.Log ("2nd Gear");
                        currentGear = 2f;
                        shiftGearPause2 = 0f;
                        playedShiftGearSound2 = false;
                        CarController.isDoubleSpeed += 0.0020f + accelerationFactor / 15000f;//0.0055f;

                    }
                }
                //3rd gear between 80 and 105 km/h
                else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 10.9f)
                {
                    //					Debug.Log ("3rd Gear");

                    if (shiftGearPause3 < 0.2f && currentGear == 2f)
                    {
                        shiftGearPause3 += Time.deltaTime;
                        if (!playedShiftGearSound3)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound3 = true;
                        }

                    }
                    else
                    {

                        CarController.isDoubleSpeed += 0.0017f + accelerationFactor / 35000f;//0.003f;
                        currentGear = 3f;
                        playedShiftGearSound3 = false;
                        shiftGearPause3 = 0f;
                        if (RPM < 3200f)
                        {
                            //RPM += 3f;
                            //RPM += actualSpeed *0.4f;
                        }
                    }

                }
                else
                {
                    RPM = RPM;

                    CarController.isDoubleSpeed += 0f;
                }
            }

            //maxSpeed < 125f
            else if (maxGear == 4f)
            {
                if (!(shiftGearPause2 > 0))
                {
                    if (currentGear == 4f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 55f + actualSpeed * 490f - currentGear * 1250f, Time.deltaTime * 6f), 2000f + currentGear * 100f, 7000f);
                    }
                    else if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 45f + actualSpeed * 490f - currentGear * 1300f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 35f + actualSpeed * 530f - currentGear * 1350f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 1f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3000f - (accelerationFactor - 50) * 20f + actualSpeed * 500f - currentGear * 1400f, Time.deltaTime * 6f), 2000f, 7000f);
                    }
                }
                //1st gear - up to 52 km/h
                if (CarController.actualSpeed < maxSpeed / 23.4f)
                {
                    //Debug.Log ("1st Gear");
                    currentGear = 1f;
                    CarController.isDoubleSpeed += 0.0034f + accelerationFactor / 9500f;//0.0075f;

                }
                //2nd gear - between 52 and 80 km/h
                else if (CarController.actualSpeed < maxSpeed / 14.5f && CarController.actualSpeed > maxSpeed / 23.4f)
                {

                    if (shiftGearPause2 < 0.2f && currentGear == 1f)
                    {
                        showNitrousParticle();
                        if (!playedShiftGearSound2)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound2 = true;
                        }
                        RPM += 30 + ((accelerationFactor - 50) / 2);
                        shiftGearPause2 += Time.deltaTime;
                        shiftSkidMarkCounter += Time.deltaTime * 2f;

                    }
                    else
                    {
                        hideNitrousParticle();
                        //Debug.Log ("2nd Gear");
                        shiftSkidMarkCounter = 0f;
                        currentGear = 2f;
                        shiftGearPause2 = 0f;
                        playedShiftGearSound2 = false;
                        CarController.isDoubleSpeed += 0.0026f + accelerationFactor / 18000f;//0.0055f;

                    }
                }
                //3rd gear between 80 and 105 km/h
                else if (CarController.actualSpeed < maxSpeed / 11.2f && CarController.actualSpeed > maxSpeed / 14.5f)
                {
                    //Debug.Log ("3rd Gear");
                    //Debug.Log (currentGear);
                    //Debug.Log (shiftGearPause);

                    if (shiftGearPause3 < 0.2f && currentGear == 2f)
                    {
                        showNitrousParticle();
                        shiftGearPause3 += Time.deltaTime;
                        if (!playedShiftGearSound3)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound3 = true;
                        }

                    }
                    else
                    {
                        hideNitrousParticle();
                        CarController.isDoubleSpeed += 0.0016f + accelerationFactor / 30000f;//0.003f;
                        currentGear = 3f;
                        playedShiftGearSound3 = false;
                        shiftGearPause3 = 0f;

                    }

                }
                //3rd gear between 80 and 105 km/h
                else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 11.2f)
                {
                    //Debug.Log ("3rd Gear");
                    //Debug.Log (currentGear);
                    //Debug.Log (shiftGearPause);

                    if (shiftGearPause4 < 0.2f && currentGear == 3f)
                    {
                        showNitrousParticle();
                        shiftGearPause4 += Time.deltaTime;
                        if (!playedShiftGearSound4)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound4 = true;
                        }

                    }
                    else
                    {
                        hideNitrousParticle();
                        CarController.isDoubleSpeed += 0.0005f + accelerationFactor / 40000f;//0.003f;
                        currentGear = 4f;
                        playedShiftGearSound4 = false;
                        shiftGearPause4 = 0f;

                    }

                }
                else
                {
                    RPM = RPM;

                    CarController.isDoubleSpeed += 0f;
                }

            }

            //maxSpeed < 150f
            else if (maxGear == 5)
            {

                if (!(shiftGearPause2 > 0) || !(shiftGearPause3 > 0))
                {
                    if (currentGear == 5f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 30f + actualSpeed * 480f - currentGear * 1200f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 4f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 480f - currentGear * 1200f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 480f - currentGear * 1200f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 15f + actualSpeed * 530f - currentGear * 1250f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 1f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3400f - (accelerationFactor - 50) * 10f + actualSpeed * 550f - currentGear * 1300f, Time.deltaTime * 6f), 2100, 7000f);
                    }
                }


                //1st gear - up to 52 km/h
                if (CarController.actualSpeed < maxSpeed / 27f)
                {
                    //Debug.Log ("1st Gear");
                    currentGear = 1f;
                    CarController.isDoubleSpeed += 0.0012f + accelerationFactor / 8000f;//0.0075f;

                }
                //2nd gear - between 52 and 80 km/h
                else if (CarController.actualSpeed < maxSpeed / 16.8f && CarController.actualSpeed > maxSpeed / 27f)
                {
                    //Debug.Log ("2nd Gear");
                    if (shiftGearPause2 < 0.2f && currentGear == 1f)
                    {
                        showNitrousParticle();
                        if (!playedShiftGearSound2)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound2 = true;
                        }
                        RPM += 40 + ((accelerationFactor - 80) / 2);
                        shiftGearPause2 += Time.deltaTime;
                        shiftSkidMarkCounter += Time.deltaTime * 2f;

                    }
                    else
                    {
                        hideNitrousParticle();
                        //Debug.Log ("2nd Gear");
                        shiftSkidMarkCounter = 0f;
                        currentGear = 2f;
                        shiftGearPause2 = 0f;
                        playedShiftGearSound2 = false;
                        CarController.isDoubleSpeed += 0.0010f + accelerationFactor / 10000f;//0.0055f;

                    }
                }
                //3rd gear between 80 and 105 km/h
                else if (CarController.actualSpeed < maxSpeed / 12.5f && CarController.actualSpeed > maxSpeed / 16.8f)
                {
                    //	Debug.Log ("3rd Gear");
                    //Debug.Log (currentGear);
                    //Debug.Log (shiftGearPause);

                    if (shiftGearPause3 < 0.2f && currentGear == 2f)
                    {
                        showNitrousParticle();
                        shiftGearPause3 += Time.deltaTime;
                        if (!playedShiftGearSound3)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound3 = true;
                        }
                        RPM += 40 + ((accelerationFactor - 80) / 2);

                    }
                    else
                    {
                        hideNitrousParticle();
                        CarController.isDoubleSpeed += 0.0008f + accelerationFactor / 24000f;//0.003f;
                        currentGear = 3f;
                        playedShiftGearSound3 = false;
                        shiftGearPause3 = 0f;

                    }

                }
                //3rd gear between 80 and 105 km/h
                else if (CarController.actualSpeed < maxSpeed / 10.3f && CarController.actualSpeed > maxSpeed / 12.5f)
                {
                    //Debug.Log ("4th Gear");
                    //Debug.Log (currentGear);
                    //Debug.Log (shiftGearPause);

                    if (shiftGearPause4 < 0.2f && currentGear == 3f)
                    {
                        showNitrousParticle();
                        shiftGearPause4 += Time.deltaTime;
                        if (!playedShiftGearSound4)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound4 = true;
                        }

                    }
                    else
                    {
                        hideNitrousParticle();
                        CarController.isDoubleSpeed += 0.0006f + accelerationFactor / 28000f;//0.003f;
                        currentGear = 4f;
                        playedShiftGearSound4 = false;
                        shiftGearPause4 = 0f;

                    }

                }
                //3rd gear between 80 and 105 km/h
                else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 10.3f)
                {

                    //Debug.Log ("5th Gear");
                    //Debug.Log (currentGear);
                    //Debug.Log (shiftGearPause);

                    if (shiftGearPause5 < 0.2f && currentGear == 4f)
                    {
                        showNitrousParticle();
                        shiftGearPause5 += Time.deltaTime;
                        if (!playedShiftGearSound5)
                        {
                            SoundController.Static.PlayShiftGearSound();
                            playedShiftGearSound5 = true;
                        }

                    }
                    else
                    {
                        hideNitrousParticle();
                        CarController.isDoubleSpeed += 0.0002f + accelerationFactor / 30000f;//0.003f;
                        currentGear = 5f;
                        playedShiftGearSound5 = false;
                        shiftGearPause5 = 0f;

                    }

                }
                else
                {
                    RPM = RPM;

                    CarController.isDoubleSpeed += 0f;
                }

            }

        }

        else
        {
            //make sure the skid marks don't remain on even if the player stops accelerating while changing gears
            shiftSkidMarkCounter = 0f;


            if (CarController.actualSpeed > 2f && !UIControl.isBrakesOn)
            { CarController.isDoubleSpeed -= Time.deltaTime / 20; }

            if (!UIControl.isBrakesOn)
            {

                //change gears, from high to low, as the car deccelerates
                if (maxGear == 3)
                {

                    if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2500f, Time.deltaTime), 2000f + currentGear * 120f, 5000f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 25) * 25f + actualSpeed * 400f - currentGear * 1450f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 5000f);
                    }
                    else if (currentGear == 1f)
                    {

                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2000f - (accelerationFactor - 25) * 20f + actualSpeed * 400f - currentGear * 1000f, Time.deltaTime * 6f), 1800, 5000f);
                    }

                    if (CarController.actualSpeed < maxSpeed / 18f)
                    {
                        if (currentGear != 1f)
                        {
                            currentGear = 1f;

                        }
                    }
                    //2nd gear - between 52 and 80 km/h
                    else if (CarController.actualSpeed < maxSpeed / 10.9f && CarController.actualSpeed > maxSpeed / 18f)
                    {
                        if (currentGear != 2f)
                        {
                            currentGear = 2f;
                            //RPM = 3000f;
                        }
                    }
                    else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 10.9f)
                    {
                        if (currentGear != 3f)
                        {
                            currentGear = 3f;
                            //RPM = 3000f;
                        }
                    }

                }
                else if (maxGear == 4f)
                {
                    if (currentGear == 4f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3000f, Time.deltaTime), 2000f + currentGear * 120f, 5000f);
                        //Debug.Log (RPM);
                    }
                    else if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 35f + actualSpeed * 450f - currentGear * 1350f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 4000f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 450f - currentGear * 1450f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 4000f);
                    }
                    else if (currentGear == 1f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 20f + actualSpeed * 400f - currentGear * 1450f, Time.deltaTime * 6f), 2000f, 4000f);
                    }



                    if (CarController.actualSpeed < maxSpeed / 23.4f)
                    {

                        if (currentGear != 1f)
                        {
                            currentGear = 1f;
                            //RPM = 3000f;
                        }
                    }
                    else if (CarController.actualSpeed < maxSpeed / 16.5f && CarController.actualSpeed > maxSpeed / 23.4f)
                    {

                        if (currentGear != 2f)
                        {
                            currentGear = 2f;
                            //RPM = 3000f;
                        }

                    }
                    else if (CarController.actualSpeed < maxSpeed / 11.2f && CarController.actualSpeed > maxSpeed / 14.5f)
                    {

                        if (currentGear != 3f)
                        {
                            currentGear = 3f;
                            //RPM = 3000f;
                        }
                    }
                    else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 11.2f)
                    {

                        if (currentGear != 4f)
                        {
                            currentGear = 4f;
                            //RPM = 3000f;
                        }

                    }


                }
                else if (maxGear == 5f)
                {

                    if (currentGear == 5f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3800f, Time.deltaTime), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 4f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 420f - currentGear * 1150f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 420f - currentGear * 1150f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 15f + actualSpeed * 450f - currentGear * 1250f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 1f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 10f + actualSpeed * 500f - currentGear * 1300f, Time.deltaTime * 6f), 2100f, 7000f);
                    }


                    if (CarController.actualSpeed < maxSpeed / 27f)
                    {
                        if (currentGear != 1f)
                        {
                            currentGear = 1f;
                            //RPM = 3000f;
                        }
                    }
                    else if (CarController.actualSpeed < maxSpeed / 16.8f && CarController.actualSpeed > maxSpeed / 27f)
                    {

                        if (currentGear != 2f)
                        {
                            currentGear = 2f;
                            //RPM = 3000f;
                        }
                    }
                    else if (CarController.actualSpeed < maxSpeed / 12.5f && CarController.actualSpeed > maxSpeed / 16.8f)
                    {

                        if (currentGear != 3f)
                        {
                            currentGear = 3f;
                            //RPM = 3000f;								
                        }

                    }
                    else if (CarController.actualSpeed < maxSpeed / 10.3f && CarController.actualSpeed > maxSpeed / 12.5f)
                    {

                        if (currentGear != 4f)
                        {
                            currentGear = 4f;
                            //RPM = 3000f;
                        }
                    }
                    else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 10.3f)
                    {
                        if (currentGear != 5f)
                        {
                            currentGear = 5f;
                            //RPM = 3000f;
                        }
                    }



                }

            }
        }






        actualSpeed = (carSpeed * isDoubleSpeed * 2f) / brakeSpeed;


        if (UIControl.isBrakesOn)
        {
            //light the taillights
            tailLights.GetComponent<Renderer>().material.SetFloat("_Metallic", 0f);
            brakeSpeed = 1;


            if (isDoubleSpeed > 0f)
            {

                if (actualSpeed > 6f)
                {
                    isDoubleSpeed -= Time.deltaTime * brakesFactor / 13f;
                }
                else if (actualSpeed > 2.4f && actualSpeed < 6f)
                {
                    isDoubleSpeed -= Time.deltaTime * brakesFactor / 22f;
                }
                else if (actualSpeed > 2f && actualSpeed < 2.4f)
                {
                    isDoubleSpeed -= Time.deltaTime * brakesFactor / 38f;
                }



                //change gears, from high to low, as the car brakes
                if (maxGear == 3)
                {

                    if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3400f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 5000f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 25) * 25f + actualSpeed * 500f - currentGear * 1450f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 5000f);
                    }
                    else if (currentGear == 1f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 25) * 20f + actualSpeed * 400f - currentGear * 1000f, Time.deltaTime * 6f), 1800, 5000f);
                    }


                    if (CarController.actualSpeed < maxSpeed / 18f)
                    {
                        currentGear = 1f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 10.9f && CarController.actualSpeed > maxSpeed / 18f)
                    {
                        currentGear = 2f;

                    }

                    else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 10.9f)
                    {
                        currentGear = 3f;
                    }

                }
                else if (maxGear == 4f)
                {

                    if (currentGear == 4f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3400f, Time.deltaTime), 2000f + currentGear * 120f, 4200f);

                    }
                    else if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 35f + actualSpeed * 450f - currentGear * 1450f, Time.deltaTime * 4f), 2000f + currentGear * 120f, 4200f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 450f - currentGear * 1400f, Time.deltaTime * 4f), 2000f + currentGear * 120f, 4200f);
                    }
                    else if (currentGear == 1f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 20f + actualSpeed * 400f - currentGear * 1400f, Time.deltaTime * 4f), 2000f, 4200f);
                    }




                    if (CarController.actualSpeed < maxSpeed / 23.4f)
                    {

                        currentGear = 1f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 14.5f && CarController.actualSpeed > maxSpeed / 23.4f)
                    {

                        currentGear = 2f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 11.2f && CarController.actualSpeed > maxSpeed / 14.5f)
                    {

                        currentGear = 3f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 11.2f)
                    {

                        currentGear = 4f;
                    }


                }
                else if (maxGear == 5f)
                {



                    if (currentGear == 5f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 3800f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 4f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 420f - currentGear * 1150f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 3f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 25f + actualSpeed * 420f - currentGear * 1150f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 2f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 15f + actualSpeed * 450f - currentGear * 1250f, Time.deltaTime * 6f), 2000f + currentGear * 120f, 7000f);
                    }
                    else if (currentGear == 1f)
                    {
                        RPM = Mathf.Clamp(Mathf.Lerp(RPM, 2400f - (accelerationFactor - 50) * 10f + actualSpeed * 500f - currentGear * 1300f, Time.deltaTime * 6f), 2100f + currentGear * 120f, 7000f);
                    }


                    if (CarController.actualSpeed < maxSpeed / 27f)
                    {
                        currentGear = 1f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 16.8f && CarController.actualSpeed > maxSpeed / 27f)
                    {
                        currentGear = 2f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 12.5f && CarController.actualSpeed > maxSpeed / 16.8f)
                    {
                        currentGear = 3f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 10.3f && CarController.actualSpeed > maxSpeed / 12.5f)
                    {
                        currentGear = 4f;
                    }

                    else if (CarController.actualSpeed < maxSpeed / 9.9f && CarController.actualSpeed > maxSpeed / 10.3f)
                    {
                        currentGear = 5f;
                    }


                }



            }


        }
        else
        {
            //turn off the taillights, when brakes are no on
            tailLights.GetComponent<Renderer>().material.SetFloat("_Metallic", 1f);
            brakeSpeed = 1;
        }




        wheelRotation = 0;
        if (moveHorizontal != 0)
        {
            wheelRotation = moveHorizontal * 10;
        }




        for (int i = 0; i < wheelObjs.Length; i++)
        {
            if (i < 2)
            {
                wheelObjs[i].Rotate(new Vector3((wheelSpeed * Time.deltaTime * Mathf.Clamp((actualSpeed / 3f), 0.99f, 5f)), 0, 0));

            }
            else
            {
                if (!UIControl.isBrakesOn)
                {
                    wheelObjs[i].Rotate(new Vector3((wheelSpeed * Time.deltaTime * Mathf.Clamp((actualSpeed / 3f), 0.99f, 5f)), 0, 0));
                }
            }


        }


        //rotate the wheels
        if (frontWheels[0] != null && frontWheels[1] != null)
        {

            float frontWheelYRotation = rotationFromAcc;
            frontWheels[0].rotation = Quaternion.Euler(frontWheels[0].rotation.eulerAngles.x, frontWheelYRotation * 85f, frontWheels[0].rotation.eulerAngles.z);
            frontWheels[1].rotation = Quaternion.Euler(frontWheels[0].rotation.eulerAngles.x, frontWheelYRotation * 85f, frontWheels[0].rotation.eulerAngles.z);
        }

        //add a handling boost based on the speed, to give a realistic sensation of speed
        if (actualSpeed < 5f)
        {
            moveHorizontalBoost = 1f;
        }
        else if (actualSpeed > 5 && actualSpeed < 7f)
        {
            moveHorizontalBoost = 1.02f;
        }
        else if (actualSpeed > 7f && actualSpeed < 9f)
        {
            moveHorizontalBoost = 1.04f;
        }
        else if (actualSpeed > 9f && actualSpeed < 12f)
        {
            moveHorizontalBoost = 1.06f;
        }
        else if (actualSpeed > 12f && actualSpeed < 13f)
        {
            moveHorizontalBoost = 1.11f;
        }
        else if (actualSpeed > 13f && actualSpeed < 15f)
        {
            moveHorizontalBoost = 1.14f;
        }

        else if (actualSpeed > 15f)
        {
            moveHorizontalBoost = 1.18f;
        }

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8


        xAccelerationInput = Mathf.Clamp(moveHorizontal * 1.1f, -0.5f, 0.5f) * moveHorizontalBoost;

        if (xAccelerationInput < 0f && xAccelerationInput > -0.25f)
        {
            moveHorizontal = Mathf.Lerp(moveHorizontal, xAccelerationInput, smoothSpeed * Time.deltaTime * (7f + handlingFactor / 30));

        }
        else if (xAccelerationInput < -0.25f)
        {
            moveHorizontal = Mathf.Lerp(moveHorizontal, xAccelerationInput, smoothSpeed * Time.deltaTime * (7f + handlingFactor / 30));

        }
        //steering right
        else if (xAccelerationInput > 0f && xAccelerationInput < 0.25f)
        {
            moveHorizontal = Mathf.Lerp(moveHorizontal, xAccelerationInput, smoothSpeed * Time.deltaTime * (7f + handlingFactor / 30));

        }
        else if (xAccelerationInput > 0.25f)
        {
            moveHorizontal = Mathf.Lerp(moveHorizontal, xAccelerationInput, smoothSpeed * Time.deltaTime * (7f + handlingFactor / 30));

        }




#endif

#if UNITY_EDITOR || UNITY_WEBPLAYER

        xAccelerationInput = Mathf.Clamp(Input.GetAxis("Horizontal") * 2.25f, -0.75f, 0.75f) * moveHorizontalBoost;

        moveHorizontal = Mathf.Lerp(myXaccel, xAccelerationInput, smoothSpeed * Time.deltaTime * 9f);

#endif

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8


        moveHorizontal = Mathf.Clamp(moveHorizontal, -0.075f - ((GetComponent<Rigidbody>().velocity.z - 1.99f) / (250f - (handlingFactor * 0.4f))), 0.075f + ((GetComponent<Rigidbody>().velocity.z - 1.99f) / (250f - (handlingFactor * 0.4f))));


#endif



        //LAKSHYA
        // TRANSFORM MOVEMENT and CLAMP
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, (carSpeed * 0.3f * isDoubleSpeed) / brakeSpeed);

        GetComponent<Rigidbody>().velocity = movement * turnSpeed;
        //this.gameObject.gameObjectposition.x = movement * turnSpeed;





        GetComponent<Rigidbody>().position = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().position.x, xLimitLeft, xLimitRight), yStart, GetComponent<Rigidbody>().position.z);


        //CAR ROTATION

        //the car rotation boost depending on the current car speed (at higher speeds it rotates more)

        zRotationBoost = 30;
        if (Application.isMobilePlatform)
        {
            //if the player car hit the margin of the road
            if (GetComponent<Rigidbody>().position.x == xLimitRight || GetComponent<Rigidbody>().position.x == xLimitLeft)
            {
                rotationFromAcc = (Mathf.Lerp(rotationFromAcc, 0f, Time.deltaTime * 20f));

                if (!reducedSpeed)
                {
                    if (isDoubleSpeed > 0.9f)
                    {
                        isDoubleSpeed -= 0.4f;
                    }
                    else
                    {
                        isDoubleSpeed = 0.5f;
                    }
                    SoundController.Static.PlayRoadSideBump();
                    reducedSpeed = true;
                }
            }
            else
            {
                //LAKSHYA
                rotationFromAcc = Mathf.Lerp(rotationFromAcc, Mathf.Clamp(moveHorizontal * 1.35f, -0.3f, 0.3f) * Mathf.Clamp(moveHorizontalBoost, 0, 1.25f), Time.deltaTime * 5f);
                reducedSpeed = false;
            }
        }
        else
        {
            //if the player car hit the margin of the road
            if (GetComponent<Rigidbody>().position.x == xLimitRight || GetComponent<Rigidbody>().position.x == xLimitLeft)
            {
                if (!reducedSpeed)
                {
                    if (isDoubleSpeed > 0.9f)
                    {
                        isDoubleSpeed -= 0.4f;
                    }
                    else
                    {
                        isDoubleSpeed = 0.5f;
                    }
                    SoundController.Static.PlayRoadSideBump();
                    reducedSpeed = true;
                }

                rotationFromAcc = (Mathf.Lerp(rotationFromAcc, 0f, Time.deltaTime * 20f));
            }
            else
            {
                rotationFromAcc = moveHorizontal;
                reducedSpeed = false;
            }
        }




        //when is accelerating rotate on x axis
        if (AccelerationIndicator.isAccelerating && accelerateRotationCounter < 3f)
        {
            brakeRotationCounter = 0f;
            //if the car is in the process of shifting gears so we have to make it look likt it stops accelerating while shifting
            if (shiftGearPause2 != 0f || shiftGearPause3 != 0f || shiftGearPause4 != 0f || shiftGearPause5 != 0f || shiftGearPause6 != 0f)
            {
                if (accelerateRotationCounter < 0f)
                {
                    accelerateRotationCounter += Time.deltaTime * 15f;
                }

            }
            else
            {
                if (accelerateRotationCounter > -maxAccelerationRotation)
                {
                    accelerateRotationCounter -= Time.deltaTime * 10f;
                }
                else if (accelerateRotationCounter > -maxAccelerationRotation && accelerateRotationCounter < -maxAccelerationRotation)
                {
                    accelerateRotationCounter -= Time.deltaTime * 10f;
                }
            }

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * 0.7f * rotationFactorZ);
            carBody.rotation = Quaternion.Euler(accelerateRotationCounter, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * 0.8f * rotationFactorZ);

        }
        //when is braking rotate on x axis with 3f
        else if (UIControl.isBrakesOn && brakeRotationCounter < 5f && actualSpeed > 2.01f)
        {
            accelerateRotationCounter = 0f;
            if (brakeRotationCounter < maxBrakesRotation)
            {
                brakeRotationCounter += Time.deltaTime * 22f;
            }



            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * 0.7f * rotationFactorZ);
            carBody.rotation = Quaternion.Euler(brakeRotationCounter, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * 0.8f * rotationFactorZ);

        }
        else
        {
            if (!(AccelerationIndicator.isAccelerating || UIControl.isBrakesOn))
            {
                if (accelerateRotationCounter < 0f)
                {
                    accelerateRotationCounter += Time.deltaTime * 15f;
                }

                brakeRotationCounter = Mathf.Lerp(brakeRotationCounter, 0f, Time.deltaTime * 12f);
            }

            if (accelerateRotationCounter != 0f)
            {

                GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * 0.7f * rotationFactorZ);
                carBody.rotation = Quaternion.Euler(accelerateRotationCounter, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * rotationFactorZ);
            }
            if (brakeRotationCounter != 0f)
            {
                GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * 0.7f * rotationFactorZ);
                carBody.rotation = Quaternion.Euler(brakeRotationCounter, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * rotationFactorZ);
            }

            if (accelerateRotationCounter == 0f && brakeRotationCounter == 0f)
            {
                GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * 0.7f * rotationFactorZ);
                carBody.rotation = Quaternion.Euler(0f, rotationFromAcc * zRotationBoost * 1f * rotationFactorY, rotationFromAcc * zRotationBoost * rotationFactorZ);
            }

        }


        acceleratia = moveHorizontal;
        testDisplay = Mathf.Round(actualSpeed * 10f).ToString();

        //SKID MARKS 
        if (actualSpeed > 10f && !UIControl.isBrakesOn)
        {


            //make skid mark only when moveHorizontal value approaches maximum
            if (moveHorizontal > 0.09f + ((GetComponent<Rigidbody>().velocity.z - 1.99f) / (250f - (handlingFactor * 0.4f))) - 0.02 && !(GetComponent<Rigidbody>().position.x == xLimitRight))
            {
                leftSkidMark.SetActive(true);
                //if the car has double wheels on the back ->  it is a truck, we need double skid marks
                if (leftSkidMark2 != null)
                {
                    leftSkidMark2.SetActive(true);
                }
                SoundController.Static.PlaySkidMarkSound(0.4f);
            }
            else if (moveHorizontal < -0.0f - ((GetComponent<Rigidbody>().velocity.z - 1.99f) / (250f - (handlingFactor * 0.4f))) + 0.02 && !(GetComponent<Rigidbody>().position.x == xLimitLeft))
            {

                rightSkidMark.SetActive(true);
                SoundController.Static.PlaySkidMarkSound(0.4f);
            }
            else if ((GetComponent<Rigidbody>().position.x == xLimitLeft) || (GetComponent<Rigidbody>().position.x == xLimitRight))
            {

                //we can also play a sound, to make it seem like the player has scratched the side of the road
                leftSkidMark.SetActive(false);
                rightSkidMark.SetActive(false);

                //if the car has double wheels on the back ->  it is a truck, we need double skid marks
                if (leftSkidMark2 != null && rightSkidMark2 != null)
                {
                    leftSkidMark2.SetActive(false);
                    rightSkidMark2.SetActive(false);
                }

                SoundController.Static.StopSkidMarkSound();
            }
            else
            {
                if (leftSkidMark.activeSelf || rightSkidMark.activeSelf)
                {
                    leftSkidMark.SetActive(false);
                    rightSkidMark.SetActive(false);
                    SoundController.Static.StopSkidMarkSound();
                }
                //if the car has double wheels on the back ->  it is a truck, we need double skid marks
                if (leftSkidMark2 != null && rightSkidMark2 != null)
                {
                    if (leftSkidMark2.activeSelf || rightSkidMark2.activeSelf)
                    {
                        leftSkidMark2.SetActive(false);
                        rightSkidMark2.SetActive(false);
                    }
                }
            }

        }
        else if (UIControl.isBrakesOn && actualSpeed > 4f)
        {
            leftSkidMark.SetActive(true);
            rightSkidMark.SetActive(true);
            SoundController.Static.PlaySkidMarkSound(2.5f);


            //if the car has double wheels on the back ->  it is a truck, we need double skid marks
            if (leftSkidMark2 != null && rightSkidMark2 != null)
            {
                leftSkidMark2.SetActive(true);
                rightSkidMark2.SetActive(true);
            }
        }
        else if (shiftSkidMarkCounter < 0.25f && shiftSkidMarkCounter != 0f)
        {
            leftSkidMark.SetActive(true);
            rightSkidMark.SetActive(true);
            SoundController.Static.PlaySkidMarkSound(1f);

            //if the car has double wheels on the back ->  it is a truck, we need double skid marks
            if (leftSkidMark2 != null && rightSkidMark2 != null)
            {
                leftSkidMark2.SetActive(true);
                rightSkidMark2.SetActive(true);
            }
        }
        else if (shiftSkidMarkCounter == 0f)
        {
            //shiftSkidMarkCounter = 0f;

            leftSkidMark.SetActive(false);
            rightSkidMark.SetActive(false);
            SoundController.Static.StopSkidMarkSound();

            //if the car has double wheels on the back ->  it is a truck, we need double skid marks
            if (leftSkidMark2 != null && rightSkidMark2 != null)
            {
                leftSkidMark2.SetActive(false);
                rightSkidMark2.SetActive(false);
            }
        }


        thisPosition = thisTrans.position;
    }



    private void Accelerate()
    {

    }

    public void switchoffmagnet()
    {

        if (switchOFFMagnetPower != null)
        {
            switchOFFMagnetPower(null, null);
        }
    }
    void showNitrousParticle()
    {
        if (particleParent != null)
            particleParent.SetActive(true);

        SoundController.Static.boostAudioControl.enabled = true;   // play acceleration sound
    }

    void hideNitrousParticle()
    {
        if (particleParent != null)
            particleParent.SetActive(false);

    }
}
