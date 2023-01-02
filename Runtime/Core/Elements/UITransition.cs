using PhEngine.Motion;
using UnityEngine;

namespace PhEngine.UI
{
    public class UITransition : MonoBehaviour
    {
        public Transition Show => show;
        [SerializeField] Transition show;

        public Transition Hide => hide;
        [SerializeField] Transition hide;

        public Transition Select => @select;
        [SerializeField] Transition @select;

        public Transition Deselect => deselect;
        [SerializeField] Transition deselect;
    }
}