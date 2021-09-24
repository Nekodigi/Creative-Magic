using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
public class Screenshot : MonoBehaviour
{
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeScreenShot()
    {
        StartCoroutine(TakeScreenshotAndSave());
//        NativeToolkit.SaveScreenshot("GenerativeArt", "GenerativeArtFolder", "jpeg");
    }

    public bool takingScreenshot = false;

    /*public void CaptureScreenshot()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }*/

    private IEnumerator TakeScreenshotAndSave()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            takingScreenshot = true;
            yield return new WaitForEndOfFrame();

            Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            /*
            canvas.enabled = false;
            ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            canvas.enabled = true;
            ss.Apply();
            */
            RenderTexture rt = new RenderTexture(ss.width, ss.height, 24);//screenshot without UI
            RenderTexture prev = Camera.main.targetTexture;
            Camera.main.targetTexture = rt;
            Camera.main.Render();
            Camera.main.targetTexture = prev;
            RenderTexture.active = rt;
            ss.ReadPixels(new Rect(0, 0, ss.width, ss.height), 0, 0);
            ss.Apply();

            //RenderTexture.active = GAHub.target;
            //ss.ReadPixels(new Rect(0, 0, GAHub.target.width, GAHub.target.height), 0, 0);
            //ss.Apply();

            // Save the screenshot to Gallery/Photos
            string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, Application.productName + " Captures", name));
            takingScreenshot = false;
            Notification.Notify();
        }
#endif
    }
}
