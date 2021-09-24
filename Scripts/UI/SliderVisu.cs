using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderVisu : MonoBehaviour
{
    Slider slider;
    Text valueTxt;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        valueTxt = transform.parent.Find("Value").gameObject.GetComponent<Text>();
        onValueChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onValueChange()
    {
        valueTxt.text = CommonUtil.fdigit(slider.value, 2).ToString();
    }
}
