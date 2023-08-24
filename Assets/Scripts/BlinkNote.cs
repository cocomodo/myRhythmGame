using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BlinkNote : MonoBehaviour
{
    [SerializeField]
    private Ease moveEase = Ease.Linear;
    // Start is called before the first frame update
    
    [SerializeField]
    private Color targetColor;

    [SerializeField]
    private Color originalColor;

    [Range(0.0f,10.0f),SerializeField]
    private float colorChangeDuration = 0.30f;

    public void FadeNote()
    {
        StartCoroutine(FadeBothWays());
        // transform.GetComponent<Renderer>().material.DOColor(targetColor,colorChangeDuration).SetEase(moveEase);
        // DOTween.Sequence().Append(transform.GetComponent<Renderer>().material.DOColor(targetColor,colorChangeDuration).SetEase(moveEase));
    }

    
    
    private IEnumerator FadeBothWays()
    {
        
        transform.GetComponent<SpriteRenderer>().material.DOColor(targetColor,colorChangeDuration).SetEase(moveEase);
        yield return new WaitForSeconds(colorChangeDuration);
        transform.GetComponent<SpriteRenderer>().material.DOColor(originalColor,colorChangeDuration).SetEase(moveEase);
        yield return new WaitForSeconds(colorChangeDuration);

        transform.GetComponent<SpriteRenderer>().material.DOColor(targetColor,colorChangeDuration).SetEase(moveEase);
        yield return new WaitForSeconds(colorChangeDuration);
        transform.GetComponent<SpriteRenderer>().material.DOColor(originalColor,colorChangeDuration).SetEase(moveEase);
        yield return new WaitForSeconds(colorChangeDuration);

        transform.GetComponent<SpriteRenderer>().material.DOColor(targetColor,colorChangeDuration).SetEase(moveEase);
        yield return new WaitForSeconds(colorChangeDuration);
        transform.GetComponent<SpriteRenderer>().material.DOColor(originalColor,colorChangeDuration).SetEase(moveEase);
        // yield return new WaitForSeconds(colorChangeDuration);

        // transform.GetComponent<Renderer>().material.DOColor(targetColor,colorChangeDuration).SetEase(moveEase);
        // yield return new WaitForSeconds(colorChangeDuration);
        // transform.GetComponent<Renderer>().material.DOColor(originalColor,colorChangeDuration).SetEase(moveEase);
        // yield return new WaitForSeconds(colorChangeDuration);

        // transform.GetComponent<Renderer>().material.DOColor(targetColor,colorChangeDuration).SetEase(moveEase);
        // yield return new WaitForSeconds(colorChangeDuration);
        // transform.GetComponent<Renderer>().material.DOColor(originalColor,colorChangeDuration).SetEase(moveEase);


        
    }
}
