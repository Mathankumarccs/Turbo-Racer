using System.Collections;
using System.Collections.Generic;
using GreedyGame.Runtime;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public string UnitId;

    void OnMouseDown()
    {

        GreedyGameAgent.Instance.showEngagementWindow(UnitId);

    }

    public void Clickbutton()
    {
        GreedyGameAgent.Instance.showEngagementWindow(UnitId);
    }
}
