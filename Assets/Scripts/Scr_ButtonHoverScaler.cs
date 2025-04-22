using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_ButtonHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject otherButton; // Reference to the other button
    public Vector3 hoverScale = new Vector3(2f, 2f, 2f);
    public Vector3 shrinkScale = new Vector3(1.2f, 1.2f, 1.2f);
    public float speed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private Scr_ButtonHoverScaler otherScaler;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        if (otherButton != null)
            otherScaler = otherButton.GetComponent<Scr_ButtonHoverScaler>();
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;
        if (otherScaler != null)
            otherScaler.SetShrink();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        if (otherScaler != null)
            otherScaler.SetNormal();
    }

    public void SetShrink()
    {
        targetScale = shrinkScale;
    }

    public void SetNormal()
    {
        targetScale = originalScale;
    }
}