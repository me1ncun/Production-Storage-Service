using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using Storage.Application.Services.Background.Interfaces;
using Contract = Storage.Core.Enitities.Contract;

namespace Storage.Application.Services.Background;

public class LoggingQueue : ILoggingQueue
{
    private readonly ConcurrentQueue<Contract> _contracts = new();

    public void Enqueue(Contract contract)
    {
        _contracts.Enqueue(contract);
    }

    public bool TryDequeue(out Contract contract)
    {
        return _contracts.TryDequeue(out contract);
    }
}