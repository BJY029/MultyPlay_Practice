using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviourPunCallbacks
{
    //현재 활성화 된 파티들을 각 ID와 저장한 딕셔너리
    private Dictionary<int, Party> activeParties = new Dictionary<int, Party>();

    //어떤 사용자에 대해서, 해당 사용자가 파티에 속해있는지 확인하는 함수
    public bool HasParty(Player player)
    {
        //활성화 된 파티 딕셔너리의 Value를 돌면서
        foreach(var party in activeParties.Values)
        {
            //각 파티에서 해당되는 player가 Member에 속하면 true 반환
            if(party.IsMember(player))
                return true;
        }
        //해당 플레이어가 어디에도 속하지 않은 경우 false 반환
        return false;
    }

    //특정 플레이어가 속한 party 객체를 반환해주는 함수
    public Party GetParty(Player player)
    {
        //활성화된 party 딕셔너리의 Value를 돌면서
        foreach(var party in activeParties.Values)
        {
            //각 파티에서 해당 player가 멤버로 존재하는지 검사
            if(party.IsMember(player))
                //존재하면 해당 party 객체 반환
                return party;
        }
        //해당 플레이어가 속한 party가 존재하지 않으면 null 반환
        return null;
    }

    //파티를 생성하고 해당 파티 객체를 반환하는 함수
    //파티 생성을 요청하는 사람은 곧 파티의 leader가 된다.
    public Party CreateParty(Player leader)
    {
        //해당 플레이어가 속한 파티가 존재하는 경우, 파티를 생성하지 않고 null 반환
        if (HasParty(leader)) return null;

        //현재 활성화된 파티 갯수 + 1 을 partyId로 설정
        int partyID = activeParties.Count + 1;
        //새로운 Party를 생성자로 생성
        Party newParty = new Party(partyID, leader);
        //생성된 Party 객체를 딕셔너리에도 삽입
        activeParties.Add(partyID, newParty);

        //해당 파티 객체 반환
        return newParty;
    }


    //파티에 참가하는 함수
    public bool JoinParty(Player player, int partyID)
    {
        //해당 플레이어가 속한 파티가 존재하거나, 요청된 파티 ID가 현재 존재하지 않는경우
        //false 반환
        if(HasParty(player) || !activeParties.ContainsKey(partyID)) 
            return false;

        //해당 파티 객체의 AddMember 함수를 통해서 파티 참가
        //(해당 함수는 파티 참여가 성공적으로 되면 true 반환)
        return activeParties[partyID].AddMember(player);
    }

    //특정 플레이어가 party를 나가는데 활용되는 함수
    public void LeaveParty(Player player)
    {
        //GetParty 함수를 통해서 해당 player가 속한 party 객체를 가져온다.
        Party party = GetParty(player);
        //해당 플레이어가 속한 party가 존재하는 경우
        if(party != null)
        {
            //해당 party 객체의 RemoveMember 함수를 호출해서 제거를 수행하고
            party.RemoveMember(player);
            //만약 해당 파티 멤버 수가 0명이면
            if (party.Members.Count == 0)
                //활성화 파티 딕셔너리에서도 제거한다.
                activeParties.Remove(party.PartyID);
        }
    }

}
