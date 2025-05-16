using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonQuitarNotas : MonoBehaviour
{
    [Header("Lista de imágenes que se desactivarán")]
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
