using UnityEngine;
using System.Collections;

public class TotalCoins : MonoBehaviour {

	// Use this for initialization
	void Start () {
		staticInstance = this ;			
		UpdateCoins();
//		if(PlayerPrefs.GetInt("TotalCoins",0) == 0)
//		{
//			AddCoins(2500000);
//		}
	}
	void Update()
	{
		coinsTxt.text = totalCoins.ToString();
	}

	// Update is called once per frame
	public   int totalCoins=0;
	public TextMesh coinsTxt;
	public static TotalCoins staticInstance ;

    void UpdateCoins () {
	 
		totalCoins = PlayerPrefs.GetInt("TotalCoins",0 );
		coinsTxt.text = ""+ totalCoins;
	}

	public void AddCoins (int coins) {

		PlayerPrefs.SetInt("TotalCoins",PlayerPrefs.GetInt("TotalCoins",0 ) + coins) ;
		UpdateCoins();
	}

	public void deductCoins (int coins) {
		PlayerPrefs.SetInt("TotalCoins",PlayerPrefs.GetInt("TotalCoins",0 ) - coins) ;
		UpdateCoins();
	 }

	public void ClearCoins()
	{
		PlayerPrefs.DeleteAll();
		UpdateCoins();
	}
}
