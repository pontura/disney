using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public float volume;
       
	public void Start () {
        GetComponent<AudioSource>().loop = false;
        OnMusicVolumeChanged(1);

        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnMusicChange += OnMusicChange;
        Events.OnVoice += OnVoice;
        Events.GotoBackTo -= GotoBackTo;
        Events.GotoTo -= GotoBackTo;
	}
    void OnDestroy()
    {
        Events.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        Events.OnMusicChange -= OnMusicChange;
        Events.OnVoice -= OnVoice;
        Events.GotoTo -= GotoTo;
        Events.GotoBackTo -= GotoBackTo;
    }
    bool voiceOn;
    float voiceOnTime;
    void OnVoice(string soundName)
    {
        voiceOnTime = 0;
        voiceOn = true;
        print("OnVoice: " + soundName);
        if (soundName == "") StopAllSounds();

        GetComponent<AudioSource>().clip = Resources.Load("saludos/" + soundName) as AudioClip;
        GetComponent<AudioSource>().Play();
        if (GetComponent<AudioSource>().clip == null || GetComponent<AudioSource>().clip.length < 0.1f)
        {
            Events.OnVoiceNotExists();
        }
    }
    void Update()
    {
        if (voiceOn)
        {
            if (GetComponent<AudioSource>().clip == null)
            {
                Reset();
                return;
            }
            voiceOnTime += Time.deltaTime;
            if (voiceOnTime > (float)GetComponent<AudioSource>().clip.length)
            {
                Reset();
                Events.OnVoiceReady();
            }
        }
    }
    void GotoTo(string n)
    {
        Reset();
        
    }
    void GotoBackTo(string n)
    {
        Reset();
    }
    void Reset()
    {
        StopAllSounds();
        voiceOnTime = 0;
        voiceOn = false;
        print("READY: ");
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



