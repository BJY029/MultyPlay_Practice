using UnityEngine;
using UnityEngine.EventSystems;

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
		//마우스 오른쪽 그리고, 마우스 포인터가 활성화된 UI 오브젝트 위에 없는 경우 수행
		if(Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject(0))
		{
			FindPlayerClick();
		}
		//우클릭시, 마우스 포인터가 활성화된 UI 오브젝트 위에 없는 경우, UI 창이 닫히도록 설정한다.
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject(0))
		{
			interactionUI.DeactiveObject();
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
			//해당 플레이어가 내 자신이라면 함수 수행 중지
			if (player.isMinePhoton()) return;
			//플레이어가 존재하면
			if (player != null)
			{
				//내 아이디를 설정해서, 나를 찾을 수 있도록 해준다.
				ActionHolder.TargetPlayerIndex = player.OwnerActorNumber;
				//UI를 활성화시키고
				interactionUI.gameObject.SetActive(true);
				//활성화 애니메이션 재생 및 해당 UI를 플레이어 위치로 이동시킨다.
				//상호작용 한 대상이 플레이어이므로, player 상태 값을 넘겨준다.
				interactionUI.Initialize(player, Interaction_State.player);
			}
			else
			{
				//플레이어가 존재하지 않는 곳에서 좌클릭을 했을 경우 UI를 닫는다.
				interactionUI.DeactiveObject();
			}
		}
		else
		{
			//플레이어가 존재하지 않는 곳에서 좌클릭을 했을 경우 UI를 닫는다.
			interactionUI.DeactiveObject();
		}
	}
}
