using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    // Start is called before the first frame update
    Text textCom;
    float previousAlpha;
    float targetAlpha;
    public float time = 0.2f;
    public float fac = 0;

    void Start()
    {
        textCom = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float a = Mathf.Lerp(previousAlpha, targetAlpha, fac);
        textCom.color = new Color(textCom.color.r, textCom.color.b, textCom.color.g, a);
        fac += Time.deltaTime / time;
    }

    public void FadeIn()
    {
        previousAlpha = textCom.color.a;
        targetAlpha = 1;
        fac = 0;
    }

    public void FadeOut()
    {
        previousAlpha = textCom.color.a;
        targetAlpha = 0;
        fac = 0;
    }
}
