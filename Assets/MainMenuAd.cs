using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAd : MonoBehaviour {

    public GameObject gameTitle;
    // Use this for initialization

    void Start () {
        Debug.Log("MainMenuAd-start of refresh");
            //GreedyGameAgent.Instance.startEventRefresh();
        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3542", delegate (string unitID, Texture2D brandedTexture)
        //{
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on showroom plane texture.
        //          **/
        //        gameTitle.GetComponent<Renderer>().material.mainTexture = brandedTexture;
        //        Debug.Log("MainMenuAd-Branded Image Found");
        //    }
        //    else
        //    {
        //        Debug.Log("MainMenuAd-No Branded Image");
        //    }
        //});

        //GreedyGameAgent.Instance.fetchFloatUnit("float-2483");
    }
    // Update is called once per frame

}
