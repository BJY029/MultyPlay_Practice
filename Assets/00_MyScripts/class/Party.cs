using UnityEngine;
using System.Collections.Generic;
using Photon.Realtime;

public class Party
{
	//�ش� ��Ƽ�� ID ��
	public int PartyID { get; private set; }
	//�ش� ��Ƽ�� ��Ƽ��
	public Player Leader { get; private set; }
	//�ش� ��Ƽ�� ���� ���
	public List<Player> Members { get; private set; }

	//������
	public Party(int partyID, Player leader)
	{
		PartyID = partyID;
		Leader = leader;
		Members = new List<Player>() { leader };
	}

	//��Ƽ�� Member�� �߰��ϴ� �Լ�
	public bool AddMember(Player player)
	{
		//��Ƽ ����Ʈ�� �ش� ����� �������� ������
		if(!Members.Contains(player))
		{
			//�ش� ����� ����Ʈ�� �߰��Ѵ�.
			Members.Add(player);
			return true;
		}
		//�̹� �����ϹǷ� �߰����� �ʰ� false ��ȯ
		return false;
	}

	//��Ƽ�� Member�� �����ϴ� �Լ�
	public bool RemoveMember(Player player)
	{
		//��Ƽ ����Ʈ�� �ش� ����� �����ϴ� ���
		if (Members.Contains(player))
		{
			//�ش� ����� ����Ʈ���� ������Ű��
			Members.Remove(player);
			//���� ������ �÷��̾ Leader����, ���� ��Ƽ ���� ����� �����ϴ� ���
			if(player == Leader && Members.Count > 0)
			{
				//��Ƽ ����Ʈ�� ���� ù��°�� ��ġ�� ������� Leader �ο�
				Leader = Members[0];
			}
			return true;
		}
		//��Ƽ ����Ʈ�� �ش� ����� �������� �����Ƿ�, false ��ȯ
		return false;
	}

	//�ش�Ǵ� ����� ��Ƽ ������� Ȯ���ϴ� �Լ�
	public bool IsMember(Player player)
	{
		return Members.Contains(player);
	}

	//��Ƽ ��ü �Լ�
	public void DisbandParty()
	{
		Members.Clear();
	}
}
