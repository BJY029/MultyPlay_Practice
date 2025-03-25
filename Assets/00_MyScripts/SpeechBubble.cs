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
    //0~5값을 조절할 수 있도록 표시
    //해당 값은 플레이어로부터의 거리값
    [Range(0.0f, 5.0f)]
    public float yPosFloat = 2.0f;
    //생성 위치
    public Transform target;

    [HideInInspector]
    public Text SpeechText;
    public RectTransform[] Contents;
    Animator animator;

    Coroutine coroutine;

    public void Initalize(int actorNumber)
    {
        animator = GetComponent<Animator>();
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
        //만약 현재 실행중인 코루틴이 존재하면
        if(coroutine != null)
        {
            //해당 코루틴을 제거 후
            StopCoroutine(coroutine);
            //새로운 코루틴을 action과 함께 넘긴다.
            //즉, 열려있는 말풍선을 닫고, 바로 다시 말풍선을 켜서 메시지를 표시한다.
            coroutine = StartCoroutine(HideCoroutine(0.0f, () =>
            {
                SpeechText.text = message;
                //말풍선이 열리는 애니메이션 제작
                animator.Play("SpeechBubble_Open");
                //3초 후 말풍선이 닫히는 코루틴 다시 재생
                coroutine = StartCoroutine(HideCoroutine(3.0f, null));
            }));
            return;
        }

        //현재 실행중인 코루틴이 없으면
        SpeechText.text = message;
        for(int i = 0; i < Contents.Length; i++)
        {
            //content size fitter를 초기화 시켜주는 함수,
            //Contents[] 리스트에는 각 content size fitter가 들어있는 오브젝트가 들어가있다.
            LayoutRebuilder.ForceRebuildLayoutImmediate(Contents[i]);
        }
        //말풍선이 열리는 애니메이션 제작
        animator.Play("SpeechBubble_Open");
        //3초 대기 후, 말풍선을 닫는 코루틴 재생
        coroutine = StartCoroutine(HideCoroutine(3.0f, null));
    }


    //말풍선을 숨기는 코루틴
    IEnumerator HideCoroutine(float timer, Action action)
    {
        //일정 이상 대기 후,
		yield return new WaitForSeconds(timer);

        //말풍선이 닫히는 애니메이션 재생 후
		animator.Play("SpeechBubble_Hide");
        //0.3초 대기
        yield return new WaitForSeconds(0.3f);

        //만약 넘어온 action이 존재한다면, 해당 action을 수행
        //해당되는 action은 말풍선을 켜는 로직이 포함되어 있다.
        if(action != null)
        {
            action?.Invoke();
        }
        //넘어온 action이 없으면 말풍선 비활성화
        else gameObject.SetActive(false);

        //저장해 둔 코루틴을 null 처리
        coroutine = null;
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
