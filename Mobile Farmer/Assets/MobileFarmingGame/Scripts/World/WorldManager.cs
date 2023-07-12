using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class WorldManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform world;

    [Header("Data")]
    private WorldData worldData;
    private string dataPath;
    private bool shouldSave = false;
    private void Awake()
    {
        Chunk.onUnlocked += ChunkUnlockedCallBack;
        Chunk.onPriceChanged += ChunkPriceChangedCallBack;
    }
    void Start()
    {
        dataPath = Application.dataPath + "/WorldData.txt";
        LoadWorld();
        Initialize();

        InvokeRepeating("TrySaveGame", 1, 1);
    }
    private void OnDestroy()
    {
        Chunk.onUnlocked -= ChunkUnlockedCallBack;
        Chunk.onPriceChanged -= ChunkPriceChangedCallBack;
    }
    private void Initialize()
    {
        for (int i = 0; i < world.childCount; i++)
        {
            world.GetChild(i).GetComponent<Chunk>().Initialaze(worldData.chunkPrices[i]);
        }
    }
    private void TrySaveGame()
    {
        if (shouldSave)
        {
            Debug.Log("Saving the world");
            SaveWold();
            shouldSave = false;
        }
    }
    #region CallBacks
    private void ChunkUnlockedCallBack()
    {
        Debug.Log("Chunk unlocked");
        SaveWold();
    }
    private void ChunkPriceChangedCallBack()
    {
        shouldSave = true;
    }
    #endregion
    #region WorldData
    private void LoadWorld()
    {       
        string data = "";

        if (!File.Exists(dataPath))
        {
            FileStream fs = new FileStream(dataPath, FileMode.Create);

            worldData = new WorldData();

            for (int i = 0; i < world.childCount; i++)
            {
                int chunkInitialPrice = world.GetChild(i).GetComponent<Chunk>().GetInitialPrice();
                worldData.chunkPrices.Add(chunkInitialPrice);
            }
            string worldDataString = JsonUtility.ToJson(worldData, true);

            byte[] worldDataBytes = Encoding.UTF8.GetBytes(worldDataString);

            fs.Write(worldDataBytes);

            fs.Close();
        }
        else
        {
            data = File.ReadAllText(dataPath);
            worldData = JsonUtility.FromJson<WorldData>(data);

            if(worldData.chunkPrices.Count < world.childCount)
            {
                UpdateData();
            }
        }
    }
    private void SaveWold()
    {
        if (worldData.chunkPrices.Count != world.childCount)
            worldData = new WorldData();

        for (int i = 0; i < world.childCount; i++)
        {
            int chunkCurrentPrice = world.GetChild(i).GetComponent<Chunk>().GetCurrentPrice();

            if (worldData.chunkPrices.Count > i)
                worldData.chunkPrices[i] = chunkCurrentPrice;
            else
                worldData.chunkPrices.Add(chunkCurrentPrice);
        }

        string data = JsonUtility.ToJson(worldData, true);
        File.WriteAllText(dataPath, data);

        Debug.LogWarning("Data saved");
    }
    private void UpdateData()
    {
        int missingData = world.childCount - worldData.chunkPrices.Count;

        for (int i = 0; i < missingData; i++)
        {
            int chunkIndex = world.childCount - missingData + i;
            int chunkPrice = world.GetChild(chunkIndex).GetComponent<Chunk>().GetInitialPrice();
            worldData.chunkPrices.Add(chunkPrice);
        }
    }
    #endregion
}
