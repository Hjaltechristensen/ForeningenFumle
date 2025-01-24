using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Server.Repositories.MemberRepository
{
	public interface IMemberRepository
	{
		List<Member> GetAllMembers();
		Member FindMember(string username);
		void AddMember(Member member);
		bool DeleteMember(int id);
		bool UpdateMember(Member member);
	}
}
