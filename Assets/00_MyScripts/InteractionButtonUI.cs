using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButtonUI : MonoBehaviour
{
    public InteractionUI baseInteraction;
    public Image LineImage;
    public Image IconImage;
    public Text ButtonName;

    Button button;

	private void Awake()
	{
		button = GetComponent<Button>();
	}

	//Action_State를 저장할 변수
	Action_State m_Action;
    public void Initalize(Action_State state)
    {
        //전달 받은 Action_state을 m_Action에 저장
        m_Action = state;
        //만약 전달받은 Action_state가 기본 상태인 경우
        //즉, 기능이 정의되지 않은 경우
        if (m_Action == Action_State.None)
        {
            //해당 버튼의 이미지 색상을 검정색으로 만든다.
            GetComponent<Image>().color = new Color(0, 0, 0, GetComponent<Image>().color.a);
            return;
        }
        //정의된 기능이 있으면, 해당 아이콘 오브젝트를 활성화 시켜준고
        IconImage.gameObject.SetActive(true);
        //해당되는 오브젝트 이미지를, 현재 Action_State 문자열을 사용하여 이미지를 찾아와서 적용시켜준다.
        IconImage.sprite = ActionHolder.GetAtlas(state.ToString());
        
        //해당되는 버튼에 연결된 함수 제거 하고
        button.onClick.RemoveAllListeners();
        //각 버튼이 클릭되면, UI창이 닫히도록 하기 위해 다음 함수를 연결시켜준다.
        button.onClick.AddListener(() => baseInteraction.DeactiveObject());
        //해당 버튼에 각 기능에 맞는 함수를 연결시켜준다.
        //즉, Actions 딕셔너리에 state를 키값으로 해서 연결된 함수를 button에 연결시켜준다.
        button.onClick.AddListener(() => ActionHolder.Actions[state]());
    }
}
