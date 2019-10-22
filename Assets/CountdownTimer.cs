using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreedyGame.Runtime;

public class CountdownTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(StartCountdown());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator StartCountdown()
 {
     
     while (true)
     {
         yield return new WaitForSeconds(70.0f);
         GreedyGameAgent.Instance.startEventRefresh();
     }
 }
}
