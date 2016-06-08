using UnityEngine;
using System.Collections;

public class Connecting : ScreenMain {

    public GameObject connectingPanel;
    public GameObject readyPanel;
	bool	is_connected;

    public states state;
    private bool canceled;
    public enum states
    {
        IDLE,
        CONNECTING,
        READY
    }


    


	void Start()
	{
		PLANBluetoothManager.Instance.OnBluetooth += OnBluetoothEvent;
		is_connected = false;
	}

    override public void OnFocus() {       

		PLANBluetoothManager.Instance.Init("Logitech X50",0);
        connectingPanel.SetActive(true);
        readyPanel.SetActive(false);

        state = states.CONNECTING;

        //HARDCODE:
        Invoke("OnConnection", 2);
        /////////////////////////////////
    }



    public void Back()
    {
        canceled = true;
        Events.GotoBackTo("ConnectDevice");
    }
    public void Go()
    {
        Events.GotoTo("Activities");
    }
    void OnConnection()
    {
        if (canceled) return;
		is_connected = true;
        connectingPanel.SetActive(false);
        readyPanel.SetActive(true);
	}

	void BluetoothPair()
	{
		if (!is_connected) {
			PLANBluetoothManager.Instance.BluetoothPair ("Logitech X50");
		} else {
			OnConnection ();
		}
	}

	void OnBluetoothEvent(string	_header)
	{
		Debug.Log ("DESDE BLUETOOTH " + _header);
		if (_header.Equals ("Init")) {
			// si vuelve esto está todo bien
			//	llamo a conectar
			BluetoothPair();

		}else if (_header.Equals ("Connected")) {
			if (!is_connected) {
				OnConnection ();
			}
		}else if (_header.Equals ("ScanEnd")) {
			//	termino el escaneo
			//	y no encontro nada
			Invoke("BluetoothPair",3);

		} else if (_header.Equals ("Disconnected")) {
			state = states.CONNECTING;

		} else if (_header.Equals ("Rejected")) {
			//	nos desconecto
			is_connected = false;
		}

		/*if (_header.Equals (PLANBluetoothManager.BLE_CONNECTED)) {
			Events.GotoTo ("Activities");

		}*/

	}


}
