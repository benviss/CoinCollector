using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:Singleton<AudioManager> {

  public bool isMuted = true;

  [SerializeField]
  private AudioClip music, death, coin, jump;

  public AudioSource musicSource;
  public AudioSource sfxSource;

  private void Start() {
    if(!isMuted) {
      PlayMusic();
    }
  }

  public void ToggleMusic() {
    isMuted = !isMuted;
  }

  void PlayMusic() {
    if(isMuted) return;
    if(musicSource.isActiveAndEnabled && !musicSource.isPlaying)
      musicSource.clip = music;
    musicSource.Play();
  }

  public void PlayDeathSound() {
    PlayClip(death);
  }

  public void PlayCoinSound() {
    PlayClip(coin);
  }

  public void PlayJumpSound() {
    PlayClip(jump);
  }

  void PlayClip(AudioClip clip) {
    if(isMuted) return;
    if(musicSource.isActiveAndEnabled && !sfxSource.isPlaying) {
      sfxSource.PlayOneShot(clip);
    }
  }
}
