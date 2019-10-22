using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public GameObject loadingSpin;
    string LevelName;

    public void loadLevel(string levelName)
    {
        LevelName = levelName;
        loadingSpin.SetActive(true);
        gameObject.SetActive(false);
        Invoke("loadGameLevel", 2.0f);
    }

    public void backBtnPressed()
    {
        loadingSpin.SetActive(true);
        gameObject.SetActive(false);
        Invoke("loadCarScene", 2.0f);
    }

    void loadGameLevel()
    {
        SceneManager.LoadScene(LevelName);
    }

    void loadCarScene()
    {
        SceneManager.LoadScene("CarSelectionMenu");
    }
}
