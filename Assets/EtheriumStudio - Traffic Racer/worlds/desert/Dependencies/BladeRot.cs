﻿using UnityEngine;
using System.Collections;

public class BladeRot : MonoBehaviour {


	public float Speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0, 0, Time.deltaTime * Speed));
	}
}
