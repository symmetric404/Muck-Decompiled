﻿// Decompiled with JetBrains decompiler
// Type: MeshSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu]
public class MeshSettings : UpdatableData
{
  public const int numSupportedLODs = 5;
  public const int numSupportedChunkSizes = 9;
  public const int numSupportedFlatshadedChunkSizes = 3;
  public static readonly int[] supportedChunkSizes = new int[9]
  {
    48,
    72,
    96,
    120,
    144,
    168,
    192,
    216,
    240
  };
  public float meshScale = 2.5f;
  public bool useFlatShading;
  [Range(0.0f, 8f)]
  public int chunkSizeIndex;
  [Range(0.0f, 2f)]
  public int flatshadedChunkSizeIndex;

  public int numVertsPerLine => MeshSettings.supportedChunkSizes[this.useFlatShading ? this.flatshadedChunkSizeIndex : this.chunkSizeIndex] + 5;

  public float meshWorldSize => (float) (this.numVertsPerLine - 3) * this.meshScale;
}
