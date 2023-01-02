using UnityEngine;

namespace PhEngine.UI
{
    [CreateAssetMenu(menuName = "PhEngine/UI/UIPanelConfig", fileName = "UIPanelConfig", order = 0)]
    public class UIPanelConfig : ScriptableObject
    {
        public UIPanelSortingMethod sortCanvasOnInitMethod;
        public UIPanelLayerConfig defaultSortingLayer;
        public UIPanel dimmerPanelPrefab;
    }
}