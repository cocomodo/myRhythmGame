using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SonicBloom.Koreo.Demos;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RhythmGameManager : MonoBehaviour
{
    public static RhythmGameManager instance;
    
    private int score=0;
    
    private int life=2;
    private int highScoreValue;
    public NoteColor color;
    public FxPool fxPool;
    
    public PlayerMove playerMove;
    public UnityEvent onMusicOver;
    public RhythmGameController controller;
    // public TextMeshProUGUI scoreText;
    // public TextMeshProUGUI highScore;
    // public TextMeshProUGUI LifeText;
    // public TextMeshProUGUI SpeedText;

    
    public GameObject restartPanel;
    public GameObject EndPanel;
    public GameObject firstPanel;
    public GameObject inGamePanel;

    private WaitForSeconds timeToWait =new WaitForSeconds(1.5f);
    public int Score
    {
        get
        {
            return score;
        }
    }
    public int Life
    {
        get
        {
            return life;
        }
    }

    public float Speed
    {
        get
        {
            return controller.audioCom.pitch;
        }
    }

    public int HighScore
    {
        get
        {
            return highScoreValue;
        }
        set
        {
            highScoreValue=value;
        }
    }
    private void Awake() 
    {
        if(instance==null)
        {
            instance=this;

        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        HighScore=PlayerPrefs.GetInt("HighScore",0);
        // highScore.text="High Score : "+PlayerPrefs.GetInt("HighScore",0);
        InitializeObjectPools();
    }

    // Update is called once per frame
    void Update()
    {
        
        //리핏 할때마다, 피치가 올라간 상태로 게임을 다시 시작할때마다 GetLatestSampleTime값이 줄어든다. 이게 줄어들어서
        //일관되게 값이 나오지 않고, 나중에 가면 하드코딩을 통해서 재시동 시키게 한 부분에서 예외값이 발생해서 시동이 안된다. 
        //현재곡의 samples는 2419200
        //샘플레이트 8000만큼 여유분 두니까 7배속까지 가더라도 작동하는데는 문제없다. 일단은 이정도만 두고 
        //다른 부분을 먼저 손본뒤에 나중에 이 부분을 더욱 확실하게 손보도록 하자. 
        if(controller.playingKoreo.GetLatestSampleTime()+8000>=controller.audioCom.clip.samples)
        {
            controller.RepeatGame();
            controller.touchStarted=1;
            
            //3.200001~3.800001 등으로 표현되는 문제때문에 작성한 코드.
            // SpeedText.text="Speed x "+ string.Format("{0:0.#}",controller.audioCom.pitch);
        }
        // Debug.Log(controller.playingKoreo.GetLatestSampleTimeDelta());
        //이건 테스트용으로 초반에 바로 피치를 바꿨을때 확인하기 위한 용도로 항상 업데이트메서드에서 불려지도록 만든 부분
        //테스트때만 쓰고 일반때는 이프문 속에서 출력하기로 한다. 
        // SpeedText.text="Speed x "+ string.Format("{0:0.#}",controller.audioCom.pitch);


    }
    
    public void AddScore(int newScore)
    {
        score +=newScore;
        // scoreText.text="Score : "+score;

        if(score>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore",score);
            // highScore.text="High Score : " +score;
        }
    }
    
    private void Reset() {
        PlayerPrefs.DeleteKey("HighScore");
        // highScore.text="High Score : 0";
    }

    public void InitializeObjectPools()
		{
            foreach (var pool in fxPool.GetComponentsInChildren<ObjectPool>())
                pool.Initialize();
		}
    public void ResetLevelData()
    {
        foreach (var pool in fxPool.GetComponentsInChildren<ObjectPool>())
            {
                pool.Reset();
            }
    }
    // public void ShowExplosionFx(FxPool pool)
    // {
    //     var particles = pool.GetNoteExplosionPool(color).GetObject();
    //         particles.transform.position = transform.position;
    // }


    public void EndTouch()
    {
        controller.audioCom.Pause();
        // restartPanel.SetActive(true);
        // playerMove.enabled=false;
        life-=1;
        // controller.CleanNoteOnScene();

        StartCoroutine(MiddleTouchCoroutine());
    }

    IEnumerator MiddleTouchCoroutine()
    {
        yield return timeToWait;
        Time.timeScale=0f;
        restartPanel.SetActive(true);
        controller.CleanNoteOnScene();
    }
    public void RestartTouch()
    {
        Time.timeScale=1f;
        controller.audioCom.UnPause();
        restartPanel.SetActive(false);
        playerMove.enabled=true;
    }

    public void CompleteEnd()
    {
        controller.audioCom.Pause();
        // EndPanel.SetActive(true);
        // playerMove.enabled=false;
        life-=1;
        // inGamePanel.SetActive(false);
        
        // controller.CleanNoteOnScene();
        StartCoroutine(CompleteEndcoroutine());
    }
    IEnumerator CompleteEndcoroutine()
    {
        yield return timeToWait;
        inGamePanel.SetActive(false);
        EndPanel.SetActive(true);
        controller.CleanNoteOnScene();
    }
    

    //일시정지 후 홈 버튼을 눌렀을때 작용하는 첫 화면으로 돌아가고 다시 시작하는 메서드.
    public void ReloadScene()
    {
        Debug.Log("ReloadScene activated");
        controller.audioCom.pitch=1f;
        controller.Restart();
        EndPanel.SetActive(false);
        //완료 버튼 누르는게 터치카운트에 +1을 해주기 때문에 이런식으로 -1을 해주어야
        //버튼을 누르고 난뒤에 터치스타티드 카운트가 0이 된다. 
        controller.touchStarted=-1;
        playerMove.enabled=true;
        firstPanel.SetActive(true);
        ResetPanelValue();
        playerMove.PositionReset();
        Time.timeScale=1f;
        
    }

    public void ResetPanelValue()
    {
        score=0;
        life=2;

    }

    //인게임포즈 버튼에 온클릭 이벤트에 등록해서 사용하는 인게임포즈 메서드.
    public void InGamePause()
    {
        Debug.Log("IngamePause");

        //생각해보니 이건 오디오가 재생중일때 멈추는 코드이기 때문에, 처음 리드인타임처럼 흘러가는 동안에는 
        //그 노트를 멈출수가 없는것이다. 
        //그리고 오디오가 재생되고 부터는 이 동작이 정상적으로 작동된다. 

        //게임컨트롤러에서 Time.unscaleddeltaTime을 Time.deltaTime으로 바꾸면 타임스케일을 바꾸어서 멈출수는 있는데, 
        //이렇게 되어버리면 코리오그레피가 시작되서 오디오가 돌기 시작하면, 타임스케일을 바꾸는걸로는 멈출수가 없다
        //이경우 오디오를 멈추어주는 aduiocom.pause를 사용해서 또 멈추어 주어야 할것 같다. 

        Time.timeScale=0f;
        controller.audioCom.Pause();
        playerMove.enabled=false;
        
    }

    // public void InGameUnPause()
    // {

    //     controller.audioCom.UnPause();
    //     Debug.Log("IngameUnPause");
    //     Time.timeScale=1f;

    // }
    

    


}
