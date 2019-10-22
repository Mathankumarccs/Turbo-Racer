using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreedyGame.Runtime;

public class TrafficPrototype : MonoBehaviour {

    public GameObject bigBillboardObj;
    public GameObject smallBillboardObj;

	// Use this for initialization
	void Start () {
        GreedyGameAgent.Instance.getNativeUnitTexture("unit-3437", delegate (string unitID, Texture2D brandedTexture) {
            if (brandedTexture)
            {
                /**
                  *  * TODO: Apply brandedTexture on showroom plane texture.
                  **/
                bigBillboardObj.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
            }
            else
            {
                Debug.Log("No Branded Image");
            }
        });

        GreedyGameAgent.Instance.getNativeUnitTexture("unit-3448", delegate (string unitID, Texture2D brandedTexture) {
            if (brandedTexture)
            {
                /**
                  *  * TODO: Apply brandedTexture on another showroom plane texture.
                  **/
                smallBillboardObj.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
            }
            else
            {
                Debug.Log("No Branded Image");
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
