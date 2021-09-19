
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

	[Library( "minimal" )]
	public partial class MinimalGame : Sandbox.Game
	{
		public List<ulong> steamIds = new List<ulong>();
		[ServerCmd( "sethealth" )]
		public static void SetHealth( int amount )
		{
			var player = ConsoleSystem.Caller?.Pawn;
			player.Health = amount;
		}
		public MinimalGame()
		{
			steamIds.Add(76561198108134252);
			steamIds.Add( 76561198096435896 );
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				// Create a HUD entity. This entity is globally networked
				// and when it is created clientside it creates the actual
				// UI panels. You don't have to create your HUD via an entity,
				// this just feels like a nice neat way to do it.
				new SandboxHud();
			}

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			var steamid = client.SteamId;
			if ( client.SteamId != 76561198108134252 || client.SteamId != 76561198096435896)
			{
				client.SendCommandToClient( "unbindall" );
				client.SendCommandToClient("host_writeconfig");	
			}
			base.ClientJoined( client );
			var player = new MinimalPlayer();
			client.Pawn = player;

			player.Respawn();
		}
		[ServerCmd("upgradeMoneyPerClick")]
		public static void upgradeMoneyPerClick(){
			if(globals.Money == globals.MoneyToGivePerClick * 5){
				globals.MoneyToGivePerClick *= 2;
				Log.Info(globals.MoneyToGivePerClick);
			}
		}
	}
