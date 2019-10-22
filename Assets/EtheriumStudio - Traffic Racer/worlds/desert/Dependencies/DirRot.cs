using UnityEngine;
using System.Collections;

public class DirRot : MonoBehaviour {

	public float Degrees;
	public float Speed;
	public float TimeS;

	public Vector3 Initial;
	public Vector3 Final;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Initial = new Vector3(0,0,0);
		Final = new Vector3(0,Degrees, 0);

		TimeS = (Mathf.Sin (Time.time * Speed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
		transform.eulerAngles = Vector3.Lerp (Initial, Final, TimeS);	
	}
}
