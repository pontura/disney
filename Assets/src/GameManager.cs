using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int gameID;

    void Start()
    {
        Events.SetGameId += SetGameId;
    }
    void SetGameId(int _gameID)
    {
        this.gameID = _gameID;
	}
}
