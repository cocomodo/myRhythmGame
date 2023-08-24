using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeCharacterSelect : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos=0f;
    float[] pos;

    [SerializeField]
    private RectTransform p_RectTransform;
    [SerializeField]
    private RectTransform c_RectTransform;

    private float x_PRectSize;
    private float x_CRectSize;
    private float paddingValue;
    private void Awake() {
        //Canvas Scaler를 scale with screen size로 하고, 1080*1980에, 스크린 매치 모드 슈린크로 만들었을때,
        //렉트트랜스폼의 메뉴 캔버스에서 보이는 위쓰 값이 출력되는 위스 값이랑 다른 상황이다. 
        //그래서 나누기 2를 해주었다. 
        //FIXME: 이렇게 했을때는 비율이 일정하게 적용되지 않는다. 나누기 비율이
        //만약 갤럭시 플립 접혀진 화면에서는. 대략 2/3배를 해주어야 값이 맞는다. 그래서 이 수식은 constant fixer size 일때만 일정하게 적용된다. 
        //어쩌면 시뮬레이터를 돌릴때 변경되는 렉트 트랜스폼의 값이, 어웨이크가 아니라 스타트에 잡히는건 아닐까? 
        //TODO: 맞았다. 이거 어웨이크에 돌리면 제대로 잡지를 못하는데, 스타트에 돌리니까 제대로 렉트트랜스폼 값을 읽어와서 동작한다. 그렇게 되는 이유는 뭘까? 
        
        // x_PRectSize=p_RectTransform.rect.width;
        // Debug.Log(x_PRectSize);
        // x_CRectSize=c_RectTransform.rect.width;
        // Debug.Log(x_CRectSize);
        // paddingValue=(x_PRectSize-x_CRectSize)/2;
        // gameObject.GetComponent<HorizontalLayoutGroup>().padding.left=(int)paddingValue;
        // gameObject.GetComponent<HorizontalLayoutGroup>().padding.right=(int)paddingValue;

        x_PRectSize=p_RectTransform.rect.width;
        Debug.Log(x_PRectSize);
        x_CRectSize=c_RectTransform.rect.width;
        Debug.Log(x_CRectSize);
        paddingValue=(x_PRectSize-x_CRectSize)/2;
        gameObject.GetComponent<HorizontalLayoutGroup>().padding.left=(int)paddingValue;
        gameObject.GetComponent<HorizontalLayoutGroup>().padding.right=(int)paddingValue;
        

        // pos=new float[transform.childCount];
        // float distance=1f/(pos.Length-1f);

        // for (int i = 0; i < pos.Length; i++)
        //     {
        //         if(scroll_pos<pos[i]+(distance/2) && scroll_pos>pos[i]-(distance/2))
        //         {
        //            transform.GetChild(i).localScale=Vector2.Lerp(transform.GetChild(i).localScale,new Vector2(1f,1f),0.1f);
        //         }    
        //     }
    }

    
    //FIXME: 경우의 수를 찾았다. 만약 어웨이크에 넣으면, 캐릭터 셀렉트 페이지를 띄워놓고 시작할때 비율이 깨지고, 
    //스타트에 넣으면, 캐릭터 셀렉트 페이지를 띄워놓고 시작해도 비율이 깨지지 않는다.
    //캐릭터 셀렉트 화면을 꺼놓고 게임을 시작한뒤에, 나중에 화면을 키게 되면, 어웨이크에 이 코드를 넣고 돌리더라도 전혀 문제없이
    //동작하게 된다. 일단 신기하긴 한데, 스크롤 뷰에서 자식들 이미지가 없어지는걸 먼저 고치도록 하고, 이건 이렇게 알고만 있고
    //그대로 어웨이크에 넣어서 이용하도록 하자. 

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //7개의 자식이 있는 상황에서 pos.length==7이고,distance값은 1/(7-1) 이다. 
        pos=new float[transform.childCount];
        float distance=1f/(pos.Length-1f);

        //pos.length는 자식의 숫자를 나타내는 플롯어레이의 렝쓰 이므로 7이다. 
        //각각의 인자에 값을 넣는 과정. 
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i]=distance*i;
        }
        if(Input.GetMouseButton(0))
        {
            scroll_pos=scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if(scroll_pos<pos[i]+(distance/2) && scroll_pos>pos[i]-(distance/2))
                {
                    scrollbar.GetComponent<Scrollbar>().value=Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value,pos[i],0.1f);
                }    
            }
        }

        //FIXME: 이게 웃긴게, 나는 지금 2차원상에서 표현해서 이미지를 만들어서 이미지에 수정을 가하고 있는데, vector2함수를 써서 그런지
        //스케일의 z축의 값이 0으로 가버리게 되고, 그렇게 되니까 이미지가 안보이게 된다. 이게 결국 2d로 표현되지만, 유니티가 기본적으로
        //유니티3D여서 그런지, 결국에는 3D로 구현되어있는 프로그램인것 같고, 기본이 3D라 z스케일이 0이 되어버리면 두께감이 0이 되면서 
        //화면상에 그려주던 이미지를 안보이게 만드는것 같다. 어쩌면 이걸 사이즈를 바꾸지 않으면서 보였다 안보였다 하는데 사용할수도 있을것 같다. 
        //사이즈를 변경시키는 부분. 
         for (int i = 0; i < pos.Length; i++)
            {
                if(scroll_pos<pos[i]+(distance/2) && scroll_pos>pos[i]-(distance/2))
                {
                   transform.GetChild(i).localScale=Vector3.Lerp(transform.GetChild(i).localScale,new Vector3(1f,1f,1f),0.05f);
                   for (int a = 0; a < pos.Length; a++)
                   {
                        if(a!=i)
                        {
                            transform.GetChild(a).localScale=Vector3.Lerp(transform.GetChild(a).localScale,new Vector3(0.6f,0.6f,1f),0.05f);
                        }
                   }
                }    
            }
        
    }
}
