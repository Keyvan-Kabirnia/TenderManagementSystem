using MediatR;

namespace Tms.Application.Common;

public interface IRequest<TResponse> : MediatR.IRequest<TResponse>
{
}