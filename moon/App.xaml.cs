using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace moon
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0 && e.Args[0] == "--screenshot-debug")
            {
                var win = new DebugDemoWin();
                win.Show();
                win.RunDemoAuto();
                win.UpdateLayout();

                int width = (int)(win.ActualWidth > 0 ? win.ActualWidth : 750);
                int height = (int)(win.ActualHeight > 0 ? win.ActualHeight : 550);
                var rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(win);
                var enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(rtb));
                Directory.CreateDirectory(@"c:\Users\Moon\Desktop\1\screenshots");
                using (var fs = File.Create(@"c:\Users\Moon\Desktop\1\screenshots\pr12_debugclasses.png"))
                {
                    enc.Save(fs);
                }
                win.Close();
                Shutdown();
                return;
            }
            base.OnStartup(e);
        }
    }
}
