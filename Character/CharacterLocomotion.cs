using System;
using UnityEngine;

public class CharacterLocomotion : IEntity
{
#region AnimationsVariables
    public float ClimbHandMatchStart = 0.17f;
    public float ClimbHandMatchEnd = 0.238f;
    public float ClimbFootMatchStart = 0.17f;
    public float ClimbFootMatchEnd = 0.65f;
    public GameObject Anchor { get; set; }

    public bool InClimbRange { get; set; }
    public bool InClimbAnimation { get; set; }
    public bool DoClimbJump { get; set; }
    public bool InJumOverAnimation { get; set; }
#endregion

    public Camera cam;
    public static CharacterLocomotion instance;

    private Vector3 cameraForward;
    private Vector3 PlayerRotation;
    AnimatorStateInfo stateInfo;

    bool IsInShootinMode;

    public void Awake()
    {
        instance = this;
    }

    public override void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Colider = GetComponent<CapsuleCollider>();
    }

    public override void Update()
    {
        CameraAndAnimatorStuff();
    }

    int ColisionTagReturn;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Climb")
        {
            ColisionTagReturn = 1;
        }
        if (collision.gameObject.tag == "JumpOver")
        {
            ColisionTagReturn = 2;
        }
        if (collision.gameObject.tag == "Slide")
        {
            ColisionTagReturn = 3;
        }
    }

    private void CheckClimbState()
    {
        if (Anchor == null)
            return;
        if(stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Climb"))
        {
            Debug.DrawLine(Animator.bodyPosition, Anchor.transform.position, Color.blue);
            Animator.MatchTarget(Anchor.transform.position,
                                Anchor.transform.rotation,
                                AvatarTarget.RightHand,
                                new MatchTargetWeightMask(new Vector3(0, 1, 0), 0),
                                ClimbHandMatchStart,
                                ClimbHandMatchEnd);
            Animator.MatchTarget(Anchor.transform.position,
                                Anchor.transform.rotation,
                                AvatarTarget.RightFoot,
                                new MatchTargetWeightMask(new Vector3(0, 1, 0), 0),
                                ClimbFootMatchStart,
                                ClimbFootMatchEnd);
        }
    }

    public void Climb()
    {
        Animator.applyRootMotion = true;
        InClimbAnimation = true;
        Animator.SetTrigger("Climb");
    }

    public void ResetClimbParams()
    {
        InClimbRange = false;
        InClimbAnimation = false;
        InJumOverAnimation = false;
        DoClimbJump = false;
    }

    public void ResetRootMotion()
    {
        Animator.applyRootMotion = false;
    }

    public void JumpOver()
    {
        Animator.applyRootMotion = true;
        InJumOverAnimation = true;
        Animator.SetTrigger("JumpOver");
    }

    public void CheckJumpOverState()
    {
        if (Anchor == null)
            return;
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.JumpOver"))
        {
            Debug.DrawLine(Animator.bodyPosition, Anchor.transform.position, Color.blue);
            Animator.MatchTarget(Anchor.transform.position,
                                Anchor.transform.rotation,
                                AvatarTarget.RightHand,
                                new MatchTargetWeightMask(new Vector3(0, 1, 5),0),
                                0.15f,
                                0.27f);

        }
    }

    public void Slide()
    {
        Animator.applyRootMotion = true;
        InJumOverAnimation = true;
        Animator.SetTrigger("Slide");
    }

    public void CheckSlideState()
    {
        if (Anchor == null)
            return;
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Slide"))
        {
            Debug.DrawLine(Animator.bodyPosition, Anchor.transform.position, Color.blue);
            Animator.MatchTarget(Anchor.transform.position,
                                Anchor.transform.rotation,
                                AvatarTarget.LeftHand,
                                new MatchTargetWeightMask(new Vector3(0, 1, 5), 0),
                                0.40f,
                                0.50f);

        }
    }

    public void CameraAndAnimatorStuff()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        PlayerRotation = cam.transform.rotation.eulerAngles;
        Vector3 targetDirection = new Vector3(h, 0, v);
        targetDirection = Camera.main.transform.TransformDirection(targetDirection);

        if (Animator.GetFloat("Horizontal") >= 0 || Animator.GetFloat("Vertical") >= 0.01)
            transform.rotation = Quaternion.Euler(0f, PlayerRotation.y, 0f);

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Climb") 
            || stateInfo.fullPathHash == Animator.StringToHash("Base Layer.JumpOver")
            || stateInfo.fullPathHash == Animator.StringToHash("Base Layer.Slide"))
            transform.rotation = Quaternion.Euler(0, 0, 0);

        Animator.SetFloat("Horizontal", h);
        Animator.SetFloat("Vertical", v);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Animator.SetBool("isShiftDown", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Animator.SetBool("isShiftDown", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && InClimbRange && DoClimbJump &&!InClimbAnimation && !ShootingHabit.Instance.hasWeapon)
        {
            if (ColisionTagReturn == 1)
                Climb();
            if (ColisionTagReturn == 2)
                JumpOver();
            if (ColisionTagReturn == 3)
                Slide();
        }

       

        stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Climb"))
            CheckClimbState();
        if (stateInfo.IsName("JumpOver"))
            CheckJumpOverState();
        if (stateInfo.IsName("Slide"))
            CheckSlideState();
    }
}
