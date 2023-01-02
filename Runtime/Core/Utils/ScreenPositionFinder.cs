using UnityEngine;

namespace PhEngine.UI
{
    public static class ScreenPositionFinder
    {
        public static Vector3 GetScreenPositionOfWorldCanvasRectTransform(RectTransform rectTransform)
        {
            return Camera.main.WorldToScreenPoint(rectTransform.transform.position)
                   - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        }
    }
}