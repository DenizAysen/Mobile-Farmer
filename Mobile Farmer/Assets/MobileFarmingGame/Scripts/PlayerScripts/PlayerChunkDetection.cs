using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChunkDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Chunk chunk;
        if(other.CompareTag("ChunkTrigger"))
        {
            chunk = other.GetComponentInParent<Chunk>();
            chunk.TryUnlock();
        }
    }
}
