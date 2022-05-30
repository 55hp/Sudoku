using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggle : MonoBehaviour
{

    /// <summary>
    /// It turn on and off the audio for the game.
    /// </summary>
    public void AudioToggler()
    {
        if(AudioListener.pause == false)
            AudioListener.pause = true;
        else
            AudioListener.pause = false;
    }

}
