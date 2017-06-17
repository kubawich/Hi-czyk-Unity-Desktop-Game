using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[System.Serializable]
public class _Quest_Manager : MonoBehaviour {

    public List<_Quest> quests;
    public  List<_Objective_Element> objectives;

    private void OnEnable()
    {
        quests = new List<_Quest>();
        objectives = new List<_Objective_Element>();
    }
}
