using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

//IChatClientListner �������̽� ���, �������̽��� �����Ͽ� Phton Chat �̺�Ʈ ������ ���� ����
public class ChatManager : MonoBehaviour, IChatClientListener
{
	//�̱��� �������� ����
	public static ChatManager Instance;
	//ä�� �̸�
	private string ChatChannel = "GlobalChannel";

	void Awake()
	{
		if(Instance == null) Instance = this;
	}

	private ChatClient chatClient;

	void Start()
	{
		////chatClient �ν��Ͻ� ����(��ũ��Ʈ�� ����� IChatClientListner�� �����)
		chatClient = new ChatClient(this);
		//Connect �޼��带 ���ؼ�, PhotonNetwork ���� App ID�� ���� ������ �����ͼ� ����
		//AuthenticationValues�� ����Ͽ� ���� �÷��̾� �г����� ���� ������ ����
		chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat,
			PhotonNetwork.AppVersion, new AuthenticationValues(PhotonNetwork.NickName));
	}

	//�ֱ������� Photon chat ���� ������Ʈ
	void Update()
	{
		//chatClient�� null�� �ƴϸ�, Service()�� ȣ���Ͽ� Photon chat�� ���� ��Ʈ��ũ ó���� �ֱ������� ������Ʈ
		chatClient?.Service();
	}

	//ä�� �޽��� ���� �޼���
	public void SendMessageToChat(string message)
	{
		//�޽����� ������� ������
		if (!string.IsNullOrEmpty(message))
		{
			//PublishMessage�� ȣ���Ͽ� ä�� ü�ο� �޽����� �Խ�
			//�޽����� "�г��� : �޽���" �������� ���۵ȴ�.\
			chatClient.PublishMessage(ChatChannel, $"{PhotonNetwork.NickName} : {message}");
		}
	}

	#region ChatClient_Interface

	//Photon.Chat Ŭ���̾�Ʈ���� �߻��ϴ� ����� �޽����� ó���Ѵ�.
	//�Ű����� - level(error, warning, info), message(����� �޽���)
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

	//Photon.Chat Ŭ���̾�Ʈ�� ���°� ����� �� ȣ��ȴ�.
	//�̸� ���� Ÿ ������ ä�� ����(Ÿ ���� �Ӹ��� ��ǳ�� ����)���� ���� ó���� �� �ִ�.
	//�Ű� ���� - state(ChatState ������ ��(ENUM), Ŭ���̾�Ʈ�� ���� ����(Connected, Connecting, Disconnected))
	public void OnChatStateChange(ChatState state)
	{
		///
		/// ConnectedToNameServer : Name Server ���� ������ �Ϸ�� ����
		/// Authenticated : ������ �Ϸ�Ǿ� ä�� ������ ������ �غ� �� ����
		/// Disconnected : ������ ���� ����
		/// ConnectedToFrontEnd : Front-End ������ ����� ����
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

	//Photon Chat ������ ����Ǿ��� �� ȣ��ȴ�.
	public void OnConnected()
	{
		Debug.Log("Photon OnConnected!");
	}

	//���� ������ �������� �� ȣ��ȴ�.
	public void OnDisconnected()
	{
		Debug.Log("Photon DisConnected");
	}

	//Ư�� ä��(���� ä��, ��Ƽ ä�� ��� �� �ϳ�)���� �޽����� ���� �� ȣ��ȴ�.
	//�Ű����� - channelName : �޽����� ���ŵ� ä�� �̸�,
	//	senders : �޽����� ���� ����� �̸� �迭, message : ���ŵ� �޽��� �迭
	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		throw new System.NotImplementedException();
	}

	//���� �޽���(�ӼӸ� ���� ��)�� ���� �� ȣ��ȴ�.
	//�Ű� ���� - sender : �޽����� ���� ������� �̸�, message : �޽��� ����, channelName : �޽����� ���� ä�� �̸�
	//���濡���� ���� �޽��������� ä�� �̸��� �ο��ȴ�.
	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		throw new System.NotImplementedException();
	}

	//Ư�� ������� ���°� ����Ǿ��� �� ȣ��ȴ�.
	//�Ű����� - user : ���°� ����� �����, status : ���ο� ���� �ڵ�(�¶���, ��������, �ڸ���� ��)
	//gotMessage : ���� ���� �� �߰� �޽��� ����, message : ���� ����� �Բ� ���� �� �޽���
	//ģ�� ��Ͽ��� �ش� ģ���� ����(�¶��� Ȥ�� ��������)�� �� �� �ִ� �Լ�
	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		throw new System.NotImplementedException();
	}

	//ä���� �������� �� ȣ��ȴ�.
	//�Ű����� - channels : ������ ä�� �̸� �迭, results : �� ä���� ���� ���� ����
	//Ư�� ĳ���Ͱ� ��忡 �����鼭, ����� ä���� ����� �� �ְ� �Ǵµ�, �̶� ��� ä�ο� �����ϴ� ��� Ȱ��
	public void OnSubscribed(string[] channels, bool[] results)
	{
		throw new System.NotImplementedException();
	}

	//ä�� ������ ������� �� ȣ��ȴ�.
	//�Ű����� - channels : ���� ������ ä�� �̸� �迭 
	//��忡 Ż���� ���
	public void OnUnsubscribed(string[] channels)
	{
		throw new System.NotImplementedException();
	}

	//Ư�� ä�ο� ����ڰ� �������� �� ȣ��ȴ�.
	//�Ű����� - channel : ����ڰ� ������ ä�� �̸�, user : ������ ����� �̸�
	//�������� �޸� ������ ���� ������ �޾ƿ´�.
	//��忡 ������ ���, �ش� ������ ��忡 �����ߴٴ� �޽����� ǥ���� �� ����� �� �ִ�.
	public void OnUserSubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}

	//Ư�� ä�ο��� ����ڰ� ������ �� ȣ��ȴ�.
	//�Ű����� - channel : ����ڰ� ���� ���� �� ä�� �̸�, user : ���� ���� �� ����� �̸�
	public void OnUserUnsubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}
	#endregion
}
