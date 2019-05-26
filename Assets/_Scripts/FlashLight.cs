using UnityEngine;
using Vuforia;

public class FlashLight : MonoBehaviour
{
    private bool toggle = false;

    void Start()
    {
         Changing();
    }


    public void FlashLightOn()
    {
        toggle = !toggle;
        CameraDevice.Instance.SetFlashTorchMode(toggle);
        if (!toggle)
        {
            PlayerPrefs.SetInt("ARFlashLight", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ARFlashLight", 1);
        }
        PlayerPrefs.Save();
    }

    private void Changing()
    {
        if (PlayerPrefs.HasKey("ARFlashLight"))
        {
            if (PlayerPrefs.GetInt("ARFlashLight") == 0)
            {
                CameraDevice.Instance.SetFlashTorchMode(false);
                toggle = false;
            }
            else
            {
                CameraDevice.Instance.SetFlashTorchMode(true);
                toggle = true;
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause) Changing();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus) Changing();
    }
}
