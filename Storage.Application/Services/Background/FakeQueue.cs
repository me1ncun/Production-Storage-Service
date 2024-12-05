using System.Diagnostics.Contracts;
using Contract = Storage.Core.Enitities.Contract;

namespace Storage.Application.Services.Background;

public static class FakeQueue
{
    private static readonly Queue<Contract> _contracts = new();
    public static Queue<Contract> Contracts => _contracts;

    public static void Add(Contract contract)
    {
        _contracts.Enqueue(contract);
    }

    public static void Dequeue()
    {
        _contracts.Dequeue();
    }

}