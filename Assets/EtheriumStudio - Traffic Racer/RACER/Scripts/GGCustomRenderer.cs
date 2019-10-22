using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreedyGame.Runtime;
using UnityEngine.UI;

public class GGCustomRenderer : MonoBehaviour {

    RawImage rawImage;
    public Texture defaultTexture;
    public string unitId;
    public static bool isTextureAvailable;

	// Use this for initialization
	void Start () {
        // Use this api to register delegates which will send you the branded texture.
        // The below delegate is an example which uses raw image to render image.
        // You should make sure that the actual rendering of the object is done inside the delegate
        GreedyGameAgent.Instance.registerGameObject(this.gameObject, defaultTexture as Texture2D, unitId, delegate (string unitId, Texture2D brandedTexture, bool isBranded)
         {
             if (GreedyGameAgent.Instance.getNativeUnitPathById(unitId) == null)
             {
                 // means the ad is not available for this unit
                 // disable the object 
                 return;
             }

             if (brandedTexture != null)
             {
                 isTextureAvailable = true;
                 //Step Delegate-A
                 rawImage = GetComponent<RawImage>();
                 if(rawImage!=null)
                 {
                     rawImage.texture = brandedTexture as Texture;
                 }
             } else
             {
                 //Step Delegate-B 
                 isTextureAvailable=false;
                 rawImage = GetComponent<RawImage>();
                 if (rawImage != null)
                 {
                     rawImage.texture = defaultTexture;
                 }
             }
         });
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(this.gameObject);
    }
}
