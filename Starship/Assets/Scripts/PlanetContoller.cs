using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour
{
    public SpaceshipController spaceshipController;

    private GameObject _planetOutlineContainer;
    private LineRenderer _planetOutlineRenderer;
    private Color _outlineColor = new Color(1f, 0f, 0f, 1f);

    private void Start()
    {
        _planetOutlineContainer = new GameObject("Planet Outline");
        _planetOutlineContainer.transform.parent = transform;
        _planetOutlineContainer.transform.localPosition = Vector3.zero;
        _planetOutlineContainer.transform.localRotation = Quaternion.identity;
        _planetOutlineContainer.transform.localScale = transform.localScale;

        _planetOutlineRenderer = _planetOutlineContainer.AddComponent<LineRenderer>();
        _planetOutlineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _planetOutlineRenderer.startWidth = 0.1f;
        _planetOutlineRenderer.endWidth = 0.1f;
        _planetOutlineRenderer.positionCount = 0;
        _planetOutlineRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spaceshipController.SetTargetPoint(transform.position, gameObject);
            ShowOutline();
            Invoke("HideOutline", 0.1f);
        }
    }

    private void ShowOutline()
    {
        UpdateOutlinePosition();
        _planetOutlineRenderer.startColor = _outlineColor;
        _planetOutlineRenderer.endColor = _outlineColor;
        _planetOutlineRenderer.enabled = true;
    }

    private void UpdateOutlinePosition()
    {
        // Получаем mesh-данные планеты
        Mesh planetMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = planetMesh.vertices;
        int[] triangles = planetMesh.triangles;

        // Обновляем положение точек обводки, используя вершины
        _planetOutlineRenderer.positionCount = triangles.Length;
        for (int i = 0; i < triangles.Length; i++)
        {
            _planetOutlineRenderer.SetPosition(i, transform.TransformPoint(vertices[triangles[i]]));
        }
    }

    public void HideOutline()
    {
        _planetOutlineRenderer.enabled = false;
    }
}