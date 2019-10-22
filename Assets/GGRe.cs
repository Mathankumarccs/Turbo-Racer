using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreedyGame.Runtime;
using GreedyGame.Platform;
using GreedyGame.Commons;


public class GGRe : MonoBehaviour {

    public Texture2D defaultTexture = null;

    public string unitId = "float-3126";

	// Use this for initialization
	void Start () {
        GreedyGameAgent.Instance.registerGameObject(this.gameObject, defaultTexture, unitId);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(this.gameObject);
    }
}
