using Sandbox;
using Sandbox.UI;

public partial class SandboxHud : HudEntity<RootPanel>
{
	public static SandboxHud Current;
	public SandboxHud()
	{
		if ( !IsClient )
			return;

		Current = this;

		RootPanel.StyleSheet.Load( "/UI/SandboxHud.scss" );
		RootPanel.SetTemplate( "/UI/starwars_devbanner.html" );		

		RootPanel.AddChild<NameTags>();
		RootPanel.AddChild<CrosshairCanvas>();
		//RootPanel.AddChild<ChatBox>();
		RootPanel.AddChild<VoiceList>();
		//RootPanel.AddChild<KillFeed>();
		RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		RootPanel.AddChild<Health>();
		RootPanel.AddChild<InventoryBar>();
		RootPanel.AddChild<CurrentTool>();
		RootPanel.AddChild<SpawnMenu>();

		RootPanel.AddChild<ClassicChatBox>();
		RootPanel.AddChild<RKillFeed>();
	}
}
