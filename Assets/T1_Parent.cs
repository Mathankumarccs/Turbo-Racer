using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1_Parent : MonoBehaviour {

    public GameObject bigBillboardObj;
    public GameObject smallBillboardObj;
    public GameObject bigSidestand;
    public string unitId;
    public Texture2D bigBillboardTexture;
    public Texture2D smallBillboardTexture;
    public Texture2D bigSidestandTexture;

    // Use this for initialization
    void Start () {

        Debug.Log("City-Register Game Object called with unitId: "+unitId);
        GreedyGameAgent.Instance.registerGameObject(bigBillboardObj, bigBillboardTexture, unitId, true);
        GreedyGameAgent.Instance.registerGameObject(smallBillboardObj, smallBillboardTexture, unitId, true);
        GreedyGameAgent.Instance.registerGameObject(bigSidestand, bigSidestandTexture, unitId, true);
        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3437", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on showroom plane texture.
        //          **/
        //        bigBillboardObj.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});

        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3513", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on another showroom plane texture.
        //          **/
        //        smallBillboardObj.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
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
        //        bigSidestand.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        Debug.Log("City-Unregister Game Object called with unitId: " + unitId);
        GreedyGameAgent.Instance.unregisterGameObject(bigBillboardObj);
        GreedyGameAgent.Instance.unregisterGameObject(smallBillboardObj);
        GreedyGameAgent.Instance.unregisterGameObject(bigSidestand);
    }
}
