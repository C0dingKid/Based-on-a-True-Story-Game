using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;

    public CameraMovement cameramovement;



    void Start()
    {
        ShowCanvas1();
        canvas3.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
        canvas4.gameObject.SetActive(false);



    }


    public void ShowCanvas1()
    {
        canvas1.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
    }

    public void ShowCanvas2()
    {
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(true);
    }


    private void Update()
    {
        if (canvas1.gameObject.activeSelf)
        {
            cameramovement.xMin = 16;
            cameramovement.xMax = 19;
        }

        if (canvas2.gameObject.activeSelf)
        {
            cameramovement.xMin = -7;
            cameramovement.xMax = 7;
        }
        if (canvas3.gameObject.activeSelf)
        {
            cameramovement.xMin = -19;
            cameramovement.xMax = -16;
        }
    }

}