using UnityEngine;

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
		if(Input.GetMouseButtonDown(1))
		{
			FindPlayerClick();
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
				interactionUI.Initialize(player);
			}
		}
	}
}
