using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PLANBluetoothManager : MonoBehaviour {


	//	emprolijar

	bool			tog = false;
	AndroidJavaClass	android_class;
	AndroidJavaObject	android_activity;
	bool				paring;
	public				bool	BLE;

	private				bool	connected;
	AudioSource			audio;
	//	Events
	public delegate void OnBluetoothEvent(string	_header);
	public event OnBluetoothEvent OnBluetooth;

	//	Definiciones
	public		static	string 	BLE_MODE = "BLE";
	public		static	string	BLUETOOTH_MODE = "BLUETOOTH";

	public 		static	string	BLE_INIT = "Init";	//	Inicia el setup para conectar con device
	public		static	string	BLE_SCAN_START = "ScanStart";	//	Inicia la busqueda de devices
	public		static	string	BLE_DEVICE_FOUND = "DeviceFound";	//	Encontro el device que buscaba
	public		static	string	BLE_CONNECTED = "Connected";	//	Conecto
	public		static	string	BLE_DISCONNECTED = "Disconnected";	//	Perdio la conexion
	public		static	string	BLE_RECEIVED = "Received";	//
	public		static	string	BLE_NOT_SUPPORTED = "BleNotSupported";
	public		static	string	BLUETOOTH_OFF = "BluetoothOff";
	public		static	string	BLUETOOTH_NULL	= "BluetoothNull";

	//BleNotSupported
	//BluetoothNull
	//BluetoothOff
	public		RectTransform	panel_reconnecting;
	private		string			device_name;


	private  	static PLANBluetoothManager instance;  	
	private 	PLANBluetoothManager(){}

	public		static	PLANBluetoothManager	Instance
	{
		get{
			if (instance == null) {
				instance = GameObject.FindObjectOfType (typeof(PLANBluetoothManager)) as PLANBluetoothManager;
			}
			return instance;
		}
	}

	public		void Init (string	_name = "",int	_total = 0) {
		#if !UNITY_EDITOR
			if (android_class == null) {
				android_class = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				if(android_class != null){
					android_activity = android_class.GetStatic<AndroidJavaObject> ("currentActivity");
				}
			}
		Setup(_name,_total);
		#endif
		if(panel_reconnecting != null)
		{
			panel_reconnecting.gameObject.SetActive (false);
		}
	}

	public void	Setup(string	_name,int	_total)
	{
		connected = false;
		if (BLE) {
			android_activity.Call ("setup",BLE_MODE,_name,_total);

		} else {
			android_activity.Call ("setup",BLUETOOTH_MODE,"",0);
		}
	}

	public void	BLEConnect(int	_id)
	{
		android_activity.Call ("ble_scan",_id);
	}

	public	void	BLEDisconnect(int	_id)
	{
		android_activity.Call ("ble_disconnect",_id);
	}

	public void	BluetoothPair(string	_device_name)
	{
		device_name = _device_name;
		android_activity.Call ("bluetooth_scan", _device_name);
	}

	public	void	SendData(int	_id,string	_data)
	{
		
		android_activity.Call ("ble_send", _id, _data);
		if (connected) {

			//MakeCall ("ble_send",_id, _data);
		} else {
			//	Reconnect ();
		}
	}

	public	string	ReceiveData(string	_header)
	{
		if (_header.Contains (";")) {
			return _header.Split (';') [1];
		} else {
			return null;
		}
	}

	public	bool	isParing()
	{
		return	paring;
	}

	public 	bool	isConnected()
	{
		return	connected;
	}

	private	void	Reconnect()
	{	
		if (panel_reconnecting != null) {
			panel_reconnecting.gameObject.SetActive (true);
		}
		BluetoothPair (device_name);
	}

	private void	MakeCall(string	_function_name,string	_params = "")
	{
		android_activity.Call (_function_name,_params);
	}

	//	Desde plugin

	public void OnBluetoothConnection(string	_header)
	{
		OnBluetooth (_header);
		/*
		if (_header.Equals (BLE_CONNECTED)) {
			connected = true;
			if (panel_reconnecting != null) {
				panel_reconnecting.gameObject.SetActive (false);
			}
		} else if (_header.Equals (BLE_DISCONNECTED)) {
			connected = false;
		//	Reconnect ();
		}*/
	}

}
