using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoomWall : MonoBehaviour {
    public GameObject Wallobj, Wallobj1;
    public Texture2D defaultTexture;
    public string unitId;

    // Use this for initialization
    void Start () {

        Debug.Log("ShowRoom-Register Game Object called with unitId: " + unitId);
        GreedyGameAgent.Instance.registerGameObject(Wallobj, defaultTexture, unitId, true);
        GreedyGameAgent.Instance.registerGameObject(Wallobj1, defaultTexture, unitId, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(Wallobj);
        GreedyGameAgent.Instance.unregisterGameObject(Wallobj1);
    }
}
