using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class SpaceshipController : MonoBehaviour
{
    //Сам полёт
    private Vector3 _targetPoint;
    private float _speed = 20.0f;

    //Для остановки перед планетой
    private bool _isFlying = false;

    //Поворот
    private float _rotationSpeed = 30f;
    private float _rotationSpeedEnd = 20f;
    
    //Время полёта
    private float _flightTime;
    private float _totalFlightTime;
    public Text flightTimeText;

    //Отображение линии полёта
    private LineRenderer _trajectoryLine;
    private GameObject _destinationObject;

    //Находим нужный объект
    private void Start()
    {
        flightTimeText = GameObject.Find("FlightTimeText").GetComponent<Text>();

        _trajectoryLine = GetComponent<LineRenderer>();
    }
    private void UpdateTargetPoint()
    {
        _targetPoint = _destinationObject.transform.position;
    }


    //Задание координат куда лететь
    public void SetTargetPoint(Vector3 targetPoint, GameObject destinationObject)
    {
        _targetPoint = targetPoint;
        _isFlying = true;

        _destinationObject = destinationObject;

        _totalFlightTime = Vector3.Distance(transform.position, _targetPoint) / _speed;
        _flightTime = _totalFlightTime;

        // Включаем отображение линии
        _trajectoryLine.enabled = true;

        UpdateTargetPoint();
    }

    // Рисуем линию траектории
    private void DrawTrajectoryLine()
    {
        _trajectoryLine.SetPosition(0, transform.position);
        _trajectoryLine.SetPosition(1, _targetPoint);
    }

    //Время полёта
    private void UpdateFlightTimeDisplay(float flightTime)
    {
        int minutes = Mathf.FloorToInt(flightTime / 60);
        int seconds = Mathf.FloorToInt(flightTime % 60);

        if (minutes == 0 && seconds == 0)
            flightTimeText.text = "";
        else
            flightTimeText.text = string.Format("Время полёта: {0:00}:{1:00}", minutes, seconds);
    }

    //Полёт
    private void Update()
    {
        if (_isFlying)
        {
            MoveObj();

            _flightTime -= Time.deltaTime;

            UpdateFlightTimeDisplay(_flightTime);

            DrawTrajectoryLine();

            if (Vector3.Distance(transform.position, _targetPoint) < 13f)
            {
                _isFlying = false;
            }
        }
    }


    //Перемещение корабля и поворот
    private void MoveObj()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed * Time.deltaTime);

        // Поворот корабля, чтобы он был перпендикулярен планете
        if (Vector3.Distance(transform.position, _targetPoint) < 14f)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = currentRotation * Quaternion.Euler(-90f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeedEnd * Time.deltaTime);
            _trajectoryLine.enabled = false;
        }
        else
        {
            // Иначе, просто поворачиваем корабль к точке полёта
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((_targetPoint - transform.position).normalized), _rotationSpeed * Time.deltaTime);
        }
    }
}
