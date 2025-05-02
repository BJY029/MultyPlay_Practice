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

	//Action_State�� ������ ����
	Action_State m_Action;
    public void Initalize(Action_State state)
    {
        //���� ���� Action_state�� m_Action�� ����
        m_Action = state;
        //���� ���޹��� Action_state�� �⺻ ������ ���
        //��, ����� ���ǵ��� ���� ���
        if (m_Action == Action_State.None)
        {
            //�ش� ��ư�� �̹��� ������ ���������� �����.
            GetComponent<Image>().color = new Color(0, 0, 0, GetComponent<Image>().color.a);
            return;
        }
        //���ǵ� ����� ������, �ش� ������ ������Ʈ�� Ȱ��ȭ �����ذ�
        IconImage.gameObject.SetActive(true);
        //�ش�Ǵ� ������Ʈ �̹�����, ���� Action_State ���ڿ��� ����Ͽ� �̹����� ã�ƿͼ� ��������ش�.
        IconImage.sprite = ActionHolder.GetAtlas(state.ToString());
        
        //�ش�Ǵ� ��ư�� ����� �Լ� ���� �ϰ�
        button.onClick.RemoveAllListeners();
        //�� ��ư�� Ŭ���Ǹ�, UIâ�� �������� �ϱ� ���� ���� �Լ��� ��������ش�.
        button.onClick.AddListener(() => baseInteraction.DeactiveObject());
        //�ش� ��ư�� �� ��ɿ� �´� �Լ��� ��������ش�.
        //��, Actions ��ųʸ��� state�� Ű������ �ؼ� ����� �Լ��� button�� ��������ش�.
        button.onClick.AddListener(() => ActionHolder.Actions[state]());
    }
}
