using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UserButton : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Text field;

    public Image boy;
    public Image girl;

    [HideInInspector]
    public UserData userData;

    private Users users;

	private bool open;
	private int MoveX;
	float	drag_x;
	bool	dragging;

	public void Start()
	{
		MoveX = (int)(70 * ScreenManager.Instance.canvas.scaleFactor);
		open = true;
		dragging = false;
	}

    public void Init(Users users, UserData userData)
    {
		open = true;
		dragging = false;
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

	//	edit

	private void Toogle()
	{
		int dest = open ? MoveX : -MoveX;

		iTween.MoveBy(gameObject, iTween.Hash(
			"x", dest,
			"time", .2,
			"easeType", "easeOutCubic",
			"oncomplete", "OnAnimationComplete"
		));
	}

	public void OnBeginDrag(PointerEventData _EventData)
	{
		drag_x = _EventData.position.x;
	}

	public void OnDrag(PointerEventData _EventData)
	{
		if (!dragging) {
			dragging = false;
			if (_EventData.position.x > drag_x) {
				if (!open) {
					open = true;
					Toogle ();
				}
			} else if(_EventData.position.x < drag_x){
				if (open) {
					open = false;
					Toogle ();
				}
			}
		}
	}

	public void OnEndDrag(PointerEventData _EventData)
	{
		dragging = false;
	}

}
