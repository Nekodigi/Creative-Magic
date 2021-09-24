using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateButton : MonoBehaviour
{
    public float duration = 0.2f;
    public float size = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnter()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.DOScale(new Vector2(size, size), 0.2f);
    }

    public void OnExit()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.DOScale(new Vector2(1f, 1f), 0.2f);
    }
}
