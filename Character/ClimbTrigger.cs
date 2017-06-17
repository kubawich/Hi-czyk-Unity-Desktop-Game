using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTrigger : MonoBehaviour {

    [Range(0, 90)]
    public int ClimbAngle = 30;
    public AnchorPoint[] ClimbAnchors;
    public GameObject Anchor { get; set; }

    bool InRange;

    private void Update()
    {
        if (Anchor == null || !InRange)
            return;
        Debug.DrawLine(CharacterLocomotion.instance.transform.position,
            Anchor.transform.position, Color.yellow);
        var angle = Quaternion.Angle(Anchor.transform.rotation,
            CharacterLocomotion.instance.transform.rotation);
        if(angle < ClimbAngle)
        {
            CharacterLocomotion.instance.Anchor = Anchor;
            CharacterLocomotion.instance.DoClimbJump = true;
        }
        else
        {
            CharacterLocomotion.instance.Anchor = null;
            CharacterLocomotion.instance.DoClimbJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (!InRange)
            return;
        float distance = float.PositiveInfinity;
        float tmp;
        foreach (var anchor in ClimbAnchors)
        {
            tmp = Vector3.Distance(CharacterLocomotion.instance.transform.position,
                anchor.transform.position);
            if (tmp < distance)
            {
                Anchor = anchor.gameObject;
                distance = tmp;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            InRange = true;
        CharacterLocomotion.instance.InClimbRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            InRange = false;
        CharacterLocomotion.instance.InClimbRange = false;
    }
}
