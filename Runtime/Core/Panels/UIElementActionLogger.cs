using UnityEngine;

namespace PhEngine.UI
{
    public class UIElementActionLogger
    {
        UIElement Element { get; }
        
        public UIElementActionLogger(UIElement element)
        {
            Element = element;
            BindActions();
        }
        
        void BindActions()
        {
            Element.OnHideBegin += ()=> Log("Hide Begin");
            Element.OnHideFinish += ()=> Log("Hide Finish");
            Element.OnShowBegin += ()=> Log("Show Begin");
            Element.OnShowFinish += ()=> Log("Show Finish");

            Element.OnSelectBegin += ()=> Log("Select Begin");
            Element.OnSelectFinish += ()=> Log("Select Finish");
            Element.OnDeselectBegin += ()=> Log("Deselect Begin");
            Element.OnDeselectFinish += ()=> Log("Deselect Finish");
        
            Element.OnCloseBegin += ()=> Log("Close Begin");
            Element.OnCloseFinish += ()=> Log("Close Finish");
        }

        void Log(string message)
        {
            if (Element.IsLogging)
                Debug.Log($"[{Element.name}] {message}");
        }
    }
}