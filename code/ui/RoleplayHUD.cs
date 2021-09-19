using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
//
// You don't need to put things in a namespace, but it doesn't hurt.
//

	/// <summary>
	/// This is the HUD entity. It creates a RootPanel clientside, which can be accessed
	/// via RootPanel on this entity, or Local.Hud.
	/// </summary>
	public partial class SandboxHud : Panel
	{
		public Panel BackPanel;
		public Panel healthAmount;
		public Panel armorAmount;
		public Label armorText;
		public Label healthText;
		public Label MoneySign;
		public Label MoneyAmount;
		public Panel ImageBackground;
		public ulong playerSteamId = Local.SteamId;
		public Label jobSign;
		public Label jobLabel;
		public SandboxHud()
		{
			StyleSheet.Load("SandboxHud.scss");
			BackPanel = Add.Panel( "backpanel" );
			healthAmount = BackPanel.Add.Panel( "healthAmount" );
			armorAmount = BackPanel.Add.Panel( "armorAmount" );
			healthText = BackPanel.Add.Label("NULL", "healthText" );
			armorText = BackPanel.Add.Label("NULL", "armorText" );
			MoneySign = BackPanel.Add.Label( "$", "moneysign" );
			MoneyAmount = BackPanel.Add.Label( "NULL", "moneyAmount" );
			ImageBackground = BackPanel.Add.Image( $"avatar:{playerSteamId}", "Image" );
			jobLabel = BackPanel.Add.Label( "job", "jobLabel" );
			jobSign = BackPanel.Add.Label( "Job: ", "jobSign" );

		}
		public override void Tick()
		{
			base.Tick();

			healthAmount.Style.Width = Length.Percent( Local.Pawn.Health / 1.9f );
			healthAmount.Style.Dirty();
			healthText.Text = $"{Local.Pawn.Health.CeilToInt()}";
			armorText.Text = $"{Local.Pawn.Health.CeilToInt()}";
			MoneyAmount.Text = "100,000";
			armorAmount.Style.Width = Length.Percent( Local.Pawn.Health / 1.9f );
			armorAmount.Style.Dirty();

		}
	}
