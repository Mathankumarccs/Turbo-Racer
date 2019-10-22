using GreedyGame.Runtime;
using GreedyGame.Commons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class menuBtnAction : MonoBehaviour {


    public Camera uiCamera;
    public GameObject loadingProgress;
    public GameObject menuBack;
    public GameObject adUnit;
    public GameObject floatUnit;
    //bool isBrandedTexAvailable;
    // Use this for initialization
    void Start () {
        GreedyGameAgent.Instance.startEventRefresh();
        //refreshMenuFloatTexture();
	}

    //public void refreshMenuFloatTexture()
    //{
    //    Debug.Log("menu-getFloatUnitTexture");
    //    GreedyGameAgent.Instance.getFloatUnitTexture("float-2536", delegate (string unitID, Texture2D brandedTexture)
    //    {
    //        Debug.Log("menuBtnAction-Inside start GG");
    //        if (brandedTexture)
    //        {
    //            isBrandedTexAvailable = true;
    //            adUnit.GetComponent<Renderer>().material.mainTexture = brandedTexture;
    //            Debug.Log("menuBtnAction-Branded Texture Available");
    //        }
    //        else
    //        {
    //            isBrandedTexAvailable = false;
    //            Debug.Log("menuBtnAction-Branded Texture not available");
    //        }
    //    });
    //}

    // Update is called once per frame
    void Update () {
        //show admob banner if it is not null, and it is not visible already
        //		if(ad_mob_banner.banner1 != null && !HelperScript.isAdMobBannerVisible)
        //		{
        //			ad_mob_banner.banner1.Show();
        //			HelperScript.isAdMobBannerVisible = true;
        //		}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!MouseDrag.isDrag)
            {
                //				MouseDown(Input.mousePosition );
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!MouseDrag.isDrag)
            {
                Debug.Log("menuBtnAction-MouseUp called");
                MouseUp(Input.mousePosition);
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            //Application.Quit();
        }
        //if (!MouseDrag.isMousePressed)
        //{
        //    foreach (GameObject go in menuButtons)
        //    {
        //        go.GetComponent<Renderer>().material.mainTexture = buttonTexture[0];
        //    }
        //}
    }

    void MouseUp(Vector3 a)
    {
        Ray ray = uiCamera.ScreenPointToRay(a);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {

            switch (hit.collider.name)
            {
                case "Play":
                    //carSelection.SetActive(true);
                    Debug.Log("menuBtnAction-Play called");
                    loadingProgress.SetActive(true);
                    gameObject.SetActive(false);
                    menuBack.SetActive(false);
                    //SceneManager.LoadScene("CarSelectionMenu", LoadSceneMode.Single);

                    //Application.LoadLevel("CarSelectionMenu");
                    //gameObject.SetActive(false);
                    break;

                case "adUnit":
                    if (GGRenderer.isBrandedTexAvailable)
                    {
                        GreedyGameAgent.Instance.showEngagementWindow("float-4144");
                        GGAdConfig adConfig = new GGAdConfig();

                    }
                    else
                    {
                        string gameUrl = "https://play.google.com/store/apps/details?id=com.greedygame.desihood";
                        Application.OpenURL(gameUrl);
                    }
                    break;

                case "more":

                    string url = "https://play.google.com/store/apps/developer?id=GreedyGame+Media";
                    Application.OpenURL(url);
                    break;
                case "rateUs":

                    string rateurl = "https://play.google.com/store/apps/developer?id=GreedyGame+Media";
                    Application.OpenURL(rateurl);
                    break;
                case "quit":
                    Application.Quit();
                    break;

            }
        }

    }
    //	void MouseDown(Vector3 a )
    //	{
    //		
    //		Ray ray = uiCamera.ScreenPointToRay(a);
    //		
    //		if (Physics.Raycast(ray, out hit, 500))
    //		{
    //			SoundController.Static.PlayButtonClickSound();
    //
    //			switch(hit.collider.name)
    //			{
    //			case "Play":
    //				menuButtonRenders[0].material.mainTexture  = buttonTexture[1];
    //				break;
    //			case "Store":
    //				menuButtonRenders[1].material.mainTexture  = buttonTexture[1];
    //				break;
    //
    //			case "more":
    //				menuButtonRenders[2].material.mainTexture  = buttonTexture[1];
    //				break;
    //			case "RateUs":
    //				menuButtonRenders[3].material.mainTexture  = buttonTexture[1];
    //				break;
    //			case "Quit":
    //				menuButtonRenders[4].material.mainTexture  = buttonTexture[1];
    //				break;
    //				
    //			}
    //
    //			 
    //		}
    //		
    //	}
}
