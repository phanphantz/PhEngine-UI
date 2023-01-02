using System;
using UnityEngine;
using TMPro;

namespace PhEngine.UI
{
    public class UIDialogPanel : UIPanel
    {
        [Header("Components")] 
        
        [SerializeField] TMP_Text titleText;
        public TMP_Text TitleText => titleText;
        
        [SerializeField] TMP_Text detailText;
        public TMP_Text DetailText => detailText;
        
        [SerializeField] UIButton cancelButton;
        public UIButton CancelButton => cancelButton;
        
        [SerializeField] UIButton confirmButton;
        public UIButton ConfirmButton => confirmButton;

        public Action onCancel;
        public Action onConfirm;

        protected override void Awake()
        {
            base.Awake();
            BindButtons();
        }

        void BindButtons()
        {
            cancelButton.OnClick += (() => onCancel?.Invoke());
            confirmButton.OnClick += (() => onConfirm?.Invoke());
        }
        
        public void SetData(UIDialogPanelData data)
        {
            SetTitle(data.Title);
            SetDetail(data.Detail);
            InitConfirmButton(data);
            InitCancelButton(data);
        }

        static bool IsTextNotNull(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public void SetTitle(string text)
        {
            if (titleText == null)
                return;

            var isValid = IsTextNotNull(text);
            if (isValid)
                titleText.text = text;
           
            titleText.gameObject.SetActive(isValid);
        }
        
        public void SetDetail(string text)
        {
            if (detailText == null)
                return;

            var isValid = IsTextNotNull(text);
            if (isValid)
                detailText.text = text;
           
            detailText.gameObject.SetActive(isValid);
        }

        public void InitCancelButton(UIDialogPanelData data)
        {
            if (cancelButton == null)
                return;

            var buttonData = data.CancelButtonData;
            cancelButton.gameObject.SetActive(buttonData != null);
            if (buttonData == null)
                return;
            
            if (data.IsClosePanelOnCancel)
                cancelButton.OnClick += Close;
            
            cancelButton.SetData(buttonData);
        }
        
        public void InitConfirmButton(UIDialogPanelData data)
        {
            if (confirmButton == null)
                return;

            var buttonData = data.ConfirmButtonData;
            confirmButton.gameObject.SetActive(buttonData != null);
            if (buttonData == null)
                return;
            
            if (data.IsClosePanelOnConfirm)
                confirmButton.OnClick += Close;
            
            confirmButton.SetData(buttonData);
        }
        
    }
    
}