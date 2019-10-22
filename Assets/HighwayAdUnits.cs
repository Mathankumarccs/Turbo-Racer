using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighwayAdUnits : MonoBehaviour {

    public GameObject bigBillboardObj;
    public GameObject bigUnitBridge;
    public GameObject topUnitBridge;
    public Texture2D bigBillboardTexture;
    public Texture2D bigUnitBridgeTexture;
    public Texture2D topUnitBridgeTexture;
    public string unitId;

    // Use this for initialization
    void Start()
    {
        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3448", delegate (string unitID, Texture2D brandedTexture) {
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

        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3437", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on another showroom plane texture.
        //          **/
        //        bigUnitBridge.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});

        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3515", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on another showroom plane texture.
        //          **/
        //        topUnitBridge.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});

        GreedyGameAgent.Instance.registerGameObject(bigBillboardObj, bigBillboardTexture, unitId);
        GreedyGameAgent.Instance.registerGameObject(bigUnitBridge, bigUnitBridgeTexture, unitId);
        GreedyGameAgent.Instance.registerGameObject(topUnitBridge, topUnitBridgeTexture, unitId);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(bigBillboardObj);
        GreedyGameAgent.Instance.unregisterGameObject(bigUnitBridge);
        GreedyGameAgent.Instance.unregisterGameObject(topUnitBridge);
    }
}
