using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Tests;

[Library]
public partial class StarList : Panel
{
	VirtualScrollPanel Canvas;

	public StarList()
	{
		AddClass( "spawnpage" );
		AddChild( out Canvas, "canvas" );

		Canvas.Layout.AutoColumns = true;
		Canvas.Layout.ItemSize = new Vector2( 100, 100 );
		Canvas.OnCreateCell = ( cell, data ) =>
		{
			var file = (string)data;
			var panel = cell.Add.Panel( "icon" );
			panel.AddEventListener( "onclick", () => ConsoleSystem.Run( "spawn", "rud3r/" + file ) );
			panel.Style.Background = new PanelBackground
			{
				Texture = Texture.Load( $"/rud3r/{file}_r.png", false )
			};
		};

		foreach ( var file in FileSystem.Mounted.FindFile( "rud3r", "*.vmdl_r.png", true ) )
		{
			if ( string.IsNullOrWhiteSpace( file ) ) continue;
			if ( file.Contains( "_lod0" ) ) continue;
			if ( file.Contains( "clothes" ) ) continue;

			Canvas.AddItem( file.Remove( file.Length - 6 ) );
		}
	}
}
