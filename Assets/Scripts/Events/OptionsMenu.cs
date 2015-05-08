using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour 
{

    public float shadowDrawDistance;
    public float refreshRate;
    public int resX;
    public int resY;

	void Start () 
    {
        shadowDrawDistance = QualitySettings.shadowDistance;

        resX = Screen.width;
        resY = Screen.height;
	}

	void Update () 
    {
	}

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
    }

    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
    }

    public void ShadowDistance(float newDistance)
    {
        shadowDrawDistance = newDistance;
        QualitySettings.shadowDistance = shadowDrawDistance;
    }

    public void AA0x()
    {
        QualitySettings.antiAliasing = 0;
        Debug.Log("0x AA");
    }

    public void AA2x()
    {
        QualitySettings.antiAliasing = 2;
        Debug.Log("2x AA");
    }

    public void AA4x()
    {
        QualitySettings.antiAliasing = 4;
        Debug.Log("4x AA");
    }

    public void AA8x()
    {
        QualitySettings.antiAliasing = 8;
        Debug.Log("8x AA");
    }

    public void TripleBufferOn()
    {
        QualitySettings.maxQueuedFrames = 3;
        Debug.Log("Triple buffering on");
    }

    public void TripleBufferOff()
    {
        QualitySettings.maxQueuedFrames = 0;
        Debug.Log("Triple buffering off");
    }

    public void AnisotropicOn()
    {
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        Debug.Log("Force enable anisotropic filtering!");
    }

    public void AnisotropicOff()
    {
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        Debug.Log("Disable anisotropic filtering!");
    }

    public void VsyncOn()
    {
        QualitySettings.vSyncCount = 1;
    }

    public void VsyncOff()
    {
        QualitySettings.vSyncCount = 0;
    }

    public void res1080p()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        resX = 1920;
        resY = 1080;
        Debug.Log ("1080p");
    }

    public void res720p()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
        resX = 1280;
        resY = 720;
        Debug.Log ("720p");
    }

    public void res480p()
    {
        Screen.SetResolution(640, 480, Screen.fullScreen);
        resX = 640;
        resY = 480;
        Debug.Log ("480p");
    }

    public void refresh60hz()
    {
        Screen.SetResolution(resX, resY, Screen.fullScreen, 60);
        Debug.Log ("60Hz");
    }

    public void refresh120hz()
    {
        Screen.SetResolution(resX, resY, Screen.fullScreen, 120);
        Debug.Log ("120Hz");
    }

    public void TextureUltra()
    {
        QualitySettings.masterTextureLimit = 0;
    }

    public void TextureHigh()
    {
        QualitySettings.masterTextureLimit = 1;
    }

    public void TextureMedium()
    {
        QualitySettings.masterTextureLimit = 2;
    }

    public void TextureLow()
    {
        QualitySettings.masterTextureLimit = 3;
    }

}
