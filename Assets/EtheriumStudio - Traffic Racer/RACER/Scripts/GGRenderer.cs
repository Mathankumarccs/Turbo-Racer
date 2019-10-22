using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreedyGame.Runtime;

public class GGRenderer : MonoBehaviour {

    public Texture2D texture;
    public string unitId;
    public static bool isBrandedTexAvailable;


    // Use this for initialization
    void Start () {
        // Attach this script to an object that needs branding. Make sure that the object has 
        // mesh or sprite renderer attached to it.
        //GreedyGameAgent.Instance.registerGameObject(this.gameObject, texture, unitId,true);
        GreedyGameAgent.Instance.registerGameObject(this.gameObject, texture, unitId, delegate (string unitID, Texture2D brandedTexture, bool isBranded) {
            if (brandedTexture)
            {
                if (!isBranded)
                {
                
                    isBrandedTexAvailable = false;
                    Debug.Log("GGT-Texture not available");
                }
                else
                {
                    Debug.Log("GGT- " + GreedyGameAgent.Instance.getClickableUnitPath(unitID));
                    /**
                      *TODO: Apply brandedTexture on whichever button you need to brand.
                      **/
                    Debug.Log("GGT-Texture available");
                    isBrandedTexAvailable = true;
                    if (this.gameObject.GetComponent<Renderer>() != null)
                    {
                        Debug.Log("GGT-Renderer found width and height" + brandedTexture.width + "    " + brandedTexture.height);
                        this.gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
                    }
                    else
                    {
                        Debug.Log("GGT-Renderer not found");
                    }
                }
            }
        });

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Destroy
    void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(this.gameObject);
        isBrandedTexAvailable = false;
    }
}
