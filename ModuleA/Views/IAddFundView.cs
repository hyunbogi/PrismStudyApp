using System;

namespace ModuleA.Views
{
    public interface IAddFundView
    {
        event EventHandler AddFund;
        string Customer { get; }
        string Fund { get; }
    }
}
