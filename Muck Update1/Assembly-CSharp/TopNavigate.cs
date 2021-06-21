﻿// Decompiled with JetBrains decompiler
// Type: TopNavigate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BACBFE5D-6724-4F02-B6BB-D6D37EC5478A
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
