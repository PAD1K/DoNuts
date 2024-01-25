using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer _audioMixer;
    public TMPro.TMP_Dropdown _resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int _currentResolutionindex = 0;
        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResolutionindex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionindex;
        _resolutionDropdown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution _resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetVSync(bool _isVSync)
    {
        if(_isVSync)
        {
            Application.targetFrameRate = 60;
            //QualitySettings.vSyncCount = 4;
        }
        else{
            Application.targetFrameRate = -1;
            //QualitySettings.vSyncCount = 0;
        }
    }
}
