using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelAdUnit2 : MonoBehaviour {

    bool isBrandedTexAvailable2;
    public GameObject adUnit2;
    // Use this for initialization
    void Start () {
        GreedyGameAgent.Instance.getFloatUnitTexture("float-2544", delegate (string unitID, Texture2D brandedTexture)
        {
            if (brandedTexture)
            {
                isBrandedTexAvailable2 = true;
                adUnit2.GetComponent<Renderer>().material.mainTexture = brandedTexture;
                Debug.Log("LevelSelAdUnit2-Branded Texture Available");
            }
            else
            {
                isBrandedTexAvailable2 = false;
                Debug.Log("LevelSelAdUnit2-Branded Texture not available");
            }
        });
    }

    public void onAdUnit2Click()
    {
        if (isBrandedTexAvailable2)
        {
            GreedyGameAgent.Instance.showEngagementWindow("float-2544");
        }
        else
        {
            string gameUrl = "https://play.google.com/store/apps/details?id=com.greedygame.desihood";
            Application.OpenURL(gameUrl);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
