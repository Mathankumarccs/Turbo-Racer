using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adUnit : MonoBehaviour {

    Texture2D buttonTexture;
    bool isBrandTexAvailable;
    public string unitId;
    // Use this for initialization
    void Start () {
        Debug.Log("AdUnit-start");
            //GreedyGameAgent.Instance.getFloatUnitTexture("float-2483", delegate (string unitID, Texture2D brandedTexture) {
            //    if (brandedTexture)
            //    {
            //        isBrandTexAvailable = true;
            //        /**
            //          *TODO: Apply brandedTexture on whichever button you need to brand.
            //          **/
            //        Debug.Log("AdUnit-Branded Image Found");
            //        buttonTexture = brandedTexture;
            //        Debug.Log("AdUnit " + "Width: " + buttonTexture.width + " Height: " + buttonTexture.height);
            //    }
            //    else
            //    {
            //        isBrandTexAvailable = false;
            //        Debug.Log("AdUnit-No branded Image");
            //    }
            //});
        GreedyGameAgent.Instance.registerGameObject(this.gameObject, buttonTexture, unitId, delegate (string unitID, Texture2D brandedTexture, bool isBranded) {
            if (brandedTexture)
            {
                isBrandTexAvailable = true;
                /**
                  *TODO: Apply brandedTexture on whichever button you need to brand.
                  **/
                Debug.Log("AdUnit-Branded Image Found");
                buttonTexture = brandedTexture;
                Debug.Log("AdUnit " + "Width: " + buttonTexture.width + " Height: " + buttonTexture.height);
            }
            else
            {
                isBrandTexAvailable = false;
                Debug.Log("AdUnit-No branded Image");
            }
        });
    }

    private void OnGUI()
    {
        float rectWidth=0;
        float rectHeight=0;
        int unitWidth = 200;
        int unitHeight = 190;
        Debug.Log("AdUnit-onGUI");
        if (isBrandTexAvailable)
        {
            float brandedTexWidth = buttonTexture.width;
            float brandedTexHeight = buttonTexture.height;
            float texAspectRatio = buttonTexture.width / buttonTexture.height;
            float factor = 2.2f;
            if(brandedTexWidth>brandedTexHeight)
            {
                rectWidth = unitWidth;
                rectHeight = rectWidth / texAspectRatio;
            }
            else
            {
                rectHeight = unitHeight;
                rectWidth = rectHeight * texAspectRatio;
            }
            //making button background transparent
            GUI.backgroundColor=Color.clear;
            //if button is created with texture passed as parameter
            if (GUI.Button(new Rect(20, Screen.height / factor, rectWidth, rectHeight), buttonTexture))
            {
                Debug.Log("AdUnit-if block");
                GreedyGameAgent.Instance.showEngagementWindow(unitId);

            }
            else
            {
                Debug.Log("AdUnit-else block");
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
