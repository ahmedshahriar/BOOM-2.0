using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

   // public AudioMixer masterMixer;
    public Slider volume; 

   // public float yourVolume = 1f;

    /*  public void SetSound(float soundLevel)
      {
          masterMixer.SetFloat("musicVol", soundLevel);
      } */


  /*  public void SetSound(float yourVolume)
    {
       
        AudioListener.volume = yourVolume;
    }   */
    private void Update()
    {
        AudioListener.volume = volume.value;
    }






}
