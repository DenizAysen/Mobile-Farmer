using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChunkDetection : MonoBehaviour
{
    [Header(" Actions ")]
    public static Action<AppleTree> onEnteredAppleZone;
    public static Action<AppleTree> onExitedAppleZone;
    private void OnTriggerStay(Collider other)
    {
        Chunk chunk;
        if(other.CompareTag("ChunkTrigger"))
        {
            chunk = other.GetComponentInParent<Chunk>();
            chunk.TryUnlock();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out AppleTree tree))
        {
            TriggeredAppleTree(tree);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AppleTree tree))
        {
            ExitedAppleTreeZone(tree);
        }
    }

    private void TriggeredAppleTree(AppleTree tree)
    {
        Debug.Log("We have entered tree zone");
        onEnteredAppleZone?.Invoke(tree);
    }
    private void ExitedAppleTreeZone(AppleTree tree)
    {
        Debug.Log("We have exited tree zone");
        onExitedAppleZone?.Invoke(tree);
    }
}
