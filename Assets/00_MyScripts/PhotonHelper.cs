using Photon.Pun;
using Photon.Realtime;
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

	//actorNumber를 통해서 player 객체를 찾아서 반환해주는 함수
	public static Player GetPlayer(int actorNumber)
	{
		//PhotonNetork 상에 있는 Player 리스트를 돌면서
		foreach(var player in PhotonNetwork.PlayerList)
		{
			//일치하는 actornumber가 존재하면
			if(player.ActorNumber == actorNumber)
				//해당 플레이어 객체를 반환한다.
				return player;
		}
		//없으면 null 반환
		return null;
	}
}
