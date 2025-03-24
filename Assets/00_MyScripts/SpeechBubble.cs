using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using UnityEngine.UI;
using UnityEditor.VersionControl;

public class SpeechBubble : MonoBehaviour
{
    //0~5���� ������ �� �ֵ��� ǥ��
    //�ش� ���� �÷��̾�κ����� �Ÿ���
    [Range(0.0f, 5.0f)]
    public float yPosFloat = 2.0f;
    //���� ��ġ
    public Transform target;

    public Text SpeechText;

    public void Initalize(int actorNumber)
    {
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
        SpeechText.text = message;
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
