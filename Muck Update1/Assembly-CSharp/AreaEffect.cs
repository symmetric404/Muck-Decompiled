﻿// Decompiled with JetBrains decompiler
// Type: AreaEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BACBFE5D-6724-4F02-B6BB-D6D37EC5478A
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
  private int damage;
  private List<GameObject> actorsHit;

  public void SetDamage(int d)
  {
    this.damage = d;
    this.GetComponent<Collider>().enabled = true;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Build"))
      return;
    Hitable component = other.GetComponent<Hitable>();
    if ((Object) component == (Object) null || other.transform.root.CompareTag("Local"))
      return;
    component.Hit(this.damage, 0.0f, 3, this.transform.position);
    Object.Destroy((Object) this);
  }
}
