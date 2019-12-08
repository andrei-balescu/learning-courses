using System;
using System.Runtime.InteropServices;

namespace DictationProcessorApp
{
    public class GtkHelper
    {
        private const string c_gtkX11Library = "libgtk-x11-2.0.so.0";

        // initialize GTK
        [DllImport(c_gtkX11Library)]
        private static extern void gtk_init(int argc, IntPtr argv);

        // instantiate GTK dialog
        [DllImport(c_gtkX11Library)]
        private static extern IntPtr gtk_message_dialog_new(IntPtr window, int flags, int type, int buttons, string message, IntPtr args);

        // display GTK dialog
        [DllImport(c_gtkX11Library)]
        private static extern int gtk_dialog_run(IntPtr dialog);

        public static void DisplayAlert(string message)
        {
            gtk_init(0, IntPtr.Zero);

            const int c_displayOkButton = 1;
            IntPtr dialog = gtk_message_dialog_new(IntPtr.Zero, 0, 0, c_displayOkButton, message, IntPtr.Zero);

            gtk_dialog_run(dialog);
        }
    }
}