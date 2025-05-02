using UnityEngine;
using System.Collections.Generic;
using Photon.Realtime;

public class Party
{
	//해당 파티의 ID 값
	public int PartyID { get; private set; }
	//해당 파티의 파티장
	public Player Leader { get; private set; }
	//해당 파티에 속한 멤버
	public List<Player> Members { get; private set; }

	//생성자
	public Party(int partyID, Player leader)
	{
		PartyID = partyID;
		Leader = leader;
		Members = new List<Player>() { leader };
	}

	//파티에 Member를 추가하는 함수
	public bool AddMember(Player player)
	{
		//파티 리스트에 해당 멤버가 존재하지 않으면
		if(!Members.Contains(player))
		{
			//해당 멤버를 리스트에 추가한다.
			Members.Add(player);
			return true;
		}
		//이미 존재하므로 추가하지 않고 false 반환
		return false;
	}

	//파티에 Member를 삭제하는 함수
	public bool RemoveMember(Player player)
	{
		//파티 리스트에 해당 멤버가 존재하는 경우
		if (Members.Contains(player))
		{
			//해당 멤버를 리스트에서 삭제시키고
			Members.Remove(player);
			//만약 삭제된 플레이어가 Leader였고, 아직 파티 내에 멤버가 존재하는 경우
			if(player == Leader && Members.Count > 0)
			{
				//파티 리스트의 가장 첫번째에 위치한 멤버에게 Leader 부여
				Leader = Members[0];
			}
			return true;
		}
		//파티 리스트에 해당 멤버가 존재하지 않으므로, false 반환
		return false;
	}

	//해당되는 멤버가 파티 멤버인지 확인하는 함수
	public bool IsMember(Player player)
	{
		return Members.Contains(player);
	}

	//파티 해체 함수
	public void DisbandParty()
	{
		Members.Clear();
	}
}
