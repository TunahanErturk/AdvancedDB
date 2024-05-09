using System;
using System.Data;
using System.Threading.Tasks; 

namespace AdvancedDB
{
    // IUser arabirimi
    public interface IUser
    {
        string ConnectionString { get; set; }
        IsolationLevel IsolationLevel { get; set; }


        // RunTransactions metodu ama void yerine Task döndürüyor
        Task RunTransactionsAsync();
    }
}
