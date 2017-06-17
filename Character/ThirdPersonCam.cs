using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class ThirdPersonCam : MonoBehaviour {
    private const float MIN_CAM_Y = -60f;
    private const float MAX_CAM_Y = 70f;
    public Vector3 camOffset = new Vector3(0.75f,0.9f,-1.4f);
    public Image croshair;

    public static ThirdPersonCam Instance;

    public Transform lookAt, camTransform, CombatCameraPos, Player;
    public Camera cam;

    float currX = 0;
    float currY = 0;

	void Start () {
        this.GetComponent<SphereCollider>();
        Instance = this;
        camTransform = transform;
        cam = Camera.main;
        croshair.enabled = false;
	}

    private void Update()
    {
        currX += Input.GetAxis("Mouse X");
        currY -= Input.GetAxis("Mouse Y");
        currY = Mathf.Clamp(currY, MIN_CAM_Y, MAX_CAM_Y);
        if (ShootingHabit.Instance.hasWeapon)
        {
            croshair.enabled = true;
            cam.transform.forward = CharacterLocomotion.instance.transform.forward;
        }    
    }

    Vector3 dir;
    void LateUpdate () {
        dir = camOffset;
        Quaternion rot = Quaternion.Euler(currY, currX, 0);
         camTransform.localPosition = lookAt.position + rot * dir;
        camTransform.LookAt(lookAt.position);
	}

    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log("Enter");
            camOffset = Vector3.Lerp(camOffset, new Vector3(.5f, .02f, -.04f), Time.time * Time.deltaTime);
    }
}
