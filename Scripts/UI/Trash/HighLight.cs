using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class HighLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void OnHighLight();

    public abstract void OnBehind();
}
