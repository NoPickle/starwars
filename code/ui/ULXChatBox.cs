using System.Linq;
using Sandbox.UI.Construct;
using Sandbox.Hooks;
using System;

namespace Sandbox.UI
{
	public partial class jaxChatBox : Panel
	{
		static jaxChatBox Current;

		public Panel Canvas { get; protected set; }
		public TextEntry Input { get; protected set; }

		public jaxChatBox()
		{
			Current = this;

			StyleSheet.Load( "ULXChatBox.scss" );

			Canvas = Add.Panel( "chat_canvas" );

			Input = Add.TextEntry( "" );
			Input.AddEventListener( "onsubmit", () => Submit() );
			Input.AddEventListener( "onblur", () => Close() );
			Input.AcceptsFocus = true;
			Input.AllowEmojiReplace = true;

			Chat.OnOpenChat += Open;
		}


		void Open()
		{
			AddClass( "open" );
			Input.Focus();
		}

		void Close()
		{
			RemoveClass( "open" );
			Input.Blur();
		}

		void Submit()
		{
			Close();

			var msg = Input.Text.Trim();
			Input.Text = "";

			if ( string.IsNullOrWhiteSpace( msg ) )
				return;

			Say( msg );
		}

		public void AddEntry( string name, string message, string avatar )
		{
			var e = Canvas.AddChild<ChatEntry>();
				//e.SetFirstSibling();
				e.Message.Text = message;
				e.NameLabel.Text = name;
				e.Avatar.SetTexture( avatar );

				e.SetClass( "noname", string.IsNullOrEmpty( name ) );
				e.SetClass( "noavatar", string.IsNullOrEmpty( avatar ) );

		}


		[ClientCmd( "jax_chat_add", CanBeCalledFromServer = true )]
		public static void AddChatEntry( string name, string message, string avatar = null )
		{
			Current?.AddEntry( name, message, avatar );
			Log.Info($"{name}: {message}");


			// Only log clientside if we're not the listen server host
			if ( !Global.IsListenServer )
			{
				Log.Info( $"{name}: {message}" ); 
			}
		}

		[ClientCmd( "jax_chat_addinfo", CanBeCalledFromServer = true )]
		public static void AddInformation( string message, string avatar = null )
		{
			Current?.AddEntry( null, message, avatar );
		}

		[ServerCmd( "jax_say" )]
		public static void Say( string message )
		{
			Assert.NotNull( ConsoleSystem.Caller );

			// todo - reject more stuff
			if ( message.Contains( '\n' ) || message.Contains( '\r' ) )
				return;
			if ( message.StartsWith("!kick"))
			{
				string[] args = message.Split( ' ' );
				Entity target = Client.All.First( c => c.Name == args[1] ).Pawn;
				ConsoleSystem.Run($"kick {args[1]}");
				AddChatEntry(To.Single(ConsoleSystem.Caller  ), "System", $"Kicked {args[1]}", null);
				Log.Info($"{ConsoleSystem.Caller} kicked {args[1]}"  );
				return;
			} 
			else if ( message.StartsWith( "!ban" ) )
			{
				 string SteamIDFromSteamID64( ulong steamId64 )
				{
					var universe = (steamId64 >> 56) & 0xFF;
					if ( universe == 1 ) universe = 0;

					var accountIdLowBit = steamId64 & 1;

					var accountIdHighBits = (steamId64 >> 1) & 0x7FFFFFF;

					return $"STEAM_{universe}:{accountIdLowBit}:{accountIdHighBits}";
				}
				string[] args = message.Split(' ');
				Client target = Client.All.First( c => c.Name.StartsWith( args[1] ) );
				if ( target.IsValid() )
				{
					Log.Info(SteamIDFromSteamID64(target.SteamId)  );
					ConsoleSystem.Run( $"banid {args[2]} {SteamIDFromSteamID64(target.SteamId)} kick");
					AddChatEntry(To.Single(ConsoleSystem.Caller  ), "System", $"Banned {args[1]}", null);
				}
				else
				{
					AddChatEntry(To.Single(ConsoleSystem.Caller  ), "System", "Player is null", null);
				}
			} else if ( message.StartsWith( "!goto" ) )
			{
				string[] args = message.Split( ' ' );
				Vector3 target = Client.All.First( c => c.Name.StartsWith(args[1]) ).Pawn.Position;
				var caller = ConsoleSystem.Caller?.Pawn;
				caller.Position = target;
				AddChatEntry(To.Everyone, "System", $"Someone teleported to {args[1]}");
			}
			else
			{

					AddChatEntry( To.Everyone, ConsoleSystem.Caller.Name, message, $"avatar:{ConsoleSystem.Caller.SteamId}" );
			}

			Log.Info( $"{ConsoleSystem.Caller}: {message}" );
		}

	}
}

namespace Sandbox.Hooks
{
	public static partial class Chat
	{
		public static event Action OnOpenChat;

		[ClientCmd( "openchat" )]
		internal static void MessageMode()
		{
			OnOpenChat?.Invoke();
		}

	}
}
