﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLogoTween : MonoBehaviour {

    // Use this for initialization

    public enum Tweenbuttons
    {

        inGameEndScroller,
        slideToLeftSide,
        slideToRightSide,
        bringToTop,
        bringToDown,
        slideToLeftSideFast,
        slideToRightSideFast

    }

    public Tweenbuttons buttonTween;
    Vector3 startPos;

    void OnEnable()
    {
        startPos = transform.localPosition;
        switch (buttonTween)
        {

            case Tweenbuttons.slideToRightSide:

                transform.Translate(20, 0, 0);
                iTween.MoveTo(gameObject, iTween.Hash("position", startPos, "time", 1.0, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack));

                if (SoundController.Static != null)
                {
                    SoundController.Static.PlaySlider();
                }

                break;

            //used for HighSpeedUI
            case Tweenbuttons.slideToRightSideFast:

                transform.Translate(20, 0, 0);
                iTween.MoveTo(gameObject, iTween.Hash("position", startPos, "time", 0.7, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack));

                break;


            case Tweenbuttons.slideToLeftSide:

                transform.Translate(-20, 0, 0);
                iTween.MoveTo(gameObject, iTween.Hash("position", startPos, "time", 1.0, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack));

                if (SoundController.Static != null)
                {
                    SoundController.Static.PlaySlider();
                }

                break;

            //Used for OvertakesUI
            case Tweenbuttons.slideToLeftSideFast:

                transform.Translate(-20, 0, 0);
                iTween.MoveTo(gameObject, iTween.Hash("position", startPos, "time", 0.5, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack));


                break;


            case Tweenbuttons.bringToTop:
                transform.Translate(0, 40, 0);
                iTween.MoveTo(gameObject, iTween.Hash("position", startPos, "time", 1.0, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack));

                if (SoundController.Static != null)
                {
                    SoundController.Static.PlaySlider();
                }


                break;

            //used for BuyPopUp
            case Tweenbuttons.bringToDown:
                transform.Translate(0, -40, 0);
                iTween.MoveTo(gameObject, iTween.Hash("position", startPos, "time", 0.5, "isLocal", true, "easetype", iTween.EaseType.linear));



                break;

            case Tweenbuttons.inGameEndScroller:

                iTween.MoveTo(gameObject, iTween.Hash("position", Vector3.zero, "time", 1.0, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack));
                if (SoundController.Static != null)
                {
                    SoundController.Static.PlaySlider();
                }

                break;

        }


    }
    void OnDisable()
    {
        transform.localPosition = startPos;
    }
}