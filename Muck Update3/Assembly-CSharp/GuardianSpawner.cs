﻿// Decompiled with JetBrains decompiler
// Type: GuardianSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSpawner : MonoBehaviour
{
  public GuardianSpawner.WeightedSpawn[] structurePrefabs;
  private int mapChunkSize;
  private float worldEdgeBuffer = 0.6f;
  public int maxCaves = 50;
  public int minCaves = 3;
  protected ConsistentRandom randomGen;
  public LayerMask whatIsTerrain;
  private List<GameObject> structures;
  public bool dontAddToResourceManager;
  private int type = 1;
  private int maxTypes = 5;
  private Vector3[] shrines;
  private float totalWeight;

  public float worldScale { get; set; } = 12f;

  private void Start()
  {
    this.structures = new List<GameObject>();
    this.randomGen = new ConsistentRandom(GameManager.GetSeed() + ResourceManager.GetNextGenOffset());
    this.shrines = new Vector3[this.maxCaves];
    this.mapChunkSize = MapGenerator.mapChunkSize;
    this.worldScale *= this.worldEdgeBuffer;
    foreach (GuardianSpawner.WeightedSpawn structurePrefab in this.structurePrefabs)
      this.totalWeight += structurePrefab.weight;
    int index = 0;
    int num = 0;
    while (index < this.maxCaves)
    {
      ++num;
      Vector3 vector3 = new Vector3((float) ((this.randomGen.NextDouble() * 2.0 - 1.0) * (double) this.mapChunkSize / 2.0), 0.0f, (float) ((this.randomGen.NextDouble() * 2.0 - 1.0) * (double) this.mapChunkSize / 2.0)) * this.worldScale;
      vector3.y = 200f;
      Debug.DrawLine(vector3, vector3 + Vector3.down * 500f, Color.cyan, 50f);
      RaycastHit hitInfo;
      if (Physics.Raycast(vector3, Vector3.down, out hitInfo, 500f, (int) this.whatIsTerrain))
      {
        if (WorldUtility.WorldHeightToBiome(hitInfo.point.y) == TextureData.TerrainType.Grass && (double) Mathf.Abs(Vector3.Angle(Vector3.up, hitInfo.normal)) <= 15.0)
        {
          this.shrines[index] = hitInfo.point;
          ++index;
          GameObject objectToSpawn = this.FindObjectToSpawn(this.structurePrefabs, this.totalWeight);
          GameObject newStructure = UnityEngine.Object.Instantiate<GameObject>(objectToSpawn, hitInfo.point, objectToSpawn.transform.rotation);
          if (!this.dontAddToResourceManager)
            newStructure.GetComponentInChildren<SharedObject>().SetId(ResourceManager.Instance.GetNextId());
          this.structures.Add(newStructure);
          this.Process(newStructure, hitInfo);
        }
        else
          continue;
      }
      if (num > this.maxCaves * 100 && index >= this.minCaves || num > this.maxCaves * 100)
        break;
    }
    if (!this.dontAddToResourceManager)
      ResourceManager.Instance.AddResources(this.structures);
    if (this.structures.Count == 5)
      return;
    Debug.LogError((object) "Failed to spawn all guardians, whopsie dopsie");
  }

  public virtual void Process(GameObject newStructure, RaycastHit hit)
  {
    newStructure.GetComponentInChildren<ShrineGuardian>().type = (Guardian.GuardianType) this.type;
    ++this.type;
  }

  private void OnDrawGizmos()
  {
  }

  public GameObject FindObjectToSpawn(
    GuardianSpawner.WeightedSpawn[] structurePrefabs,
    float totalWeight)
  {
    float num1 = (float) this.randomGen.NextDouble();
    float num2 = 0.0f;
    for (int index = 0; index < structurePrefabs.Length; ++index)
    {
      num2 += structurePrefabs[index].weight;
      if ((double) num1 < (double) num2 / (double) totalWeight)
        return structurePrefabs[index].prefab;
    }
    return structurePrefabs[0].prefab;
  }

  [Serializable]
  public class WeightedSpawn
  {
    public GameObject prefab;
    public float weight;
  }
}
