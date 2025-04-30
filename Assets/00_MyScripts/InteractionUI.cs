using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//���� ��ȣ�ۿ� ���� enum
public enum Interaction_State
{
	player,
}
public class InteractionUI : MonoBehaviour
{
	[SerializeField] private float yPosFloat;
	Animator animator;
	Player_Controller playerController;

	//�� ��ư���� �����ϴ� �迭
	public InteractionButtonUI[] interactionButtons;
	//���� ��ȣ�ۿ� �ϴ� ���¸� �����ϴ� ����
	Interaction_State m_State;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		//���� Player_Controller�� null�� �ƴϸ�
		if(playerController != null)
		{
			//�ش� Player_Controller ��ũ��Ʈ�� ��ġ ���� targetPosition���� �����ϰ�
			Vector3 targetPosition = playerController.transform.position + new Vector3(0.0f, yPosFloat, 0.0f);
			//World ��ǥ�迡�� UI 2D ȭ�� ��ǥ��(RectTransform)�� ��ȯ�ؼ� ����
			transform.position = Camera.main.WorldToScreenPoint(targetPosition);
		}	
	}

	//UI ���� �ִϸ��̼� ���
	//�̶�, Player_Controller ��ũ��Ʈ�� �޾ƿ��ش�.(�ش� ��ũ��Ʈ�� ���� �÷��̾��� ��ġ Ư��)
	//���� �� ���� �޾ƿ��ش�.
	public void Initialize(Player_Controller controller, Interaction_State state)
	{
		playerController = controller;

		//���޹��� ���¸� m_State�� �����Ѵ�.
		m_State = state;
		//�� ���¿� ���� ��ư�� ��ҵ��� �Ҵ����ִ� �Լ��� ȣ���Ͽ� Action_State���� �����Ѵ�.
		var actions = InteractionActions(state);
		//���޹��� Action_state���� �� ��ư ��ҵ鿡 �Ѱ��ش�.
		for(int i = 0; i < interactionButtons.Length; i++)
		{
			interactionButtons[i].Initalize(actions[i]);
		}
		animator.Play("Hexagon_Open");
	}

	//UI �ݴ� �ִϸ��̼� ���
	public void DeactiveObject()
	{
		//UI ������Ʈ�� ��Ȱ��ȭ �Ǿ� ������, �ִϸ��̼��� ������� �ʴ´�.
		if (gameObject.activeSelf == false) return;
		animator.Play("Hexagon_Out");
	}

	//�ִϸ��̼��� �̺�Ʈ�� ȣ��
	public void Deactive() => gameObject.SetActive(false);

	//��ȣ�ۿ� �ϴ� ��� ���� ��ư�� �ο��ϴ� Action_State ���� �ٸ��� �ο����ִ� �Լ� 
	private Action_State[] InteractionActions(Interaction_State state)
	{
		//Action_state enum �迭 ����
		Action_State[] actions = new Action_State[6];
		switch(state)
		{
			//��ȣ�ۿ� ����� Player �̸�
			case Interaction_State.player:
				//������ ���� �ʴ�, �ŷ�, ����ʴ� ��� Action_State����
				actions[0] = Action_State.InviteParty;
				actions[1] = Action_State.Trade;
				actions[2] = Action_State.InviteGuild;
				break;
		}
		//enum �迭 ��ȯ
		return actions;
	}
}
