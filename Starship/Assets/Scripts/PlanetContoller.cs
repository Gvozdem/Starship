using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetController : MonoBehaviour
{
    public SpaceshipController spaceshipController;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
                // Вызываем метод установки целевой точки в контроллере корабля, передавая координаты планеты
             spaceshipController.SetTargetPoint(transform.position);

        }
    }
}
