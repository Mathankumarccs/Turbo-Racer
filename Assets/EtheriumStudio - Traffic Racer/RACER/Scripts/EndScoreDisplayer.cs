 using UnityEngine;
using System.Collections;
using System;
using GreedyGame.Runtime;

public class EndScoreDisplayer : MonoBehaviour {

	// Use this for initialization

	public TextMesh cashText,distancetext,highSpeedText,OvertakesText, overtakesTextCash, CheckpointsText, CheckpointsTextCash, distanceTextCash, highspeedTextCash, scoreText, availableCashText;
	private float coins, distance, overtakes;
	public GameObject playAgainButton,mainMenuButton;
    public GameObject adUnitBtn;
    public GameObject scoreboardTextObj;
	public Vector3[] originalPositions;
    public static bool isBrandTexAvailable;
    public string unitId;
    public GameObject NewSticker;
	private bool madeHighScore;
    public Texture2D adUnitBtnTexture;

	private bool doubleCash = false;

    private void Start()
    {
        Debug.Log("AdUnit-start");

        //GreedyGameAgent.Instance.registerGameObject(adUnitBtn, adUnitBtnTexture, unitId, delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        isBrandTexAvailable = true;
        //        /**
        //          *TODO: Apply brandedTexture on whichever button you need to brand.
        //          **/
        //        Debug.Log("EndScoreDisplayer-Branded Image Found");
        //        adUnitBtn.GetComponent<Renderer>().material.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        isBrandTexAvailable = false;
        //        adUnitBtn.GetComponent<Renderer>().enabled = false;
        //        Debug.Log("EndScoreDisplayer-No branded Image");
        //    }
        //});
        //GreedyGameAgent.Instance.getFloatUnitTexture("float-2483", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        isBrandTexAvailable = true;
        //        /**
        //          *TODO: Apply brandedTexture on whichever button you need to brand.
        //          **/
        //        Debug.Log("EndScoreDisplayer-Branded Image Found");
        //        adUnitBtn.GetComponent<Renderer>().material.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        isBrandTexAvailable = false;
        //        adUnitBtn.GetComponent<Renderer>().enabled = false;
        //        Debug.Log("EndScoreDisplayer-No branded Image");
        //    }
        //});
        //GreedyGameAgent.Instance.getNativeUnitTexture("unit-3518", delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        /**
        //          *  * TODO: Apply brandedTexture on another showroom plane texture.
        //          **/
        //        scoreboardTextObj.GetComponent<Renderer>().material.mainTexture = brandedTexture;
        //    }
        //    else
        //    {
        //        Debug.Log("No Branded Image");
        //    }
        //});
    }

    void OnEnable( )
	{
		if(PlayerPrefs.GetInt("CashDoubler",0) == 1)
		{
			doubleCash = true;	
		}
		else 
		{
			doubleCash = false;
		}
        originalPositions[0] = playAgainButton.transform.localPosition;
		originalPositions[1] = mainMenuButton.transform.localPosition;
        originalPositions[2] = adUnitBtn.transform.localPosition;

        playAgainButton.transform.Translate(40,0,0);
		mainMenuButton.transform.Translate(-40,0,0);
        adUnitBtn.transform.Translate(-50,0,0);
        cashText.text="0";
		distancetext.text="0";
		OvertakesText.text = "0";
		Invoke ("startDistanceCount", 0.8f);



		//PlayerPrefs.DeleteKey("TotalCoins");
		iTween.ColorTo(cashText.gameObject,iTween.Hash("color",Color.red,"time",1.0f,"delay",1.2f ));

		 
		PlayerPrefs.SetInt("TotalCoins",PlayerPrefs.GetInt("TotalCoins",0 ) + GamePlayController.totalCash) ;
		availableCashText.text = PlayerPrefs.GetInt ("TotalCoins", 0).ToString();
		//to stop bgsounds on GameoVer
		SoundController.Static.BgSoundsObj.SetActive (false);
		SoundController.Static.PlayCarCrashSound ();

	}


    void changeCashText(float newValue)
	{

		 cashText.text = ""+ Mathf.RoundToInt( newValue);


		SoundController.Static.playCoinHit2();
	}

	void startDistanceCount()
	{

		int bestScore = PlayerPrefs.GetInt( Application.loadedLevelName + "HighScore",0);

		if(GamePlayController.finalScore > bestScore)
		{
			NewSticker.SetActive(true);
			PlayerPrefs.SetInt(Application.loadedLevelName + "HighScore",(int)GamePlayController.finalScore);
			iTween.ScaleTo(NewSticker,new Vector3(0.8f,0.8f,0.8f),0.5f);

		}

		scoreText.text = GamePlayController.finalScore.ToString ("F0");
		distancetext.text = GamePlayController.distanceTravelled.ToString("F2") + "Km";
		distanceTextCash.text = GamePlayController.distanceCash.ToString(); 

		SoundController.Static.playCoinHit();

		iTween.ColorTo(distancetext.gameObject,Color.red,1.0f);
		Invoke ("HighSpeed", 0.3f);
	}

	void changeDistanceText(float newValue)
	{
		SoundController.Static.playCoinHit2();
		distancetext.text = ""+ Mathf.RoundToInt(  newValue )+"m";
	}
	 
	void HighSpeed()
	{

		SoundController.Static.playCoinHit();
		highSpeedText.text = GamePlayController.totalHighSpeed.ToString("F2") + "'s";
		highspeedTextCash.text = GamePlayController.highSpeedCash.ToString(); 
		Invoke("ShowOvertakes",0.3f);

	}

	void ShowOvertakes()
	{
		SoundController.Static.playCoinHit();
		OvertakesText.text = GamePlayController.totalOvertakes.ToString();
		overtakesTextCash.text = GamePlayController.overtakesCash.ToString(); 

		Invoke("ShowCheckpoints",0.3f);
	}

	void ShowCheckpoints()
	{
		SoundController.Static.playCoinHit();

		CheckpointsText.text = GamePlayController.checkpointsCount.ToString();
		CheckpointsTextCash.text = GamePlayController.checkpointsCash.ToString();
		iTween.ValueTo(gameObject,iTween.Hash("from",coins,"to",GamePlayController.totalCash,"time",0.5,"easetype",iTween.EaseType.easeInOutCubic,
		                                      "onupdate","changeCashText","delay",0.5f,"oncomplete","showButtons") );
	}



	void showButtons()
	{
		GamePlayController.checkpointOffset = 0;
		GamePlayController.checkpointsCash = 0;
		GamePlayController.checkpointsCount = 0;
		GamePlayController.distTraveled = 0;

		GamePlayController.distanceCash = 0;
		GamePlayController.highSpeedCash = 0;
		GamePlayController.overtakesCash = 0;


		CarController.distanceToZeroOnZ = 0;
		CarController.thisPosition = new Vector3(0,0,0);
		SoundController.Static.PlaySlider();

		iTween.MoveTo(playAgainButton,iTween.Hash("position",originalPositions[0] ,"time",0.5f,"easetype",iTween.EaseType.easeInOutBounce,"islocal",true ) );
		iTween.MoveTo(mainMenuButton,iTween.Hash("position",originalPositions[1],"time",0.5f,"easetype",iTween.EaseType.easeInOutBounce,"islocal",true,"delay",0.6f ) );
        iTween.MoveTo(adUnitBtn, iTween.Hash("position", originalPositions[2], "time", 0.5f, "easetype", iTween.EaseType.easeInOutBounce, "islocal", true, "delay",1));
	}

    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(adUnitBtn);
    }
}
