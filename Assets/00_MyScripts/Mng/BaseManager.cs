using UnityEngine;

public class BaseManager : MonoBehaviour
{
	public static BaseManager instance;
	public static PartyManager Party;

	private void Awake()
	{
		if(instance == null) instance = this;

		Party = GetComponentInChildren<PartyManager>();
	}
}
