using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMove : MonoBehaviour
{
    private float deltaX, deltaY;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //여기서의 델타x의 값은 처음 찍은 손 위치와 캐릭터의 초기 위치와의 차이값이다. 
                    // 이 값은 변하지 않는 상태로 유지된다. 
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    float tmp;
                    tmp = touchPos.x - deltaX;
                    //rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));

                    if (transform.position.x <= -1.25f && tmp < -1.25f)
                    {
                        tmp = -1.25f;
                        rb.MovePosition(new Vector2(Mathf.Clamp(tmp, -1.25f, 1.25f), transform.position.y));

                    }
                    if (transform.position.x >= 1.25f && tmp > 1.25f)
                    {
                        tmp = 1.25f;
                        rb.MovePosition(new Vector2(Mathf.Clamp(tmp, -1.25f, 1.25f), transform.position.y));

                    }
                    rb.MovePosition(new Vector2(Mathf.Clamp(tmp, -1.25f, 1.25f), transform.position.y));
                    Debug.Log(tmp);
                    Debug.Log(touchPos.x);
                    Debug.Log(deltaX);
                    Debug.Log($"transform.position.x is {transform.position.x}");
                    break;

                // rb를 dynamic으로 남겨두고, gravity를 0으로 만들었을때 필요한 코드 부분.
                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;


            }
        }
        Debug.Log($"transform.position.x is {transform.position.x}");

    }
}
