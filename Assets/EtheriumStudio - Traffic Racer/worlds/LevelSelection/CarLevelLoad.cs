using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarLevelLoad : MonoBehaviour {

    IEnumerator Start()
    {

        yield return new WaitForSeconds(2);
        Debug.Log("LoadingProgress");
        SceneManager.LoadScene("LevelSelection");
        //Async
    }
}
