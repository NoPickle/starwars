using Sandbox;
using Sandbox.UI;

[Library]
public partial class SandboxHud : HudEntity<RootPanel>
{
	public SandboxHud()
	{
		if ( !IsClient )
			return;

		RootPanel.StyleSheet.Load( "/ui/SandboxHud.scss" );
		RootPanel.SetTemplate( "/ui/starwars_devbanner.html" );
		RootPanel.StyleSheet.Load( "/Styles/hud.scss" );	

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
		RootPanel.AddChild<KillFeed>();
		RootPanel.AddChild<Ammo>();
		RootPanel.AddChild<DamageIndicator>();
		RootPanel.AddChild<HitIndicator>();
	}
}
