using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class BubbleUIManager : MonoBehaviourPunCallbacks
{
	//�̱���
	public static BubbleUIManager instance = null;

	private void Awake()
	{
		if(instance == null) instance = this;
	}

	//��ǳ�� ������
	public GameObject bubblePrefab;
	//�� �÷��̾�� ��ǳ���� �Ҵ��ϱ� ���� ���� ������ ���� ��ųʸ�
	private Dictionary<int, SpeechBubble> playerBubbles = new Dictionary<int, SpeechBubble>();

	//�ڱ� �ڽ��� ��ǳ���� �ʱ�ȭ �ϴ� �Լ�
	public void InitalizeBubble()
	{
		//�� ������ �Լ��� ���� �����Ѵ�.
		CreateBubbleForPlayer(PhotonNetwork.LocalPlayer.ActorNumber);

		//���� ���� �ٸ� �÷��̾�� �ʰ� ���ӿ� ������ ���, OnPlayerEnteredRoom()�� ������� �ʴ´�.
		//���� ������ ���� ���� ó���� ���ش�.
		foreach(Player player in PhotonNetwork.PlayerList)
		{
			//���ӿ� ������ �÷��̾�� ��, ���� ������ ��� �÷��׾���� ��ǳ���� �����Ѵ�.
			if(player.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
			{
				CreateBubbleForPlayer(player.ActorNumber);
			}
		}
	}


	//�÷��̾ ������ �������� �� ȣ��Ǵ� �Լ�
	//�ڱ� �ڽ��� ó������ �ʴ´�.
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		CreateBubbleForPlayer(newPlayer.ActorNumber);
	}

	//�÷��̾ ������ ������ �� ȣ��Ǵ� �Լ�
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		RemoveBubbleForPlayer(otherPlayer.ActorNumber);
	}

	//�÷��̾� ��ǳ���� �����ϴ� �Լ�
	private void CreateBubbleForPlayer(int actorNumber)
	{
		//����, �ش�Ǵ� �÷��̾��� ��ǳ���� ���� ���
		if(!playerBubbles.ContainsKey(actorNumber))
		{
			//��ǳ�� �������� ���� ��
			GameObject bubble = Instantiate(bubblePrefab, transform);
			//�ش� ��ǳ���� ��Ȱ��ȭ�Ѵ�.
			bubble.SetActive(false);
			//�� ���� speech ��ü�� ������ ��,
			SpeechBubble speech = bubble.GetComponent<SpeechBubble>();
			//�ش� �������� ����� SpeechBubble ��ũ��Ʈ�� Initalize �Լ��� ȣ���ؼ�, ��ǳ�� ��ġ�� �����Ѵ�.
			speech.Initalize(actorNumber);
			//�׸��� �ش� �÷��̾�� ��ǳ���� �Ҵ��Ѵ�.(��ųʸ� ����)
			playerBubbles[actorNumber] = speech;
		}
	}

	//�÷��̾��� ��ǳ���� �ı��ϴ� �Լ�
	private void RemoveBubbleForPlayer(int actorNumber)
	{
		//�ش�Ǵ� �÷��̾��� ��ǳ���� ��ųʸ��� �����ϴ� ���
		if (playerBubbles.ContainsKey(actorNumber))
		{
			//�ش� ��ǳ���� �ı� ��
			Destroy(playerBubbles[actorNumber]);
			//��ųʸ������� �����.(�Ҵ� ����)
			playerBubbles.Remove(actorNumber);
		}
	}

	//�÷��̾��� ��ǳ���� Ȱ��ȭ��Ű�� �Լ�
	public void ShowBubbleForPlayer(int actorNumber, string message)
	{
		//��ųʸ� �ȿ� �ش�Ǵ� �÷��̾ �����ϸ�, Ű�� �ش�Ǵ� ���� ��ȯ�����ش�.
		if(playerBubbles.TryGetValue(actorNumber, out SpeechBubble bubble))
		{
			bubble.gameObject.SetActive(true);
			bubble.SetText(message);
		}
	}

	//�÷��̾��� ��ǳ���� ��Ȱ��ȭ ��Ű�� �Լ�
	public void HideBubbleForPlayer(int actorNumber)
	{
		//��ųʸ� �ȿ� �ش�Ǵ� �÷��̾ �����ϸ�, Ű�� �ش�Ǵ� ���� ��ȯ�����ش�.
		if (playerBubbles.TryGetValue(actorNumber, out SpeechBubble bubble))
		{
			bubble.gameObject.SetActive(false);
		}
	}
}
