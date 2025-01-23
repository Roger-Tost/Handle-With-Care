using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Sentarse2MasSentarseQueNunca : MonoBehaviour
{
    private bool isSitting = false; // Indica si el personaje está sentado.
    private bool nearChair = false; // Verifica si hay una silla cerca para sentarse.
    private Transform chairTransform; // Referencia a la silla cercana.
    public float sitDistance = 2f; // Distancia para detectar sillas cercanas.
    public Vector3 sittingOffset = new Vector3(0f, 0.5f, 0f); // Offset para ajustar la posición al sentarse.

    private void Update()
    {
        // Detectar si hay una silla cerca
        DetectNearbyChair();

        // Presiona "E" para sentarse o levantarse si está cerca de una silla
        if (Input.GetKeyDown(KeyCode.E) && nearChair)
        {
            ToggleSitting();
        }
    }

    private void DetectNearbyChair()
    {
        // Busca objetos con la etiqueta "Chair" dentro del rango de detección
        Collider[] colliders = Physics.OverlapSphere(transform.position, sitDistance);
        nearChair = false;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Chair"))
            {
                nearChair = true;
                chairTransform = collider.transform;
                break;
            }
        }
    }

    private void ToggleSitting()
    {
        if (isSitting)
        {
            // Si está sentado, se levanta
            isSitting = false;
            transform.position += Vector3.up; // Simplemente levanta al personaje un poco.
        }
        else
        {
            // Si no está sentado, se mueve a la silla
            isSitting = true;
            if (chairTransform != null)
            {
                transform.position = chairTransform.position + sittingOffset;
                transform.rotation = chairTransform.rotation;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja un círculo para visualizar el rango de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sitDistance);
    }
}
