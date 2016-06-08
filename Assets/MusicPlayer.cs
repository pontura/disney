using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class MusicPlayer : MonoBehaviour {

    public Slider slider;
    public GameObject container;
    public MusicButton musicButton;
    public int songID;

    public Song[] songs;
    public Song activeSong;
    public List<MusicButton> buttons;
    private MusicButton ActiveButton;
    [Serializable]
    public class Song
    {
        public string title;
        public AudioClip clip;
    }
    public bool playing;
    public float totalDuration;
    public float songTime;
    public AudioSource audioSource;

	void Start () {
        Events.OnSelectSong += OnSelectSong;
        Events.OnToogleMusicState += OnToogleMusicState;

        int id = 0;

        foreach (Song song in songs)
        {
            MusicButton newMusicButton = Instantiate(musicButton);
            newMusicButton.transform.SetParent(container.transform);
            newMusicButton.transform.localScale = Vector3.one;
            newMusicButton.Init(id, song);
            id++;
            buttons.Add(newMusicButton);
        }
	}
    void OnDestroy()
    {
        Events.OnSelectSong -= OnSelectSong;
        Events.OnToogleMusicState -= OnToogleMusicState;
    }	
    void OnSelectSong(Song song, int id)
    {
        songID = id;
        activeSong = song;
        StartPlaying();
        foreach (MusicButton musicButton in buttons)
        {
            if (musicButton.id == id)
            {
                musicButton.SetState(true);
                ActiveButton = musicButton;
            }
            else
                musicButton.SetState(false);
        }
	}
    void OnToogleMusicState()
    {
        if (playing)
            Pause();
        else
            Resume();
    }
    void StartPlaying()
    {
        songTime = 0;
        totalDuration = activeSong.clip.length;
        print("OnSelectSong " + activeSong.title);
        audioSource.clip = activeSong.clip;
        audioSource.Play();
        playing = true;
    }
    public void Pause()
    {
        playing = false;
        audioSource.Pause();
        ActiveButton.SetPaused(true);
    }
    public void Resume()
    {
        playing = true;
        audioSource.Play();
        ActiveButton.SetPaused(false);
    }
    void Update()
    {
        if (playing)
        {
            songTime += Time.deltaTime;
            if (songTime >= totalDuration)
                PlayNextTrack();
            slider.value = songTime / totalDuration;
        }
    }
    public void PlayNextTrack()
    {
        songID++;
        if (songID >= songs.Length)
            songID = 0;
        OnSelectSong(songs[songID], songID);
    }
    public void PlayPrevTrack()
    {
        songID--;
        if (songID < 0)
            songID = songs.Length-1;
        OnSelectSong(songs[songID], songID);
    }
}
