﻿// Decompiled with JetBrains decompiler
// Type: LobbySettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BACBFE5D-6724-4F02-B6BB-D6D37EC5478A
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class LobbySettings : MonoBehaviour
{
  public UiSettings difficultySetting;
  public UiSettings friendlyFireSetting;
  public UiSettings gamemodeSetting;
  public TMP_InputField seed;
  public GameObject startButton;
  public static LobbySettings Instance;

  private void Awake() => LobbySettings.Instance = this;

  private void Start()
  {
    this.difficultySetting.AddSettings(1, Enum.GetNames(typeof (GameSettings.Difficulty)));
    this.friendlyFireSetting.AddSettings(0, Enum.GetNames(typeof (GameSettings.FriendlyFire)));
    this.gamemodeSetting.AddSettings(0, Enum.GetNames(typeof (GameSettings.GameMode)));
  }
}
