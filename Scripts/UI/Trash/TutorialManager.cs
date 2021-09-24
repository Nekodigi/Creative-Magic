using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> messages = new List<string>();
    string message;
    Text textCom;
    TextFade tf;
    int index_ = 0;
    int index
    {
        get
        {
            return index_;
        }
        set
        {
            index_ = Mathf.Clamp(value, 0, messages.Count-1);
        }
    }


    void Start()
    {
        GameObject textObj = transform.GetChild(0).gameObject;
        textCom = textObj.GetComponent<Text>();
        tf = textObj.GetComponent<TextFade>();
        Next();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bank()
    {
        tf.FadeOut();
        Invoke("LateBack", tf.time);
    }

    void LateBack()
    {
        index--;
        message = messages[index];
        string[] splitted = message.Split('/');
        //message = splitted[0];
        //textCom.text = message;
        if (splitted.Length == 3)
        {
            string highlightName = splitted[2];
            string behindName = splitted[1];

            if (highlightName != "")
            {
                GameObject highlight = GameObject.Find(highlightName);
                highlight.transform.SetAsLastSibling();
                HighLight hl = highlight.GetComponent<HighLight>();
                hl.OnHighLight();
            }
            if (behindName != "")
            {
                GameObject behind = GameObject.Find(behindName);
                behind.transform.SetSiblingIndex(1);
                HighLight hlb = behind.GetComponent<HighLight>();
                hlb.OnBehind();
            }
        }
        index--;
        message = messages[index];
        splitted = message.Split('/');
        message = splitted[0];
        textCom.text = message;

        index++;
        tf.FadeIn();
    }

    public void Next()
    {
        //if (index < message.Length - 1)
        {
            tf.FadeOut();
            Invoke("LateNext", tf.time);
        }
    }

    void LateNext()
    {
        Debug.Log(index);
        message = messages[index];
        string[] splitted = message.Split('/');
        message = splitted[0];
        textCom.text = message;
        Debug.Log(splitted.Length);
        if(splitted.Length == 3)
        {
            string highlightName = splitted[1];
            string behindName = splitted[2];
            
            if (highlightName != "")
            {
                GameObject highlight = GameObject.Find(highlightName);
                highlight.transform.SetAsLastSibling();
                HighLight hl = highlight.GetComponent<HighLight>();
                hl.OnHighLight();
            }
            if (behindName != "")
            {
                GameObject behind = GameObject.Find(behindName);
                behind.transform.SetSiblingIndex(1);
                HighLight hlb = behind.GetComponent<HighLight>();
                hlb.OnBehind();
            }
        }
        
        index++;
        tf.FadeIn();
    }
}
