using GreedyGame.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelAdUnit : MonoBehaviour {

    bool isBrandedTexAvailable1;
    bool isBrandedTexAvailable2;
    public GameObject adUnit1;
    public GameObject adUnit2;
    public Texture2D defaultTexture1;
    public Texture2D defaultTexture2;
    public string unitId;
    Thread _thread;
    // Use this for initialization
    void Start () {
        Debug.Log("LevelSelAdUnit-StartCoroutine called: "+ adUnit1.GetHashCode());
        //StartCoroutine("setFloatTexture");
        //GreedyGameAgent.Instance.registerGameObject(adUnit1, defaultTexture1, unitId, delegate (string unitID, Texture2D brandedTexture) {
        //    Debug.Log("LevelSelAdUnit-Inside setFloatTexture");
        //    if (brandedTexture)
        //    {
        //        if (adUnit1 != null)
        //        {
        //            Debug.Log("LevelSelAdUnit-the adunit is not null");
        //        }
        //        isBrandedTexAvailable1 = true;
        //        Sprite s = Sprite.Create(brandedTexture, new Rect(0, 0, brandedTexture.width, brandedTexture.height), new Vector2(0, 0), 1);
        //        adUnit1.GetComponent<Image>().sprite = s;
        //        Debug.Log("LevelSelAdUnit-Branded Texture 1 Available");
        //    }
        //    else
        //    {
        //        isBrandedTexAvailable1 = false;
        //        Sprite s = Sprite.Create(defaultTexture1, new Rect(0, 0, defaultTexture1.width, defaultTexture1.height), new Vector2(0, 0), 1);
        //        adUnit1.GetComponent<Image>().sprite = s;
        //        Debug.Log("LevelSelAdUnit-Branded Texture 1 not available");
        //    }
        //    Debug.Log("LevelSelAdUnit-isBrandedTexAvailable1" + isBrandedTexAvailable1);
        //});
        //GreedyGameAgent.Instance.registerGameObject(adUnit2, defaultTexture2, unitId, delegate (string unitID, Texture2D brandedTexture) {
        //    if (brandedTexture)
        //    {
        //        Debug.Log("LevelSelAdUnit-Branded Texture 1 available with width :  " + brandedTexture.width);
        //        isBrandedTexAvailable2 = true;
        //        Sprite s = Sprite.Create(brandedTexture, new Rect(0, 0, brandedTexture.width, brandedTexture.height), new Vector2(0, 0), 1);
        //        adUnit2.GetComponent<Image>().sprite = s;
        //        Debug.Log("LevelSelAdUnit-Branded Texture 2 Available");
        //    }
        //    else
        //    {
        //        isBrandedTexAvailable2 = false;
        //        Sprite s = Sprite.Create(defaultTexture2, new Rect(0, 0, defaultTexture2.width, defaultTexture2.height), new Vector2(0, 0), 1);
        //        adUnit2.GetComponent<Image>().sprite = s;
        //        Debug.Log("LevelSelAdUnit-Branded Texture 2 not available");
        //    }
        //    Debug.Log("LevelSelAdUnit-isBrandedTexAvailable2" + isBrandedTexAvailable2);
        //});
        //setFloatTexture();

    }

    public void onAdUnit1Click()
    {
        Debug.Log("LevelSelAdUnit-isBrandedTexAvailable1" + isBrandedTexAvailable1+" "+ adUnit1.GetHashCode());
        if (GGCustomRenderer.isTextureAvailable)
        {
            GreedyGameAgent.Instance.showEngagementWindow("float-3126");
            //Mixpanel.Track("Level Selection-AdUnit1 Clicked");
        }
        else
        {
            string gameUrl = "https://play.google.com/store/apps/details?id=com.greedygame.cannibalcountry";
            Application.OpenURL(gameUrl);
        }
    }

    public void onAdUnit2Click()
    {
        Debug.Log("LevelSelAdUnit-isBrandedTexAvailable2" + isBrandedTexAvailable2);
        if (GGCustomRenderer.isTextureAvailable)
        {
            GreedyGameAgent.Instance.showEngagementWindow("float-3126");
        }
        else
        {
            string gameUrl = "https://play.google.com/store/apps/details?id=com.greedygame.desihood";
            Application.OpenURL(gameUrl);
        }
    }

    public void setFloatTexture()
    {
        GreedyGameAgent.Instance.getFloatUnitTexture("float-2536", delegate (string unitID, Texture2D brandedTexture)
        {
            Debug.Log("LevelSelAdUnit-Inside setFloatTexture");
            if (brandedTexture)
            {
                if (adUnit1 != null)
                {
                    Debug.Log("LevelSelAdUnit-the adunit is not null");
                }
                isBrandedTexAvailable1 = true;
                Sprite s = Sprite.Create(brandedTexture, new Rect(0, 0, brandedTexture.width, brandedTexture.height), new Vector2(0, 0), 1);
                adUnit1.GetComponent<Image>().sprite = s;
                Debug.Log("LevelSelAdUnit-Branded Texture 1 Available");
            }
            else
            {
                isBrandedTexAvailable1 = false;
                Debug.Log("LevelSelAdUnit-Branded Texture 1 not available");
            }           
            Debug.Log("LevelSelAdUnit-isBrandedTexAvailable1" + isBrandedTexAvailable1);
        });
        GreedyGameAgent.Instance.getFloatUnitTexture("float-2536", delegate (string unitID, Texture2D brandedTexture)
        {
            if (brandedTexture)
            {
                Debug.Log("LevelSelAdUnit-Branded Texture 1 available with width :  " + brandedTexture.width);
                isBrandedTexAvailable2 = true;
                Sprite s = Sprite.Create(brandedTexture, new Rect(0, 0, brandedTexture.width, brandedTexture.height), new Vector2(0, 0), 1);
                adUnit2.GetComponent<Image>().sprite = s;
                Debug.Log("LevelSelAdUnit-Branded Texture 2 Available");
            }
            else
            {
                isBrandedTexAvailable2 = false;
                Debug.Log("LevelSelAdUnit-Branded Texture 2 not available");
            }
            Debug.Log("LevelSelAdUnit-isBrandedTexAvailable2" + isBrandedTexAvailable2);
        });
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnDestroy()
    {
        GreedyGameAgent.Instance.unregisterGameObject(adUnit1);
        GreedyGameAgent.Instance.unregisterGameObject(adUnit2);
    }
}
