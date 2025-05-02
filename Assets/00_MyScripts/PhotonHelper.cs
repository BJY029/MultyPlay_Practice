using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonHelper
{
	//�Ѱܹ��� actorNumber�� �г����� ��ȯ���ִ� �Լ�
	public static string GetPlayerNickName(int actorNumber)
	{
		//���� Room�� �����ϰ�, �ش� Room �ȿ� �Էµ� actorNumber �� �����ϴ��� Ȯ���Ѵ�.
		if(PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.Players.ContainsKey(actorNumber))
		{
			//�����ϸ�, �ش� �÷��̾��� �г����� ��ȯ�Ѵ�.
			return PhotonNetwork.CurrentRoom.Players[actorNumber].NickName;
		}
		//�������� ������, ������ ��ȯ�Ѵ�.
		return "Unknown Player!";
	}

	//actorNumber�� ���ؼ� player ��ü�� ã�Ƽ� ��ȯ���ִ� �Լ�
	public static Player GetPlayer(int actorNumber)
	{
		//PhotonNetork �� �ִ� Player ����Ʈ�� ���鼭
		foreach(var player in PhotonNetwork.PlayerList)
		{
			//��ġ�ϴ� actornumber�� �����ϸ�
			if(player.ActorNumber == actorNumber)
				//�ش� �÷��̾� ��ü�� ��ȯ�Ѵ�.
				return player;
		}
		//������ null ��ȯ
		return null;
	}
}
