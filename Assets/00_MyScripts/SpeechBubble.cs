using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using UnityEngine.UI;
using UnityEditor.VersionControl;
using System.Collections;
using System;

public class SpeechBubble : MonoBehaviour
{
    //0~5���� ������ �� �ֵ��� ǥ��
    //�ش� ���� �÷��̾�κ����� �Ÿ���
    [Range(0.0f, 5.0f)]
    public float yPosFloat = 2.0f;
    //���� ��ġ
    public Transform target;

    [HideInInspector]
    public Text SpeechText;
    public RectTransform[] Contents;
    Animator animator;

    Coroutine coroutine;

    public void Initalize(int actorNumber)
    {
        animator = GetComponent<Animator>();
        //���� �濡 �ִ� �÷��̾� ��� ����, actorNumber�� ��ġ�ϴ� �ش� �÷��̾� ��ü�� ã�� targetPlayer�� �����Ѵ�.
        Player targetPlayer = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == actorNumber);
        //���� �ش� �÷��̾ �����ϸ�
        if(targetPlayer != null)
        {
            //Photon.Realtime.Playter.TagObject : Photon���� ����� ���� �����͸� �����ϱ� ���� �Ӽ�
            //�÷��̾ ������ �ƹ�Ÿ ������Ʈ, ĳ����, ��Ÿ ������ ���⿡ �Ҵ��ص� �� �ִ�.
            //�ش� �÷��̾��� TargetObject�� GameObject�� ĳ������ ��, �� ������Ʈ�� transform�� target ������ ����
            target = ((GameObject)targetPlayer.TagObject).transform;
        }
    }

    public void SetText(string message)
    {
        //���� ���� �������� �ڷ�ƾ�� �����ϸ�
        if(coroutine != null)
        {
            //�ش� �ڷ�ƾ�� ���� ��
            StopCoroutine(coroutine);
            //���ο� �ڷ�ƾ�� action�� �Բ� �ѱ��.
            //��, �����ִ� ��ǳ���� �ݰ�, �ٷ� �ٽ� ��ǳ���� �Ѽ� �޽����� ǥ���Ѵ�.
            coroutine = StartCoroutine(HideCoroutine(0.0f, () =>
            {
                SpeechText.text = message;
                //��ǳ���� ������ �ִϸ��̼� ����
                animator.Play("SpeechBubble_Open");
                //3�� �� ��ǳ���� ������ �ڷ�ƾ �ٽ� ���
                coroutine = StartCoroutine(HideCoroutine(3.0f, null));
            }));
            return;
        }

        //���� �������� �ڷ�ƾ�� ������
        SpeechText.text = message;
        for(int i = 0; i < Contents.Length; i++)
        {
            //content size fitter�� �ʱ�ȭ �����ִ� �Լ�,
            //Contents[] ����Ʈ���� �� content size fitter�� ����ִ� ������Ʈ�� ���ִ�.
            LayoutRebuilder.ForceRebuildLayoutImmediate(Contents[i]);
        }
        //��ǳ���� ������ �ִϸ��̼� ����
        animator.Play("SpeechBubble_Open");
        //3�� ��� ��, ��ǳ���� �ݴ� �ڷ�ƾ ���
        coroutine = StartCoroutine(HideCoroutine(3.0f, null));
    }


    //��ǳ���� ����� �ڷ�ƾ
    IEnumerator HideCoroutine(float timer, Action action)
    {
        //���� �̻� ��� ��,
		yield return new WaitForSeconds(timer);

        //��ǳ���� ������ �ִϸ��̼� ��� ��
		animator.Play("SpeechBubble_Hide");
        //0.3�� ���
        yield return new WaitForSeconds(0.3f);

        //���� �Ѿ�� action�� �����Ѵٸ�, �ش� action�� ����
        //�ش�Ǵ� action�� ��ǳ���� �Ѵ� ������ ���ԵǾ� �ִ�.
        if(action != null)
        {
            action?.Invoke();
        }
        //�Ѿ�� action�� ������ ��ǳ�� ��Ȱ��ȭ
        else gameObject.SetActive(false);

        //������ �� �ڷ�ƾ�� null ó��
        coroutine = null;
    }


	private void LateUpdate()
	{
        //target�� �����ϸ�
		if(target != null)
        {
            //�ش� ��ġ ���� ����
            Vector3 targetPoition = target.position + new Vector3(0.0f, yPosFloat, 0.0f);
            //World ��ǥ�迡�� UI 2D ȭ�� ��ǥ��(RectTransform)�� ��ȯ�ؼ� ����
            transform.position = Camera.main.WorldToScreenPoint(targetPoition);
        }
	}
}
