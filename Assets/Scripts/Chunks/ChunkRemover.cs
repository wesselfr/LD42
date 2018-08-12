using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRemover : MonoBehaviour {

    private void OnApplicationQuit()
    {
        Chunk[] chunks = GameObject.FindObjectsOfType<Chunk>();
        StartCoroutine(InitializeQuit(chunks));
    }

    private IEnumerator InitializeQuit(Chunk[] chunks)
    {
        Application.Quit();
        foreach(Chunk chunk in chunks)
        {
            yield return StartCoroutine(chunk.Dispose());
        }
        
        yield return new WaitForEndOfFrame();
    }
}
