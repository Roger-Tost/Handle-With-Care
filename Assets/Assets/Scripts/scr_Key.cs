using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Key : MonoBehaviour
{
    // Referencia al script de la puerta para permitirla abrirse
    public scr_Door doorScript;

    // Variable para verificar si la llave ya fue recogida
    private bool llaveRecogida = false;

    void Start()
    {
        // Busca un objeto con el script SCR_Door en la escena, si no est� asignado manualmente
        if (doorScript == null)
        {
            doorScript = FindObjectOfType<scr_Door>();
        }
    }

    // Cuando el jugador entra en el �rea de la llave
    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en el �rea y tiene la etiqueta "Player"
        if (other.CompareTag("Player") && !llaveRecogida)
        {
            // Le damos la llave al jugador, habilitando la acci�n para abrir la puerta
            doorScript.HasKey = true;

            // Marcar que la llave ha sido recogida
            llaveRecogida = true;

            // Destruir el objeto de la llave para que desaparezca de la escena
            Destroy(gameObject);
        }
    }
}
