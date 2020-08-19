using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController
{
    public float volume { get; private set; }

    public bool musicIsOn { get; private set; }

    public void Init()
    {
        if (DataController.GetValue<int>(Constants.VOLUME) == 0)
            DataController.SaveValue(Constants.VOLUME, 100);

        if (DataController.GetValue<int>(Constants.VOLUME) >= 0)
            volume = (DataController.GetValue<int>(Constants.VOLUME) / 100.0f);
        else
            volume = 0;

        if (DataController.GetValue<int>(Constants.MUSIC) >= 0)
            musicIsOn = true;
        else
            musicIsOn = false;

        ChangeVolume();
    }

    public bool UpdValues(float _volume, bool _musicIsOn)
    {
        if ((int)volume != _volume || musicIsOn != _musicIsOn)
        {
            volume = _volume;

            musicIsOn = _musicIsOn;

            Save();

            return true;
        }

        return false;
    }

    private void Save()
    {
        if (volume > 0.06f)
            DataController.SaveValue(Constants.VOLUME, (int)(volume * 100.0f));
        else
            DataController.SaveValue(Constants.VOLUME, -1);

        if (musicIsOn)
            DataController.SaveValue(Constants.MUSIC, 1);
        else
            DataController.SaveValue(Constants.MUSIC, -1);
        Debug.Log(musicIsOn);
        ChangeVolume();
    }

    private void ChangeVolume()
    {
        if (musicIsOn)
        {
            AudioManager.instance.ChangeVolume(volume, true);
        }
        else
        {
            AudioManager.instance.ChangeVolume(volume, false);
        }
    }

    public Sprite GetMusicSprite()
    {
        Sprite musicSprite;
        if (musicIsOn)
            musicSprite = Resources.Load<Sprite>("Music_1");
        else
            musicSprite = Resources.Load<Sprite>("Music_0");
        return musicSprite;
    }

    public Sprite GetSoundSprite()
    {
        Sprite soundSprite;
        if (volume >= 0.66f)
            soundSprite = Resources.Load<Sprite>("Sound_3");
        else if (volume >= 0.33f)
            soundSprite = Resources.Load<Sprite>("Sound_2");
        else if (volume > 0.05f)
            soundSprite = Resources.Load<Sprite>("Sound_1");
        else
            soundSprite = Resources.Load<Sprite>("Sound_0");
        return soundSprite;
    }

}
