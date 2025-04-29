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
public class ActionHolder : MonoBehaviour
{
	//Atlas �ҷ�����
	public static SpriteAtlas Atlas;
	//�� action ���� �� ����Ǵ� action�� �����ϴ� ��ųʸ�
	public static Dictionary<Action_State, Action> Actions = new Dictionary<Action_State, Action>();

	//�̸� ���� ���� Atlas���� �̹����� ��ȯ���ִ� �Լ�
	public static Sprite GetAtlas(string temp)
	{
		return Atlas.GetSprite(temp);
	}

	private void Start()
	{
		//Resoucres ���Ͽ��� Atlas �ҷ�����
		Atlas = Resources.Load<SpriteAtlas>("Atlas");
		//��ųʸ� ����
		Actions[Action_State.InviteGuild] = InviteParty;
		Actions[Action_State.Trade] = Trade;
		Actions[Action_State.InviteGuild] = InviteGuild;
	}

	//��Ƽ �ʴ� �Լ�
	public static void InviteParty()
	{

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
