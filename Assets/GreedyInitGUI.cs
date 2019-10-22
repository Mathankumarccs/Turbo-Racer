using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreedyGame.Commons;
using GreedyGame.Platform;
using GreedyGame.Runtime;

public class GreedyInitGUI : MonoBehaviour {

    public List<string> unitList;

    public string gameId;

    public bool AdmobMediation = false;

    public bool FacebookMediation = false;

    public bool MopubMediation = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 200, 100), "Init")) {
            if (RuntimePlatform.Android == Application.platform || RuntimePlatform.IPhonePlayer == Application.platform)
            {
                GGAdConfig adConfig = new GGAdConfig();
                adConfig.setListener(new GreedyAgentListener());
                adConfig.setGameId(gameId);
                adConfig.enableAdmobMediation(AdmobMediation);
                adConfig.enableFacebookMediation(FacebookMediation);
                adConfig.enableMopubMediation(MopubMediation);
                adConfig.disableReflection(false);
                adConfig.addUnitList(unitList);
                GreedyGameAgent.Instance.init(adConfig);
                //Task.Delay(1000).ContinueWith(t => GreedyGameAgent.Instance.startEventRefresh());
            }
        }
    }

    public class GreedyAgentListener : IAgentListener
    {

        public void onAvailable(string campaignId)
        {
            /**
         * TODO: New campaign is available and ready to use for the next scene.
         **/
            Debug.Log("GreedyCampaignLoader onAvailable");

        }

        public void onUnavailable()
        {
            /**
         * TODO: No campaign is available, proceed with normal flow of the game.
         **/
            Debug.Log("GreedyCampaignLoader onUnavailable");
        }

        public void onFound()
        {
            /**
         * TODO: Campaign is found. Starting download of assets. This will be followed by onAvailable callback once download completes successfully.
         **/
            Debug.Log("GreedyCampaignLoader onFound");
        }

        public void onError(string error)
        {
            /**
         * TODO: No Campaign will be served since the initialization resulted in an error. 
         * If device api level is below 15 this callback is invoked.
         **/
            Debug.Log("GreedyCampaignLoader onError");
        }
    }


}
