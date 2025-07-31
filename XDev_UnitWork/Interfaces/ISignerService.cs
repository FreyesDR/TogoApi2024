
using XDev_UnitWork.Services;

namespace XDev_UnitWork.Interfaces
{
	public interface ISignerService
	{
		Task<string> SignDocument(CertificadoMH cert, string key, string document);
	}
}
