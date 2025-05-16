using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCollisionTrigger : MonoBehaviour
{
    [Header("Imágenes que deben colisionar")]
    public RectTransform imagenA;
    public RectTransform imagenB;

    [Header("Imagen que se activará si colisionan")]
    public GameObject imagenActivar;

    private bool yaActivado = false;

    void Start()
    {
        if (imagenActivar != null)
            imagenActivar.SetActive(false);
    }

    void Update()
    {
        if (imagenA == null || imagenB == null || imagenActivar == null) return;

        if (!yaActivado && RectOverlaps(imagenA, imagenB))
        {
            imagenActivar.SetActive(true);
            yaActivado = true; // solo se activa una vez
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect rectA = GetWorldRect(a);
        Rect rectB = GetWorldRect(b);
        return rectA.Overlaps(rectB);
    }

    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0], corners[2] - corners[0]);
    }
}