using System.Collections;
using System.Collections.Generic;
using GreedyGame.Runtime;
using UnityEngine;
using UnityEngine.Advertisements;



public class AdsRefresh : MonoBehaviour
{
    public int Seconds;
    public int Flag=1;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag == 1)
        {
            StartCoroutine(WaitForRefresh());
        }
    }

    public IEnumerator WaitForRefresh()
    {

            Flag = 0;
            Debug.Log("GG[Ads] Refresh");
            yield return new WaitForSeconds(Seconds);
        Debug.Log("GG[Ads] Refresh 2");
        GreedyGameAgent.Instance.startEventRefresh();
        Debug.Log("GG[Ads] Refresh 3");

        Flag = 1;
        
    }

}
