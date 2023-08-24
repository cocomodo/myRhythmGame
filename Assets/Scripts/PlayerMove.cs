using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMove : MonoBehaviour
{
    private float deltaX;
    private float a2position;
    private float a1Position;
    private float tmp;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    private Vector2 startPosition;
    private void Awake() {
        startPosition=transform.position;
        Debug.Log(startPosition);
    }
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)==false)
            {
                Touch touch = Input.GetTouch(0);

                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        //첫 클릭 위치 고정
                        tmp = touchPos.x;
                        break;

                    case TouchPhase.Moved:
                        //이 디버그 로그값을 확인해보니 레이트업데이트에서 클릭이 시작되는 것을 통해서 tmp값이 정상적으로 
                        //입혀지고, 그걸 무브드 부터는 업데이트가 이어받아서 진행되는걸 확인했다. 
                        // Debug.Log($"tmp is on update in Phase.Moved {tmp}");

                        a1Position = tmp;
                        a2position = touchPos.x;
                        //마지막 손가락 뗀 위치에 tmp포지션의 값이 들어가게 된다. 

                        tmp = a2position;
                        //deltaX<0이면 오른쪽으로 이동, deltaX>0이면 왼쪽으로 이동.  
                        deltaX = (a1Position - a2position);

                        //**유니티 리지드 바디를 매 FixedUpdate 호출에 움직인다고 나온다. 그래서 매 프레임이 x좌표를 
                        //**기반으로 한 이 이동방법은 Update 함수로 실행할시, 마우스 커서를 따라붙지 못하는 문제를 발생시키는 것으로 보여진다. 
                        // rb.MovePosition(new Vector2(Mathf.Clamp(transform.position.x - deltaX, -1.25f, 1.25f), transform.position.y));

                        transform.position=new Vector2(Mathf.Clamp(transform.position.x - deltaX, -1.25f, 1.25f), transform.position.y);
                        //오른쪽으로 이동하면
                        if (deltaX < 0)
                        {
                            sp.flipX = false;
                        }
                        else if (deltaX > 0)
                        {
                            sp.flipX = true;
                        }
                        break;
                    // rb를 dynamic으로 남겨두고, gravity를 0으로 만들었을때 필요한 코드 부분.
                    case TouchPhase.Ended:

                        //손가락을 떼면 엔디드 할때 tmp값은 a2포지션 값이 들어가고, 일시정지 한뒤에 다시 긁으면, a1포지션 값에 tmp값을
                        //넣을때, 맨 마지막에 손을 떼었던 a2포지션값이 그대로 남아있게 된다. 
                        //그래서 델타x의 값이 커져서 한번에 쑥 이동하게 된다. 
                        // Debug.Log($"TouchPhase.Ended tmp is {tmp}");
                        break;
                }

            }
        }
    }

    private void LateUpdate() {
        if(Time.deltaTime==0)
        {
            if (Input.touchCount > 0)
            {

                if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)==false)
                {

                    Touch touch = Input.GetTouch(0);

                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            //델타아임이 0일때 터치 비겐까지는 정상적으로 작동되는것 확인
                            // Debug.Log("구현이 되는지 확인하기4 ");
                            
                            //첫 클릭 위치 고정
                            tmp = touchPos.x;
                            break;
                    }
                }
            }
        }
        
    }
    public void PositionReset()
    {
        transform.position=startPosition;
        sp.flipX=true;
    }

}
