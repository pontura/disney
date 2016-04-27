using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollContent : MonoBehaviour {

    public Text field;
    public int id;

    public void Init(int id, string addSimbol)
    {
        if (id >= 0)
        {
            string num = id.ToString();
            if (id < 10)
                num = "0" + id;

            string texto = addSimbol.ToString() + num;
            field.text = texto;
          
           
        }
        else
            field.text = "";
        this.id = id;
    }
}
