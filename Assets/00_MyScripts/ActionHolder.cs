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
public class ActionHolder : MonoBehaviour
{
	//Atlas 불러오기
	public static SpriteAtlas Atlas;
	//각 action 상태 및 실행되는 action을 저장하는 딕셔너리
	public static Dictionary<Action_State, Action> Actions = new Dictionary<Action_State, Action>();

	//이름 값을 통해 Atlas에서 이미지를 반환해주는 함수
	public static Sprite GetAtlas(string temp)
	{
		return Atlas.GetSprite(temp);
	}

	private void Start()
	{
		//Resoucres 파일에서 Atlas 불러오기
		Atlas = Resources.Load<SpriteAtlas>("Atlas");
		//딕셔너리 대입
		Actions[Action_State.InviteGuild] = InviteParty;
		Actions[Action_State.Trade] = Trade;
		Actions[Action_State.InviteGuild] = InviteGuild;
	}

	//파티 초대 함수
	public static void InviteParty()
	{

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
