using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Llave : MonoBehaviour
{
    // Referencia al script de la puerta para permitirla abrirse
    public SCR_Door doorScript;

    // Variable para verificar si la llave ya fue recogida
    private bool llaveRecogida = false;

    void Start()
    {
        // Busca un objeto con el script SCR_Door en la escena, si no está asignado manualmente
        if (doorScript == null)
        {
            doorScript = FindObjectOfType<SCR_Door>();
        }
    }

    // Cuando el jugador entra en el área de la llave
    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en el área y tiene la etiqueta "Player"
        if (other.CompareTag("Player") && !llaveRecogida)
        {
            // Le damos la llave al jugador, habilitando la acción para abrir la puerta
            doorScript.HasKey = true;

            // Marcar que la llave ha sido recogida
            llaveRecogida = true;

            // Destruir el objeto de la llave para que desaparezca de la escena
            Destroy(gameObject);
        }
    }
}
