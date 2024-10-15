using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.AI;
using System.IO;
using System;


public class SpaceshipController : MonoBehaviour
{
    //Сам полёт
    private Vector3 _targetPoint;
    private float _speed = 20.0f;

    //Для остановки перед планетой
    private bool _isFlying = false;

    //Поворот
    private float _rotationSpeed = 250f;

    //Время полёта
    private float _flightTime;
    public Text flightTimeText;

    //Новый путь
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath path = new NavMeshPath();
    private float pathLength = 0;


    //Находим нужный объект
    private void Start()
    {
        //Для создания окна со временем
        flightTimeText = GameObject.Find("FlightTimeText").GetComponent<Text>();
        
        //Для работы с автопилотом
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
        _navMeshAgent.angularSpeed = _rotationSpeed;
        _navMeshAgent.acceleration = 999f;

    }


    //Задание координат куда лететь
    public void SetTargetPoint(Vector3 targetPoint)
    {
        //Получаем точку куда летим и задаем в автопилот
        _targetPoint = targetPoint;
        _isFlying = true;
        _navMeshAgent.SetDestination(_targetPoint);
    }

    //Время полёта (иногда немного задерживается)
    private void UpdateFlightTimeDisplay(float flightTime)
    {
        int minutes = Mathf.FloorToInt(flightTime / 60);
        int seconds = Mathf.FloorToInt(flightTime % 60);

        if (minutes == 0 && seconds == 0)
            flightTimeText.text = "";
        else
            flightTimeText.text = string.Format("Оставшееся время полёта: {0:00}:{1:00}", minutes, seconds);
    }

    //Полёт
    private void Update()
    {
        if (_isFlying)
        {
            pathLength = _navMeshAgent.remainingDistance;
            _navMeshAgent.speed = _speed;
            _flightTime = pathLength / _speed;

            _flightTime -= Time.deltaTime;
            UpdateFlightTimeDisplay(_flightTime);

            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 20f)
            {
                _navMeshAgent.speed = 0f;
                _isFlying = false;
            }
        }
    }

}