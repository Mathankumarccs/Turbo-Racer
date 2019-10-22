using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	
	// Use this for initialization
	public GameObject overtakesGameObject;
	public AudioClip Slider,buttonsClickSound,paintWheelsClickSound,CarCrashSound, SkidMarkSound, OvertakeWooshSound, PoliceSound, IceCreamTruckSound, TrafficCarBump, RoadSideBump,AccidentCarHornSound, AccidentCarHornSound2, checkpointSound;

	public AudioClip coinHitSound;
	public AudioClip coinHitSound2;
	public AudioClip shiftGearSound;

	public AudioClip sedanEngineSound, muscleEngineSound, sportEngineSound;
	
	
	public static SoundController Static ;
	public AudioSource[]  audioSources;
	public AudioSource boostAudioControl;
	public AudioSource brakesAudioControl;
	public AudioSource overtakesWooshAudioControl;
	public AudioSource genericAudioSource;
	public GameObject BgSoundsObj;

	float rpmPitchDivisionFactor;    //how fast does the pitch increment based on rpm
	float rpmVolumeDivisionFactor;		//how fast does the volume rise based on the rp
	float basePitch;		//the base pitch for the engine soundm to which we will add a portion of rpm divided by a specified factor
	float baseVolume;		//the base volume of the engine sound, to which we will add a portion ofrpm divided by a specified factor
	public float volume;			//the current volume
	float pitch;			//the current pitch
	float RPM;			//the current RPM value
	
	void Start () {

		boostAudioControl.volume = 0;
		RPM=0;
		if(Application.loadedLevel > 1)
		{

			if(carSelection.carTransformName != null)
			{
				if(carSelection.carTransformName == "Classic" || carSelection.carTransformName == "BMW" || carSelection.carTransformName == "Mitsu")
				{
					boostAudioControl.clip = sedanEngineSound;
					basePitch = 0.1f;
					baseVolume = 0.3f;

					rpmPitchDivisionFactor = 6000;
					rpmVolumeDivisionFactor = 10000;
				}
				else if(carSelection.carTransformName == "Lambo")
				{
					boostAudioControl.clip = sportEngineSound;
					basePitch = 0.1f;
					baseVolume = 0.2f;

					rpmPitchDivisionFactor = 4800;
					rpmVolumeDivisionFactor = 20000;
				}

				else if (carSelection.carTransformName == "Mustang" || carSelection.carTransformName == "Truck")
				{
					boostAudioControl.clip = muscleEngineSound;
					basePitch = 0.45f;
					baseVolume = 0.3f;

					rpmPitchDivisionFactor = 4000;
					rpmVolumeDivisionFactor = 18000;
				}
			}
			//if we run the level scene without going through the menu
			else
			{
				boostAudioControl.clip = sedanEngineSound;
				basePitch = 0.1f;
				baseVolume = 0.3f;
				
				rpmPitchDivisionFactor = 6000;
				rpmVolumeDivisionFactor = 10000;
			}
		}
	
		volume = 0.399f;
		Static = this;

		//toStop bg music on mainMenu and Splash Screen
		if (Application.loadedLevel < 2) {
			boostAudioControl.volume = 0f;
			BgSoundsObj.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//if the game is not paused

			RPM = CarController.RPM; 
			if(RPM > 0f)
			{				
				pitch =  basePitch + (RPM/rpmPitchDivisionFactor);
				volume = baseVolume + RPM/rpmVolumeDivisionFactor;
			}
			else
			{
				pitch=0f;
				volume = 0f;
			}				
			
			boostAudioControl.volume = volume;  
			boostAudioControl.pitch = Mathf.Clamp(pitch, 0.4f, 2.5f);
	
		
	
	}

	
	public void PlaySkidMarkSound(float volume)
	{


			brakesAudioControl.volume =	volume;

			

		brakesAudioControl.mute = false;

	}
	
	public void StopSkidMarkSound()
	{
		brakesAudioControl.mute = true;
	}

	public void PlayOvertakeWooshSound(float volume)
	{

		overtakesWooshAudioControl.pitch = Random.Range (0.8f, 1.6f);
		overtakesWooshAudioControl.PlayOneShot (OvertakeWooshSound, 2.5f);
	}

	public void StopOvertakeWooshSound()
	{
		overtakesWooshAudioControl.mute = true;

	}
	
	
	
	public void PlayButtonClickSound()
	{
		
		//disable button click sound effect (so it wont click when accelerating or using brakes)
		if(Application.loadedLevel < 2)
		{
			genericAudioSource.PlayOneShot (buttonsClickSound, 1f);		
		}
	}

	public void PlayPaintWheelClickSound()
	{
		
		//disable button click sound effect (so it wont click when accelerating or using brakes)
		if(Application.loadedLevel < 2)
		{
			genericAudioSource.PlayOneShot (paintWheelsClickSound, 1f);		
		}
	}

	public void PlayCheckpointSound()
	{		
		
		genericAudioSource.PlayOneShot (checkpointSound, 2f);		
	}


	public void PlayCarCrashSound()
	{		
		
		genericAudioSource.PlayOneShot (CarCrashSound, 1.5f);		
	}

	public void PlayAccidentCarHornSound()
	{		

		int rnd = Random.Range(0,2);


		if(rnd == 0)
		{
			genericAudioSource.PlayOneShot (AccidentCarHornSound, 1f);
		}
		else if(rnd == 1)
		{
			genericAudioSource.PlayOneShot (AccidentCarHornSound2, 1f);
		}
	}

	public void PlayTrafficCarBump()
	{
		genericAudioSource.PlayOneShot(TrafficCarBump,1.5f);
	}

	public void PlayRoadSideBump()
	{
		genericAudioSource.PlayOneShot(RoadSideBump, 1f);
	}
	
	public void playCoinHit()
	{
		
		genericAudioSource.PlayOneShot(coinHitSound, 3f);
	}
	public void playCoinHit2()
	{
		genericAudioSource.PlayOneShot(coinHitSound2);
	}

	public void playPoliceSound()
	{
		genericAudioSource.PlayOneShot(PoliceSound, 1f);
	}
	public void playIceCreamTruckSound()
	{

		genericAudioSource.PlayOneShot(IceCreamTruckSound, 3f);

	}
	

	public void PlaySlider()
	{
		genericAudioSource.PlayOneShot(Slider, 1f);

	}
	
	public void PlayShiftGearSound()
	{
		
		genericAudioSource.PlayOneShot(shiftGearSound, 2f);
	
	}
	
	
	
	
	
	void swithAudioSources( AudioClip clip)
	{
		if(audioSources[0].isPlaying) 
		{
			audioSources[1].PlayOneShot(clip);
		}
		else audioSources[0].PlayOneShot(clip);

		
	}
}
