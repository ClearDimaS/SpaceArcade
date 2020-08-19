using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuComponents : MonoBehaviour
{
    [SerializeField]
    SettingsView settingsView;
    void Start()
    {
        settingsView.InitUI();
    }

    public void Quit()
    {
        Debug.Log("Application is quiting");
        Application.Quit();
    }
}
