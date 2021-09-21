using Sandbox;
using Sandbox.UI;

[Library]
public partial class hud : HudEntity<RootPanel>
{
	public hud()
	{
		if ( !IsClient )
			return;

		RootPanel.StyleSheet.Load( "/ui/hud.scss" );

		RootPanel.AddChild<NameTags>();
		RootPanel.AddChild<CrosshairCanvas>();
		RootPanel.AddChild<ChatBox>();
		RootPanel.AddChild<VoiceList>();
		RootPanel.AddChild<KillFeed>();
		RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		RootPanel.AddChild<Health>();
		RootPanel.AddChild<InventoryBar>();
		RootPanel.AddChild<CurrentTool>();
		RootPanel.AddChild<SpawnMenu>();
	}
}
