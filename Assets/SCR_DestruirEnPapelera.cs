using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DestruirEnPapelera : MonoBehaviour
{
    public GameObject Carne;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Carne)
        {
            Destroy(Carne);
        }
    }
}
