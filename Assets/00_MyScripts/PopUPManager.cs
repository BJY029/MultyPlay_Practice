using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUPManager : MonoBehaviour
{
	//�̱���
    public static PopUPManager instance = null;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		gameObject.SetActive(false);
	}

	//PopUP â�� ����â, ����, ���� ��ư ����
	public Text Description;
	public Button YesBtn;
	public Button NoBtn;

	//PopUP â�� Ȱ��ȭ �ϰ�, ���� �������� �����ϴ� �Լ�
	public void Initialize(string temp, Action Yes, Action NO)
	{
		//�ش� PopUP â�� Ȱ��ȭ�ϰ�
		gameObject.SetActive(true);
		//������ �����ϰ�
		Description.text = temp;

		//��ư Listener �ʱ�ȭ
		RemoveAllButtons();

		//��ư Listener �翬��
		YesBtn.onClick.AddListener(() => Yes());
		NoBtn.onClick.AddListener(() => NO());	
	}

	//�� ��ư�� onClick�� ����� �Լ����� �����ϴ� �Լ�
	private void RemoveAllButtons()
	{
		YesBtn.onClick.RemoveAllListeners();
		NoBtn.onClick.RemoveAllListeners();
	}
}
