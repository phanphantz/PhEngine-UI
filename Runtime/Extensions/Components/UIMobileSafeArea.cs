using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhEngine.UI.Utils
{
    public class UIMobileSafeArea : MonoBehaviour
    {
        [SerializeField] GameObject safeAreaRectGameObject;
        
        int screenWidth => Screen.width;
        int screenHeight => Screen.height;

        void AdjustSafeArea()
        {
            var ratio = screenWidth / (float) screenHeight;
            var isShouldHasSafeArea = Math.Abs(ratio - 3f / 4f) > 0.1f;
            SetSafeAreaVisible(isShouldHasSafeArea);
        }

        public void SetSafeAreaVisible(bool on)
        {
            safeAreaRectGameObject.SetActive(on);
        }

        void Update()
        {
            if (Application.isEditor)
                AdjustSafeArea();
        }
        
    }
 }