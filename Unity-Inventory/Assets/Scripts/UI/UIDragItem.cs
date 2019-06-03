using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnItemAction(int index);
    public delegate void OnItemMovement(Vector3 position);
    public OnItemAction OnItemBeginDrag;
    public OnItemAction OnItemEndDrag;
    public OnItemMovement OnItemDrag;

    public bool isMoving;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnItemBeginDrag != null)
        {
            //Debug.Log("--------------------- INDEX 3 IS   " + this.GetComponent<PanelIndex>().index3);
            OnItemBeginDrag(this.GetComponent<SlotIndex>().index);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnItemEndDrag != null)
        {
            OnItemEndDrag(GetComponent<SlotIndex>().index);
        }
    }
}
