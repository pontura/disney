using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Texts : MonoBehaviour
{
    private bool yaSaludoPorCumple;
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
        public string sound_masculino;
        public string sound_femenino;
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

        Fill(arriba_abajo, Json["arriba_abajo"], "arriba_abajo");
        Fill(cosquillas, Json["cosquillas"], "cosquillas");
        Fill(musica_suave, Json["musica_suave"], "musica_suave");
        Fill(cumpleaños, Json["cumpleaños"], "cumpleaños");
    }
    private void Fill(List<TextSaludosData> arr, JSONNode content, string tema)
    {
        for (int a = 0; a < content.Count; a++)
        {
            TextSaludosData saludo = new TextSaludosData();
            saludo.tema = content[a]["tema"];
            saludo.masculino = content[a]["masculino"];
            saludo.femenino = content[a]["femenino"];
            if (tema == "cumpleaños")
            {
                saludo.sound_masculino = tema + "-" + (int)(a+1) + "-" + "masculino";
                saludo.sound_femenino = tema + "-" + (int)(a + 1) + "-" + "femenino";
            }
            else
            {
                saludo.sound_masculino = tema + "-" + content[a]["tema"] + "-" + "masculino";
                saludo.sound_femenino = tema + "-" + content[a]["tema"] + "-" + "femenino";
            }
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
        print((DateTime.Now.Day + " _" + Data.Instance.userData.day + " _" + month + " _" + month_birthday));
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
    public string GetNameForSound(string text)
    {
        print("GetNameForSound : " + text);
        string musicName = "";

        List<TextSaludosData> data;
        switch (ScreenManager.Instance.ActivityActiveID)
        {
            case 1:
                data = arriba_abajo; musicName = "arriba_abajo"; break;
            case 2:
                data = cosquillas; musicName = "cosquillas";  break;
            case 3:
                data = musica_suave; musicName = "musica_suave";  break;
            default:
                data = cumpleaños; musicName = "cumpleaños"; break;
        }
        foreach(TextSaludosData td in data)
        {
            if (td.masculino == text)
                musicName = td.sound_masculino;
            else if (td.femenino == text)
                musicName = td.sound_femenino;
        }
        return musicName;

    }
    public string GetSaludo()
    {
        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;
        int rand = UnityEngine.Random.Range(0, 100);
        int hour = System.DateTime.Now.Hour;

        TextSaludosData saludosData = null;

        if (CumpleanosCerca() && !yaSaludoPorCumple)
        {
            yaSaludoPorCumple = true;
            saludosData = GetSaludoByTema("CUMPLEAÑOS");
        }
        else if (SessionTimeController.Instance.lastTimeConnected_days > 5 && !Frecuencia_baja_ready)
        {
            Frecuencia_baja_ready = true;
            saludosData = GetSaludoByTema("FRECUENCIA_BAJA");
        }
        else if (rand < 40)
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
            if (hour > 5 && hour < 12)
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
    public TextSaludosData GetRandomCumpleanos()
    {
        return cumpleaños[cumpleaños.Count-1];
    }
    public string GetRandomCumpleanosVoice(TextSaludosData data)
    {
        if (Data.Instance.userData.sex == UserData.sexs.BOY)
            return data.sound_masculino;
        else
            return data.sound_femenino;
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
