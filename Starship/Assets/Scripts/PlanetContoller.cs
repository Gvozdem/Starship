using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour
{
    public SpaceshipController spaceshipController;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spaceshipController.SetTargetPoint(transform.position);
        }
    }
}