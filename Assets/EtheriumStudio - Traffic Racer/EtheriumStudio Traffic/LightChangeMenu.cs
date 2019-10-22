using UnityEngine;
using System.Collections;

public class LightChangeMenu : MonoBehaviour {



	float x=1;
	float y=1;
	float z=2;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(x > 1.9)
		{
			x = Mathf.Lerp(x,1,Time.deltaTime *13);
		}

		if(gameObject.transform.localScale.x < 1.1)
		{
			x = Mathf.Lerp(x,2,Time.deltaTime *13);
		}

		Debug.Log (x);

		gameObject.transform.localScale = new Vector3(x,x,2f);

	}
}
	

		
	








