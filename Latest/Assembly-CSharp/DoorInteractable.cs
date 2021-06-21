﻿// Decompiled with JetBrains decompiler
// Type: DoorInteractable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BACBFE5D-6724-4F02-B6BB-D6D37EC5478A
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DoorInteractable : MonoBehaviour, Interactable, SharedObject
{
  private int id;
  private bool opened;
  private float desiredYRotation;
  public Transform pivot;

  public void Interact() => ClientSend.PickupInteract(this.id);

  public void LocalExecute()
  {
  }

  public void AllExecute()
  {
    this.opened = !this.opened;
    if (this.opened)
      this.desiredYRotation = 90f;
    else
      this.desiredYRotation = 0.0f;
  }

  private void Update() => this.pivot.rotation = Quaternion.Lerp(this.pivot.rotation, Quaternion.Euler(0.0f, this.desiredYRotation, 0.0f), Time.deltaTime * 5f);

  public void ServerExecute()
  {
  }

  public void RemoveObject()
  {
  }

  private void OnDestroy()
  {
    MonoBehaviour.print((object) "door destroyed");
    ResourceManager.Instance.RemoveItem(this.id);
  }

  public string GetName() => this.opened ? "Close Door" : "Open Door";

  public void SetId(int id) => this.id = id;

  public int GetId() => this.id;
}
