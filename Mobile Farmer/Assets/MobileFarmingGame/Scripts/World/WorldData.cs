using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData 
{
    public int[] chunkPrices;
    public WorldData(int Amount)
    {
        chunkPrices = new int[Amount];
    }
}
