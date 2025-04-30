using Photon.Pun;
using UnityEngine;

//�ش� ��ũ��Ʈ�� ���� ������Ʈ�� �ڵ����� CharacterController ������Ʈ�� �߰�
[RequireComponent(typeof(CharacterController))]
public class Player_Controller : MonoBehaviour
{
	//�̵� ó�� ���
	private CharacterController characterController;
	//�ִϸ��̼� ���
	private Animator animator;

	private float gravity = -9.81f;
	private Vector3 velocity;

	//�� �ڽ��� ID ���� �� �����ϴ� ������Ƽ
	public int OwnerActorNumber {  get; private set; }


	//�÷��̾��� �̵� �ӵ�
    public float speed;
	//Photon���� ��ü�� ������ ������ Ȯ���ϴ� ������Ʈ
	PhotonView view;


	public void Initalize(int actorNumber)
	{
		//�ش� ������Ʈ�� ���� �������� ����(ĳ���Ͱ� A�� ������, A�� �ش� �Լ����� �ڵ� ����)
		if (isMinePhoton())
		{
			//���ÿ��� ���� �ڽ��� actornumber ����
			OwnerActorNumber = actorNumber;
			//SetActorNumber��� �̸��� RPC  �Լ��� ���� Ŭ���̾�Ʈ + ���߿� ������ Ŭ���̾�Ʈ���� ������� ����
			view.RPC("SetActorNumber", RpcTarget.AllBuffered, actorNumber);
		}
	}

	public bool isMinePhoton()
	{
		return view.IsMine;
	}

	//�ش� �Լ��� RPC�� ȣ��� �� �ִٴ� ���� ���
	[PunRPC]
	public void SetActorNumber(int actorNumber)
	{
		//���� ActorNumber�� ���޹��� ������ �������ش�.
		OwnerActorNumber = actorNumber;
	}


	private void Awake()
	{
		//���� ��ü�� PhotonView�� �����´�.
		//�̸� ���ؼ� ���� �÷��̾ ������ ĳ���и� ������ �� �ִ��� Ȯ���� �� �ִ�.
		view = GetComponent<PhotonView>();

		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		//���� �÷��̾ ���� �����ϰ� ����
		if (!view.IsMine) return;

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(h, 0, v);
		//�̵� ������ �����ϴ� ���
		//magnitude�� ������ ũ�⸦ ��Į��(����)������ ��ȯ�ϴ� �������, 
		//�÷��̾��� �̵� ���θ� �ش� magnitude ���� ���� Ȯ���� �����ϴ�.
		if(movement.magnitude >= 0.1f)
		{
			//�ش� ����� �̿��Ͽ� �̵� �������� ȸ���� �����Ѵ�.
			//Mathf.Atan2(y, x)�� �� ��ǥ ��(������ x, y ����)�� �޾Ƽ� �ش� ������ ȸ�� ����(����)�� ��ȯ
			//Mathf.Rad2Deg �� ������ ������ ��ȯ�Ѵ�.
			float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
			//���� ������ Y���� ���� ȸ�� ������ Quaternion���� ��ȯ
			transform.rotation = Quaternion.Euler(0, targetAngle, 0);

			//���� �̵��� �����Ѵ�.
			characterController.Move(movement * speed * Time.deltaTime);
			//�ִϸ��̼� ����
			animator.SetFloat("Movement", movement.magnitude);
		}
		else
		{
			//���� �̵� �Է��� ���� ���, �ִϸ��̼��� ���� ���·� ���̰� �Ѵ�.
			animator.SetFloat("Movement", 0f);
		}

	}
}
