using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using UnityEngine.UI;
using UnityEditor.VersionControl;

public class SpeechBubble : MonoBehaviour
{
    //0~5값을 조절할 수 있도록 표시
    //해당 값은 플레이어로부터의 거리값
    [Range(0.0f, 5.0f)]
    public float yPosFloat = 2.0f;
    //생성 위치
    public Transform target;

    public Text SpeechText;

    public void Initalize(int actorNumber)
    {
        //현재 방에 있는 플레이어 목록 에서, actorNumber가 일치하는 해당 플레이어 객체를 찾아 targetPlayer에 저장한다.
        Player targetPlayer = PhotonNetwork.PlayerList.FirstOrDefault(p => p.ActorNumber == actorNumber);
        //만약 해당 플레이어가 존재하면
        if(targetPlayer != null)
        {
            //Photon.Realtime.Playter.TagObject : Photon에서 사용자 정의 데이터를 저장하기 위한 속성
            //플레이어가 소유한 아바타 오브젝트, 캐릭터, 기타 정보를 여기에 할당해둘 수 있다.
            //해당 플레이어의 TargetObject를 GameObject로 캐스팅한 후, 그 오브젝트의 transform을 target 변수에 저장
            target = ((GameObject)targetPlayer.TagObject).transform;
        }
    }

    public void SetText(string message)
    {
        SpeechText.text = message;
    }

	private void LateUpdate()
	{
        //target가 존재하면
		if(target != null)
        {
            //해당 위치 값을 설정
            Vector3 targetPoition = target.position + new Vector3(0.0f, yPosFloat, 0.0f);
            //World 좌표계에서 UI 2D 화면 좌표계(RectTransform)로 변환해서 저장
            transform.position = Camera.main.WorldToScreenPoint(targetPoition);
        }
	}
}
