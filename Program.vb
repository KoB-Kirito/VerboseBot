Imports DSharpPlus
Imports DSharpPlus.EventArgs
Imports Newtonsoft.Json

Class Program

    Public Client As DiscordClient

    Public Shared Sub Main(args As String())
        Dim prog = New Program()
        prog.RunBotAsync().GetAwaiter().GetResult()
    End Sub

    Public Async Function RunBotAsync() As Task
        Dim json = IO.File.ReadAllText("config.json", New Text.UTF8Encoding(False))

        Dim cfgJson = JsonConvert.DeserializeObject(Of ConfigJson)(json)
        Dim cfg = New DiscordConfiguration With {
            .Token = cfgJson.Token,
            .TokenType = TokenType.Bot,
 _
            .AutoReconnect = True,
            .LogLevel = LogLevel.Debug,
            .UseInternalLogHandler = True
        }

        Me.Client = New DiscordClient(cfg)


        AddHandler AppDomain.CurrentDomain.FirstChanceException, AddressOf Me.FirstChanceException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Me.UnhandledException
        AddHandler AppDomain.CurrentDomain.ProcessExit, AddressOf Me.ProcessExit


        AddHandler Me.Client.ChannelCreated, AddressOf Me.ChannelCreated
        AddHandler Me.Client.ChannelDeleted, AddressOf Me.ChannelDeleted
        AddHandler Me.Client.ChannelPinsUpdated, AddressOf Me.ChannelPinsUpdated
        AddHandler Me.Client.ChannelUpdated, AddressOf Me.ChannelUpdated

        AddHandler Me.Client.ClientErrored, AddressOf Me.ClientErrored

        AddHandler Me.Client.DmChannelCreated, AddressOf Me.DmChannelCreated
        AddHandler Me.Client.DmChannelDeleted, AddressOf Me.DmChannelDeleted

        AddHandler Me.Client.GuildAvailable, AddressOf Me.GuildAvailable

        AddHandler Me.Client.GuildBanAdded, AddressOf Me.GuildBanAdded
        AddHandler Me.Client.GuildBanRemoved, AddressOf Me.GuildBanRemoved

        AddHandler Me.Client.GuildCreated, AddressOf Me.GuildCreated
        AddHandler Me.Client.GuildDeleted, AddressOf Me.GuildDeleted

        AddHandler Me.Client.GuildDownloadCompleted, AddressOf Me.GuildDownloadCompleted
        AddHandler Me.Client.GuildEmojisUpdated, AddressOf Me.GuildEmojisUpdated
        AddHandler Me.Client.GuildIntegrationsUpdated, AddressOf Me.GuildIntegrationsUpdated

        AddHandler Me.Client.GuildMemberAdded, AddressOf Me.GuildMemberAdded
        AddHandler Me.Client.GuildMemberRemoved, AddressOf Me.GuildMemberRemoved
        AddHandler Me.Client.GuildMembersChunked, AddressOf Me.GuildMembersChunked
        AddHandler Me.Client.GuildMemberUpdated, AddressOf Me.GuildMemberUpdated

        AddHandler Me.Client.GuildRoleCreated, AddressOf Me.GuildRoleCreated
        AddHandler Me.Client.GuildRoleDeleted, AddressOf Me.GuildRoleDeleted
        AddHandler Me.Client.GuildRoleUpdated, AddressOf Me.GuildRoleUpdated

        AddHandler Me.Client.GuildUnavailable, AddressOf Me.GuildUnavailable
        AddHandler Me.Client.GuildUpdated, AddressOf Me.GuildUpdated

        AddHandler Me.Client.Heartbeated, AddressOf Me.Heartbeated

        AddHandler Me.Client.InviteCreated, AddressOf Me.InviteCreated
        AddHandler Me.Client.InviteDeleted, AddressOf Me.InviteDeleted

        AddHandler Me.Client.MessageAcknowledged, AddressOf Me.MessageAcknowledged
        AddHandler Me.Client.MessageCreated, AddressOf Me.MessageCreated
        AddHandler Me.Client.MessageDeleted, AddressOf Me.MessageDeleted

        AddHandler Me.Client.MessageReactionAdded, AddressOf Me.MessageReactionAdded
        AddHandler Me.Client.MessageReactionRemoved, AddressOf Me.MessageReactionRemoved
        AddHandler Me.Client.MessageReactionRemovedEmoji, AddressOf Me.MessageReactionRemovedEmoji
        AddHandler Me.Client.MessageReactionsCleared, AddressOf Me.MessageReactionsCleared

        AddHandler Me.Client.MessagesBulkDeleted, AddressOf Me.MessagesBulkDeleted
        AddHandler Me.Client.MessageUpdated, AddressOf Me.MessageUpdated

        AddHandler Me.Client.PresenceUpdated, AddressOf Me.PresenceUpdated

        AddHandler Me.Client.Ready, AddressOf Me.Ready
        AddHandler Me.Client.Resumed, AddressOf Me.Resumed

        AddHandler Me.Client.SocketClosed, AddressOf Me.SocketClosed
        AddHandler Me.Client.SocketErrored, AddressOf Me.SocketErrored
        AddHandler Me.Client.SocketOpened, AddressOf Me.SocketOpened

        AddHandler Me.Client.TypingStarted, AddressOf Me.TypingStarted

        AddHandler Me.Client.UnknownEvent, AddressOf Me.UnknownEvent

        AddHandler Me.Client.UserSettingsUpdated, AddressOf Me.UserSettingsUpdated
        AddHandler Me.Client.UserUpdated, AddressOf Me.UserUpdated

        AddHandler Me.Client.VoiceServerUpdated, AddressOf Me.VoiceServerUpdated
        AddHandler Me.Client.VoiceStateUpdated, AddressOf Me.VoiceStateUpdated

        AddHandler Me.Client.WebhooksUpdated, AddressOf Me.WebhooksUpdated


        Await Me.Client.ConnectAsync()

        SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED)

        Await Task.Delay(Threading.Timeout.Infinite)
    End Function


    Private Function WebhooksUpdated(e As WebhooksUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function VoiceStateUpdated(e As VoiceStateUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function VoiceServerUpdated(e As VoiceServerUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function UserUpdated(e As UserUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function UserSettingsUpdated(e As UserSettingsUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function UnknownEvent(e As UnknownEventArgs) As Task
        AutoLog(LogLevel.Warning, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function TypingStarted(e As TypingStartEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function SocketOpened() As Task
        AutoLog(LogLevel.Info, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function SocketErrored(e As SocketErrorEventArgs) As Task
        AutoLog(LogLevel.Critical, "Raised", e.Exception)
        Return Task.CompletedTask
    End Function

    Private Function SocketClosed(e As SocketCloseEventArgs) As Task
        AutoLog(LogLevel.Critical, e.CloseCode & e.CloseMessage)
        Return Task.CompletedTask
    End Function

    Private Function Resumed(e As ReadyEventArgs) As Task
        AutoLog(LogLevel.Info, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function Ready(e As ReadyEventArgs) As Task
        AutoLog(LogLevel.Info, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function PresenceUpdated(e As PresenceUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageUpdated(e As MessageUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessagesBulkDeleted(e As MessageBulkDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageReactionsCleared(e As MessageReactionsClearEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageReactionRemovedEmoji(e As MessageReactionRemoveEmojiEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageReactionRemoved(e As MessageReactionRemoveEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageReactionAdded(e As MessageReactionAddEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageDeleted(e As MessageDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageCreated(e As MessageCreateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function MessageAcknowledged(e As MessageAcknowledgeEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function InviteDeleted(e As InviteDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function InviteCreated(e As InviteCreateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function Heartbeated(e As HeartbeatEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildUpdated(e As GuildUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildUnavailable(e As GuildDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildRoleUpdated(e As GuildRoleUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildRoleDeleted(e As GuildRoleDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildRoleCreated(e As GuildRoleCreateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildMemberUpdated(e As GuildMemberUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildMembersChunked(e As GuildMembersChunkEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildMemberRemoved(e As GuildMemberRemoveEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildMemberAdded(e As GuildMemberAddEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildIntegrationsUpdated(e As GuildIntegrationsUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildEmojisUpdated(e As GuildEmojisUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildDownloadCompleted(e As GuildDownloadCompletedEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildDeleted(e As GuildDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildCreated(e As GuildCreateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildBanRemoved(e As GuildBanRemoveEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildBanAdded(e As GuildBanAddEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function GuildAvailable(e As GuildCreateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function DmChannelDeleted(e As DmChannelDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function DmChannelCreated(e As DmChannelCreateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function ClientErrored(e As ClientErrorEventArgs) As Task
        AutoLog(LogLevel.Error, "Raised", e.Exception)
        Return Task.CompletedTask
    End Function

    Private Function ChannelUpdated(e As ChannelUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function ChannelPinsUpdated(e As ChannelPinsUpdateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function ChannelDeleted(e As ChannelDeleteEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Function ChannelCreated(e As ChannelCreateEventArgs) As Task
        AutoLog(LogLevel.Debug, "Raised")
        Return Task.CompletedTask
    End Function

    Private Sub ProcessExit(sender As Object, e As System.EventArgs)
        AutoLog(LogLevel.Debug, "Raised")
    End Sub

    Private Sub UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
        AutoLog(LogLevel.Critical, "Raised", e.ExceptionObject)
    End Sub

    Private Sub FirstChanceException(sender As Object, e As Runtime.ExceptionServices.FirstChanceExceptionEventArgs)
        AutoLog(LogLevel.Warning, "Raised", e.Exception)
    End Sub


    Private Sub AutoLog(Level As LogLevel, Message As String, Optional ex As Exception = Nothing, <Runtime.CompilerServices.CallerMemberName> Optional Source As String = Nothing)
        Me.Client.DebugLogger.LogMessage(Level, Source, Message, Date.Now, ex)
    End Sub

End Class

Public Structure ConfigJson
    <JsonProperty("Token")>
    Public Property Token As String
    <JsonProperty("Prefix")>
    Public Property CommandPrefix As String
End Structure

Module DenyEnergySaving
    'Because I run this testbot on windows

    <FlagsAttribute()>
    Public Enum EXECUTION_STATE As UInteger
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
        ES_CONTINUOUS = &H80000000UI
    End Enum

    <Runtime.InteropServices.DllImport("Kernel32.DLL", CharSet:=Runtime.InteropServices.CharSet.Auto, SetLastError:=True)>
    Public Function SetThreadExecutionState(ByVal state As EXECUTION_STATE) As EXECUTION_STATE
    End Function

End Module

