using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour
{
    [Tooltip("Lista de objetos UI que podrán moverse con el ratón.")]
    public List<RectTransform> draggableUIElements;

    private RectTransform selectedElement = null;
    private Vector2 offset;
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            TrySelectElementUnderMouse();
        }

        if (Input.GetMouseButton(0) && selectedElement != null)
        {
            DragSelectedElement();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedElement = null;
        }
    }

    void TrySelectElementUnderMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        foreach (RectTransform element in draggableUIElements)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(element, mousePos, canvas.worldCamera))
            {
                selectedElement = element;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    mousePos,
                    canvas.worldCamera,
                    out Vector2 localPoint
                );

                offset = (Vector2)element.localPosition - localPoint;
                break;
            }
        }
    }

    void DragSelectedElement()
    {
        Vector2 mousePos = Input.mousePosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            canvas.worldCamera,
            out Vector2 localPoint
        ))
        {
            selectedElement.localPosition = localPoint + offset;
        }
    }
}