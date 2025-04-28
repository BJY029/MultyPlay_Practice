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
	public void Initialize(Player_Controller controller)
	{
		playerController = controller;
		animator.Play("Hexagon_Open");
	}

	//UI �ݴ� �ִϸ��̼� ���
	public void DeactiveObject()
	{
		animator.Play("Hexagon_Out");
	}

	//�ִϸ��̼��� �̺�Ʈ�� ȣ��
	public void Deactive() => gameObject.SetActive(false);
}
