﻿// Decompiled with JetBrains decompiler
// Type: Skills
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 68ECCA8E-CF88-4CE2-9D74-1A5BFC0637BB
// Assembly location: D:\Repo\Muck Update2\Assembly-CSharp.dll

using UnityEngine;

public class Skills : MonoBehaviour
{
  public static int woodcuttingXp;
  public static int miningXp;
  public static int buildingXp;
  public static int craftingXp;
  private static float x = 0.07f;
  private static float y = 1.55f;

  public static int XpToLevel(int xp) => Mathf.FloorToInt(Skills.NthRoot((float) xp, Skills.y) * Skills.x);

  public static int XpForLevel(int level) => (int) Mathf.Pow((float) level / Skills.x, Skills.y);

  public static float LevelProgress(int xp)
  {
    int level = Skills.XpToLevel(xp);
    return (float) (xp - Skills.XpForLevel(level)) / (float) (Skills.XpForLevel(level + 1) - Skills.XpForLevel(level));
  }

  public static float NthRoot(float A, float N) => Mathf.Pow(A, 1f / N);

  public Skills() => base.\u002Ector();
}
