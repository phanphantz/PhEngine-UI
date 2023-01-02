using System;

namespace PhEngine.UI
{
    [Serializable]
    public class SelectionCategory
    {
        public string categoryId;
        public SelectableComponent linkedTarget;
        
        public SelectionCategory(string categoryId, SelectableComponent linkedTarget)
        {
            this.categoryId = categoryId;
            this.linkedTarget = linkedTarget;
        }
    }
}