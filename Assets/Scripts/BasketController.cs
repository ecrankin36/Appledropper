using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float yPosition = -3f;   // fixed Y position of basket
    public float zPosition = 0f;    // usually 0 in 2D games

    void Update()
    {
        // Get mouse position in world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Lock the basket's Y and Z, only use X from the mouse
        transform.position = new Vector3(mouseWorldPos.x, yPosition, zPosition);
    }
}
