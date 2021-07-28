using UnityEngine;

public class ViewManager : PlayerDeactivator
{
    [SerializeField] private GameObject mousepointer;

    [Header("Cameras")]
    public Camera defaultCamera;
    public Camera[] objCameras;

    [Header("Lights")]
    public Light defaultLight, defaultLightCenter = null;
    [SerializeField] private Light[] objLights = null;

    [Header("Force camera back to default")]
    public bool camIsForced = false;

    private void Start()
    {
        if (mousepointer == null)
        {
            mousepointer = GameObject.FindGameObjectWithTag("MousePointer");
        }
    }

    private void Update()
    {
        if (camIsForced)
        {
            PlayerReturn();
            defaultCamera.enabled = true;
            defaultLight.enabled = true;
            defaultLightCenter.enabled = true;
            mousepointer.SetActive(true);

            for (var i = 0; i < objLights.Length; i++)
            {
                objLights[i].enabled = false;
            }

            for (var i = 0; i < objCameras.Length; i++)
            {
                objCameras[i].enabled = false;
            }

            camIsForced = false;
        }
    }

    public void SelectCamera(int num)
    {
        if (defaultCamera.enabled == true)
        {
            for (var i = 0; i < objCameras.Length; i++)
            {
                if (i == num)
                {
                    objCameras[i].enabled = true;
                }
                else
                {
                    objCameras[i].enabled = false;
                }
            }
            mousepointer.SetActive(false);
            Cursor.visible = false;
            defaultCamera.enabled = false;
            PlayerDisable();
        }

        else
        {
            for (var i = 0; i < objCameras.Length; i++)
            {
                objCameras[i].enabled = false;
            }
            mousepointer.SetActive(true);
            defaultCamera.enabled = true;
            PlayerReturn();
        }
    }

    public void SelectLight(int num)
    {
        if (defaultLight.enabled == true)
        {
            for (var i = 0; i < objLights.Length; i++)
            {
                if (i == num)
                {
                    objLights[i].enabled = true;
                }
                else
                {
                    objLights[i].enabled = false;
                }
            }

            defaultLight.enabled = false;
            defaultLightCenter.enabled = false;
        }

        else
        {
            for (var i = 0; i < objLights.Length; i++)
            {
                objLights[i].enabled = false;
            }

            defaultLight.enabled = true;
            defaultLightCenter.enabled = true;
        }
    }
}
