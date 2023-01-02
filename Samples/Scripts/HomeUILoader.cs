using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PhEngine.Item.UI;
using PhEngine.UI;
using UnityEngine;

public class HomeUILoader : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return ExampleUILoadingRoutine();
    }

    IEnumerator ExampleUILoadingRoutine()
    {
        var loadingScreen = LoadLoadingScreen();

        yield return new WaitForSeconds(2f);
        LoadHomeMainUI();

        yield return new WaitForSeconds(2f);
        loadingScreen?.Close();

        yield return new WaitForSeconds(1f);
        ShowSampleDialogBoxWithYesNo();

        yield return new WaitForSeconds(1f);
        ShowSampleNotificationBox();
    }

    UIPanel LoadLoadingScreen()
    {
        return UIPanelManager.Instance.Spawn("UILoadingScreen");
    }

    void LoadHomeMainUI()
    {
        UIPanelManager.Instance.Spawn("UIHomeMain");
    }

    void ShowSampleDialogBoxWithYesNo()
    {
        var sampleDialogBox = UIPanelManager.Instance.Spawn("UIDialogBox") as UIDialogPanel;
        var cancelButtonData = new UIPointerInputListenerData("No", null ,  ()=> Debug.Log("Click No"));
        var confirmButtonData = new UIPointerInputListenerData("Yes" , null , ()=>  Debug.Log("Click Yes"));
        var dialogBoxData = new UIDialogPanelData
        ("Sample Dialog Box", 
            "Sample Detail", 
            confirmButtonData,
            cancelButtonData
        );
        
        sampleDialogBox.SetData(dialogBoxData);
    }

    void ShowSampleNotificationBox()
    {
        var sampleNotificationBox = UIPanelManager.Instance.Spawn("UIDialogBox") as UIDialogPanel;
        var confirmButtonData = new UIPointerInputListenerData("OK");
        var notificationBoxData = new UIDialogPanelData
        (null, 
            "Sample Notification Box", 
            confirmButtonData
        );
        
        sampleNotificationBox.SetData(notificationBoxData);
    }
    
}
