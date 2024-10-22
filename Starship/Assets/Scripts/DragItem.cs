using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;


public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Vector3 startPosition;

    private void Start()
    {
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        Vector3 offset = eventData.position - eventData.pressPosition;
        transform.position = startPosition + offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        startPosition = transform.position;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
            return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        GetComponentInChildren<Image>().raycastTarget = true;

        transform.SetParent(oldSlot.transform);
        transform.SetAsLastSibling();
        transform.position = oldSlot.transform.position;

        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            InventorySlot newSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>();
            ExchangeSlotData(newSlot);
            if (newSlot.transform.parent.name == "Inventary" && oldSlot.transform.parent.name != "Inventary")
                newSlot.ApplySpeedBoostFromItem();

            else if (newSlot.transform.parent.name == "Storage" && oldSlot.transform.parent.name != "Storage")
                newSlot.RemoveSpeedBoostFromItem();
        }
    }

    void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemScriptableObject item = newSlot.item;
        bool isEmpty = newSlot.isEmpty;
        Sprite iconSprite = newSlot.iconGO.GetComponent<Image>().sprite;

        newSlot.item = oldSlot.item;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
        }
        newSlot.isEmpty = oldSlot.isEmpty;

        oldSlot.item = item;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconSprite);
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
        }

        oldSlot.isEmpty = isEmpty;
    }
}
