using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingProgress : MonoBehaviour {

    // Use this for initialization

    public Vector3 rotationDirection;

    IEnumerator Start()
    {

        yield return new WaitForSeconds(1);
        Debug.Log("LoadingProgress");
        SceneManager.LoadScene("CarSelectionMenu");
        //Async
    }


    // Update is called once per frame
    void Update()
    {

        //transform.Rotate (rotationDirection * Time.deltaTime);
    }
}
