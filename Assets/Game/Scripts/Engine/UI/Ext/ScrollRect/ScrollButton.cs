using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Engine
{
    public class ScrollButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public GameObject scrollRect;
        private Vector2 startDragPosition;
        private bool hasDragged = false;
        private const float DRAG_THRESHOLD = 2f;

        private Action onClick;

        public void SetScrollRect(GameObject scrollRect)
        {
            this.scrollRect = scrollRect;
        }
        public void AddOnClick(Action onClick)
        {
            this.onClick = onClick;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startDragPosition = eventData.position;

            hasDragged = false;

            ExecuteEvents.Execute(scrollRect, eventData, ExecuteEvents.beginDragHandler);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Vector2.Distance(startDragPosition, eventData.position) > DRAG_THRESHOLD)
            {
                hasDragged = true;
            }
            ExecuteEvents.Execute(scrollRect, eventData, ExecuteEvents.dragHandler);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ExecuteEvents.Execute(scrollRect, eventData, ExecuteEvents.endDragHandler);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!hasDragged)
            {
                onClick();
            }
            hasDragged = false;
        }
    }
}