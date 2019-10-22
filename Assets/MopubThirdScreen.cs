using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopubThirdScreen : MonoBehaviour {

    public string interstitialId3 = "281439c8f5b448f39ba70694372f2c6f";


    private void Start()
    {
        // Create and instantiate an SdkConfiguration

        // Publishers can override MoPubEventListener.OnSdkInitializedEvent() to get a callback.
        // Alternatively, publishers can call "yield return WaitUntil(() => MoPub.IsSdkInitialized)" to suspend the coroutine execution until the SDK has initialized.
    }

    public void OnGUI()
    {
        
        if (GUI.Button(new Rect(10, 10, 100, 40), "MoPub Show Interstitial"))
        {
            MoPub.ShowInterstitialAd(interstitialId3);
        }


    }
}
