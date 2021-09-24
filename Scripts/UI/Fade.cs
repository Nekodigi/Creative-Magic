using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    float duration = 0.2f;
    Image img;
    float defAlpha;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeOut()
    {
        if(img==null){img = GetComponent<Image>(); defAlpha = img.color.a;}
        img.DOFade(0, duration);
        if (img.material.name == "BlurBG")
        {
            img.material.DOFloat(0, "_Size", duration);
        }
        Invoke("Disable", duration);
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }

    public void FadeIn()//
    {
        gameObject.SetActive(true);
        if (img == null) { img = GetComponent<Image>(); defAlpha = img.color.a; }
        img.DOFade(defAlpha, duration);
        Debug.Log(img.material.name);
        if(img.material.name == "BlurBG")
        {
            img.material.DOFloat(5, "_Size", duration);
        }
    }
}
