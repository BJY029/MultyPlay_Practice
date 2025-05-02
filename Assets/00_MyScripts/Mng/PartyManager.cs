using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviourPunCallbacks
{
    //���� Ȱ��ȭ �� ��Ƽ���� �� ID�� ������ ��ųʸ�
    private Dictionary<int, Party> activeParties = new Dictionary<int, Party>();

    //� ����ڿ� ���ؼ�, �ش� ����ڰ� ��Ƽ�� �����ִ��� Ȯ���ϴ� �Լ�
    public bool HasParty(Player player)
    {
        //Ȱ��ȭ �� ��Ƽ ��ųʸ��� Value�� ���鼭
        foreach(var party in activeParties.Values)
        {
            //�� ��Ƽ���� �ش�Ǵ� player�� Member�� ���ϸ� true ��ȯ
            if(party.IsMember(player))
                return true;
        }
        //�ش� �÷��̾ ��𿡵� ������ ���� ��� false ��ȯ
        return false;
    }

    //Ư�� �÷��̾ ���� party ��ü�� ��ȯ���ִ� �Լ�
    public Party GetParty(Player player)
    {
        //Ȱ��ȭ�� party ��ųʸ��� Value�� ���鼭
        foreach(var party in activeParties.Values)
        {
            //�� ��Ƽ���� �ش� player�� ����� �����ϴ��� �˻�
            if(party.IsMember(player))
                //�����ϸ� �ش� party ��ü ��ȯ
                return party;
        }
        //�ش� �÷��̾ ���� party�� �������� ������ null ��ȯ
        return null;
    }

    //��Ƽ�� �����ϰ� �ش� ��Ƽ ��ü�� ��ȯ�ϴ� �Լ�
    //��Ƽ ������ ��û�ϴ� ����� �� ��Ƽ�� leader�� �ȴ�.
    public Party CreateParty(Player leader)
    {
        //�ش� �÷��̾ ���� ��Ƽ�� �����ϴ� ���, ��Ƽ�� �������� �ʰ� null ��ȯ
        if (HasParty(leader)) return null;

        //���� Ȱ��ȭ�� ��Ƽ ���� + 1 �� partyId�� ����
        int partyID = activeParties.Count + 1;
        //���ο� Party�� �����ڷ� ����
        Party newParty = new Party(partyID, leader);
        //������ Party ��ü�� ��ųʸ����� ����
        activeParties.Add(partyID, newParty);

        //�ش� ��Ƽ ��ü ��ȯ
        return newParty;
    }


    //��Ƽ�� �����ϴ� �Լ�
    public bool JoinParty(Player player, int partyID)
    {
        //�ش� �÷��̾ ���� ��Ƽ�� �����ϰų�, ��û�� ��Ƽ ID�� ���� �������� �ʴ°��
        //false ��ȯ
        if(HasParty(player) || !activeParties.ContainsKey(partyID)) 
            return false;

        //�ش� ��Ƽ ��ü�� AddMember �Լ��� ���ؼ� ��Ƽ ����
        //(�ش� �Լ��� ��Ƽ ������ ���������� �Ǹ� true ��ȯ)
        return activeParties[partyID].AddMember(player);
    }

    //Ư�� �÷��̾ party�� �����µ� Ȱ��Ǵ� �Լ�
    public void LeaveParty(Player player)
    {
        //GetParty �Լ��� ���ؼ� �ش� player�� ���� party ��ü�� �����´�.
        Party party = GetParty(player);
        //�ش� �÷��̾ ���� party�� �����ϴ� ���
        if(party != null)
        {
            //�ش� party ��ü�� RemoveMember �Լ��� ȣ���ؼ� ���Ÿ� �����ϰ�
            party.RemoveMember(player);
            //���� �ش� ��Ƽ ��� ���� 0���̸�
            if (party.Members.Count == 0)
                //Ȱ��ȭ ��Ƽ ��ųʸ������� �����Ѵ�.
                activeParties.Remove(party.PartyID);
        }
    }

}
