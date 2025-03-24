using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
	private void Start()
	{
		//������Ʈ�� Photon ������ ����Ͽ� �ڵ����� ������ �����ϴ� �Լ�
		PhotonNetwork.ConnectUsingSettings();
	}

	//Photon ������ ������ ����Ǿ��� �� �ڵ����� ȣ��Ǵ� �ݹ� �Լ�
	public override void OnConnectedToMaster()
	{
		Debug.Log("���� ������ ������ �����Ͽ����ϴ�.");
		//������ ������ ������ �Ǹ�, �ش� �Լ��� ȣ���Ͽ� �� ���� ������ �ڵ����� ���� �õ�
		//���� �뿡 �����ϰų� ���ο� ���� ����
		PhotonNetwork.JoinRandomRoom();
	}

	//���� �� ������ �������� �� �ڵ� ȣ��Ǵ� �Լ�
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("�� ������ �����߽��ϴ�. ���� ���� ����ϴ�.");
		//�� ���� ���� ���, ���ο� ���� �����Ѵ�.
		//���ڷ� ù��° ���� ���� �̸����� null�� ������ �ڵ����� �� �̸��� �����Ѵ�.
		//�� ��° ���ڴ� �ִ� �÷��̾� ��������, ���� �ִ� 2���� �÷��̾ ���� �����ϵ��� ����
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
	}

	//�濡 ���������� �������� �� ȣ��Ǵ� �Լ�
	public override void OnJoinedRoom()
	{
		Debug.Log("�뿡 �����Ͽ����ϴ�.");
		//�濡 �����ϸ� �ش� �Լ��� ���� �÷��̾ �����Ѵ�.
		SpawnPlayer();

		//ä�� ���� �ʱ�ȭ
		ChatManager.Instance.Initialize();
		BubbleUIManager.instance.InitalizeBubble();
	}

	//�÷��̾ �����ϴ� �Լ�
	void SpawnPlayer()
	{
		//�÷��̾� ���� ��ġ�� ������ ��ġ�� ����
		Vector3 spawnPosition = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
		//Instantiate : ������, Destroy : �ı���
		//���� ��Ʈ��ũ�� ����� ��, ������ �޸� �����ڸ� ����� �� PhotonNetwork�� �տ� �ٿ���� �Ѵ�.(�׷��� ����ȭ ��)
			//���� ���� ��Ʈ��ũ���� �����ڸ� ����� ��, ���ҽ��� Ȱ���Ѵ�.
			//���� Resource ������ ������� ��, �ű� �ȿ� Player�� �巡�� ����ؼ� �������� ������ش�.
		GameObject playerObject = PhotonNetwork.Instantiate("PlayerPrefab", spawnPosition, Quaternion.identity);
		/* �� ���� Ǯ� ���� ������ ���� ��ɾ��̴�.
		GameObject obj = Resources.Load<GameObject>("PlayerPrefab");
		Instantiate(obj, spawnPosition, Quaternion.identity)
		 */

		//������ ������Ʈ�� ���� ���� �÷��̾��� TagObject�� �����Ѵ�.
		PhotonNetwork.LocalPlayer.TagObject = playerObject;
	}
	//test
}
