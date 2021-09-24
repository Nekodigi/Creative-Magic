using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector2 posAround(Vector2 pos, float spreadingR)
    {
        return pos + new Vector2(Random.Range(-spreadingR, spreadingR), Random.Range(-spreadingR, spreadingR));
    }

    public static Vector2 ClampScreen(Vector2 pos, Camera cam)//min value is 0
    {
        return new Vector2(Mathf.Clamp(pos.x, 0, cam.pixelWidth), Mathf.Clamp(pos.y, 0, cam.pixelHeight));
    }

    public static float roundTrip(float value, float n)
    {
        return Mathf.Abs((float)value / n * 2 - 1);
    }

    public static RenderTexture tryCreateRenTex(RenderTexture target, Camera cam)
    {
        if (needRenTex(target, cam))
        {
            return createRenTex(target, cam);
        }
        return target;
    }

    public static RenderTexture createRenTex(RenderTexture target, Camera cam)
    {
        if (target != null)
        {
            target.Release();
        }
        target = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
        target.enableRandomWrite = true;
        target.Create();
        return target;
    }

    public static bool needRenTex(RenderTexture target, Camera cam)
    {
        return target == null || target.width != cam.pixelWidth || target.height != cam.pixelHeight;
    }

    public static float fdigit(float x, int digit)
    {
        float mult = Mathf.Pow(10, digit);
        return (int)(x * mult) / mult;
    }
}
