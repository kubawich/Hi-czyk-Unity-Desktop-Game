using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PlayerCanvasController : MonoBehaviour {

	public static _PlayerCanvasController Instance;
    public Text WeaponSmallInfoText;
	void Awake () {
		Instance = this;
        WeaponSmallInfoText.enabled = false;
	}

	void Update () {
        if (ShootingHabit.Instance.hasWeapon)
        {
            WeaponSmallInfoText.enabled = true;
            WeaponSmallInfoText.text = "F to drop";
        }

        else if (!ShootingHabit.Instance.hasWeapon && ShootingHabit.Instance.IsColidingWithWepon)
        {
            WeaponSmallInfoText.enabled = true;
            WeaponSmallInfoText.text = "G to pick";
        }

        else if(!ShootingHabit.Instance.hasWeapon && !ShootingHabit.Instance.IsColidingWithWepon)
        {
            WeaponSmallInfoText.text = "";
            WeaponSmallInfoText.enabled = false;
        }
    }
}
