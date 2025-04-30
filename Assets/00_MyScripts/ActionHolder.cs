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
		photonView.RPC("ReceivePartyInvite", RpcTarget.Others, 
			PhotonNetwork.LocalPlayer.ActorNumber, TargetPlayerIndex);
	}

	[PunRPC]
	public void ReceivePartyInvite(int inviterID, int targetPlayerID)
	{
		//������ �� ���ڿ��� ����
		string temp = string.Format(
			"<color=#FFF200>{0}</color>\r\n���� ��Ƽ�� �ʴ��Ͽ����ϴ�.\r\n�����Ͻðڽ��ϱ�?",
			PhotonHelper.GetPlayerNickName(inviterID));
		//PopUPManger�� instance �Լ��� ȣ���Ѵ�.
		PopUPManager.instance.Initialize(temp, null, null);
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
