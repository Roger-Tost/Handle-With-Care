using UnityEngine;

public class SquareTarget : MonoBehaviour
{
    private Renderer rend;
    private bool isLookingAt = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isLookingAt)
        {
            rend.enabled = true;
            rend.material.color = Color.green; // El cuadrado se vuelve verde
        }
        else
        {
            rend.enabled = false;
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
