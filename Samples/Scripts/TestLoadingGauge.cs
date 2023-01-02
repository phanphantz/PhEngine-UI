using System;
using System.Collections;
using UnityEngine;

namespace PhEngine.UI.Tests
{
    public class TestLoadingGauge : MonoBehaviour
    {
        [SerializeField] UIGauge loadingGauge;
        [SerializeField] private float targetProgress = 10f;
        
        IEnumerator Start()
        {
            var currentProgress = 0f;

            while (currentProgress < targetProgress)
            {
                var percentage = Mathf.RoundToInt((currentProgress / targetProgress) * 100f);
                loadingGauge.SetFill(currentProgress/targetProgress, new UITextAndIconData(percentage + "%"));
                
                currentProgress += Time.deltaTime;
                
                yield return new WaitForEndOfFrame();
            }
        }
    }
}