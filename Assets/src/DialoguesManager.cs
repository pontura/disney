using UnityEngine;
using System.Collections;
using SimpleJSON;
 
public class DialoguesManager : MonoBehaviour {

    public string lat = "-34.5";
    public string lon = "-58.5";

    public string currentIP;
    public string currentCountry;
    public string currentCity;
 
    //retrieved from weather API
    public string retrievedCountry;
    public string retrievedCity;
    public int conditionID;
    public string weather_main;
    public string weather_desc;
    public float temperature;

    public static DialoguesManager mInstance;

    void Awake()
    {
        mInstance = this;
    }
    public static DialoguesManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    void Start()
    {
        StartCoroutine(SendRequest());
    }
 
    IEnumerator SendRequest()
    {

        Network.Connect("http://google.com");
        currentIP = Network.player.externalIP;
        Network.Disconnect();

        
        WWW cityRequest;
        if (currentIP != "UNASSIGNED_SYSTEM_ADDRESS")
            cityRequest = new WWW("http://www.geoplugin.net/json.gp?ip=" + currentIP); //get our location info
        else
            cityRequest = new WWW("http://api.openweathermap.org/data/2.5/weather?lat=" + lat  +"&lon=" + lon + "&appid=c38e78ba5697a7f11bb5927e98bbca8a"); //get our location info

        yield return cityRequest;
 
        if (cityRequest.error == null || cityRequest.error == "")
        {
            var N = JSON.Parse(cityRequest.text);
            currentCity = N["geoplugin_city"].Value;
            currentCountry = N["geoplugin_countryName"].Value;
        }
 
        else
        {
            Debug.Log("WWW error: " + cityRequest.error);
        }
 
        //get the current weather
        WWW request;        
        if(currentCity != "")
            request = new WWW("http://api.openweathermap.org/data/2.5/weather?q=" + currentCity);
        else
            request = new WWW("http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=c38e78ba5697a7f11bb5927e98bbca8a");

        yield return request;
 
        if (request.error == null || request.error == "")
        {
            var N = JSON.Parse(request.text);
 
            retrievedCountry = N["sys"]["country"].Value; //get the country
            retrievedCity = N["name"].Value; //get the city
 
            string temp = N["main"]["temp"].Value; //get the temperature
            float tempTemp; //variable to hold the parsed temperature
            float.TryParse(temp, out tempTemp); //parse the temperature
            temperature = Mathf.Round((tempTemp - 273.0f) * 10) / 10; //holds the actual converted temperature
            weather_main = N["weather"][0]["main"];
            weather_desc = N["weather"][0]["description"]; 
        }
        else
        {
            Debug.Log("WWW error: " + request.error);
        }
 
        ////get our weather image
        //WWW conditionRequest = new WWW("http://openweathermap.org/img/w/" + conditionImage + ".png");
        //yield return conditionRequest;
 
        //if (conditionRequest.error == null || conditionRequest.error == "")
        //{
        //    //create the material, put in the downloaded texture and make it visible
        //    var texture = conditionRequest.texture;
        //    Shader shader = Shader.Find("Unlit/Transparent Colored");
        //    if (shader != null)
        //    {
        //        var material = new Material(shader);
        //        material.mainTexture = texture;
        //    }
        //}
        //else
        //{
        //    Debug.Log("WWW error: " + conditionRequest.error);
        //}
    }
}
 