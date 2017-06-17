using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class IEntity : MonoBehaviour  {
    public virtual float AnimSpeed { get; set; }
    public virtual float CameraSmoothingSetting { get; set; }
    public virtual Animator Animator { get; set; }
    public virtual AnimatorStateInfo CurrentStateOfBaseLayer { get; set; }
    public virtual CapsuleCollider Colider { get; set; }
    public virtual Rigidbody Rigidbody { get; set; }

    public static int idleState = Animator.StringToHash("Base Layer.Idle");
    public static int locomotionState = Animator.StringToHash("Base Layer.Locomotion");
    public static int jumpState = Animator.StringToHash("Base Layer.Jump");
    public static int jumpDownState = Animator.StringToHash("Base Layer.JumpDown");
    public static int fallDownState = Animator.StringToHash("Base Layer.Fall");
    public static int turnState = Animator.StringToHash("Base Layer.Turn");

    public virtual void Start() { }
    public virtual void FixedUpdate() { }
    public virtual void Update() { }
}
