using UnityEngine;
using System.Collections;
using System;
using GreedyGame.Runtime;

public class UIControl : MonoBehaviour
{
    public Camera UICamera;



    public GameObject pauseMenu, gameOverMenu, coinIngameCointainer, distanceInGameContainer, pauseButton, NitrousUiParent;
    public RaycastHit hit;
    public Texture[] buttonTex, pauseButtonTex, brakeButtonTex, nitrousButton, leftButtonTex, rightButtonTex;
    public Renderer pauseButtonRenderer, brakeRenderer, nitrousButtonRenderer, changeCameraButtonRenderer,
                    leftButtonRenderer, rightButtonRenderer;
    public static bool isBrakesOn = false;
    public Renderer[] buttonRenders;
    public Transform nitrousTransform, brakeTransform, mainMenu, playAgain;

    private Vector3 initialScale;


    public TextMesh coinsText;
    private float wheelRotation;

    void OnEnable()
    {

        CarController.gameEnded += onGameEnd;
    }
    void OnDisable()
    {
        CarController.gameEnded -= onGameEnd;
    }

    void onGameEnd(System.Object obj, System.EventArgs args)
    {
        pauseMenu.SetActive(false);
        coinIngameCointainer.SetActive(false);
        distanceInGameContainer.SetActive(false);
        NitrousUiParent.SetActive(false);
        pauseButton.SetActive(false);
    }
    void downState(Vector3 a)
    {

        Ray ray = UICamera.ScreenPointToRay(a);

        if (Physics.Raycast(ray, out hit, 500))
        {
            string objName = hit.collider.name;
            SoundController.Static.PlayButtonClickSound();
            switch (objName)
            {
                case "PlayAgain":


                    SoundController.Static.PlayButtonClickSound();



                    break;
                case "mainmenu":

                    SoundController.Static.PlayButtonClickSound();
                    break;
                case "fShare":

                    break;
                case "resume":
                    SoundController.Static.PlayButtonClickSound();

                    break;
                case "pauseIngame":
                    SoundController.Static.PlayButtonClickSound();

                    break;

                case "changeCameraIngame":
                    SoundController.Static.PlayButtonClickSound();

                    break;

                    //case "NitrousButton":

                    //        for (int i = 0; i < Input.touchCount; i++)
                    //        {
                    //            if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Stationary)
                    //            {
                    //                AccelerationIndicator.Static.isAccelerationPressed = true; // is accelerating
                    //                nitrousButtonRenderer.material.mainTexture = nitrousButton[1];
                    //                UIControl.isBrakesOn = false;
                    //                brakeRenderer.material.mainTexture = brakeButtonTex[0];
                    //            }
                    //            if(Input.GetTouch(i).phase == TouchPhase.Moved)
                    //            {
                    //                AccelerationIndicator.Static.isAccelerationPressed = false;
                    //                nitrousButtonRenderer.material.mainTexture = nitrousButton[0];
                    //            }
                    //        }


                    //        //addedd
                    //        //AccelerationIndicator.Static.isAccelerationPressed = false;
                    //        //CarController.isDoubleSpeed=1.0f;
                    //        //CarController.isDoubleSpeed -= Time.deltaTime * 2;

                    //        break;

                    //case "BrakeButton":
                    //        for (int i = 0; i < Input.touchCount; i++)
                    //        {
                    //            if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
                    //            {
                    //                AccelerationIndicator.Static.isAccelerationPressed = false; // is accelerating
                    //                nitrousButtonRenderer.material.mainTexture = nitrousButton[0];
                    //                if (CarController.actualSpeed > 2.01f)
                    //                {
                    //                    UIControl.isBrakesOn = true;
                    //                }
                    //                else
                    //                {
                    //                    UIControl.isBrakesOn = false;
                    //                }
                    //                brakeRenderer.material.mainTexture = brakeButtonTex[1];
                    //            }
                    //        }

                    //        break;


            }

        }

    }
    void upState(Vector3 a)
    {



        Ray ray = UICamera.ScreenPointToRay(a);

        if (Physics.Raycast(ray, out hit, 500))
        {

            string objName = hit.collider.name;

            switch (objName)
            {
                case "PlayAgain":
                    GamePlayController.isGameEnded = false;
                    Application.LoadLevel(Application.loadedLevelName);
                    break;
                case "mainmenu":


                    Time.timeScale = 1;
                    Application.LoadLevel("NewMainMenu");
                    break;
                case "fShare":
                    Debug.Log("fb share post");

                    break;
                case "resume":

                    Time.timeScale = 1;
                    pauseButton.SetActive(true);
                    pauseMenu.SetActive(false);
                    coinIngameCointainer.SetActive(true);
                    distanceInGameContainer.SetActive(true);
                    NitrousUiParent.SetActive(true);

                    break;
                case "pauseIngame":


                    SoundController.Static.boostAudioControl.volume = 0;
                    Time.timeScale = 0;
                    pauseMenu.SetActive(true);
                    coinIngameCointainer.SetActive(false);
                    distanceInGameContainer.SetActive(false);
                    NitrousUiParent.SetActive(false);
                    pauseButton.SetActive(false);
                    GreedyGameAgent.Instance.startEventRefresh();

                    break;

                case "changeCameraIngame":

                    if (carCamera.cameraMode == 3)
                    {
                        carCamera.cameraMode = 1;
                        carCamera.cameraChanged = false;
                    }
                    else if (carCamera.cameraMode == 2)
                    {
                        carCamera.cameraMode = 3;
                        carCamera.cameraChanged = false;
                    }
                    else if (carCamera.cameraMode == 1)
                    {
                        carCamera.cameraMode = 2;
                        carCamera.cameraChanged = false;
                    }
                    break;

                    //case "NitrousButton":
                    //    AccelerationIndicator.Static.isAccelerationPressed = false;
                    //    nitrousButtonRenderer.material.mainTexture = nitrousButton[0];
                    //    break;

                    //case "BrakeButton":
                    //    //if (CarController.actualSpeed > 2.01f)
                    //    //{
                    //    //    UIControl.isBrakesOn = true;
                    //    //}
                    //    //else
                    //    //{
                    //    //    UIControl.isBrakesOn = false;
                    //    //}
                    //    UIControl.isBrakesOn = false;
                    //    brakeRenderer.material.mainTexture = brakeButtonTex[0];
                    //    break;

            }
        }


    }
    // Use this for initialization
    void Start()
    {
        CarController.moveHorizontal = 0;
    }


    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            downState(Input.mousePosition);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            upState(Input.mousePosition);
        }

        //when he stops acceleration
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightControl))
        {

            AccelerationIndicator.Static.isAccelerationPressed = false;

            CarController.isDoubleSpeed -= Time.deltaTime / 4;
            AccelerationIndicator.isAccelerating = false;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightControl))
        {


            AccelerationIndicator.Static.isAccelerationPressed = false;
            nitrousButtonRenderer.material.mainTexture = nitrousButton[0];

            CarController.isDoubleSpeed -= Time.deltaTime / 4;
            AccelerationIndicator.isAccelerating = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightControl))
        {

            AccelerationIndicator.Static.isAccelerationPressed = true;
            nitrousButtonRenderer.material.mainTexture = nitrousButton[1];

        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (CarController.actualSpeed > 2.01)
            {
                isBrakesOn = true;
            }
            else
            {
                isBrakesOn = false;
            }
            brakeRenderer.material.mainTexture = brakeButtonTex[1];
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            isBrakesOn = false;
            brakeRenderer.material.mainTexture = brakeButtonTex[0];
        }

        if (Application.isMobilePlatform)
        {
            //handle all the touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                Debug.Log("Debuggg Touch Count: " + i);


                if (Input.GetTouch(i).phase == TouchPhase.Stationary)
                {
                    //get the touch position
                    downStateMovement(Input.GetTouch(i).position);
                }
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    upStateMovement(Input.GetTouch(i).position);
                }
                if(Input.GetTouch(i).phase== TouchPhase.Began)
                {
                    touchStateMovement(Input.GetTouch(i).position);
                }

            }

            //reset the button textures and accelerate/brakes variables when there are no touches
            if (Input.touchCount == 0)
            {
                Debug.Log("Debuggg Touch Count: no touch  ");
                AccelerationIndicator.Static.isAccelerationPressed = false;
                nitrousButtonRenderer.material.mainTexture = nitrousButton[0];
                leftButtonRenderer.material.mainTexture = leftButtonTex[0];
                rightButtonRenderer.material.mainTexture = rightButtonTex[0];
                UIControl.isBrakesOn = false;
                brakeRenderer.material.mainTexture = brakeButtonTex[0];
            }

        }

    }

    private void touchStateMovement(Vector2 position)
    {
        Ray ray = UICamera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hit, 500))
        {
            string objName = hit.collider.name;
            switch (objName)
            {
                //case "adButton":
                //    if (EndScoreDisplayer.isBrandTexAvailable)
                //    {
                //        GreedyGameAgent.Instance.showEngagementWindow("float-3126");
                //    }
                //    break;
            }
        }
    }

    private void upStateMovement(Vector2 position)
    {
        AccelerationIndicator.Static.isAccelerationPressed = false;
        nitrousButtonRenderer.material.mainTexture = nitrousButton[0];
        UIControl.isBrakesOn = false;
        leftButtonRenderer.material.mainTexture = leftButtonTex[0];
        rightButtonRenderer.material.mainTexture = rightButtonTex[0];
        brakeRenderer.material.mainTexture = brakeButtonTex[0];
        CarController.moveHorizontal = 0;
    }

    private void downStateMovement(Vector2 position)
    {
        Debug.Log("Debugg X Position: ");
        Debug.Log("Debugg X Position value : " + position.x);
        Ray ray = UICamera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hit, 500))
        {
            string objName = hit.collider.name;
            //SoundController.Static.PlayButtonClickSound();
            switch (objName)
            {
                case "NitrousButton":
                    Debug.Log("DEBUGGGG1 NitrousButton called");
                    AccelerationIndicator.Static.isAccelerationPressed = true; // is accelerating
                    nitrousButtonRenderer.material.mainTexture = nitrousButton[1];
                    UIControl.isBrakesOn = false;
                    brakeRenderer.material.mainTexture = brakeButtonTex[0];
                    //addedd
                    //AccelerationIndicator.Static.isAccelerationPressed = false;
                    //CarController.isDoubleSpeed=1.0f;
                    //CarController.isDoubleSpeed -= Time.deltaTime * 2;
                    break;

                case "BrakeButton":
                    Debug.Log("DEBUGGGG1 BrakeButton called");
                    AccelerationIndicator.Static.isAccelerationPressed = false; // is accelerating
                    nitrousButtonRenderer.material.mainTexture = nitrousButton[0];
                    if (CarController.actualSpeed > 2.01f)
                    {
                        UIControl.isBrakesOn = true;
                    }
                    else
                    {
                        UIControl.isBrakesOn = false;
                    }
                    brakeRenderer.material.mainTexture = brakeButtonTex[1];
                    break;

                case "RightButton":
                    Debug.Log("DEBUGGGG1 RightButton called");
                    CarController.moveHorizontal = 0.1f;
                    CarController.rotationFromAcc = 0.15f;
                    leftButtonRenderer.material.mainTexture = leftButtonTex[0];
                    rightButtonRenderer.material.mainTexture = rightButtonTex[1];

                    break;
                case "LeftButton":
                    Debug.Log("DEBUGGGG1 LeftButton called");
                    CarController.moveHorizontal = -0.1f;
                    CarController.rotationFromAcc = -0.15f;
                    leftButtonRenderer.material.mainTexture = leftButtonTex[1];
                    rightButtonRenderer.material.mainTexture = rightButtonTex[0];
                    break;
            }
        }
    }


#if (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8) && !UNITY_EDITOR

//		//orientation change
//		if ((Screen.orientation == ScreenOrientation.Portrait) || (Screen.orientation == ScreenOrientation.PortraitUpsideDown) ) 
//		{
//			nitrousTransform.localPosition = new Vector3(-7,-16.17969f,0);
//			brakeTransform.localPosition = new Vector3(7,-16.17969f,0);
//		}
//		else if ((Screen.orientation == ScreenOrientation.LandscapeLeft) || (Screen.orientation == ScreenOrientation.LandscapeRight) ) 
//		{
//			nitrousTransform.localPosition =  new Vector3(0,-16.17969f,0);
//			brakeTransform.localPosition = new Vector3(0,-16.17969f,0);
//		}
#endif
}
