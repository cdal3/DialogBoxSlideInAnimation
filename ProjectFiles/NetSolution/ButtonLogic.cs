#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
#endregion

public class ButtonLogic : BaseNetLogic
{
    public override void Start()
    {
    }

    public override void Stop()
    {
        delayedCloseTask.Dispose();
    }

    [ExportMethod]
    public void CloseDialogWithSlideAnimation()
    {
        var dialogBox = Owner.Owner as DialogBoxWithSlideAnimation;
        if (dialogBox != null)
        {
            var ExitAnimationRunning = dialogBox.Get<NumberAnimation>("ExitAnimation").GetVariable("Running");
            ExitAnimationRunning.Value = true;
            var duration = dialogBox.GetVariable("SlideDuration").Value;
            delayedCloseTask = new DelayedTask(CloseDialogBox, duration, LogicObject);
            delayedCloseTask.Start(); 
            
        } else
        {
            Log.Error("ButtonLogic", "Owner.Owner is not a DialogBoxWithSlideAnimation");
        }
    }

    private void CloseDialogBox()
    {
        var dialogBox = Owner.Owner as DialogBoxWithSlideAnimation;
        dialogBox.Close();
    }

    private DelayedTask delayedCloseTask;
}
