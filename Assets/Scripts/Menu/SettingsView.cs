using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{

    public Slider slider;

    public Toggle toggle_mus;

    public Image Sound_img;

    public Image Music_img;

    public List<Image> Sound_UI_components;

    SettingsController settingsController;

    bool loaded;

    public void InitUI()
    {
        settingsController = new SettingsController();

        settingsController.Init();

        Sound_UI_components.Add(slider.image);

        slider.value = settingsController.volume;

        toggle_mus.isOn = settingsController.musicIsOn;

        settingsController.UpdValues(slider.value, toggle_mus.isOn);

        LoadPics();

        loaded = true;
    }


    public void onValueChange() 
    {
        if (loaded) 
        {
            if (settingsController.UpdValues(slider.value, toggle_mus.isOn))
            {
                LoadPics();
            }
        }
    }

    private void LoadPics() 
    {
        Music_img.sprite = settingsController.GetMusicSprite();

        Sound_img.sprite = settingsController.GetSoundSprite();

        Color color;
        if (settingsController.volume > 0.05f)
            color = Color.Lerp(Color.red, Color.black, 0.5f);
        else
            color = Color.grey;

        foreach (Image img in Sound_UI_components)
        {
            img.color = color;
        }
    }
}
