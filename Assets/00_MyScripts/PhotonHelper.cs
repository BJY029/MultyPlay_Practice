using Photon.Pun;
using UnityEngine;

public class PhotonHelper
{
	//넘겨받은 actorNumber의 닉네임을 반환해주는 함수
	public static string GetPlayerNickName(int actorNumber)
	{
		//현재 Room이 존재하고, 해당 Room 안에 입력된 actorNumber 가 존재하는지 확인한다.
		if(PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.Players.ContainsKey(actorNumber))
		{
			//존재하면, 해당 플레이어의 닉네임을 반환한다.
			return PhotonNetwork.CurrentRoom.Players[actorNumber].NickName;
		}
		//존재하지 않으면, 다음을 반환한다.
		return "Unknown Player!";
	}
}
