using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Slide : MonoBehaviour
{
    public float duration = 0.2f;
    public Vector2 moveDir;
    RectTransform rt;
    Vector2 defPos;//default pos
    public bool disable = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Away()
    {
        if (rt == null) { rt = GetComponent<RectTransform>(); defPos = rt.anchoredPosition; }
        rt.DOAnchorPos(defPos+moveDir, duration).SetEase(Ease.InQuad);
        Invoke("Disable", duration);
    }

    void Disable()
    {
        if(disable) gameObject.SetActive(false);
    }

    public void Back()//
    {
        if (rt == null) { rt = GetComponent<RectTransform>(); defPos = rt.anchoredPosition; }
        rt.anchoredPosition = defPos + moveDir;
        if (disable) gameObject.SetActive(true);
        rt.DOAnchorPos(defPos, duration).SetEase(Ease.OutQuad);
    }
}
