using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

//�� action���� �����ϴ� enum
public enum Action_State
{
	None = 0,
	InviteParty,
	Trade,
	InviteGuild
}
public class ActionHolder : MonoBehaviourPunCallbacks
{
	//Atlas �ҷ�����
	public static SpriteAtlas Atlas;
	//�� action ���� �� ����Ǵ� action�� �����ϴ� ��ųʸ�
	public static Dictionary<Action_State, Action> Actions = new Dictionary<Action_State, Action>();

	//RPC ����� ���� PhotonView ����
	public static PhotonView photonView;
	//������ �÷��̾� ID �� �����ϴ� ����
	public static int TargetPlayerIndex;

	//�̸� ���� ���� Atlas���� �̹����� ��ȯ���ִ� �Լ�
	public static Sprite GetAtlas(string temp)
	{
		return Atlas.GetSprite(temp);
	}

	private void Start()
	{
		photonView = GetComponent<PhotonView>();
		//Resoucres ���Ͽ��� Atlas �ҷ�����
		Atlas = Resources.Load<SpriteAtlas>("Atlas");
		//��ųʸ� ����
		Actions[Action_State.InviteParty] = InviteParty;
		Actions[Action_State.Trade] = Trade;
		Actions[Action_State.InviteGuild] = InviteGuild;
	}

	//��Ƽ �ʴ� �Լ�
	//RPC
	public static void InviteParty()
	{
		//��Ƽ �ʴ� ��û�� ���� �÷��̾��� ID�� �� ID�� ReceivePartyInvite �Լ��� �Ѱ��ش�.
		photonView.RPC("ReceivePartyInvite", PhotonHelper.GetPlayer(TargetPlayerIndex), 
			PhotonNetwork.LocalPlayer.ActorNumber, TargetPlayerIndex);

		string Toast = string.Format(
		"<color=#FFF200>{0}</color>�Կ��� ��Ƽ�� �ʴ��Ͽ����ϴ�.", PhotonHelper.GetPlayerNickName(TargetPlayerIndex));
		ToastPopUPManager.Instance.Initialize(Toast);
	}

	[PunRPC]
	public void ReceivePartyInvite(int inviterID, int targetPlayerID)
	{
		//������ �� ���ڿ��� ����
		string temp = string.Format(
			"<color=#FFF200>{0}</color>\r\n���� ��Ƽ�� �ʴ��Ͽ����ϴ�.\r\n�����Ͻðڽ��ϱ�?",
			PhotonHelper.GetPlayerNickName(inviterID));

		//���� ��ư action
		Action YES = () =>
		{
			//��Ƽ �ʴ븦 ���� �÷��̾� ��ü�� �޾ƿ´�.
			Photon.Realtime.Player HOST = PhotonHelper.GetPlayer(inviterID);
			//��Ƽ �ʴ븦 ���� �÷��̾��� ��ü�� �޾ƿ´�.
			Photon.Realtime.Player CLIENT = PhotonHelper.GetPlayer(targetPlayerID);

			//��Ƽ �ʴ븦 ���� �÷��̾ ���� ��Ƽ�� party�� ����
			Party party = BaseManager.Party.GetParty(HOST);
			//���� ��Ƽ �ʴ븦 ���� �÷��̾ ���� ��Ƽ�� ���� ��� ��Ƽ�� ���� ������ش�.
			if (party == null)
			{
				party = BaseManager.Party.CreateParty(HOST);
			}
			//��Ƽ �ʴ븦 �޴� �÷��̾ �ش� ��Ƽ�� �߰���Ų��.
			BaseManager.Party.JoinParty(CLIENT, party.PartyID);
		};

		//���� ��ư action
		Action NO = () =>
		{
			ToastPopUPManager.Instance.Initialize("��Ƽ �ʴ븦 �����Ͽ����ϴ٤Ф�");
			//������ ȭ�鿡 ��Ƽ �ʴ� �ź� �ߴٴ� ���� �˸��� ���� RPC ����ؼ� ���濡�� �Լ� ���� ��û
			photonView.RPC("IgonrePartyInvite", PhotonHelper.GetPlayer(inviterID), inviterID);
		};

		//PopUPManger�� instance �Լ��� ȣ���Ѵ�.
		PopUPManager.instance.Initialize(temp, YES, NO);
	}

	//��Ƽ �ʴ밡 �źεǾ��ٴ� �Լ� RPC
	[PunRPC]
	public void IgonrePartyInvite(int targetPlayerID)
	{
		string Toast = string.Format(
		"<color=#FFF200>{0}</color>���� ��Ƽ �ʴ븦 �����Ͽ����ϴ�....",
		PhotonHelper.GetPlayerNickName(targetPlayerID));

		ToastPopUPManager.Instance.Initialize(Toast);
	}

	//�ŷ� �Լ�
	public static void Trade()
	{

	}

	//��� �ʴ� �Լ�
	public static void InviteGuild()
	{

	}
}
