using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUPManager : MonoBehaviour
{
	//싱글턴
    public static PopUPManager instance = null;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		gameObject.SetActive(false);
	}

	//PopUP 창의 설명창, 수락, 거절 버튼 정의
	public Text Description;
	public Button YesBtn;
	public Button NoBtn;

	//PopUP 창을 활성화 하고, 각종 정보들을 설정하는 함수
	public void Initialize(string temp, Action Yes, Action NO)
	{
		//해당 PopUP 창을 활성화하고
		gameObject.SetActive(true);
		//설명문을 설정하고
		Description.text = temp;

		//버튼 Listener 초기화
		RemoveAllButtons();

		//버튼 Listener 재연결
		YesBtn.onClick.AddListener(() => Yes());
		NoBtn.onClick.AddListener(() => NO());	
	}

	//각 버튼의 onClick에 연결된 함수들을 제거하는 함수
	private void RemoveAllButtons()
	{
		YesBtn.onClick.RemoveAllListeners();
		NoBtn.onClick.RemoveAllListeners();
	}
}
