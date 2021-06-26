﻿// Decompiled with JetBrains decompiler
// Type: TopNavigate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class TopNavigate : MonoBehaviour
{
  public GameObject[] settingMenus;
  public TextMeshProUGUI[] texts;
  public Color selectedColor;
  public Color idleColor;

  private void OnEnable() => this.Select(0);

  public void Select(int selected)
  {
    for (int index = 0; index < this.settingMenus.Length; ++index)
    {
      if (index == selected)
      {
        this.settingMenus[index].SetActive(true);
        this.texts[index].color = this.selectedColor;
      }
      else
      {
        this.settingMenus[index].SetActive(false);
        this.texts[index].color = this.idleColor;
      }
    }
  }
}
