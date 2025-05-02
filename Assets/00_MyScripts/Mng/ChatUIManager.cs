using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ChatUIManager : MonoBehaviour
{
	//�̱���
	public static ChatUIManager instance;

	void Awake()
	{
		if(instance == null) instance = this;
	}

	public RectTransform content;
	public InputField chatInputField;
	public Text chatLogText;
	public ScrollRect scrollRect;

	//�ִ� �޽��� ����
	public int MaxMessages;
	//ä�� �޽������� ������ ����Ʈ
	private List<string> chatMessages = new List<string>();

	private void Update()
	{
		//KeyCode.Return == EnterŰ
		//���� �̺�Ʈ �ý����� chatInputField�� ��� ������, ���ÿ� EnterŰ�� ������
		if (EventSystem.current.currentSelectedGameObject == chatInputField.gameObject &&
			Input.GetKeyDown(KeyCode.Return))
		{
			//ä��â�� ���� �޽����� ������.
			SendChatMessage();
		}
	}

	//ä�� �޽����� ������ �Լ�
	private void SendChatMessage()
	{
		//InputField�� �ִ� �޽����� �޾ƿ´�.
		string message = chatInputField.text;
		//�ش� �޽����� ������� ���� ���
		if(!string.IsNullOrEmpty(message))
		{
			//ChatManager�� SendMessageToChat() �Լ��� ���� �ش� �޽����� ������ ������.
			ChatManager.Instance.SendMessageToChat(message);

			//�޽����� ������, �ش� �޽��� ĭ�� �������� �ʱ�ȭ�Ѵ�.
			chatInputField.text = "";

			//ä�� ������ null���� �ǵ�, ��Ŀ���� �����ǵ��� �����Ѵ�.
			chatInputField.ActivateInputField();
		}
	}

	//��ũ�Ѻ信 �޽����� ǥ���ϴ� �Լ�
	public void DisplayMessage(string Message)
	{
		//���� ���� �޽����� ä�� �޽��� ����Ʈ�� �����Ѵ�.
		chatMessages.Add(Message);

		//���� ���� ���޹��� �޽����� �� ������ �ִ� �޽��� ���� �̻��� ���
		if(chatMessages.Count > MaxMessages)
		{
			//���� ������ �޽��� ����� �����Ѵ�.
			chatMessages.RemoveAt(0);
		}

		//��ũ���� �Ʒ��� ����
		scrollRect.verticalNormalizedPosition = 0.0f;

		UpdateChatLog();
	}

	private void UpdateChatLog()
	{
		//chatMessage ����Ʈ�� ��ҵ��� �ٹٲ��� �������� �ϳ��� ���ڿ��� ��ģ ��
		//chatLogText�� �����Ѵ�.
		chatLogText.text = string.Join("\n", chatMessages);
		//content�� recttranform ���� �����Ͽ���, ���� chatLogText�� y�� ũ�⿡ 100�� ���� ������ ũ�⸦ ������Ʈ �Ѵ�.
		content.sizeDelta = new Vector2(content.sizeDelta.x, chatLogText.GetComponent<RectTransform>().sizeDelta.y + 100);
	}
}
