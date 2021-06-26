﻿// Decompiled with JetBrains decompiler
// Type: TestScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TestScroll : MonoBehaviour
{
  public NoiseData noise;
  public TerrainData terrain;
  public bool ready;
  public static TestScroll Instance;

  private void Awake()
  {
    TestScroll.Instance = this;
    this.Invoke("GetReady", 4f);
  }

  private void GetReady() => this.ready = true;

  private void Update()
  {
    if (!this.ready || (double) this.terrain.heightMultiplier > 300.0)
      return;
    this.terrain.heightMultiplier += 20f * Time.deltaTime;
  }
}
