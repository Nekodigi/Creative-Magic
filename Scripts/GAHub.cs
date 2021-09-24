using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]

public class GAHub : MonoBehaviour
{
    public ComputeShader fragShader;
    public int shaderIndex;
    public ComputeShader[] fragShaders;

    [SerializeField]
    public Texture2D[] textures;

    int n = 100000;
    public static RenderTexture target;
    RenderTexture mask;
    Camera cam;
    float totalTime;
    Slider scaleMultSli;
    Slider speedMultSli;
    Slider transMultSli;
    Toggle[] typeToggles;

    int iteration = 0;
    int textureIndex;
    int index = 0;//particle index
    Vector2 prevMousePos;

    ComputeBuffer particleBuffer;
    List<ComputeBuffer> buffersToDispose;
    Particle[] particles = null;

    // Start is called before the first frame update
    void Start()
    {
        scaleMultSli = GameObject.Find("Sliders").transform.Find("Scale").Find("Scale").GetComponent<Slider>();
        speedMultSli = GameObject.Find("Sliders").transform.Find("Speed").Find("Speed").GetComponent<Slider>();
        transMultSli = GameObject.Find("Sliders").transform.Find("Trans").Find("Trans").GetComponent<Slider>();
        Transform typesTrans = GameObject.Find("Types").transform;
        int toggleCount = typesTrans.childCount;
        typeToggles = new Toggle[toggleCount];
        for(int i = 0; i<toggleCount; i++)
        {
            typeToggles[i] = typesTrans.GetChild(i).GetComponent<Toggle>();
        }
        fragShader = fragShaders[shaderIndex];
        Application.targetFrameRate = 60;
        StartCoroutine(FPS());
    }

    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;
    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
        }
    }
    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.width - 100, 10, 150, 20), fps);
        //GameObject.Find("FPS").GetComponent<Text>().text = fps;
    }

    void UniformParticle()
    {
        particles = new Particle[n];
        for (int i = 0; i < n; i++)
        {
            Vector2 pos = new Vector2(Random.Range(0, cam.pixelWidth), Random.Range(0, 1 * cam.pixelHeight) + cam.pixelHeight * 0f);
            particles[i] = new Particle()
            {
                pos = pos,
                speed = 1f,
                color = Color.HSVToRGB((ColorManager.hue + ColorManager.range*pos.y/cam.pixelHeight) % 1, 0.8f, 1)
            };
        }
    }

    void CreateScene()
    {
        
        if (fragShader.name != "PerlinPixelSortRender")
        {
            if (particles == null)
            {
                UniformParticle(); totalTime = Random.Range(0, 10000f);//also used like seed
            }
            particleBuffer = new ComputeBuffer(particles.Length, Particle.GetSize());
            particleBuffer.SetData(particles);
            fragShader.SetBuffer(fragShader.FindKernel("Particles"), "particles", particleBuffer);
            buffersToDispose.Add(particleBuffer);
        }
        else
        {
            cam = Camera.current;
            if (CommonUtil.needRenTex(target, cam))
            {
                totalTime = Random.Range(0, 10000f);//also used like seed
                target = CommonUtil.createRenTex(target, cam);
                Texture resizedTexture = new Texture2D(1920, 1080);
                Graphics.ConvertTexture(textures[textureIndex], resizedTexture);
                Graphics.Blit(resizedTexture, target);
                mask = CommonUtil.createRenTex(mask, cam);

            }
        }
    }

    [ImageEffectOpaque]
    //based on this site https://github.com/SebLague/Ray-Marching/blob/master/Assets/Scripts/SDF/Master.cs
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        cam = Camera.current;
        buffersToDispose = new List<ComputeBuffer>();
        if (fragShader.name != "PerlinPixelSortRender") target = CommonUtil.tryCreateRenTex(target, cam);
        CreateScene();
        if (fragShader.name != "PerlinPixelSortRender") totalTime += Time.deltaTime * Mathf.Pow(10.0f, transMultSli.value-0.5f);// -1.5f
        int threadGroupsX = Mathf.CeilToInt(cam.pixelWidth / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(cam.pixelHeight / 8.0f);
        fragShader.SetTexture(fragShader.FindKernel("CSMain"), "Result", target);
        fragShader.SetFloat("time", totalTime);
        fragShader.SetFloat("deltaTime", Time.deltaTime);
        fragShader.SetFloat("height", cam.pixelHeight);
        fragShader.SetFloat("width", cam.pixelWidth);
        fragShader.SetFloat("scaleMult", Mathf.Pow(10.0f, scaleMultSli.value));
        fragShader.SetFloat("speedMult", Mathf.Pow(10.0f, speedMultSli.value+2.0f));//apply only particle *Time.deltaTime
        fragShader.SetVector("touchPos", Input.mousePosition);
        fragShader.SetInt("iteration", iteration++);

        if (fragShader.name == "PerlinPixelSortRender") fragShader.SetTexture(fragShader.FindKernel("CSMain"), "Mask", mask);
        //            kernel id
        fragShader.Dispatch(fragShader.FindKernel("CSMain"), threadGroupsX, threadGroupsY, 1);

        if (fragShader.name != "PerlinPixelSortRender") fragShader.SetTexture(fragShader.FindKernel("Particles"), "Result", target);
        if (fragShader.name != "PerlinPixelSortRender") fragShader.Dispatch(fragShader.FindKernel("Particles"), Mathf.CeilToInt(n/ 100), 1, 1);


        Graphics.Blit(target, destination);

        if (fragShader.name != "PerlinPixelSortRender") particleBuffer.GetData(particles);
        foreach (var buffer in buffersToDispose)
        {
            buffer.Dispose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int prevShaderIndex = shaderIndex;
        for(int i=0; i<typeToggles.Length; i++)
        {
            Toggle toggle = typeToggles[i];
            if (toggle.isOn) shaderIndex = i;
        }
        if(shaderIndex!= prevShaderIndex)
        {
            scaleMultSli.value = 0;
            speedMultSli.value = 0;
            transMultSli.value = 0;
        }

        fragShader = fragShaders[shaderIndex];
        switch (fragShader.name)
        {
            case "PerlinPixelSortRender":
                break;
            default:
                if (Input.GetMouseButtonDown(0))
                {
                    prevMousePos = Input.mousePosition;
                    //prevMousePos = Input.mousePosition;
                    //particles = new Particle[n];
                    //Time.timeScale = 0;
                }
                if (Input.GetMouseButtonUp(0))
                {

                    //Time.timeScale = 1;
                }
                if (Input.GetMouseButton(0))
                {

                    //particles = new Particle[n]
                    int total = (int)(10* Vector2.Distance(prevMousePos, Input.mousePosition));//(int)(10000*Time.deltaTime)
                    float spreading = 4;
                    if (fragShader.name == "PerlinWaveRender" || fragShader.name == "PerlinWoodRender") spreading = 0;
                    for (int i = 0; i < total; i++)
                    {
                        particles[index] = new Particle()
                        {
                            //pos = Vector2.Lerp(prevMousePos, Input.mousePosition, (float)(i)/total),
                            pos =  CommonUtil.ClampScreen(CommonUtil.posAround(Vector2.Lerp(prevMousePos, Input.mousePosition, (float)(i) / total), spreading), cam),
                            speed = 1,
                            color = Color.HSVToRGB((ColorManager.hue + CommonUtil.roundTrip(index, n) * ColorManager.range)%1f, 0.8f, 1)
                        };
                        index++;
                        index = index %= particles.Length;
                    }
                }
                break;
        }
        prevMousePos = Input.mousePosition;
    }

    public void Regen()
    {
        
        if (fragShader.name != "PerlinPixelSortRender")
        {
            target = CommonUtil.createRenTex(target, cam);
            particles = new Particle[n];
            //UniformParticle();// totalTime = Random.Range(0, 10000f);//also used like seed
        }
        else
        {
            textureIndex++;
            textureIndex = textureIndex % textures.Length;
            totalTime = Random.Range(0, 10000f);//also used like seed
            target = CommonUtil.createRenTex(target, cam);
            Texture resizedTexture = new Texture2D(1920, 1080);
            Graphics.ConvertTexture(textures[textureIndex], resizedTexture);
            Graphics.Blit(resizedTexture, target);
            mask = CommonUtil.createRenTex(mask, cam);
        }
    }

    struct Particle
    {
        public Vector2 pos;
        public float speed;
        public Vector4 color;
        public static int GetSize()
        {
            return sizeof(float) * 7;
        }
    }
}
