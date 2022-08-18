using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class settingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resDropDown;
    Resolution[] res;
    private void Start()
    {
        res = Screen.resolutions;
        resDropDown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for(int i = 0; i< res.Length; i++)
        {
            string option = res[i].width + " x " + res[i].height;
            options.Add(option);
            if(res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropDown.AddOptions(options);
        resDropDown.value = currentResIndex;
        resDropDown.RefreshShownValue();
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void setQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void setResolution(int index)
    {
        Resolution resolution = res[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
