using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenViewManager : MonoBehaviour
{
    public static ScreenViewManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        screenBorders = new List<float> { 0, 0, 0, 0 };
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
        CalculateBorders();
    }

    public List<float> screenBorders { get; private set; }

    ScreenOrientation deviceOrientation;

    void FixedUpdate()
    {
        if (deviceOrientation != Screen.orientation) 
        {
            CalculateBorders();
            deviceOrientation = Screen.orientation;
        }
    }

    private void CalculateBorders()
    {
        screenBorders[0] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.0f, 0.0f, 0.0f)).x; // x_min

        screenBorders[1] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x; // x_max

        screenBorders[2] = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height * 0.0f, 0.0f)).y; // y_min

        screenBorders[3] = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y; // y_max
    }

    public bool IsInsideBounds(Vector3 pos, float gapSize) 
    {
        return screenBorders[0] - gapSize < pos.x && screenBorders[1] + gapSize > pos.x &&
            screenBorders[2] - gapSize < pos.y  && screenBorders[3] + gapSize > pos.y ;
    }
}
