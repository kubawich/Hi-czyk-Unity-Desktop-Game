using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


public class _Objective_Element : MonoBehaviour 
{
    public _Objective_Element(bool f, string n, string d, UnityEngine.Object dest, int a)
    {
        IsFinished = f; Name = n; description = d; Destination = dest; Amount = a;
    }
    public bool IsFinished;
    public string Name, description;
    public UnityEngine.Object Destination;
    public int Amount;
}


