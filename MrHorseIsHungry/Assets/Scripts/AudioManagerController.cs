using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource audioSourceMusicBackground;
    [SerializeField] AudioSource audioSourceMusicCharacter;

    [SerializeField] AudioClip musicBackgroundMsGiraffe;
    [SerializeField] AudioClip musicBackgroundMrElephant;
    [SerializeField] AudioClip musicBackgroundMsHen;
    [SerializeField] AudioClip musicBackgroundEndScene;

    void FadeIn(AudioSource audioSource, float duration = 1f)
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, duration, 0f, 1f));
    }

    void FadeOut(AudioSource audioSource, float duration = 1f)
    {
        StartCoroutine(FadeAudioSource.StartFade(audioSource, duration, 1f, 0f));
    }

    public void PlayBackgroundMusic()
    {
        print("PlayBackgroundMusic");

        if(!audioSourceMusicBackground.isPlaying){
            audioSourceMusicBackground.Play();
        }

        FadeOut(audioSourceMusicCharacter, 0.5f);
        FadeIn(audioSourceMusicBackground, 0.5f);
    }

    void PlayBackgroundMusicCharacter(AudioClip clip)
    {
        audioSourceMusicCharacter.clip = clip;
        audioSourceMusicCharacter.Play();

        FadeOut(audioSourceMusicBackground, 0.5f);
        FadeIn(audioSourceMusicCharacter, 0.5f);
    }

    public void PlayBackgroundMusicMsGiraffe()
    {
        print("PlayBackgroundMusicMsGiraffe");
        PlayBackgroundMusicCharacter(musicBackgroundMsGiraffe);
    }

    public void PlayBackgroundMusicMrElephant()
    {
        print("PlayBackgroundMusicMrElephant");
        PlayBackgroundMusicCharacter(musicBackgroundMrElephant);
    }

    public void PlayBackgroundMusicMsHen()
    {
        print("PlayBackgroundMusicMsHen");
        PlayBackgroundMusicCharacter(musicBackgroundMsHen);
    }

    public void PlayBackgroundMusicEndScene()
    {
        print("PlayBackgroundMusicEndScene");

        audioSourceMusicCharacter.clip = musicBackgroundEndScene;
        audioSourceMusicCharacter.Play();

        FadeOut(audioSourceMusicBackground, 0.2f);
        FadeIn(audioSourceMusicCharacter, 0f);
    }
}
