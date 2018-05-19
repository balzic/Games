using System;
using System.Threading;
using GameConcepts;
using Gtk;

public partial class GameWindow: Gtk.Window
{
    private static Drawing.GameArea _area;
    private int _absolute_size;
    private int _size;

    /// <summary>
    /// The draw timer. This is public to hide the never used warning.
    /// It is used, just not in an assignment or a method call.
    /// </summary>
    public readonly Timer DrawTimer;

    private readonly TimerCallback _timerDelegate;

    public event DrawSceneHandler DrawScene;
    public event KeyPressedHandler KeyPressed;

    public System.IO.Stream ImageStream(string resourceName)
    {
        var thisExe = System.Reflection.Assembly.GetExecutingAssembly();
        return thisExe.GetManifestResourceStream(resourceName);
    }

    public GameWindow() : base(Gtk.WindowType.Toplevel)
    {
        BorderWidth = 8;

        var monitor = Screen.GetMonitorGeometry(Screen.GetMonitorAtWindow(GdkWindow));
        _absolute_size = (int)(Math.Round(Math.Min(monitor.Width, monitor.Height) / 200.0)) * 100;
        _size = (int)(Math.Round(Math.Min(Screen.Width, Screen.Height) / 200.0)) * 100;

        _area = new Drawing.GameArea(_size, _size);

        Add(_area);
        Title = "DIKUArcade";
        Drawing.Scaler.InitScale(_area.Pixmap);

        SetSizeRequest(_size, _size);
        _area.SetSizeRequest(_absolute_size, _absolute_size);

        WindowPosition = WindowPosition.Center;

        KeyPressEvent += KeyPressEventHandler;
        DeleteEvent += OnDeleteEvent;

        _timerDelegate = new System.Threading.TimerCallback(TimerCallback);
        DrawTimer = new Timer(_timerDelegate, this, 0, 100);
        // set timer to 200 ms (8-bit style)
    }

    public Drawing.GameArea Area { get { return _area; } }

    private void TimerCallback(object state)
    {
        Gdk.Threads.Enter();
        if (DrawScene != null)
        {
            DrawScene(_area);
        }

        _area.QueueDrawArea(0, 0, _absolute_size, _absolute_size);
        Gdk.Threads.Leave();
    }

    [GLib.ConnectBefore]
    private void KeyPressEventHandler(object sender, KeyPressEventArgs e)
    {
        if (KeyPressed != null)
        {
            KeyPressed(e);
        }
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}