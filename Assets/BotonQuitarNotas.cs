using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonQuitarNotas : MonoBehaviour
{
    [Header("Lista de im�genes que se desactivar�n")]
    public List<GameObject> imagenesUI;

    public void DesactivarImagenes()
    {
        foreach (GameObject img in imagenesUI)
        {
            if (img != null)
                img.SetActive(false);
        }
    }
}
