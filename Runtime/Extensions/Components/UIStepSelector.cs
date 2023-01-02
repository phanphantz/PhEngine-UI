using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace PhEngine.UI
{

    public class UIStepSelector : MonoBehaviour
    {
        [SerializeField] UIButton minusButton;
        [SerializeField] UIButton plusButton;
        [SerializeField] TextMeshProUGUI text;

        [SerializeField] int currentValue = 0;
        public int CurrentValue => currentValue;

        [SerializeField] int minValue = 0;
        public int MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                minValue = value;
                ValidateValue();
                RefreshDisplay();
            }
        }

        [SerializeField] int maxValue = 1;
        public int MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = value;
                ValidateValue();
                RefreshDisplay();
            }
        }

        [SerializeField] List<string> m_LabelList = new List<string>();
        public List<string> labelList
        {
            get
            {
                return m_LabelList;
            }
            set
            {
                m_LabelList = value;
                ValidateValue();
                RefreshDisplay();
            }
        }

        public UnityEvent<int> onValueChange;

        void Awake()
        {
            minusButton.OnClick += Minus;
            plusButton.OnClick += Plus;
            ValidateValue();
            RefreshDisplay();
        }

        void ValidateValue()
        {
            if (currentValue < minValue)
                currentValue = minValue;

            if (currentValue > maxValue)
                currentValue = maxValue;
        }

        public void RefreshDisplay()
        {
            if (m_LabelList != null && m_LabelList.Count > 0 && currentValue >= 0 && currentValue < m_LabelList.Count)
            {
                text.text = m_LabelList[Mathf.RoundToInt(currentValue)];
            }
            else
            {
                if (Mathf.RoundToInt(currentValue) == currentValue)
                    text.text = Mathf.RoundToInt(currentValue).ToString();
                else
                    text.text = currentValue.ToString("0.0");
            }

            plusButton.IsInteractable = (currentValue < maxValue);
            minusButton.IsInteractable = (currentValue > minValue);
        }

        public void SetValue(int value, bool notify)
        {
            if (value == currentValue)
                return;

            currentValue = value;
            ValidateValue();
            RefreshDisplay();

            if (notify)
                onValueChange?.Invoke(value);
        }

        public void Plus()
        {
            SetValue(currentValue + 1, true);
        }

        public void Minus()
        {
            SetValue(currentValue -1,  true);
        }
    }

}