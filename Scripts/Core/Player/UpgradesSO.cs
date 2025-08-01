using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesSO", menuName = "Scriptable Objects/UpgradesSO")]
public class UpgradesSO : ScriptableObject
{
    public LevelUpgrade[] levels;
}

[Serializable]
public class LevelUpgrade
{
    public float diameter = 0;
    public int cost = 0;
}