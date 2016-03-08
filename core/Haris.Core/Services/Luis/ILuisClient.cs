using System.Threading;
using System.Threading.Tasks;
using Haris.DataModel.Luis;

namespace Haris.Core.Services.Luis
{
	public interface ILuisClient
	{
		Task<LuisResponseDto> AskLuis(string command, CancellationToken ct);
	}
}