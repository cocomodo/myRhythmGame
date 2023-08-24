using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class EndPanel : MonoBehaviour
{
    public UnityEvent OnEnableEvents;
    public UnityEvent OnDisalbeEvents;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighScoreText;

    private void Awake() 
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text=$"Score : {RhythmGameManager.instance.Score}";
        HighScoreText.text=$"High Score : {RhythmGameManager.instance.HighScore}";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text=$"Score : {RhythmGameManager.instance.Score}";
        HighScoreText.text=$"High Score : {RhythmGameManager.instance.HighScore}";
    }

    private void OnEnable() 
    {
        OnEnableEvents.Invoke();
    }

    private void OnDisable() 
    {
        OnDisalbeEvents.Invoke();
        
    }
}


