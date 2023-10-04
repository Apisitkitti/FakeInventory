using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rov.InventorySystem
{
public class ScrollMovement : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 10f;

    public void ScrollUp()
    {
        Vector2 position = scrollRect.normalizedPosition;
        position.y = Mathf.Clamp01(position.y + scrollSpeed * Time.deltaTime);
        scrollRect.normalizedPosition = position;
    }

    public void ScrollDown()
    {
        Vector2 position = scrollRect.normalizedPosition;
        position.y = Mathf.Clamp01(position.y - scrollSpeed * Time.deltaTime);
        scrollRect.normalizedPosition = position;
    }
}
}
