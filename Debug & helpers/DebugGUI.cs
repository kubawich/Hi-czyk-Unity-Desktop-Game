using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC.TimeOfDaySystemFree;

public class DebugGUI : MonoBehaviour {
   
    float deltaTime = 0f;
    bool showDebugInfo = false;
    bool lockMouse = false;
    bool isTimeLocked = false;

    public Rect windowRect;
    public TimeOfDayManager TOD;

    private void Awake()
    {
        TOD.GetComponent<TimeOfDayManager>();
    }
    private void OnGUI()
    {
        QualitySettings.vSyncCount = 1;
        Cursor.visible = true;
        
        windowRect = GUI.Window(0, windowRect, DragWin, "Debug window");   
    }
    void ElementsInWindow()
    {
        float fps = 1f / deltaTime;
        GUI.Label(new Rect(10, 20, 100, 20), "H " + Input.GetAxis("Horizontal").ToString());
        GUI.Label(new Rect(10, 35, 100, 20), "V " + Input.GetAxis("Vertical").ToString());
        GUI.Label(new Rect(10, 50, 100, 20), "FPS " + fps.ToString().PadRight(3));
        isTimeLocked = GUI.Toggle(new Rect(10, 65, 100, 20), isTimeLocked,"Stop time");
        GUI.Label(new Rect(10, 80, 150, 20), "Time of day " + TOD.Hour + ":" + TOD.Minute);
        TOD.timeline = GUI.HorizontalSlider(new Rect(10, 100, 100, 20), TOD.timeline, 0f, 24f);
    }
    void DragWin(int WindowID)
    {
        ElementsInWindow();
        GUI.DragWindow(new Rect(0, 0, 200, 400));
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        if (Input.GetKeyDown(KeyCode.L) && !lockMouse)
        {
            lockMouse = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.L) && lockMouse)
        {
            lockMouse = false;
            Cursor.lockState = CursorLockMode.None;
        }

        TOD.playTime = isTimeLocked;
        if (isTimeLocked)
        {
            TOD.playTime = false;
        }
        else TOD.playTime = true;
    }
}
