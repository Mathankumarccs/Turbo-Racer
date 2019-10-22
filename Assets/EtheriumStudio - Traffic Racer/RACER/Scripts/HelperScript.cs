using UnityEngine;
using System.Collections;

public class HelperScript : MonoBehaviour {

	public static double score = 0;			//the score of the player
	public static int deathCount = 0;     //used to count how many times the player died, or pressed back/reload button when being stuck, 
										// to know when to show the Interstitial ad
	public static int deathCountAdColony = 0;
	public static string levelName = null;  //the name of the current level
	public static bool startupAdShown = false;

	public static bool startAppBannerVisibility = false;

	public static bool moreGamesVisibility = false;

	public static bool isAdMobBannerVisible = false;

	// Use this for initialization
	void Start () {
	
	}
	

}
