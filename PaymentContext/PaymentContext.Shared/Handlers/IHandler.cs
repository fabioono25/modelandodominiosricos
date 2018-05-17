using PaymentContext.Shared.Commands;

namespace PaymentContext.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        //command de entrada nao necessariamente o mesmo de saída (por isso o commandresult)
        ICommandResult Handle(T command);
    }
}