using ForeningenFumle.Shared.Models;

namespace ForeningenFumle.Client.Services.MemberServices
{
	public interface IMemberService
	{
		Task<Member[]?> GetAllMembers();

		Task<Member?> GetMember(string username);

		Task<int> AddMember(RegisterModel member);

		Task<int> DeleteMember(int id);

		Task<int> UpdateMember(Member member);
	}
}
