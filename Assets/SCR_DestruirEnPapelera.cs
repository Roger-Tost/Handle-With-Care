using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DestruirEnPapelera : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carne_Podrida"))
        {
            Debug.Log("[Papelera] Carne podrida destruida.");
            Destroy(other.gameObject);
        }
    }
}
