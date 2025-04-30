using Photon.Pun;
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
}
