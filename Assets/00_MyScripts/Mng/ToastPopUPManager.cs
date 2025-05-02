using UnityEngine;
using UnityEngine.UI;

public class ToastPopUPManager : MonoBehaviour
{
	//싱글턴
    public static ToastPopUPManager Instance;
	public Text PopUPText;
	Animator animator;

	private void Awake()
	{
		if(Instance == null) Instance = this;
		//정상 싱글턴화가 이루어지도록 하기위해, 오브젝트 크기를 처음에 0으로 설정
		//따라서 다음과 같이 오브젝트 크기를 다시 1로 만들어주고
		this.transform.localScale = Vector3.one;
		//비활성화시킨다.
		this.gameObject.SetActive(false);
		animator = GetComponent<Animator>();
	}

	//해당 UI가 실행되면
	public void Initialize(string temp)
	{
		//활성화시키고
		this.gameObject.SetActive(true);
		//text를 설정해주고
		PopUPText.text = temp;
		//애니메이션을 재생한다.
		animator.Play("Toast_Open");
	}

	//Toast_Hide 애니메이션의 마지막 부분에 삽입될 이벤트 함수
	//오브젝트를 비활성화 시킨다.
	public void Deactive() => gameObject.SetActive(false);
}
