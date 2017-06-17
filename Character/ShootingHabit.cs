using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for shooting character
public class ShootingHabit : MonoBehaviour {
    public static ShootingHabit Instance;
    public bool hasWeapon = false;
    public bool IsColidingWithWepon = false;
    public Animator animator;
    public Transform HandPosition;
    public Transform SpineBone, ShotCamTransform;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        hasWeapon = false;
    }

    private void Update()
    {
        if (hasWeapon)
        {
            animator.SetBool("hasWeapon", true);
            animator.SetLayerWeight(1, 1f);
            Camera.main.transform.localPosition = ShotCamTransform.transform.localPosition;
        }
        else if (!hasWeapon )
        {
            animator.SetBool("hasWeapon", false);
            animator.SetLayerWeight(1, 0f);
        }
        if (hasWeapon && Input.GetKeyDown(KeyCode.F))
        {
            if (obj)
            {
                DropDownWeapon(obj);
            }
        }
    }
    
    //Set weapon which u have trigerred
    public GameObject SetWeapon(GameObject weaponObject)
    {
        GameObject obj;
		if (!hasWeapon )
        {
            obj = Instantiate(weaponObject, HandPosition.position, Quaternion.identity);
            return obj;
        }
        else return null;

    }

    //Same as above
    GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        IsColidingWithWepon = true;

        if (other.gameObject.tag == "Weapon"  && !hasWeapon )
        {
            obj = SetWeapon(other.gameObject);
			if (obj )
            {
                obj.GetComponent<BoxCollider>().enabled = false;
                obj.transform.SetParent(HandPosition);
                obj.transform.localRotation = Quaternion.Euler(0, 0, -150);
                obj.transform.localPosition = new Vector3(0.011f, 0.038f, 0);
                Destroy(other);
            }
        }
        else return;
        
        other.gameObject.SetActive(false);
        hasWeapon = true;
    }

    //Drop handled weapon
    void DropDownWeapon(GameObject obj)
    {
        IsColidingWithWepon = false;
        Destroy(obj);      
        hasWeapon = false;
    }
}
