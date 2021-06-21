﻿// Decompiled with JetBrains decompiler
// Type: Assets.Scripts.Networking.GameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 68ECCA8E-CF88-4CE2-9D74-1A5BFC0637BB
// Assembly location: D:\Repo\Muck Update2\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Networking
{
  public class GameManager
  {
    public static GameState state;

    public static void StartGame()
    {
      if (GameManager.NumPlayersLeftInServer() < 4 || GameManager.state == GameState.Playing)
        return;
      GameManager.state = GameState.Playing;
      Debug.Log((object) "Starting game");
      List<int> intList = new List<int>();
      foreach (Client client in Server.clients.Values)
      {
        if (client?.player != null)
          intList.Add(client.id);
      }
    }

    public void FindSpawnPositions()
    {
    }

    public static int NumPlayersLeftInGame()
    {
      int num = 0;
      foreach (Client client in Server.clients.Values)
      {
        if (client?.player != null)
          ++num;
      }
      return num;
    }

    public static int NumPlayersLeftAlive()
    {
      int num = 0;
      foreach (Client client in Server.clients.Values)
      {
        if (client?.player != null && !client.player.dead)
          ++num;
      }
      return num;
    }

    public static int NumPlayersLeftInServer()
    {
      int num = 0;
      foreach (Client client in Server.clients.Values)
      {
        if (client.player != null)
          ++num;
      }
      return num;
    }

    public static int NumPlayersReady()
    {
      int num = 0;
      foreach (Client client in Server.clients.Values)
      {
        if (client?.player != null && client.player.ready)
          ++num;
      }
      return num;
    }
  }
}
