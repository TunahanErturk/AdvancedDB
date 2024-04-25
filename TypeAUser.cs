using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedDB
{
    public class TypeAUser
    {
        private string connectionString; // Veritabanı bağlantı dizesi
        private int transactionsCount; // İşlem sayısı
        private IsolationLevel isolationLevel; // İzolasyon seviyesi

        public TypeAUser(string connectionString, int transactionsCount, IsolationLevel isolationLevel)
        {
            this.connectionString = connectionString;
            this.transactionsCount = transactionsCount;
            this.isolationLevel = isolationLevel;
        }

        public void RunTransactions()
        {
            for (int i = 0; i < transactionsCount; i++)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SET TRANSACTION ISOLATION LEVEL " + IsolationLevelToString(isolationLevel);
                        command.ExecuteNonQuery();

                        command.CommandText = "BEGIN TRANSACTION";
                        command.ExecuteNonQuery();

                        // Rastgele tarihler oluştur
                        Random random = new Random();
                        DateTime beginDate = new DateTime(2011, 1, 1).AddDays(random.Next(3650));
                        DateTime endDate = beginDate.AddYears(1);

                        // Güncelleme sorgusu
                        string updateQuery = "UPDATE Sales.SalesOrderDetail " +
                                             "SET UnitPrice = UnitPrice * 10.0 / 10.0 " +
                                             "WHERE UnitPrice > 100 " +
                                             "AND EXISTS (SELECT * FROM Sales.SalesOrderHeader " +
                                                         "WHERE Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderDetail.SalesOrderID " +
                                                         "AND Sales.SalesOrderHeader.OrderDate BETWEEN @BeginDate AND @EndDate " +
                                                         "AND Sales.SalesOrderHeader.OnlineOrderFlag = 1)";

                        command.CommandText = updateQuery;
                        command.Parameters.AddWithValue("@BeginDate", beginDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        // 50% olasılıkla sorguyu çalıştır
                        if (random.NextDouble() < 0.5)
                        {
                            command.ExecuteNonQuery();
                        }

                        command.CommandText = "COMMIT TRANSACTION";
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // Deadlock durumunda devam et
                    if (ex.Number == 1205)
                    {
                        Console.WriteLine("Deadlock occurred. Continuing...");
                    }
                    else
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        // ROLLBACK TRANSACTION if an error occurs
                       
      
                     }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private string IsolationLevelToString(IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case IsolationLevel.ReadUncommitted:
                    return "READ UNCOMMITTED";
                case IsolationLevel.ReadCommitted:
                    return "READ COMMITTED";
                case IsolationLevel.RepeatableRead:
                    return "REPEATABLE READ";
                case IsolationLevel.Serializable:
                    return "SERIALIZABLE";
                default:
                    throw new ArgumentException("Invalid isolation level.");
            }
        }
    }
}


