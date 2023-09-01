using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragHandler draggableItem = dropped.GetComponent<DragHandler>();
        draggableItem.parentAfterDrag = transform;

    }


}
