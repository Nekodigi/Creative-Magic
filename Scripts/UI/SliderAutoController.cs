using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAutoController : MonoBehaviour
{
    Slider slider;
    public float interval  = 3;
    float fac;
    public float easing = 0.99f;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        int state = (int)(Time.time / interval % 2);
        if (state == 0)
        {
            fac = Mathf.Lerp(1, fac, easing);
        }
        else{
            fac = Mathf.Lerp(0, fac, easing);
        }
        //fac = Mathf.Abs(Time.time*speed%2f-1f);
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, fac);
    }

    void setMid()
    {
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, fac);
    }
}
