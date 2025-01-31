using UnityEngine;

public class Scr_PosicionObjetos : MonoBehaviour
{
    private Renderer rend;
    private bool isLookingAt = false;
    public bool PuedeDejarObjeto = false;
    public bool ObjetoDejado = false;

    private scr_PickupObject PickupObject;

    void Start()
    {
        rend = GetComponent<Renderer>();
        PickupObject = FindObjectOfType<scr_PickupObject>();
    }

    void Update()
    {
        if (PickupObject.TieneElTeclado)
        {
            if (isLookingAt)
            {
                rend.enabled = true;
                rend.material.color = Color.green; // El cuadrado se vuelve verde
                PuedeDejarObjeto = true;
            }
            else
            {
                rend.enabled = true;
                rend.material.color = Color.red;
                PuedeDejarObjeto = false;
            }
        }

        else if (PickupObject.TieneElTeclado == false)
        {
            rend.enabled = false;
        }

        if (ObjetoDejado)
        {
            Destroy(gameObject);
        }


    }

    void OnMouseEnter()
    {
        isLookingAt = true; // El ratón está mirando el cuadrado
    }

    void OnMouseExit()
    {
        isLookingAt = false; // El ratón dejó de mirar el cuadrado
    }

    public bool IsLookingAt()
    {
        return isLookingAt;
    }
}
