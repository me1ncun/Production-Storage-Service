using Storage.Core.Enitities;

namespace Storage.Application.Services.Background.Interfaces;

public interface ILoggingQueue
{
    void Enqueue(Contract contract);
    bool TryDequeue(out Contract contract);
}