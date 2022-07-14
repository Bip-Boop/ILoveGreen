using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int drops;
    public bool[] unlockedSkins;
    public int maxStageReached;
    public int rank;

    public GameData(int drops, bool[] unlockedSkins, int maxStageReached, int rank)
    {
        this.drops = drops;
        this.unlockedSkins = unlockedSkins;
        this.maxStageReached = maxStageReached;
        this.rank = rank;
    }
}
