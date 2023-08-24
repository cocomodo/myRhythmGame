using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCounter : MonoBehaviour
{
    private float deltaTime = 0;

    [SerializeField, Range(1, 100)]
    private int size = 70;

    [SerializeField]
    private Color color = Color.black;

    //public bool isShow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // if (Input.GetKeyDown(KeyCode.F1))
        // {
        //     isShow = !isShow;
        // }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;

        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms) + Time.timeScale= {2:0.0}", fps, ms,Time.timeScale);


        GUI.Label(rect, text, style);

        // if (isShow)
        // {
        //     GUIStyle style = new GUIStyle();

        //     Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        //     style.alignment = TextAnchor.UpperLeft;
        //     style.fontSize = size;
        //     style.normal.textColor = color;

        //     float ms = deltaTime * 1000f;
        //     float fps = 1.0f / deltaTime;
        //     string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);


        //     GUI.Label(rect, text, style);
        // }
    }
}
