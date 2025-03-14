using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

//IChatClientListner 인터페이스 상속, 인터페이스를 구현하여 Phton Chat 이벤트 리스너 역할 수행
public class ChatManager : MonoBehaviour, IChatClientListener
{
	//싱글턴 패턴으로 구현
	public static ChatManager Instance;
	//채널 이름
	private string ChatChannel = "GlobalChannel";

	void Awake()
	{
		if(Instance == null) Instance = this;
	}

	private ChatClient chatClient;

	void Start()
	{
		////chatClient 인스턴스 생성(스크립트에 적용된 IChatClientListner가 적용됨)
		chatClient = new ChatClient(this);
		//Connect 메서드를 통해서, PhotonNetwork 에서 App ID와 버전 정보를 가져와서 연결
		//AuthenticationValues를 사용하여 현제 플레이어 닉네임을 인정 정보로 전달
		chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat,
			PhotonNetwork.AppVersion, new AuthenticationValues(PhotonNetwork.NickName));
	}

	//주기적으로 Photon chat 서비스 업데이트
	void Update()
	{
		//chatClient가 null이 아니면, Service()를 호출하여 Photon chat의 내부 네트워크 처리를 주기적으로 업데이트
		chatClient?.Service();
	}

	//채팅 메시지 전송 메서드
	public void SendMessageToChat(string message)
	{
		//메시지가 비어있지 않으면
		if (!string.IsNullOrEmpty(message))
		{
			//PublishMessage를 호출하여 채팅 체널에 메시지를 게시
			//메시지는 "닉네임 : 메시지" 형식으로 전송된다.\
			chatClient.PublishMessage(ChatChannel, $"{PhotonNetwork.NickName} : {message}");
		}
	}

	#region ChatClient_Interface

	//Photon.Chat 클라이언트에서 발생하는 디버깅 메시지를 처리한다.
	//매개변수 - level(error, warning, info), message(디버깅 메시지)
	public void DebugReturn(DebugLevel level, string message)
	{
		switch (level)
		{
			case DebugLevel.ERROR:
				Debug.Log($"Photon Chat Error : {message}");
				break;
			case DebugLevel.WARNING:
				Debug.Log($"Photon Chat Warnning : {message}");
				break;
			default:
				Debug.Log($"Photon Chat : {message}");
				break;
		}
	}

	//Photon.Chat 클라이언트의 상태가 변경될 때 호출된다.
	//이를 통해 타 유저의 채팅 반응(타 유저 머리에 말풍선 띄우기)같은 것을 처리할 수 있다.
	//매개 변수 - state(ChatState 열거형 값(ENUM), 클라이언트의 현재 상태(Connected, Connecting, Disconnected))
	public void OnChatStateChange(ChatState state)
	{
		///
		/// ConnectedToNameServer : Name Server 와의 연결이 완료된 상태
		/// Authenticated : 인증이 완료되어 채팅 서버와 연결할 준비가 된 상태
		/// Disconnected : 연결이 끊긴 상태
		/// ConnectedToFrontEnd : Front-End 서버와 연결된 상태
		///
		Debug.Log($"Chat State Changed : {state}");
		switch (state)
		{
			case ChatState.ConnectedToNameServer:
				Debug.Log("Connected to Name Server");
				break;
			case ChatState.Authenticated:
				Debug.Log("Authenticated successfully.");
				break;
			case ChatState.Disconnected:
				Debug.LogWarning("Disconnected from Chat Server");
				break;
			case ChatState.ConnectingToFrontEnd:
				Debug.Log("Connected to Front End Server");
				break;
			default:
				Debug.Log($"Unhandled Chat State : {state}");
				break;
		}
	}

	//Photon Chat 서버와 연결되었을 때 호출된다.
	public void OnConnected()
	{
		Debug.Log("Photon OnConnected!");
	}

	//서버 연결이 끊어졌을 때 호출된다.
	public void OnDisconnected()
	{
		Debug.Log("Photon DisConnected");
	}

	//특정 채널(월드 채널, 파티 채널 등등 중 하나)에서 메시지를 받을 때 호출된다.
	//매개변수 - channelName : 메시지가 수신된 채널 이름,
	//	senders : 메시지를 보낸 사용자 이름 배열, message : 수신된 메시지 배열
	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		throw new System.NotImplementedException();
	}

	//개인 메시지(귓속말 같은 것)를 받을 때 호출된다.
	//매개 변수 - sender : 메시지를 보낸 사용자의 이름, message : 메시지 내용, channelName : 메시지가 속한 채널 이름
	//포톤에서는 개인 메시지에서도 채널 이름이 부여된다.
	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		throw new System.NotImplementedException();
	}

	//특정 사용자의 상태가 변경되었을 때 호출된다.
	//매개변수 - user : 상태가 변경된 사용자, status : 새로운 상태 코드(온라인, 오프라인, 자리비움 등)
	//gotMessage : 상태 변경 시 추가 메시지 여부, message : 상태 변경과 함께 전달 된 메시지
	//친구 목록에서 해당 친구의 상태(온라인 혹은 오프라인)을 알 수 있는 함수
	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		throw new System.NotImplementedException();
	}

	//채널을 구독했을 때 호출된다.
	//매개변수 - channels : 구독한 채널 이름 배열, results : 각 채널의 구독 성공 여부
	//특정 캐릭터가 길드에 들어오면서, 길드의 채널을 사용할 수 있게 되는데, 이때 길드 채널에 구독하는 방식 활용
	public void OnSubscribed(string[] channels, bool[] results)
	{
		throw new System.NotImplementedException();
	}

	//채널 구독을 취소했을 때 호출된다.
	//매개변수 - channels : 구독 해제된 채널 이름 배열 
	//길드에 탈퇴한 경우
	public void OnUnsubscribed(string[] channels)
	{
		throw new System.NotImplementedException();
	}

	//특정 채널에 사용자가 가입했을 때 호출된다.
	//매개변수 - channel : 사용자가 구독한 채널 이름, user : 구독한 사용자 이름
	//구독과는 달리 유저에 대한 정보를 받아온다.
	//길드에 가입한 경우, 해당 유저가 길드에 가입했다는 메시지를 표현할 때 사용할 수 있다.
	public void OnUserSubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}

	//특정 채널에서 사용자가 나갔을 때 호출된다.
	//매개변수 - channel : 사용자가 구독 해제 한 채널 이름, user : 구독 해제 한 사용자 이름
	public void OnUserUnsubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}
	#endregion
}
