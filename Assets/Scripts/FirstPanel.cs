using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPanel : MonoBehaviour
{
    public GameObject inGamePanel;
    public GameObject inGamePause;
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

                            break;

                        case TouchPhase.Moved:
                            gameObject.SetActive(false);
                            if(inGamePanel.activeSelf==false)
                            {
                                inGamePanel.SetActive(true);
                                inGamePause.SetActive(true);
                            }
                            break;
                        // rb를 dynamic으로 남겨두고, gravity를 0으로 만들었을때 필요한 코드 부분.
                        case TouchPhase.Ended:
                            //rb.velocity = Vector2.zero;
                            break;
                    }
            }

        }
    }
}
