using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class ColorManager : MonoBehaviour
{
    PostProcessVolume volume;
    Bloom bloom;
    
    public Material gradientMat;
    Image img;
    Slider hueSli;
    Slider rangeSli;
    public static float hue;
    public static float range;
    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
        //gradientMat = gameObject.GetComponent<Renderer>().material;
        Transform colorSlidersTrans = transform.parent.parent.Find("ColorSliders");
        hueSli = colorSlidersTrans.Find("Color").Find("Color").gameObject.GetComponent<Slider>();
        rangeSli = colorSlidersTrans.Find("Range").Find("Range").gameObject.GetComponent<Slider>();

        volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Bloom>(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        hue = hueSli.value;
        range = rangeSli.value;
        gradientMat.SetFloat("_Hue", hue);
        gradientMat.SetFloat("_Hue2", hue+range);
        float intens = 2.4f;
        float sat = 0.5f;

        bloom.color.value = Color.HSVToRGB((hue+range/2)%1.0f, sat, 1) * intens;
    }
}
