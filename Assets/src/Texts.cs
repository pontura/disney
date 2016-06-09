using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Texts : MonoBehaviour
{

    public List<TextSaludosData> arriba_abajo;
    public List<TextSaludosData> cosquillas;
    public List<TextSaludosData> musica_suave;
    public List<TextSaludosData> cumpleaños;

    [Serializable]
    public class TextSaludosData
    {
        public string tema;
        public string masculino;
        public string femenino;
    }

    private bool Frecuencia_baja_ready;

    string json_musica_suave = "content";

    void Start()
    {
        Encoding utf8 = Encoding.UTF8;
        TextAsset file = Resources.Load(json_musica_suave) as TextAsset;
        LoadDataromServer(file.text);
    }
    public void LoadDataromServer(string json_data)
    {
        var Json = SimpleJSON.JSON.Parse(json_data);

        Fill(arriba_abajo, Json["arriba_abajo"]);
        Fill(cosquillas, Json["cosquillas"]);
        Fill(musica_suave, Json["musica_suave"]);
        Fill(cumpleaños, Json["cumpleaños"]);
    }
    private void Fill(List<TextSaludosData> arr, JSONNode content)
    {
        for (int a = 0; a < content.Count; a++)
        {
            TextSaludosData saludo = new TextSaludosData();
            saludo.tema = content[a]["tema"];
            saludo.masculino = content[a]["masculino"];
            saludo.femenino = content[a]["femenino"];
            arr.Add(saludo);
        }
    }
    public string GetCierra()
    {
        TextSaludosData data;

        switch (ScreenManager.Instance.ActivityActiveID)
        {
            case 1:
                data = arriba_abajo[arriba_abajo.Count - 1];
                break;
            case 2:
                data = cosquillas[cosquillas.Count - 1];
                break;
            case 3:
                data = musica_suave[musica_suave.Count - 1];
                break;
            default:
                data = cumpleaños[cumpleaños.Count - 1];
                break;
        }
        if (Data.Instance.userData.sex == UserData.sexs.BOY)
            return data.masculino;
        else
            return data.femenino;
    }
    public TextSaludosData GetCumpleCerca()
    {
        TextSaludosData data;
        switch (ScreenManager.Instance.ActivityActiveID)
        {
            case 1:
                data = arriba_abajo[0];  break;
            case 2:
                data = cosquillas[0];  break;
            case 3:
                data = musica_suave[0];  break;
            default:
                data = cumpleaños[0];  break;
        }
        return data;
    }
    public bool CumpleanosCerca()
    {
        int month = DateTime.Now.Month;
        int month_birthday = Data.Instance.userData.month;
        if (
            month + 1 == month_birthday
            || (month == 12 && month_birthday == 1)
            || (DateTime.Now.Day < Data.Instance.userData.day && month == month_birthday)
            )
            return true;
        return false;
    }
    public bool isBirthday()
    {
        if (DateTime.Now.Month == Data.Instance.userData.month && DateTime.Now.Day == Data.Instance.userData.day )
            return true;
        return false;
    }
    public string GetSaludo()
    {
        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;
        int rand = UnityEngine.Random.Range(0, 100);
        int hour = System.DateTime.Now.Hour;

        print("hour: " + hour);

        TextSaludosData saludosData = null;

        if (day == Data.Instance.userData.day && month == Data.Instance.userData.month)
            saludosData = GetRandomCumpleanos();
        else if (SessionTimeController.Instance.lastTimeConnected_days > 5 && !Frecuencia_baja_ready)
        {
            Frecuencia_baja_ready = true;
            saludosData = GetSaludoByTema("FRECUENCIA_BAJA");
        } else if (rand < 40)
        {
            if (DialoguesManager.Instance.weather_main == "Clouds")
                saludosData = GetSaludoByTema("LLUVIA");
            else if (DialoguesManager.Instance.temperature < 20)
                saludosData = GetSaludoByTema("FRESCO");
            else
                saludosData = GetSaludoByTema("SOL");
        }
        else if (rand < 80)
        {
            if (hour>5 && hour<12)
                saludosData = GetSaludoByTema("MAÑANA");
            else if (hour > 11 && hour < 19)
                saludosData = GetSaludoByTema("TARDE");
            else
                saludosData = GetSaludoByTema("NOCHE");
        }
        else
            saludosData = GetSaludoByTema("FRECUENCIA_ALTA");

        if(Data.Instance.userData.sex == UserData.sexs.BOY)
            return saludosData.masculino;
        else
            return saludosData.femenino;
    }
    private TextSaludosData GetRandomCumpleanos()
    {
        return cumpleaños[cumpleaños.Count-1];
    }
    private TextSaludosData GetSaludoByTema(string _tema)
    {
        List<TextSaludosData> dataArr;
        switch (ScreenManager.Instance.ActivityActiveID)
        {
            case 1: dataArr = arriba_abajo; break;
            case 2: dataArr = cosquillas; break;
            default: dataArr = musica_suave; break;
        }
        foreach (TextSaludosData textSaludosData in dataArr)
        {
            if (textSaludosData.tema == _tema)
                return textSaludosData;
        }
        return null;
    }

}
