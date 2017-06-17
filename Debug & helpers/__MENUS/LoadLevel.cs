using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    public Image optionsPanel, loadingPanel;

    public void Load()
    {
        loadingPanel.gameObject.SetActive(true);
        Application.LoadLevel(1);
    } 
    public void Options()
    {
        optionsPanel.gameObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
