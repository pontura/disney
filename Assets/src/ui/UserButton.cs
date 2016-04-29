using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserButton : MonoBehaviour {

    public Text field;

    public Image boy;
    public Image girl;

    [HideInInspector]
    public UserData userData;

    private Users users;

    public void Init(Users users, UserData userData)
    {
        this.userData = userData;
        this.users = users;
        field.text = userData.username;
        if (userData.sex == UserData.sexs.BOY)
        {
            boy.enabled = true;
            girl.enabled = false;
        }
        else
        {
            boy.enabled = false;
            girl.enabled = true;
        }
	}
    public void Clicked()
    {
        users.Ready(this);
    }
    public void Delete()
    {
        users.Delete(this);
    }
    public void Edit()
    {
        users.Edit(this);
    }
}
