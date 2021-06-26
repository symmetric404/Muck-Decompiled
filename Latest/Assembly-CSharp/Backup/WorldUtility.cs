﻿// Decompiled with JetBrains decompiler
// Type: WorldUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 557671A5-6448-43F1-8017-7CE07FCBB682
// Assembly location: D:\SteamLibrary\steamapps\common\Muck\Muck_Data\Managed\Assembly-CSharp.dll

public class WorldUtility
{
  public static TextureData.TerrainType WorldHeightToBiome(float height)
  {
    float heightMultiplier = MapGenerator.Instance.terrainData.heightMultiplier;
    height /= heightMultiplier;
    TextureData.Layer[] layers = MapGenerator.Instance.textureData.layers;
    for (int index = layers.Length - 1; index > 0; --index)
    {
      if ((double) height >= (double) layers[index].startHeight)
        return layers[index].type;
    }
    return TextureData.TerrainType.Water;
  }
}
