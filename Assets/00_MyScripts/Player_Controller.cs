using Photon.Pun;
using UnityEngine;

//해당 스크립트가 붙은 오브젝트에 자동으로 CharacterController 컴포넌트를 추가
[RequireComponent(typeof(CharacterController))]
public class Player_Controller : MonoBehaviour
{
	//이동 처리 담당
	private CharacterController characterController;
	//애니메이션 담당
	private Animator animator;

	private float gravity = -9.81f;
	private Vector3 velocity;

	//내 자신의 ID 설정 및 저장하는 프로퍼티
	public int OwnerActorNumber {  get; private set; }


	//플레이어의 이동 속도
    public float speed;
	//Photon에서 객체가 누구의 것인지 확인하는 컴포넌트
	PhotonView view;


	public void Initalize(int actorNumber)
	{
		//해당 오브젝트를 내가 소유했을 때만(캐릭터가 A의 소유면, A만 해당 함수안의 코드 실행)
		if (isMinePhoton())
		{
			//로컬에서 먼저 자신의 actornumber 설정
			OwnerActorNumber = actorNumber;
			//SetActorNumber라는 이름의 RPC  함수를 현재 클라이언트 + 나중에 접속한 클라이언트들을 대상으로 실행
			view.RPC("SetActorNumber", RpcTarget.AllBuffered, actorNumber);
		}
	}

	public bool isMinePhoton()
	{
		return view.IsMine;
	}

	//해당 함수가 RPC로 호출될 수 있다는 것을 명시
	[PunRPC]
	public void SetActorNumber(int actorNumber)
	{
		//나의 ActorNumber를 전달받은 값으로 설정해준다.
		OwnerActorNumber = actorNumber;
	}


	private void Awake()
	{
		//현재 객체의 PhotonView를 가져온다.
		//이를 통해서 현재 플레이어가 본인의 캐릭털를 조작할 수 있는지 확인할 수 있다.
		view = GetComponent<PhotonView>();

		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		//현재 플레이어만 조작 가능하게 설정
		if (!view.IsMine) return;

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(h, 0, v);
		//이동 방향이 존재하는 경우
		//magnitude는 벡터의 크기를 스칼라(숫자)값으로 반환하는 기능으로, 
		//플레이어의 이동 여부를 해당 magnitude 값을 통해 확인이 가능하다.
		if(movement.magnitude >= 0.1f)
		{
			//해당 계산을 이용하여 이동 방향으로 회전을 진행한다.
			//Mathf.Atan2(y, x)는 두 좌표 값(벡터의 x, y 성분)을 받아서 해당 벡터의 회전 각도(라디안)을 반환
			//Mathf.Rad2Deg 는 라디안을 각도로 변환한다.
			float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
			//나온 각도를 Y축을 기준 회전 값으로 Quaternion으로 변환
			transform.rotation = Quaternion.Euler(0, targetAngle, 0);

			//실제 이동을 적용한다.
			characterController.Move(movement * speed * Time.deltaTime);
			//애니메이션 실행
			animator.SetFloat("Movement", movement.magnitude);
		}
		else
		{
			//만약 이동 입력이 없는 경우, 애니메이션을 정지 상태로 보이게 한다.
			animator.SetFloat("Movement", 0f);
		}

	}
}
