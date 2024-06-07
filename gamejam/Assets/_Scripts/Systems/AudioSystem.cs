using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Insanely basic audio system which supports 3D sound.
/// Ensure you change the 'Sounds' audio source to use 3D spatial blend if you intend to use 3D sounds.
/// </summary>
public class AudioSystem : StaticInstance<AudioSystem> {
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioSource _birdsSource;

    [SerializeField] private AudioClip _mainTheme;
    [SerializeField] private AudioClip _mainMenuTheme;
    [SerializeField] private AudioClip _birds;

    [SerializeField] private List<AudioClip> _stoneClips;

    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _looseSound;
    [SerializeField] private List<AudioClip> _bombsSounds;

    public void PlayMusic(AudioClip clip) {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1) {
        _soundsSource.transform.position = pos;
        PlaySound(clip, vol);
    }

    public void PlaySound(AudioClip clip, float vol = 1) {
        _soundsSource.PlayOneShot(clip, vol);
    }

    public void PlayRandomStone()
    {
        var rand = Random.Range(0, _stoneClips.Count);
        var stoneClip = _stoneClips[rand];
        PlaySound(stoneClip);
    }

    public void PlayMainTheme()
    {
        PlayMusic(_mainTheme);

        _birdsSource.clip = _birds;
        _birdsSource.Play();
    }

    public void PlayMainMenuTheme()
    {
        PlayMusic(_mainMenuTheme);
    }

    public void PlayWinSound()
    {
        PlaySound(_winSound);
    }

    public void PlayLoseSound()
    {
        PlaySound(_looseSound);
    }

    public void PlayRandomBombSound()
    {
        var rand = Random.Range(0, _bombsSounds.Count);
        PlaySound(_bombsSounds[rand]);
    }
}
