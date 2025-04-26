using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    private void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
    }
}
