using UnityEngine;
using UnityEngine.UI;

public class ToastPopUPManager : MonoBehaviour
{
	//�̱���
    public static ToastPopUPManager Instance;
	public Text PopUPText;
	Animator animator;

	private void Awake()
	{
		if(Instance == null) Instance = this;
		//���� �̱���ȭ�� �̷�������� �ϱ�����, ������Ʈ ũ�⸦ ó���� 0���� ����
		//���� ������ ���� ������Ʈ ũ�⸦ �ٽ� 1�� ������ְ�
		this.transform.localScale = Vector3.one;
		//��Ȱ��ȭ��Ų��.
		this.gameObject.SetActive(false);
		animator = GetComponent<Animator>();
	}

	//�ش� UI�� ����Ǹ�
	public void Initialize(string temp)
	{
		//Ȱ��ȭ��Ű��
		this.gameObject.SetActive(true);
		//text�� �������ְ�
		PopUPText.text = temp;
		//�ִϸ��̼��� ����Ѵ�.
		animator.Play("Toast_Open");
	}

	//Toast_Hide �ִϸ��̼��� ������ �κп� ���Ե� �̺�Ʈ �Լ�
	//������Ʈ�� ��Ȱ��ȭ ��Ų��.
	public void Deactive() => gameObject.SetActive(false);
}
