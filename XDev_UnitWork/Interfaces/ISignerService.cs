
namespace XDev_UnitWork.Interfaces
{
	public interface ISignerService
	{
		Task<string> SignDocument(string pathCert, string key, string document);
	}
}
