﻿// Decompiled with JetBrains decompiler
// Type: MusicController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BACBFE5D-6724-4F02-B6BB-D6D37EC5478A
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MusicController : MonoBehaviour
{
  public AudioClip[] day;
  public AudioClip[] night;
  public AudioClip[] boss;
  private AudioSource audio;
  public static MusicController Instance;
  private AudioClip queuedSong;
  private float fadeTime = 6f;
  private float targetVolume = 0.2f;
  private MusicController.SongType currentSong;
  private float currentTime;
  private float newFadeTime;
  private float desiredVolume;
  private float startVolume;

  private void Awake()
  {
    MusicController.Instance = this;
    this.audio = this.GetComponent<AudioSource>();
  }

  private void Start() => this.targetVolume = CurrentSettings.Instance.music;

  public void SetVolume(float f)
  {
    this.targetVolume = f;
    this.StartFade(this.audio, 0.1f, f);
  }

  public void PlaySong(MusicController.SongType s, bool chanceToSkip = true)
  {
    AudioClip song = (AudioClip) null;
    if (this.currentSong == MusicController.SongType.Boss && (Object) BossUI.Instance.currentBoss != (Object) null)
      return;
    this.currentSong = s;
    switch (s)
    {
      case MusicController.SongType.Day:
        if (!chanceToSkip || (double) Random.Range(0.0f, 1f) <= 0.5)
        {
          song = this.day[Random.Range(0, this.day.Length)];
          break;
        }
        break;
      case MusicController.SongType.Night:
        song = this.night[Random.Range(0, this.night.Length)];
        break;
      case MusicController.SongType.Boss:
        song = this.boss[Random.Range(0, this.boss.Length)];
        break;
    }
    if ((Object) song == (Object) null)
      this.StartFade(this.audio, this.fadeTime, 0.0f);
    else if (this.audio.isPlaying)
    {
      this.queuedSong = song;
      this.StartFade(this.audio, this.fadeTime, 0.0f);
      this.Invoke("NextSong", this.fadeTime);
    }
    else
      this.NextSong(song);
  }

  private void NextSong()
  {
    this.StartFade(this.audio, this.fadeTime, this.targetVolume);
    this.audio.clip = this.queuedSong;
    this.audio.Play();
  }

  private void NextSong(AudioClip song)
  {
    this.StartFade(this.audio, this.fadeTime, this.targetVolume);
    this.audio.clip = song;
    this.audio.Play();
  }

  public void StopSong() => this.StartFade(this.audio, this.fadeTime, 0.0f);

  private void Update()
  {
    this.currentTime += Time.deltaTime;
    this.audio.volume = Mathf.Lerp(this.startVolume, this.desiredVolume * this.targetVolume, this.currentTime / this.newFadeTime);
  }

  private void StartFade(AudioSource audioSource, float duration, float targetVolume)
  {
    this.currentTime = 0.0f;
    this.newFadeTime = duration;
    this.desiredVolume = targetVolume;
    this.startVolume = audioSource.volume;
  }

  public enum SongType
  {
    Day,
    Night,
    Boss,
  }
}
