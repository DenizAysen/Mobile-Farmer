using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class WorldManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform world;
    Chunk[,] grid;

    [Header("Settings")]
    [SerializeField] private int gridSize;
    [SerializeField] private int gridScale;

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

        InitializeGrid();
        UpdateChunkWalls();
        UpdateGridRenderers();
    }
    private void InitializeGrid()
    {
        grid = new Chunk[20, 20];

        Chunk chunk;

        for (int i = 0; i < world.childCount; i++)
        {
            chunk = world.GetChild(i).GetComponent<Chunk>();

            Vector2Int chunkGridPosition = new Vector2Int((int)(chunk.transform.position.x/gridScale),
                (int)(chunk.transform.position.z/gridScale));

            chunkGridPosition += new Vector2Int(gridSize / 2, gridSize / 2);

            grid[chunkGridPosition.x, chunkGridPosition.y] = chunk;
        }
    }
    private bool IsValidGridPosition(int x , int y)
    {
        if(x < 0 || x >= gridSize  || y < 0 || y>= gridSize)
        {
            return false;
        }
        return true;
    }
    private void UpdateChunkWalls()
    {//Collums
        Chunk chunk,frontChunk,backChunk
            ,rightChunk,leftChunk = null;
        int configuration;
        for (int x = 0; x < grid.GetLength(0); x++)
        {//Lines
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                chunk = grid[x, y];

                if (chunk == null)
                    continue;

                configuration = 0;

                if (IsValidGridPosition(x, y + 1))
                {
                    frontChunk = grid[x, y + 1];
                    if (frontChunk != null && frontChunk.IsUnlocked())
                    {
                        configuration = configuration + 1;
                    }
                }

                if (IsValidGridPosition(x + 1, y))
                {
                    rightChunk = grid[x + 1, y];
                    if (rightChunk != null && rightChunk.IsUnlocked())
                        configuration = configuration + 2;
                }

                if (IsValidGridPosition(x, y - 1))
                {
                    backChunk = grid[x, y - 1];
                    if (backChunk != null && backChunk.IsUnlocked())
                    {
                        configuration = configuration + 4;
                    }
                }

                if (IsValidGridPosition(x - 1, y))
                {
                    leftChunk = grid[x - 1, y];
                    if (leftChunk != null && leftChunk.IsUnlocked())
                        configuration = configuration + 8;
                }

                chunk.UpdateWalls(configuration);
            }
        }
    }
    private void UpdateGridRenderers()
    {
        Chunk chunk, frontChunk, backChunk
            , rightChunk, leftChunk = null;
        for (int x = 0; x < grid.GetLength(0); x++)
        {//Lines
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                chunk = grid[x, y];

                if (chunk == null)
                    continue;

                if (chunk.IsUnlocked())
                    continue;

                frontChunk = IsValidGridPosition(x, y + 1) ? grid[x, y + 1] : null;
                rightChunk = IsValidGridPosition(x + 1, y) ? grid[x + 1, y] : null;
                backChunk = IsValidGridPosition(x, y - 1) ? grid[x, y - 1] : null;
                leftChunk = IsValidGridPosition(x - 1, y) ? grid[x - 1, y] : null;

                if (frontChunk != null && frontChunk.IsUnlocked())
                   chunk.DisplayLockedElements();
                else if(rightChunk != null && rightChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
                else if(backChunk != null && backChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
                else if (leftChunk != null && leftChunk.IsUnlocked())
                    chunk.DisplayLockedElements();
            }
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

        UpdateChunkWalls();
        UpdateGridRenderers();

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
