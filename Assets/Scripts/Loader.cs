using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public RhythmGameManager gameManager;
    // Start is called before the first frame update
    private void Awake()
    {
        if (RhythmGameManager.instance==null)
        {
            Instantiate(gameManager);
        }
    }
}
