using System;
using UnityEngine;

namespace PhEngine.UI
{
    [CreateAssetMenu(menuName = "PhEngine/UI/UIPanelLayerPriority" , fileName = "UIPanelLayerPriority")]
    public class UIPanelLayerPriority : ScriptableObject
    {
        [Header("Layer Priorities (Bottom to Top)")]
        public UIPanelLayerConfig[] priorities;

        public int GetPriority(UIPanelLayerConfig config)
        {
            return Array.IndexOf(priorities, config);
        }
    }
}