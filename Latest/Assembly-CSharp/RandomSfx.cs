﻿// Decompiled with JetBrains decompiler
// Type: RandomSfx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BACBFE5D-6724-4F02-B6BB-D6D37EC5478A
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RandomSfx : MonoBehaviour
{
  public AudioClip[] sounds;
  [Range(0.0f, 2f)]
  public float maxPitch = 0.8f;
  [Range(0.0f, 2f)]
  public float minPitch = 1.2f;
  private AudioSource s;
  public bool playOnAwake = true;

  private void Awake()
  {
    this.s = this.GetComponent<AudioSource>();
    if (!this.playOnAwake)
      return;
    this.Randomize(0.0f);
  }

  public void Randomize(float delay)
  {
    this.s.clip = this.sounds[Random.Range(0, this.sounds.Length)];
    this.s.pitch = Random.Range(this.minPitch, this.maxPitch);
    this.s.PlayDelayed(delay);
  }
}
