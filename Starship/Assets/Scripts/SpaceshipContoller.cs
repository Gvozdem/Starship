using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.AI;
using System.IO;
using System;
using TMPro;


public class SpaceshipController : MonoBehaviour
{
    //Сам полёт
    private Vector3 _targetPoint;
    public float _speed = 20.0f;

    //Для остановки перед планетой
    private bool _isFlying = false;

    //Поворот
    private float _rotationSpeed = 250f;

    //Время полёта
    private float _flightTime;
    public Text flightTimeText;

    //Новый путь
    private NavMeshAgent _navMeshAgent;

    private NavMeshPath _path;
    private float pathLength;

    //Отображение пути
    private LineRenderer _lineRenderer;


    //Находим нужный объект
    private void Start()
    {
        //Для создания окна со временем
        flightTimeText = GameObject.Find("FlightTimeText").GetComponent<Text>();

        _path = new NavMeshPath();

        //Для работы с автопилотом
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
        _navMeshAgent.angularSpeed = _rotationSpeed;
        _navMeshAgent.acceleration = 999f;

        // Инициализация LineRenderer
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.red; 
        _lineRenderer.endColor = Color.green;
    }

    public void SetTargetPoint(Vector3 targetPoint)
    {
        _targetPoint = targetPoint;
    }

    //Задание координат куда лететь
    public void Flight()
    {
        //Получаем точку куда летим и задаем в автопилот
        _isFlying = true;
        _lineRenderer.enabled = true;
        _navMeshAgent.speed = _speed;
        _navMeshAgent.SetDestination(_targetPoint);
        UpdateLenghtPath();
    }

    private void UpdateLenghtPath()
    {
        pathLength = 0;
        if (_navMeshAgent.CalculatePath(_targetPoint, _path))
        {
            Vector3[] corners = _path.corners;
            _path.GetCornersNonAlloc(corners);

            for (int i = 1; i < corners.Length; i++)
            {
                Vector3 segmentStart = corners[i - 1];
                Vector3 segmentEnd = corners[i];
                pathLength += Vector3.Distance(segmentStart, segmentEnd);
            }
        }
        _flightTime = pathLength / _speed;
    }

    //Время полёта
    private void UpdateFlightTimeDisplay(float flightTime)
    {
        int minutes = Mathf.FloorToInt(flightTime / 60);
        int seconds = Mathf.FloorToInt(flightTime % 60);

        if (minutes == 0 && seconds == 0)
            flightTimeText.text = "";
        else
            flightTimeText.text = string.Format("Оставшееся время полёта: {0:00}:{1:00}", minutes, seconds);
    }

    //Увеличение скорости для модулей
    public void AddSpeed(float speedBoost)
    {
        _speed += speedBoost;
        _navMeshAgent.speed = _speed;
    }

    public void RemoveSpeed(float speedBoost)
    {
        _speed -= speedBoost;
        _navMeshAgent.speed = _speed;
    }

    //Отображение линии
    private void UpdateLineRender()
    {
        _lineRenderer.positionCount = _navMeshAgent.path.corners.Length;
        for (int i = 0; i < _navMeshAgent.path.corners.Length; i++)
        {
            _lineRenderer.SetPosition(i, _navMeshAgent.path.corners[i]);
        }
    }

    //Полёт
    private void Update()
    {
        if (_isFlying)
        {
            _flightTime -= Time.deltaTime;
            UpdateFlightTimeDisplay(_flightTime);

            UpdateLineRender();
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 22)
            {
                _navMeshAgent.speed = 0f;
                _lineRenderer.enabled = false;
                _isFlying = false;
            }
        }
    }
}