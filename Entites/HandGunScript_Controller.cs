using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for shooting
[ExecuteInEditMode]
public class HandGunScript_Controller : MonoBehaviour {

    public static HandGunScript_Controller Instance;
    public Transform weaponImpactPoint;
    public AudioSource shotAudio;
    public float shotDelay = .25f;
    public GameObject shootImpactPoint;
    public ParticleSystem Muzzle, Blood;

    public Vector3 CamOrigin;

    private void Awake()
    {
        shotAudio.GetComponent<AudioSource>();
        Muzzle.GetComponentInChildren<ParticleSystem>();
        Blood.GetComponentInChildren<ParticleSystem>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        CamOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        if (Input.GetMouseButtonDown(0) && ShootingHabit.Instance.hasWeapon)
        {
            BasicShot();
        }
        else return;
    }

    public RaycastHit hit;
    public void BasicShot()
    {
        var ray = Physics.Raycast(CamOrigin, Camera.main.transform.forward, out hit, float.PositiveInfinity);
        shotAudio.Play();
        Muzzle.Play();
        try
        {
            if (hit.collider.tag == "NPC")
                Instantiate(Blood, hit.point, Quaternion.identity);
            else
                Instantiate(shootImpactPoint, hit.point, Quaternion.Euler(hit.point.x * -1, hit.point.y * -1, hit.point.z * -1));
        }
        catch (System.Exception ex)
        {
            return;
        }

        Debug.DrawRay(CamOrigin, Camera.main.transform.forward);
    }

}
