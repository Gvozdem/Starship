using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SpaceshipController : MonoBehaviour
{
    //Создаем переменную для точки назначения
    private Vector3 _targetPoint; //брать из кликнутого объекта
    private float _speed = 20.0f;
    private bool _isFlying = false;

    public void SetTargetPoint(Vector3 targetPoint)
    {
        _targetPoint = targetPoint;
        _isFlying = true;
    }

    private void Update()
    {
        if (_isFlying)
        {
            MoveObj(); // Вызываем метод для движения, вызов происходит каждый фрейм или что то вроде того

            if (Vector3.Distance(transform.position, _targetPoint) < 0.1f)
            {
                _isFlying = false;
            }
        }
        
    }

    void MoveObj()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed * Time.deltaTime);
        //Двигаем объект с помощью метода MoveTowards, в скобках слева на право 1. Текущее положение, 2. Точка назначения, 3. скорость
    }
}
