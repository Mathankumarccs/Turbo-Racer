using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desert_adunits : MonoBehaviour {

    public GameObject windmill_plane_unit;
    public GameObject sidestand_plane_unit;
    public GameObject bigboard_plane_unit;
    public Texture2D windmill_plane_texture;
    public Texture2D sidestand_plane_texture;
    public Texture2D bigboard_plane_texture;
    public string unitId;

    // Use this for initialization
    void Start()
    {
        GreedyGameAgent.Instance.registerGameObject(windmill_plane_unit, windmill_plane_texture, unitId);
        GreedyGameAgent.Instance.registerGameObject(sidestand_plane_unit, sidestand_plane_texture, unitId);
        GreedyGameAgent.Instance.registerGameObject(bigboard_plane_unit, bigboard_plane_texture, unitId);
        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3437", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on showroom plane texture.
        //          **/
        //        windmill_plane_unit.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
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
        //        bigboard_plane_unit.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
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
        //        sidestand_plane_unit.GetComponent<Renderer>().sharedMaterial.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(windmill_plane_unit);
        GreedyGameAgent.Instance.unregisterGameObject(sidestand_plane_unit);
        GreedyGameAgent.Instance.unregisterGameObject(bigboard_plane_unit);
    }
}
