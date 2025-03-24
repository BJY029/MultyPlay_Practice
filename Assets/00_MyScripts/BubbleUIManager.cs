using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class BubbleUIManager : MonoBehaviourPunCallbacks
{
	//싱글턴
	public static BubbleUIManager instance = null;

	private void Awake()
	{
		if(instance == null) instance = this;
	}

	//말풍선 프리팹
	public GameObject bubblePrefab;
	//각 플레이어마다 말풍선을 할당하기 위해 관련 정보를 담을 딕셔너리
	private Dictionary<int, SpeechBubble> playerBubbles = new Dictionary<int, SpeechBubble>();

	//자기 자신의 말풍선을 초기화 하는 함수
	public void InitalizeBubble()
	{
		//내 버블을 함수를 통해 생성한다.
		CreateBubbleForPlayer(PhotonNetwork.LocalPlayer.ActorNumber);

		//만약 내가 다른 플레이어보다 늦게 게임에 입장한 경우, OnPlayerEnteredRoom()은 실행되지 않는다.
		//따라서 다음과 같이 따로 처리를 해준다.
		foreach(Player player in PhotonNetwork.PlayerList)
		{
			//게임에 입장한 플레이어들 중, 나를 제외한 모든 플레잉어들의 말풍선을 생성한다.
			if(player.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
			{
				CreateBubbleForPlayer(player.ActorNumber);
			}
		}
	}


	//플레이어가 서버에 입장했을 때 호출되는 함수
	//자기 자신은 처리하지 않는다.
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		CreateBubbleForPlayer(newPlayer.ActorNumber);
	}

	//플레이어가 서버를 나갔을 때 호출되는 함수
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		RemoveBubbleForPlayer(otherPlayer.ActorNumber);
	}

	//플레이어 말풍선을 생성하는 함수
	private void CreateBubbleForPlayer(int actorNumber)
	{
		//만약, 해당되는 플레이어의 말풍선이 없는 경우
		if(!playerBubbles.ContainsKey(actorNumber))
		{
			//말풍선 프리팹을 생성 후
			GameObject bubble = Instantiate(bubblePrefab, transform);
			//해당 말풍선을 비활성화한다.
			bubble.SetActive(false);
			//그 다음 speech 객체를 생성한 후,
			SpeechBubble speech = bubble.GetComponent<SpeechBubble>();
			//해당 프리펩이 적용된 SpeechBubble 스크립트의 Initalize 함수를 호출해서, 말풍선 위치를 설정한다.
			speech.Initalize(actorNumber);
			//그리고 해당 플레이어에게 말풍선을 할당한다.(딕셔너리 저장)
			playerBubbles[actorNumber] = speech;
		}
	}

	//플레이어의 말풍선을 파괴하는 함수
	private void RemoveBubbleForPlayer(int actorNumber)
	{
		//해당되는 플레이어의 말풍선이 딕셔너리에 존재하는 경우
		if (playerBubbles.ContainsKey(actorNumber))
		{
			//해당 말풍선을 파괴 후
			Destroy(playerBubbles[actorNumber]);
			//딕셔너리에서도 지운다.(할당 해제)
			playerBubbles.Remove(actorNumber);
		}
	}

	//플레이어의 말풍선을 활성화시키는 함수
	public void ShowBubbleForPlayer(int actorNumber, string message)
	{
		//딕셔너리 안에 해당되는 플레이어가 존재하면, 키에 해당되는 값을 반환시켜준다.
		if(playerBubbles.TryGetValue(actorNumber, out SpeechBubble bubble))
		{
			bubble.gameObject.SetActive(true);
			bubble.SetText(message);
		}
	}

	//플레이어의 말풍선을 비활성화 시키는 함수
	public void HideBubbleForPlayer(int actorNumber)
	{
		//딕셔너리 안에 해당되는 플레이어가 존재하면, 키에 해당되는 값을 반환시켜준다.
		if (playerBubbles.TryGetValue(actorNumber, out SpeechBubble bubble))
		{
			bubble.gameObject.SetActive(false);
		}
	}
}
