using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
	private void Start()
	{
		//프로젝트의 Photon 설정을 사용하여 자동으로 서버에 연결하는 함수
		PhotonNetwork.ConnectUsingSettings();
	}

	//Photon 마스터 서버에 연결되었을 때 자동으로 호출되는 콜백 함수
	public override void OnConnectedToMaster()
	{
		Debug.Log("포톤 마스터 서버에 연결하였습니다.");
		//마스터 서버에 연결이 되면, 해당 함수를 호출하여 빈 방이 있으면 자동으로 입장 시도
		//랜덤 룸에 참가하거나 새로운 룸을 생성
		PhotonNetwork.JoinRandomRoom();
	}

	//랜덤 방 참가가 실패했을 때 자동 호출되는 함수
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("방 참가에 실패했습니다. 방을 새로 만듭니다.");
		//빈 방이 없는 경우, 새로운 방을 생성한다.
		//인자로 첫번째 값은 방의 이름으로 null을 보내면 자동으로 방 이름을 지정한다.
		//두 번째 인자는 최대 플레이어 설정으로, 현재 최대 2명의 플레이어가 참가 가능하도록 설정
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
	}

	//방에 성공적으로 참가했을 때 호출되는 함수
	public override void OnJoinedRoom()
	{
		Debug.Log("룸에 접속하였습니다.");
		//방에 참가하면 해당 함수를 통해 플레이어를 생성한다.
		SpawnPlayer();

		//채팅 서버 초기화
		ChatManager.Instance.Initialize();
		BubbleUIManager.instance.InitalizeBubble();
	}

	//플레이어를 생성하는 함수
	void SpawnPlayer()
	{
		//플레이어 생성 위치를 랜덤한 위치로 지정
		Vector3 spawnPosition = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
		//Instantiate : 생성자, Destroy : 파괴자
		//포톤 네트워크를 사용할 땐, 기존과 달리 생성자를 사용할 때 PhotonNetwork를 앞에 붙여줘야 한다.(그래야 동기화 됨)
			//또한 포톤 네트워크에서 생성자를 사용할 땐, 리소스를 활용한다.
			//따라서 Resource 폴더를 만들어준 후, 거기 안에 Player를 드래그 드롭해서 프리팹을 만들어준다.
		GameObject playerObject = PhotonNetwork.Instantiate("PlayerPrefab", spawnPosition, Quaternion.identity);
		/* 즉 위를 풀어서 쓰면 다음과 같은 명령어이다.
		GameObject obj = Resources.Load<GameObject>("PlayerPrefab");
		Instantiate(obj, spawnPosition, Quaternion.identity)
		 */

		//생성된 오브젝트를 현재 로컬 플레이어의 TagObject에 저장한다.
		PhotonNetwork.LocalPlayer.TagObject = playerObject;
	}
	//test
}
