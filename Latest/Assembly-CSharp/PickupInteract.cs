﻿// Decompiled with JetBrains decompiler
// Type: PickupInteract
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BACBFE5D-6724-4F02-B6BB-D6D37EC5478A
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PickupInteract : MonoBehaviour, Interactable, SharedObject
{
  public InventoryItem item;
  public int amount;
  public int id;
  private Vector3 defaultScale;
  private Vector3 desiredScale;

  private void Awake()
  {
    this.defaultScale = this.transform.localScale;
    this.desiredScale = this.defaultScale;
  }

  public void Interact() => ClientSend.PickupInteract(this.id);

  public void LocalExecute()
  {
    InventoryItem instance = ScriptableObject.CreateInstance<InventoryItem>();
    instance.Copy(this.item, this.amount);
    InventoryUI.Instance.AddItemToInventory(instance);
  }

  public void AllExecute() => this.RemoveObject();

  public void ServerExecute()
  {
  }

  public void RemoveObject()
  {
    Object.Destroy((Object) this.gameObject);
    ResourceManager.Instance.RemoveInteractItem(this.id);
  }

  public string GetName() => this.item.name + "\n<size=50%>(Press \"E\" to pickup";

  public void SetId(int id) => this.id = id;

  private void Update()
  {
    this.desiredScale = Vector3.Lerp(this.desiredScale, this.defaultScale, Time.deltaTime * 15f);
    this.desiredScale.y = this.defaultScale.y;
    this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.desiredScale, Time.deltaTime * 15f);
  }

  public int GetId() => this.id;
}
