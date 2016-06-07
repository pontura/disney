using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ReceiverTest : MonoBehaviour {

	public	ScreenManager	screen_man;


	void Start () {
		PLANBluetoothManager.Instance.OnBluetooth += OnBluetoothEvent;


	}
	
	void Update () {
	
	}

	public	void BleSendData()
	{
	//	PLANBluetoothManager.Instance.BleSend (text_console.text);

	}

 	void OnBluetoothEvent(string	_header)
	{
		Debug.Log (_header);
		/*if (_header.Equals (PLANBluetoothManager.BLE_CONNECTED)) {
			Events.GotoTo ("Activities");

		}*/
	
	}


}
