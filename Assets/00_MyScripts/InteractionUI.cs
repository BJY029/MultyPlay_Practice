using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//현재 상호작용 상태 enum
public enum Interaction_State
{
	player,
}
public class InteractionUI : MonoBehaviour
{
	[SerializeField] private float yPosFloat;
	Animator animator;
	Player_Controller playerController;

	//각 버튼들을 저장하는 배열
	public InteractionButtonUI[] interactionButtons;
	//현재 상호작용 하는 상태를 저장하는 변수
	Interaction_State m_State;

	private void Awake()
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
	//상태 값 또한 받아와준다.
	public void Initialize(Player_Controller controller, Interaction_State state)
	{
		playerController = controller;

		//전달받은 상태를 m_State에 저장한다.
		m_State = state;
		//각 상태에 따라서 버튼에 요소들을 할당해주는 함수을 호출하여 Action_State들을 정의한다.
		var actions = InteractionActions(state);
		//전달받은 Action_state들을 각 버튼 요소들에 넘겨준다.
		for(int i = 0; i < interactionButtons.Length; i++)
		{
			interactionButtons[i].Initalize(actions[i]);
		}
		animator.Play("Hexagon_Open");
	}

	//UI 닫는 애니메이션 재생
	public void DeactiveObject()
	{
		//UI 오브젝트가 비활성화 되어 있으면, 애니메이션을 재생하지 않는다.
		if (gameObject.activeSelf == false) return;
		animator.Play("Hexagon_Out");
	}

	//애니메이션의 이벤트로 호출
	public void Deactive() => gameObject.SetActive(false);

	//상호작용 하는 대상에 따라서 버튼에 부여하는 Action_State 들을 다르게 부여해주는 함수 
	private Action_State[] InteractionActions(Interaction_State state)
	{
		//Action_state enum 배열 구성
		Action_State[] actions = new Action_State[6];
		switch(state)
		{
			//상호작용 대상이 Player 이면
			case Interaction_State.player:
				//다음과 같이 초대, 거래, 길드초대 기능 Action_State들을
				actions[0] = Action_State.InviteParty;
				actions[1] = Action_State.Trade;
				actions[2] = Action_State.InviteGuild;
				break;
		}
		//enum 배열 반환
		return actions;
	}
}
