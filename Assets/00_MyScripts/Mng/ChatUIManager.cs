using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ChatUIManager : MonoBehaviour
{
	//싱글턴
	public static ChatUIManager instance;

	void Awake()
	{
		if(instance == null) instance = this;
	}

	public RectTransform content;
	public InputField chatInputField;
	public Text chatLogText;
	public ScrollRect scrollRect;

	//최대 메시지 개수
	public int MaxMessages;
	//채팅 메시지들을 저장할 리스트
	private List<string> chatMessages = new List<string>();

	private void Update()
	{
		//KeyCode.Return == Enter키
		//현재 이벤트 시스템이 chatInputField를 잡고 있으며, 동시에 Enter키가 눌리면
		if (EventSystem.current.currentSelectedGameObject == chatInputField.gameObject &&
			Input.GetKeyDown(KeyCode.Return))
		{
			//채팅창에 적힌 메시지를 보낸다.
			SendChatMessage();
		}
	}

	//채팅 메시지를 보내는 함수
	private void SendChatMessage()
	{
		//InputField에 있는 메시지를 받아온다.
		string message = chatInputField.text;
		//해당 메시지가 비어있지 않은 경우
		if(!string.IsNullOrEmpty(message))
		{
			//ChatManager의 SendMessageToChat() 함수를 통해 해당 메시지를 서버로 보낸다.
			ChatManager.Instance.SendMessageToChat(message);

			//메시지를 보내면, 해당 메시지 칸은 공백으로 초기화한다.
			chatInputField.text = "";

			//채팅 내용이 null값이 되도, 포커싱이 유지되도록 설정한다.
			chatInputField.ActivateInputField();
		}
	}

	//스크롤뷰에 메시지를 표시하는 함수
	public void DisplayMessage(string Message)
	{
		//전달 받은 메시지를 채팅 메시지 리스트에 삽입한다.
		chatMessages.Add(Message);

		//만약 현재 전달받은 메시지의 총 개수가 최대 메시지 개수 이상인 경우
		if(chatMessages.Count > MaxMessages)
		{
			//가장 오래된 메시지 기록을 삭제한다.
			chatMessages.RemoveAt(0);
		}

		//스크롤을 아래로 고정
		scrollRect.verticalNormalizedPosition = 0.0f;

		UpdateChatLog();
	}

	private void UpdateChatLog()
	{
		//chatMessage 리시트의 요소들을 줄바꿈을 기준으로 하나의 문자열로 합친 후
		//chatLogText에 적용한다.
		chatLogText.text = string.Join("\n", chatMessages);
		//content의 recttranform 값을 조정하여서, 현재 chatLogText의 y축 크기에 100을 더한 값으로 크기를 업데이트 한다.
		content.sizeDelta = new Vector2(content.sizeDelta.x, chatLogText.GetComponent<RectTransform>().sizeDelta.y + 100);
	}
}
