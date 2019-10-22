using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountrysideAdUnits : MonoBehaviour {

    public GameObject bigBillboardObj;
    public GameObject smallBillboardObj;
    public GameObject sideStandObj;
    public Texture2D bigBillboardTexture;
    public Texture2D smallBillboardTexture;
    public Texture2D sideStandTexture;
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
        //        sideStandObj.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
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

        GreedyGameAgent.Instance.registerGameObject(bigBillboardObj, bigBillboardTexture, unitId);
        GreedyGameAgent.Instance.registerGameObject(smallBillboardObj, smallBillboardTexture, unitId);
        GreedyGameAgent.Instance.registerGameObject(sideStandObj, sideStandTexture, unitId);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(bigBillboardObj);
        GreedyGameAgent.Instance.unregisterGameObject(smallBillboardObj);
        GreedyGameAgent.Instance.unregisterGameObject(sideStandObj);
    }
}
