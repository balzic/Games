
// This file has been generated by the GUI designer. Do not modify.

public partial class GameWindow
{
	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget GameWindow
		this.Name = "GameWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		if((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 400;
		this.DefaultHeight = 300;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
	}
}