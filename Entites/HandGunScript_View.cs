using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGunScript_View: MonoBehaviour {

	public static  HandGunScript_View Instance;
	public ParticleSystem Muzzle, Blood;
	public AudioSource shotAudio;

	void Awake (){
		Instance = this;
		shotAudio.GetComponent<AudioSource>();
		Muzzle.GetComponentInChildren<ParticleSystem>();
		Blood.GetComponentInChildren<ParticleSystem>();
	}
			
	public void BasicShot_View () {
		shotAudio.Play();
		Muzzle.Play();
		if (HandGunScript_Controller.Instance.hit.collider.tag == "NPC")
			Instantiate(Blood, HandGunScript_Controller.Instance.hit.point, Quaternion.identity);
		else
			Instantiate(HandGunScript_Controller.Instance.shootImpactPoint, 
				HandGunScript_Controller.Instance.hit.point, 
				Quaternion.identity);
		Debug.DrawRay(HandGunScript_Controller.Instance.CamOrigin, Camera.main.transform.forward);
	}
}
