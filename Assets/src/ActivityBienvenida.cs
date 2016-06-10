using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivityBienvenida : ScreenMain
{
    public Text field;
    public int ActivityActiveID;
    private bool notActive;
    int textID;

    void Start()
    {
        Events.OnVoiceReady += OnVoiceReady;
        Events.OnVoiceNotExists += OnVoiceNotExists;
    }
    override public void OnFocus()
    {
        textID = 1;
        ScreenManager.Instance.ActivityActiveID = ActivityActiveID;
       // 
        string voiceSoundName = "";
        if (ActivityActiveID == 4)
        {
            Texts.TextSaludosData data = Data.Instance.texts.GetRandomCumpleanos();

            if (Data.Instance.userData.sex == UserData.sexs.BOY)
                field.text = data.masculino;
            else
                field.text = data.femenino;

            voiceSoundName = Data.Instance.texts.GetRandomCumpleanosVoice(data);
            print("____voiceSoundName: " + voiceSoundName);
        }
        else
        {
            notActive = false;
            string texto = Data.Instance.texts.GetSaludo();
            field.text = texto;
            print("GetSaludo:: " + texto);
            voiceSoundName = Data.Instance.texts.GetNameForSound(texto);
        }
        
        Events.OnVoice(voiceSoundName);

	}
    void OnVoiceNotExists()
    {
        print("OnVoiceNotExists");
        if(textID == 1)
            Invoke("OnVoiceReady", 5);
        else
            Invoke("Next", 3);
    }
    override public void LoseFocus()
    {
        notActive = true;
    }
    void OnVoiceReady()
    {
        print("OnVoiceReady textID: " + textID + "    notActive: " + notActive + "   ScreenManager.Instance.ActivityActiveID: " + ScreenManager.Instance.ActivityActiveID);
        if (textID == 1)
        {
            textID++;
            if (notActive) return;
            if (ScreenManager.Instance.ActivityActiveID == 4)
            {
                Events.GotoTo("Playing_4");
                return;
            }
            string texto = Data.Instance.texts.GetCierra();
            field.text = texto;

            string voiceSoundName = Data.Instance.texts.GetNameForSound(texto);
            Events.OnVoice(voiceSoundName);
        }
        else
        {
            Next();
        }
    }
    public void Next()
    {
        if (notActive) return;
        
        if (ScreenManager.Instance.ActivityActiveID == 4)
            Events.GotoTo("Playing_4");
        else
            Events.GotoTo("Activity_" + ScreenManager.Instance.ActivityActiveID + "_instructions");
    }
}
