using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showroom : MonoBehaviour {
    public GameObject planeObj, planeObj1;
    public Texture2D defaultTexture;
    public string unitId1, unitId2;

    // Use this for initialization
    void Start () {
        //GreedyGameAgent.Instance.removeFloatUnit("float-2483");
        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3437", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on showroom plane texture.
        //          **/
        //        planeObj.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});

        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3448", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on another showroom plane texture.
        //          **/
        //        planeObj1.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});
        Debug.Log("ShowRoom-Register Game Object called with unitId: " + unitId1 +" "+unitId2);
        GreedyGameAgent.Instance.registerGameObject(planeObj, defaultTexture, unitId1);
        GreedyGameAgent.Instance.registerGameObject(planeObj1, defaultTexture, unitId2);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(planeObj);
        GreedyGameAgent.Instance.unregisterGameObject(planeObj1);
    }
}
