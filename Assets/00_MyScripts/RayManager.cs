using UnityEngine;

public class RayManager : MonoBehaviour
{
    [SerializeField] private InteractionUI interactionUI;
    [SerializeField] private LayerMask playerLayer;

	private void Update()
	{
		//0 : 마우스 왼쪽 클릭, 1 : 마우스 오른쪽 클릭
		//GetMouseButton : 마우스 버튼이 눌린 경우에만 지속됨
		//GetMouseButtonDown : 마우스 버튼이 한번 눌리면 지속됨
		//GetMouseButtonUp : 마우스 버튼이 눌린 후 떼지면 지속됨
		if(Input.GetMouseButtonDown(1))
		{
			FindPlayerClick();
		}
	}

	//마우스 클릭 위치로 Ray를 쏴서 플레이어를 찾는 함수
	private void FindPlayerClick()
	{
		//Ray 및 오브젝트 정의
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		//마우스 클릭이 발생한 곳에 광선을 무한거리로 발사하여서, PlayerLayer 중 부딪힌 오브젝트를 hit 변수에 저장
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayer))
		{
			//맞은 플레이어의 collider에 있는 Player_Controller 스크립트를 받아온다.
			Player_Controller player = hit.collider.GetComponent<Player_Controller>();
			//플레이어가 존재하면
			if (player != null)
			{
				//UI를 활성화시키고
				interactionUI.gameObject.SetActive(true);
				//활성화 애니메이션 재생 및 해당 UI를 플레이어 위치로 이동시킨다.
				interactionUI.Initialize(player);
			}
		}
	}
}
