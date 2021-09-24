using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ToggleGray : MonoBehaviour
{
    Material mat;
    Image img;
    Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        img = transform.Find("Background").GetComponent<Image>();
        mat = (Material)Resources.Load("Gray", typeof(Material));
        toggle = GetComponent<Toggle>();
        onChange(toggle.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onChange(bool value)
    {
        if (value)
        {
            img.material = null;
            img.color = new Color(1, 1, 1, 1);
        }
        else
        {
            img.material = mat;
            img.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }
}
