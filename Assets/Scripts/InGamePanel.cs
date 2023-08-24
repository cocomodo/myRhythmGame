using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class InGamePanel : MonoBehaviour
{
    public UnityEvent OnEnableEvents;
    public UnityEvent OnDisableEvents;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI SpeedText;

    void Start()
    {
        
        scoreText.text=$"Score : {RhythmGameManager.instance.Score}";
        LifeText.text=$"Life : {RhythmGameManager.instance.Life}";
        SpeedText.text=$"Speed x {RhythmGameManager.instance.Speed}";
    }
    void Update()
    {
        AddScore();
        LifeChange();
        SpeedChange();
    }

    void AddScore()
    {
        scoreText.text=$"Score : {RhythmGameManager.instance.Score}";

    }

    void LifeChange()
    {
        if(RhythmGameManager.instance.Life>0)
        {
            LifeText.text=$"Life : {RhythmGameManager.instance.Life}";
        }
        else
        {
            LifeText.text=$"Life : 0";
            
        }

    }

    void SpeedChange()
    {
        SpeedText.text=$"Speed x {RhythmGameManager.instance.Speed}";

    }

    private void OnEnable() 
    {
        OnEnableEvents.Invoke();
    }

    private void OnDisable() 
    {
        OnDisableEvents.Invoke();
    }
}
