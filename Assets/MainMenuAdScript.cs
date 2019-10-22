using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreedyGame.Runtime;

public class MainMenuAdScript : MonoBehaviour {

    public Texture2D texture;
    public string unitId;

    // Use this for initialization
    void Start () {
        GreedyGameAgent.Instance.registerGameObject(this.gameObject,texture,unitId);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
