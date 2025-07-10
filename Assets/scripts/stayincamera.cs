using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StayInCameraBounds : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (Camera.main == null) return;

        Vector3 position = transform.position;
        float distanceZ = position.z - Camera.main.transform.position.z;

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distanceZ));

        float halfWidth = spriteRenderer.bounds.extents.x;
        float halfHeight = spriteRenderer.bounds.extents.y;

        position.x = Mathf.Clamp(position.x, bottomLeft.x + halfWidth, topRight.x - halfWidth);
        position.y = Mathf.Clamp(position.y, bottomLeft.y + halfHeight, topRight.y - halfHeight);

        transform.position = position;
    }
}
