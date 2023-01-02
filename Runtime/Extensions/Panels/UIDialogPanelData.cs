using System;

namespace PhEngine.UI
{
    public class UIDialogPanelData
    {
        public string Title { get; }
        public string Detail { get; }
        public UIPointerInputListenerData ConfirmButtonData { get; }
        public UIPointerInputListenerData CancelButtonData { get; }
        public bool IsClosePanelOnConfirm { get; }
        public bool IsClosePanelOnCancel { get; }
        
        public UIDialogPanelData
        (
            string title = null, 
            string detail = null, 
            UIPointerInputListenerData confirmButtonData = null, 
            UIPointerInputListenerData cancelButtonData = null,
            bool isClosePanelOnCancel = true,
            bool isClosePanelOnConfirm = true
            )
        {
            Title = title;
            Detail = detail;
            ConfirmButtonData = confirmButtonData;
            CancelButtonData = cancelButtonData;
            IsClosePanelOnCancel = isClosePanelOnCancel;
            IsClosePanelOnConfirm = isClosePanelOnConfirm;
        }
        
    }
}