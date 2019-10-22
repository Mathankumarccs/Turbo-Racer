using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelAdUnit1 : MonoBehaviour {

    bool isBrandedTexAvailable1;
    public GameObject adUnit1;
    // Use this for initialization
    void Start () {
        GreedyGameAgent.Instance.getFloatUnitTexture("float-2536", delegate (string unitID, Texture2D brandedTexture)
        {
            if (brandedTexture)
            {
                isBrandedTexAvailable1 = true;
                adUnit1.GetComponent<Renderer>().material.mainTexture = brandedTexture;
                Debug.Log("LevelSelAdUnit1-Branded Texture Available");
            }
            else
            {
                isBrandedTexAvailable1 = false;
                Debug.Log("LevelSelAdUnit1-Branded Texture not available");
            }
        });
    }

    public void onAdUnit1Click()
    {
        if (isBrandedTexAvailable1)
        {
            GreedyGameAgent.Instance.showEngagementWindow("float-2536");
            //Mixpanel.Track("Level Selection-AdUnit1 Clicked");
        }
        else
        {
            string gameUrl = "https://play.google.com/store/apps/details?id=com.greedygame.cannibalcountry";
            Application.OpenURL(gameUrl);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
