using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MusicPlayer : MonoBehaviour
{

    [SerializeField] private List<AudioClip> audioClips;

    [SerializeField] private AudioSource source;
    [SerializeField] private TMP_Text audioName;
    [SerializeField] private Slider progressBar;

    private float _lastProgressBarValue;
    private int _currentIndex;

    private void Start()
    {
        UpdateAudioData(audioClips[_currentIndex]);
    }

    public void Next()
    {
        _currentIndex++;
        UpdateAudioData(audioClips[_currentIndex]);
    }

    private void UpdateAudioData(AudioClip audioClip)
    {
        source.clip = audioClip;
        source.Play();
        audioName.text = audioClip.name;

        progressBar.value = 0;
        _lastProgressBarValue = 0;
        source.time = 0;
    }

    private void Update()
    {
        var timeValue = source.time / source.clip.length;
        if (progressBar.value != _lastProgressBarValue)
        {
            source.time = progressBar.value * source.clip.length;
        }
        else
        {
            progressBar.value    = timeValue;
        }
        _lastProgressBarValue = progressBar.value;
    }
}
