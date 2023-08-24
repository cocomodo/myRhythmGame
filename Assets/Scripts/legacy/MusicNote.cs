using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    //일단은 기본적으로 제일 왼쪽걸로 만들어보고, 그뒤에는 태그별로 내려오는 위치를 5군데로 나누어서 한 스크립트로
    //모든 노트에 적용할 수 있도록 해주자. 

    //아니면 하나의 스크립트에, 이 노트의 비트와, y포지션을 넣으면, 모든 노트를 한 스크립트에서 구현하고 위치별로 내려
    //보낼 수 있을듯. 
    public Vector2 SpawnPos = new Vector2(-1.25f, 6f);
    public Vector2 RemovePos = new Vector2(-1.25f, -1.786f);

    public Conductor conductor;
    public int beatOfThisNote = 6;
    public int beatsShownInAdvance = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(
            SpawnPos,
            RemovePos,
            //(conductor.beatsShownInAdvance - (beatOfThisNote - conductor.songPositionInBeats)) / conductor.beatsShownInAdvance
            (beatsShownInAdvance - (beatOfThisNote - conductor.songPositionInBeats)) / beatsShownInAdvance
            );
        Debug.Log(beatsShownInAdvance);
        Debug.Log(beatOfThisNote - conductor.songPositionInBeats);
        Debug.Log((beatsShownInAdvance - (beatOfThisNote - conductor.songPositionInBeats)) / beatsShownInAdvance);
    }
}
