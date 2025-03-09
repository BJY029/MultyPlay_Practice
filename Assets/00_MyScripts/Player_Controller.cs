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

	//�÷��̾��� �̵� �ӵ�
    public float speed;
	//Photon���� ��ü�� ������ ������ Ȯ���ϴ� ������Ʈ
	PhotonView view;

	private void Start()
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
