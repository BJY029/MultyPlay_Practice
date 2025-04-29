using UnityEngine;
using UnityEngine.EventSystems;

public class RayManager : MonoBehaviour
{
    [SerializeField] private InteractionUI interactionUI;
    [SerializeField] private LayerMask playerLayer;

	private void Update()
	{
		//0 : ���콺 ���� Ŭ��, 1 : ���콺 ������ Ŭ��
		//GetMouseButton : ���콺 ��ư�� ���� ��쿡�� ���ӵ�
		//GetMouseButtonDown : ���콺 ��ư�� �ѹ� ������ ���ӵ�
		//GetMouseButtonUp : ���콺 ��ư�� ���� �� ������ ���ӵ�
		//���콺 ������ �׸���, ���콺 �����Ͱ� Ȱ��ȭ�� UI ������Ʈ ���� ���� ��� ����
		if(Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject(0))
		{
			FindPlayerClick();
		}
		//��Ŭ����, ���콺 �����Ͱ� Ȱ��ȭ�� UI ������Ʈ ���� ���� ���, UI â�� �������� �����Ѵ�.
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject(0))
		{
			interactionUI.DeactiveObject();
		}
	}

	//���콺 Ŭ�� ��ġ�� Ray�� ���� �÷��̾ ã�� �Լ�
	private void FindPlayerClick()
	{
		//Ray �� ������Ʈ ����
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		//���콺 Ŭ���� �߻��� ���� ������ ���ѰŸ��� �߻��Ͽ���, PlayerLayer �� �ε��� ������Ʈ�� hit ������ ����
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayer))
		{
			//���� �÷��̾��� collider�� �ִ� Player_Controller ��ũ��Ʈ�� �޾ƿ´�.
			Player_Controller player = hit.collider.GetComponent<Player_Controller>();
			//�÷��̾ �����ϸ�
			if (player != null)
			{
				//UI�� Ȱ��ȭ��Ű��
				interactionUI.gameObject.SetActive(true);
				//Ȱ��ȭ �ִϸ��̼� ��� �� �ش� UI�� �÷��̾� ��ġ�� �̵���Ų��.
				//��ȣ�ۿ� �� ����� �÷��̾��̹Ƿ�, player ���� ���� �Ѱ��ش�.
				interactionUI.Initialize(player, Interaction_State.player);
			}
			else
			{
				//�÷��̾ �������� �ʴ� ������ ��Ŭ���� ���� ��� UI�� �ݴ´�.
				interactionUI.DeactiveObject();
			}
		}
		else
		{
			//�÷��̾ �������� �ʴ� ������ ��Ŭ���� ���� ��� UI�� �ݴ´�.
			interactionUI.DeactiveObject();
		}
	}
}
