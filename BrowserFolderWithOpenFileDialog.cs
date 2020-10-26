using System;
using System.Reflection;
using System.Windows.Forms;

public static class BrowserFolderWithOpenFileDialog {
    #region Reflection Infos
    private static readonly Assembly asmForm = typeof(Form).Assembly;
    private static readonly Type     typeFD  = typeof(FileDialog);
    private static readonly Type     typeOFD = typeof(OpenFileDialog);
    private static readonly Type     typeIFD =
        asmForm.GetType("System.Windows.Forms.FileDialogNative")
        .GetNestedType("IFileDialog", BindingFlags.NonPublic);

    private static readonly BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    private static readonly MethodInfo MethodCreateVistaDialog   = typeOFD.GetMethod("CreateVistaDialog",   flags);
    private static readonly MethodInfo MethodOnBeforeVistaDialog = typeOFD.GetMethod("OnBeforeVistaDialog", flags);
    private static readonly MethodInfo MethodAdvise              = typeIFD.GetMethod("Advise",              flags);
    private static readonly MethodInfo MethodUnadvise            = typeIFD.GetMethod("Unadvise",            flags);
    private static readonly MethodInfo MethodShow                = typeIFD.GetMethod("Show",                flags);
    private static readonly MethodInfo MethodGetOptions          = typeFD .GetMethod("GetOptions",          flags);
    private static readonly MethodInfo MethodSetOptions          = typeIFD.GetMethod("SetOptions",          flags);

    private static readonly ConstructorInfo MethodNewVistaDialogEvents = 
        asmForm.GetType("System.Windows.Forms.FileDialog")
        .GetNestedType("VistaDialogEvents", BindingFlags.NonPublic)
        .GetConstructors()[0];
    #endregion
    #region Reflection Invoke Wrappers
    private static object CreateVistaDialog(this OpenFileDialog ofd)
        => MethodCreateVistaDialog.Invoke(ofd, null);
    private static object OnBeforeVistaDialog(this OpenFileDialog ofd, object dialog)
        => MethodOnBeforeVistaDialog.Invoke(ofd, new object[] { dialog });
    private static object new_VistaDialogEvents(object ofd)
        => MethodNewVistaDialogEvents.Invoke(new object[] { ofd });
    private static void Advise(this object IFileDialog, object VistaDialogEvents, out uint id) {
        id = 0u;
        var param = new object[] { VistaDialogEvents, id };
        MethodAdvise.Invoke(IFileDialog, param);
        id = (uint)param[1];
    }
    private static void Unadvise(this object IFileDialog, uint id)
        => MethodUnadvise.Invoke(IFileDialog, new object[] { id });
    private static DialogResult Show(this object IFileDialog, IntPtr hWndOwner)
        => 0 == (int)MethodShow.Invoke(IFileDialog, new object[] { hWndOwner })
            ? DialogResult.OK : DialogResult.Cancel;
    private static uint GetOptions(this OpenFileDialog ofd)
        => (uint)MethodGetOptions.Invoke(ofd, null);
    private static void SetOptions(this object IFileDialog, uint val)
        => MethodSetOptions.Invoke(IFileDialog, new object[] { val });
    #endregion

    // OpenFileDialog.RunDialog
    //   inherits FileDialog.RunDialog
    //     invokes FileDialog.RunDialogVista
    //       invokes FileDialog.CreateVistaDialog
    //         impledby OpenFileDialog.CreateVistaDialog
    //           invokes extern FileDialogNative.FileOpenDialogRCW
    //         invokes FileDialog.OnBeforeVistaDialog
    //         invokes FileDialogNative.IFileDialog.Advise
    //         invokes FileDialogNative.IFileDialog.Show
    //         invokes FileDialogNative.IFileDialog.UnAdvise
    public static DialogResult ShowFolderDialog(this OpenFileDialog ofd)
        => ShowFolderDialog(ofd, new IntPtr(0));
    public static DialogResult ShowFolderDialog(this OpenFileDialog ofd, IntPtr hWndOwner) {
        if (Environment.OSVersion.Version.Major < 6)
            throw new NotSupportedException();
        var oldVal = ofd.CheckFileExists;
        if (oldVal == true) ofd.CheckFileExists = false;

        //System.Windows.Forms.FileDialog::RunDialogVista(IntPtr hWndOwner)
        /*IFileDialog*/var dialog = ofd.CreateVistaDialog();
        ofd.OnBeforeVistaDialog(dialog);
        /*VistaDialogEvents*/var events = new_VistaDialogEvents(ofd);
        dialog.SetOptions(ofd.GetOptions() | 32U); /*FOS_PICKFOLDERS*/
        dialog.Advise(events, out uint id);
        try {
            return Show(dialog, hWndOwner);
        } finally {
            dialog.Unadvise(id);
            GC.KeepAlive(events);
            if (oldVal == true) ofd.CheckFileExists = true;
        }
    }
}