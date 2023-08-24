using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StopPanel : MonoBehaviour
{
    public UnityEvent OnEnalbeEvents;
    public UnityEvent OnDisableEvents;
    // public RhythmGameManager gameManager;
    public PlayerMove dinoMove;

    public WaitForSecondsRealtime waitforsconds =new WaitForSecondsRealtime(0.001f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
        {

            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)==false)
            {
                Touch touch = Input.GetTouch(0);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:

                                // 코루틴 실행을 업데이트 내부로 넣어버리면 중복으로 계속해서 코루틴 함수가 호출되어서 작동한다. 
                                //키 입력을 받아서 이프문 등으로 한번 호출하게 해야 정상적으로 작동이 된다. 
                                
                                // 매니저에 있는 리스타트터치 매서드가 이 스탑패널을 셋엑티브(폴스)하기 떄문에, 그 뒷 부분들이 구현이 안되고 있다. 

                                RhythmGameManager.instance.RestartTouch();
                                // dinoMove.enabled=true;
                            break;

                        case TouchPhase.Moved:
                            
                            //나중에 이 부분에 터치 부분의 범위를 한정짓는 조건을 추가해서 넣도록 하자.
                                // RhythmGameManager.instance.RestartTouch();
                            
                            break;
                        // rb를 dynamic으로 남겨두고, gravity를 0으로 만들었을때 필요한 코드 부분.
                        case TouchPhase.Ended:
                                // RhythmGameManager.instance.RestartTouch();

                            //rb.velocity = Vector2.zero;
                            break;
                    }

            }
        }
        // if(Input.touchCount>0)
        // {
        //     Touch touch = Input.GetTouch(0);

        //         switch (touch.phase)
        //         {
        //             case TouchPhase.Began:

        //                     // 코루틴 실행을 업데이트 내부로 넣어버리면 중복으로 계속해서 코루틴 함수가 호출되어서 작동한다. 
        //                     //키 입력을 받아서 이프문 등으로 한번 호출하게 해야 정상적으로 작동이 된다. 
                            
        //                     // 매니저에 있는 리스타트터치 매서드가 이 스탑패널을 셋엑티브(폴스)하기 떄문에, 그 뒷 부분들이 구현이 안되고 있다. 

        //                     RhythmGameManager.instance.RestartTouch();
        //                     // dinoMove.enabled=true;
        //                 break;

        //             case TouchPhase.Moved:
                        
        //                 //나중에 이 부분에 터치 부분의 범위를 한정짓는 조건을 추가해서 넣도록 하자.

        //                 break;
        //             // rb를 dynamic으로 남겨두고, gravity를 0으로 만들었을때 필요한 코드 부분.
        //             case TouchPhase.Ended:
        //                 //rb.velocity = Vector2.zero;
        //                 break;
        //         }

        // }
    }

    private void OnEnable() 
    {
        OnEnalbeEvents.Invoke();
    }
    private void OnDisable()
    {
        OnDisableEvents.Invoke();
    }
}   
