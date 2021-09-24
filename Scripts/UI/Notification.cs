using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    static Slide slide;
    static Text text;
    static Notification thisN;
    
    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slide>();
        text = transform.Find("Text").gameObject.GetComponent<Text>();
        thisN = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Notify()
    {
        thisN._Notify();
    }

    public void _Notify()
    {
        slide.Away();
        //text.text = message;
        Invoke("Back", 3f);
    }

    public void Back()
    {
        slide.Back();
    }

}
