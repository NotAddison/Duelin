using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public float OSTVol = 1f;
    public float SFXVol = 1f;
    public float AmbienceVol = 1f;

    public static SettingsMenu getInstance() => GameObject.FindWithTag("Settings")?.GetComponent<SettingsMenu>();
    
    void Start(){
        GameObject.Find("Music").GetComponent<UnityEngine.UI.Slider>().value = PreserveSound.Instance.gameObject.GetComponent<AudioSource>().volume;
        GameObject.Find("SFX").GetComponent<UnityEngine.UI.Slider>().value = 1f;
        GameObject.Find("Ambience").GetComponent<UnityEngine.UI.Slider>().value = 1f;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetOSTVol(float vol){
        PreserveSound.Instance.gameObject.GetComponent<AudioSource>().volume = vol;
        Debug.LogError($"[Settings] Volume set to {vol}");
        OSTVol = vol;
    }

    public float GetOSTVol(){
        return OSTVol;
    }

    public void SetSFXVol(float vol){
        AudioListener.volume = vol;
        Debug.LogError($"[Settings] Volume set to {vol}");
        SFXVol = vol;
    }

    public float GetSFXVol(){
        return SFXVol;
    }

    public void SetAmbienceVol(float vol){
        AudioListener.volume = vol;
        Debug.LogError($"[Settings] Volume set to {vol}");
        AmbienceVol = vol;
    }

    public float GetAmbienceVol(){
        return AmbienceVol;
    }
}
