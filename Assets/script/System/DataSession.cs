using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class DataSession
{
  public static DataSession Default = new DataSession();
  public MonoBase player;
  public MonoBase damageGunAndRocet;
  public MonoBase gun;
  public MonoBase rocet;
  public MonoBase fire;
  public MonoBase csPoint;
  public MonoBase numberRocet;
  public MonoBase maxRocet;
  public List<MonoBase> enemies = new List<MonoBase>();
}