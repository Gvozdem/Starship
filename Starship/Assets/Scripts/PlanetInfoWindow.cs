using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoWindow : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowInfo()
    {
        gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            HideInfo();
    }
}
