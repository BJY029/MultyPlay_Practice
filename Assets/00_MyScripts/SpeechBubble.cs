using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using UnityEngine.UI;
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
    Animator animator;

    Coroutine coroutine;

    public void Initalize(int actorNumber)
    {
        animator = GetComponent<Animator>();
		////���� �濡 �ִ� �÷��̾� ��� ����, actorNumber�� ��ġ�ϴ� �ش� �÷��̾� ��ü�� ã�� targetPlayer�� �����Ѵ�.
		//Player targetPlayer = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == actorNumber);
		////���� �ش� �÷��̾ �����ϸ�
		//if(targetPlayer != null)
		//{
		//    //Photon.Realtime.Playter.TagObject : Photon���� ����� ���� �����͸� �����ϱ� ���� �Ӽ�
		//    //�÷��̾ ������ �ƹ�Ÿ ������Ʈ, ĳ����, ��Ÿ ������ ���⿡ �Ҵ��ص� �� �ִ�.
		//    //�ش� �÷��̾��� TargetObject�� GameObject�� ĳ������ ��, �� ������Ʈ�� transform�� target ������ ����
		//    target = ((GameObject)targetPlayer.TagObject).transform;
		//}

		//ActorNumber�� Ȱ���ؼ� �÷��̾��� ��ġ�� ã�´�.
		target = FindPlayerTransformByActorNumber(actorNumber);
	}

    //ActorNumber�� Player�� ��ġ�� ã�� �Լ�
    private Transform FindPlayerTransformByActorNumber(int targetActorNumber)
    {
        //���� ���� �����ϴ� ��� Player_Controller ������Ʈ�� �迭�� �����´�.
        Player_Controller[] allPlayers = FindObjectsByType<Player_Controller>(FindObjectsSortMode.None);
        //�� Player_Controller�� ���鼭
        foreach (Player_Controller player in allPlayers)
        {
            //�ش� ��ũ��Ʈ�� ActornNumber�� ã���ִ� ��ȣ�� ������
            if(player.OwnerActorNumber == targetActorNumber)
            {
                //�ش� ��ũ��Ʈ�� ����� �÷��̾� ��ġ�� ��ȯ�Ѵ�.
                return player.transform;
            }
        }
        //������ null�� ��ȯ�Ѵ�.
        return null;
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
            yield break;
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
