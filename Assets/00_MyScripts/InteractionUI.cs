using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InteractionUI : MonoBehaviour
{
	[SerializeField] private float yPosFloat;
	Animator animator;
	Player_Controller playerController;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		//만약 Player_Controller가 null이 아니면
		if(playerController != null)
		{
			//해당 Player_Controller 스크립트의 위치 값을 targetPosition으로 설정하고
			Vector3 targetPosition = playerController.transform.position + new Vector3(0.0f, yPosFloat, 0.0f);
			//World 좌표계에서 UI 2D 화면 좌표계(RectTransform)로 변환해서 저장
			transform.position = Camera.main.WorldToScreenPoint(targetPosition);
		}	
	}

	//UI 여는 애니메이션 재생
	//이때, Player_Controller 스크립트도 받아와준다.(해당 스크립트를 통해 플레이어의 위치 특정)
	public void Initialize(Player_Controller controller)
	{
		playerController = controller;
		animator.Play("Hexagon_Open");
	}

	//UI 닫는 애니메이션 재생
	public void DeactiveObject()
	{
		animator.Play("Hexagon_Out");
	}

	//애니메이션의 이벤트로 호출
	public void Deactive() => gameObject.SetActive(false);
}
