using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public float volume;
       
	public void Start () {
        GetComponent<AudioSource>().loop = false;
        OnMusicVolumeChanged(1);

        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnMusicChange += OnMusicChange;
	}
    void OnDestroy()
    {
        Events.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        Events.OnMusicChange -= OnMusicChange;
    }
    void OnMusicChange(string soundName)
    {
        print("OnMusicChange" + soundName);
        if (soundName == "") StopAllSounds();

        GetComponent<AudioSource>().clip = Resources.Load("music/" + soundName) as AudioClip;
        GetComponent<AudioSource>().Play();
    }
    
    void OnMusicVolumeChanged(float value)
    {
        GetComponent<AudioSource>().volume = value;
        volume = value;
    }
   
    void StopAllSounds()
    {
        GetComponent<AudioSource>().Stop();
    }
}



