using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour {

    public GameObject PausedGO;
    public GameObject onGO;
    public GameObject offGO;
    public int id;
    public Text field;
    public MusicPlayer.Song song;
    public bool isOn;

	public void Init(int id, MusicPlayer.Song song) {
        this.song = song;
        this.id = id;
        field.text = song.title;
        PausedGO.SetActive(false);
	}
    public void SetState(bool isOn)
    {
        this.isOn = isOn;
        if (isOn) onGO.SetActive(true);
        else onGO.SetActive(false);
    }
    public void Clicked()
    {
        if (!isOn)
            Events.OnSelectSong(song, id);
        else
            Events.OnToogleMusicState();
    }
    public void SetPaused(bool isPaused)
    {
        PausedGO.SetActive(isPaused);
    }
}
