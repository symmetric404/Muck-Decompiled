﻿// Decompiled with JetBrains decompiler
// Type: Cave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Cave : MonoBehaviour
{
  public TextureData textureData;
  public SpawnChestsInLocations chestSpawner;
  private MeshRenderer rend;
  private ConsistentRandom rand;

  public void SetCave(ConsistentRandom rand)
  {
    this.rand = rand;
    this.rend = this.GetComponent<MeshRenderer>();
    this.rend.material.color = (this.rend.material.color + this.textureData.layers[2].tint) / 2f;
    this.chestSpawner.SetChests(rand);
    foreach (SpawnResourcesInLocations componentsInChild in this.GetComponentsInChildren<SpawnResourcesInLocations>())
      componentsInChild.SetResources(rand);
  }
}
