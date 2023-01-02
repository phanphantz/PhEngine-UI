using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using PhEngine.Sound;

namespace PhEngine.UI
{
    public class UISoundPlayer : MonoBehaviour
    {
        public List<UIPointerInputSoundEntry> soundEntryList = new List<UIPointerInputSoundEntry>();

        public void Play(EventTriggerType type)
        {
            foreach (var soundEntry in soundEntryList.Where(soundEntry => soundEntry.inputType == type))
                Play(soundEntry.soundIds);
        }

        public void Play(params string[] soundIds)
        {
            foreach (var soundId in soundIds)
                SoundManager.Instance.PlayUISound(soundId);
        }
    }
}