using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

//각 action들을 정의하는 enum
public enum Action_State
{
	None = 0,
	InviteParty,
	Trade,
	InviteGuild
}
public class ActionHolder : MonoBehaviourPunCallbacks
{
	//Atlas 불러오기
	public static SpriteAtlas Atlas;
	//각 action 상태 및 실행되는 action을 저장하는 딕셔너리
	public static Dictionary<Action_State, Action> Actions = new Dictionary<Action_State, Action>();

	//RPC 사용을 위해 PhotonView 선언
	public static PhotonView photonView;
	//목적지 플레이어 ID 를 저장하는 변수
	public static int TargetPlayerIndex;

	//이름 값을 통해 Atlas에서 이미지를 반환해주는 함수
	public static Sprite GetAtlas(string temp)
	{
		return Atlas.GetSprite(temp);
	}

	private void Start()
	{
		photonView = GetComponent<PhotonView>();
		//Resoucres 파일에서 Atlas 불러오기
		Atlas = Resources.Load<SpriteAtlas>("Atlas");
		//딕셔너리 대입
		Actions[Action_State.InviteParty] = InviteParty;
		Actions[Action_State.Trade] = Trade;
		Actions[Action_State.InviteGuild] = InviteGuild;
	}

	//파티 초대 함수
	//RPC
	public static void InviteParty()
	{
		//파티 초대 요청을 보낸 플레이어의 ID와 내 ID를 ReceivePartyInvite 함수로 넘겨준다.
		photonView.RPC("ReceivePartyInvite", PhotonHelper.GetPlayer(TargetPlayerIndex), 
			PhotonNetwork.LocalPlayer.ActorNumber, TargetPlayerIndex);

		string Toast = string.Format(
		"<color=#FFF200>{0}</color>님에게 파티를 초대하였습니다.", PhotonHelper.GetPlayerNickName(TargetPlayerIndex));
		ToastPopUPManager.Instance.Initialize(Toast);
	}

	[PunRPC]
	public void ReceivePartyInvite(int inviterID, int targetPlayerID)
	{
		//설명문에 들어갈 문자열을 설정
		string temp = string.Format(
			"<color=#FFF200>{0}</color>\r\n님이 파티를 초대하였습니다.\r\n수락하시겠습니까?",
			PhotonHelper.GetPlayerNickName(inviterID));

		//수락 버튼 action
		Action YES = () =>
		{
			//파티 초대를 보낸 플레이어 객체를 받아온다.
			Photon.Realtime.Player HOST = PhotonHelper.GetPlayer(inviterID);
			//파티 초대를 받은 플레이어의 객체를 받아온다.
			Photon.Realtime.Player CLIENT = PhotonHelper.GetPlayer(targetPlayerID);

			//파티 초대를 보낸 플레이어가 속한 파티를 party에 저장
			Party party = BaseManager.Party.GetParty(HOST);
			//만약 파티 초대를 보낸 플레이어가 속한 파티가 없는 경우 파티를 새로 만들어준다.
			if (party == null)
			{
				party = BaseManager.Party.CreateParty(HOST);
			}
			//파티 초대를 받는 플레이어를 해당 파티에 추가시킨다.
			BaseManager.Party.JoinParty(CLIENT, party.PartyID);
		};

		//거절 버튼 action
		Action NO = () =>
		{
			ToastPopUPManager.Instance.Initialize("파티 초대를 거절하였습니다ㅠㅠ");
			//상대방의 화면에 파티 초대 거부 했다는 것을 알리기 위해 RPC 사용해서 상대방에게 함수 실행 요청
			photonView.RPC("IgonrePartyInvite", PhotonHelper.GetPlayer(inviterID), inviterID);
		};

		//PopUPManger의 instance 함수를 호출한다.
		PopUPManager.instance.Initialize(temp, YES, NO);
	}

	//파티 초대가 거부되었다는 함수 RPC
	[PunRPC]
	public void IgonrePartyInvite(int targetPlayerID)
	{
		string Toast = string.Format(
		"<color=#FFF200>{0}</color>님이 파티 초대를 거절하였습니다....",
		PhotonHelper.GetPlayerNickName(targetPlayerID));

		ToastPopUPManager.Instance.Initialize(Toast);
	}

	//거래 함수
	public static void Trade()
	{

	}

	//길드 초대 함수
	public static void InviteGuild()
	{

	}
}
