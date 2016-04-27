using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserButton : MonoBehaviour {

    public Text field;
    public int id;
    public Users users;

	public void Init (Users users, UserData data) {
        this.users = users;
        field.text = data.username;
	}
    public void Clicked()
    {
        users.Ready();
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
