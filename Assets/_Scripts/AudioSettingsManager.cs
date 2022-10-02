using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private float _timeToWaitForSave;

    readonly string MIXER_MASTER = "Master";
    readonly string MIXER_BGM = "BGM";
    readonly string MIXER_SFX = "SFX";
    readonly string SETTINGS_MASTER = "Master_Volume";
    readonly string SETTINGS_BGM = "BGM_Volume";
    readonly string SETTINGS_SFX = "SFX_Volume";

    private float _defaultVolume = 0.70f;
    public float MasterVolume { get; set; }
    public float MusicVolume { get; set; }
    public float EffectsVolume { get; set; }

    public static AudioSettingsManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        LoadVolumeData();
    }

    private void SaveVolumeData()
    {
        PlayerPrefs.SetFloat(SETTINGS_MASTER, MasterVolume);
        PlayerPrefs.SetFloat(SETTINGS_BGM, MusicVolume);
        PlayerPrefs.SetFloat(SETTINGS_SFX, EffectsVolume);
    }

    private void LoadVolumeData()
    {
        if (!PlayerPrefs.HasKey(SETTINGS_MASTER))
        {
            SetMixerVolume(MIXER_MASTER, _defaultVolume);
            SetMixerVolume(MIXER_BGM, _defaultVolume);
            SetMixerVolume(MIXER_SFX, _defaultVolume);
            SaveVolumeData();
            return;
        }

        SetMixerVolume(MIXER_MASTER, PlayerPrefs.GetFloat(SETTINGS_MASTER));
        SetMixerVolume(MIXER_BGM, PlayerPrefs.GetFloat(SETTINGS_BGM));
        SetMixerVolume(MIXER_SFX, PlayerPrefs.GetFloat(SETTINGS_SFX));
    }

    private IEnumerator WaitForSave()
    {
        yield return new WaitForSeconds(_timeToWaitForSave);
        SaveVolumeData();
    }

    private void SetMixerVolume(string mixerName, float volume)
    {
        float normalized = Mathf.Log10(volume) * 20;
        _audioMixer.SetFloat(mixerName, normalized);

        if (mixerName == MIXER_MASTER) MasterVolume = volume;
        if (mixerName == MIXER_BGM) MusicVolume = volume;
        if (mixerName == MIXER_SFX) EffectsVolume = volume;
    }

    public float GetMixerVolume(string mixerName)
    {
        if (mixerName == MIXER_MASTER) return MasterVolume;
        if (mixerName == MIXER_BGM) return MusicVolume;
        if (mixerName == MIXER_SFX) return EffectsVolume;
        return _defaultVolume;
    }

    public void SetMixerVolumeFromSlider(string mixerName, float volume)
    {
        StopCoroutine(WaitForSave());
        SetMixerVolume(mixerName, volume);
        StartCoroutine(WaitForSave());
    }
}
