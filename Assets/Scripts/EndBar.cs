using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class EndBar : MonoBehaviour
{
    public UnityEvent EndBarTouchCheerUpEventsOne;
    public UnityEvent EndBarTouchCheerUpEventsTwo;
    public UnityEvent EndBarTouchEvents;
    public UnityEvent completeEndTouchEvents;
    public PlayerMove dinoMove;

    // [SerializeField]
    // private string Fade="Fade";
    WaitForSeconds waitSeconds = new WaitForSeconds(1.5f);

    //이렇게 온트리거엔터 로만 해놓으면 속도가 너무 빠르면 뚫고 지나가는 경우가 있다
    //대략 8배속은 뚫고 지나가는듯. 4배속까지는 그래도 커버가 가능한것 같다. 5배속도 바로 걸려들어서 커버가 가능
    //일단은 이렇게 냅두고, 다른 부분을 손대는것이 나을것 같다. 실질적으로 디노처럼도 4배속까지 접근한 사람은 없는듯 하다.
    ///
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"life is {RhythmGameManager.instance.Life}");
        if(RhythmGameManager.instance.Life>0)
        {
            RhythmGameManager.instance.EndTouch();
            EndBarTouchEvents.Invoke();        
            
        }else
        {
            RhythmGameManager.instance.CompleteEnd();
            completeEndTouchEvents.Invoke();
            
        }

        StartCoroutine(StopPlayerMove());
        other.GetComponent<BlinkNote>().FadeNote();
        
    }
    
    IEnumerator StopPlayerMove()
    {
        //여기는 순서상 매니저에서 라이프 -1 을 한 뒤에 나타나는 부분으로서,
        //온트리거엔터에서 확인할 수 있는 라이프에서 -1을 해야 life값이 맞다. 
        Debug.Log($"life is {RhythmGameManager.instance.Life}");

        dinoMove.enabled=false;
        yield return waitSeconds;
        if(RhythmGameManager.instance.Life>=0)
        {
            // dinoMove.enabled=true;
        }
        if(RhythmGameManager.instance.Life==1)
        {

            EndBarTouchCheerUpEventsOne.Invoke();

        }
        if(RhythmGameManager.instance.Life==0)
        {
            EndBarTouchCheerUpEventsTwo.Invoke();
            
        }

    }
}

