using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideMenu : MonoBehaviour
{
    RectTransform openTrans;
    // Start is called before the first frame update
    void Start()
    {
        openTrans = GameObject.Find("OpenControl").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClose()
    {
        GetComponent<RectTransform>().DOAnchorPosX(255f, 0.2f).SetEase(Ease.OutQuad);
        openTrans.DOAnchorPosX(-15f, 0.2f).SetEase(Ease.OutQuad);
    }

    public void onOpen()
    {
        GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f).SetEase(Ease.OutQuad);
        openTrans.DOAnchorPosX(40f, 0.2f).SetEase(Ease.OutQuad);
    }

    float easeOutCubic(float x) {
        return 1 - Mathf.Pow(1 - x, 3);
    }
}
