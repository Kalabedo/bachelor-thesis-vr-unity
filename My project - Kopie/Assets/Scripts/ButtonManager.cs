using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;

    public void ShowCanvas1()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }

    public void ShowCanvas2()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }
}
