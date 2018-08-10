using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    private AudioSource audioSource = null;
    public AudioClip enterAudio;
    public AudioClip onclickAudio;
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        if (audioSource== null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
        }
    }
     

    public void OnEnterAudio()
    {
        audioSource.PlayOneShot(enterAudio);
    }

    public void OnClickAudio()
    {
        audioSource.PlayOneShot(onclickAudio);
    }
}
