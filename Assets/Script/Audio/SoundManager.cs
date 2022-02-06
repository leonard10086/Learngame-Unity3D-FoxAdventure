using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioSource bgmSource;
    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, cherryAudio;
    [SerializeField]
    private Slider slider;

    private void Awake()
    {
        instance = this;
    }
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }

    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }

    public void CherryAudio()
    {
        audioSource.clip = cherryAudio;
        audioSource.Play();
    }

    //滑条控制音量
    public void Volume()
    {
        bgmSource.volume = slider.value;
    }
}
