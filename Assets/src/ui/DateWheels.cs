using UnityEngine;
using System.Collections;

public class DateWheels : MonoBehaviour {

    public ScrollSnap scrollSnap_day;
    public ScrollSnap scrollSnap_month;
    public ScrollSnap scrollSnap_year;

	void Start () {
        scrollSnap_day.Init(0);
        scrollSnap_month.Init(0);
        scrollSnap_year.Init(15);
	}

    
}
