using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    public string url
    {
        get
        {
            return _url;
        }
        set
        {
            _url = value;
        }
    }
    public string _url;

    public void Open()
    {
        Application.OpenURL(url);
    }
}
