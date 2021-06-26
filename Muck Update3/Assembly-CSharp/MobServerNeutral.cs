﻿// Decompiled with JetBrains decompiler
// Type: MobServerNeutral
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MobServerNeutral : MobServer
{
  public int mobZoneId { get; set; }

  private void Start() => this.FindPositionInterval = 12f;

  protected override void Behaviour()
  {
  }

  public override void TookDamage()
  {
    this.mob.SetSpeed(2f);
    this.SyncFindNextPosition();
  }

  protected override Vector3 FindNextPosition()
  {
    this.Invoke("SyncFindNextPosition", 12f);
    return MobZoneManager.Instance.zones[this.mobZoneId].FindRandomPos();
  }

  private void OnDisable() => this.CancelInvoke();

  private void OnEnable()
  {
    this.FindPositionInterval = 10f;
    this.StartRoutines();
  }
}
