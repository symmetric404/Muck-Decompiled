﻿// Decompiled with JetBrains decompiler
// Type: SteamLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 68ECCA8E-CF88-4CE2-9D74-1A5BFC0637BB
// Assembly location: D:\Repo\Muck Update2\Assembly-CSharp.dll

using Steamworks;
using Steamworks.Data;
using System.Collections.Generic;
using UnityEngine;

public class SteamLobby : MonoBehaviour
{
  private Lobby currentLobby;
  public static Dictionary<ulong, int> steamIdToClientId = new Dictionary<ulong, int>();
  public GameObject startButton;
  public static int lobbySize = 10;
  private bool started;
  public static SteamLobby Instance;

  private void Awake()
  {
    if (Object.op_Implicit((Object) SteamLobby.Instance))
    {
      Object.Destroy((Object) ((Component) this).get_gameObject());
    }
    else
    {
      SteamLobby.Instance = this;
      Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
    }
  }

  private void InitLobby(Lobby l)
  {
    this.currentLobby = l;
    this.InitLobbyClients();
    SteamLobby.steamIdToClientId = new Dictionary<ulong, int>();
  }

  public void StartLobby(SteamId hostSteamId, Lobby l)
  {
    this.InitLobby(l);
    LobbySettings.Instance.startButton.SetActive(true);
    this.AddPlayerToLobby(new Friend(hostSteamId));
    this.started = false;
  }

  public void CloseLobby()
  {
    SteamLobby.steamIdToClientId = new Dictionary<ulong, int>();
    this.startButton.SetActive(false);
    this.started = false;
  }

  public void AddPlayerToLobby(Friend friend)
  {
    SteamId steamId = SteamId.op_Implicit((ulong) ((SteamId) friend.Id).Value);
    int availableLobbyId = this.FindAvailableLobbyId();
    if (availableLobbyId == -1)
      return;
    Debug.Log((object) ("Found available id in steam as: " + (object) availableLobbyId + ", steam name: " + ((Friend) ref friend).get_Name()));
    SteamLobby.steamIdToClientId[SteamId.op_Implicit(steamId)] = availableLobbyId;
    Client client = Server.clients[availableLobbyId];
    client.inLobby = true;
    client.player = new Player(availableLobbyId, ((Friend) ref friend).get_Name(), Color.get_black(), steamId);
    MonoBehaviour.print((object) "finished adding player");
  }

  public void RemovePlayerFromLobby(Friend friend)
  {
    SteamId steamId = SteamId.op_Implicit((ulong) ((SteamId) friend.Id).Value);
    int num = SteamLobby.steamIdToClientId[(ulong) steamId.Value];
    Server.clients[num] = new Client(num);
    SteamLobby.steamIdToClientId.Remove((ulong) ((SteamId) friend.Id).Value);
    if (!this.started || !Object.op_Implicit((Object) GameManager.instance))
      return;
    ServerSend.DisconnectPlayer(num);
  }

  private void InitLobbyClients()
  {
    MonoBehaviour.print((object) "initing lobby");
    Server.clients = new Dictionary<int, Client>();
    for (int index = 0; index < SteamLobby.lobbySize; ++index)
      Server.clients[index] = new Client(index);
  }

  private int FindAvailableLobbyId()
  {
    for (int key = 0; key < SteamLobby.lobbySize; ++key)
    {
      if (!Server.clients[key].inLobby)
        return key;
    }
    return -1;
  }

  public void StartGame()
  {
    if (SteamClient.get_SteamId().Value != ((SteamId) ((Lobby) ref this.currentLobby).get_Owner().Id).Value)
    {
      Debug.LogError((object) "not owner, so cant start lobby");
    }
    else
    {
      MonoBehaviour.print((object) "starting lobby");
      GameSettings settings = this.MakeSettings();
      if (settings.gameMode == GameSettings.GameMode.Versus && ((Lobby) ref this.currentLobby).get_MemberCount() < 2)
      {
        StatusMessage.Instance.DisplayMessage("Need at least 2 players to play versus.");
      }
      else
      {
        foreach (Client client in Server.clients.Values)
        {
          MonoBehaviour.print((object) client);
          if (client != null)
            MonoBehaviour.print((object) client.player);
          if (client?.player != null)
          {
            MonoBehaviour.print((object) "sending start game");
            ServerSend.StartGame(client.player.id, settings);
          }
        }
        ((Lobby) ref this.currentLobby).SetJoinable(false);
        this.started = true;
        MonoBehaviour.print((object) "Starting game done");
        LocalClient.serverOwner = true;
        LocalClient.instance.serverHost = SteamManager.Instance.PlayerSteamId;
      }
    }
  }

  private int FindSeed()
  {
    string text = LobbySettings.Instance.seed.get_text();
    int result;
    return !(text == "") ? (!int.TryParse(text, out result) ? text.GetHashCode() : result) : Random.Range(int.MinValue, int.MaxValue);
  }

  private GameSettings MakeSettings()
  {
    GameSettings gameSettings = new GameSettings(this.FindSeed());
    gameSettings.difficulty = (GameSettings.Difficulty) LobbySettings.Instance.difficultySetting.setting;
    gameSettings.friendlyFire = (GameSettings.FriendlyFire) LobbySettings.Instance.friendlyFireSetting.setting;
    gameSettings.gameMode = (GameSettings.GameMode) LobbySettings.Instance.gamemodeSetting.setting;
    gameSettings.multiplayer = (GameSettings.Multiplayer) Mathf.Clamp(((Lobby) ref this.currentLobby).get_MemberCount() - 1, 0, 1);
    if (gameSettings.gameMode == GameSettings.GameMode.Versus)
      gameSettings.friendlyFire = GameSettings.FriendlyFire.On;
    return gameSettings;
  }

  public SteamLobby() => base.\u002Ector();
}
