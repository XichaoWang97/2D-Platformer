using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource BgmAudio;
    public AudioSource SfxAudio;

    public AudioClip bgm;
    public AudioClip dash;
    public AudioClip jump;
    public AudioClip touchGround;

    void Start()
    {
        BgmAudio.clip = bgm;
        BgmAudio.Play();

        BgmAudio.volume = 0.5f;
        SfxAudio.volume = 0.5f;
    }

    public void PlaySfx(AudioClip clip)
    {
        SfxAudio.PlayOneShot(clip);
    }
}
