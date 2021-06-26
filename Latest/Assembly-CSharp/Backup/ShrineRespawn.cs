﻿// Decompiled with JetBrains decompiler
// Type: ShrineRespawn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ShrineRespawn : MonoBehaviour, SharedObject, Interactable
{
  private int id;

  private void Start()
  {
  }

  public void Interact() => RespawnTotemUI.Instance.Show();

  public void LocalExecute()
  {
  }

  public void AllExecute()
  {
  }

  public void ServerExecute(int fromClient)
  {
  }

  public void RemoveObject() => Object.Destroy((Object) this.gameObject);

  public string GetName() => "Revive the homies";

  public bool IsStarted() => false;

  public void SetId(int id) => this.id = id;

  public int GetId() => this.id;
}
