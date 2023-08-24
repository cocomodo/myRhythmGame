using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingHand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.SetAutoKill(false)
        .Append(transform.DOLocalMove(new Vector3(150,-200,0),1f,false).SetEase(Ease.Linear));
        mySequence.SetLoops(-1,LoopType.Yoyo).SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
