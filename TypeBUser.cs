using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AdvancedDB
{
    public class TypeBUser : IUser
    {
        private string connectionString;
        private int transactionsCount;
        private IsolationLevel isolationLevel;

        public TypeBUser() { }

        public TypeBUser(string connectionString, int transactionsCount, IsolationLevel isolationLevel)
        {
            this.connectionString = connectionString;
            this.transactionsCount = transactionsCount;
            this.isolationLevel = isolationLevel;
        }

        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public IsolationLevel IsolationLevel { get => isolationLevel; set => isolationLevel = value; }

        // IUser arabirimini uygulamak için RunTransactionsAsync metodu eklenmeli
        public async Task RunTransactionsAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Random random = new Random();

                for (int i = 0; i < transactionsCount; i++)
                {
                    SqlTransaction transaction = null;

                    try
                    {
                        connection.Open();

                        transaction = connection.BeginTransaction(isolationLevel);
                        {
                            using (SqlCommand command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;

                                // Rastgele tarihler oluştur
                                DateTime beginDate = new DateTime(2011, 1, 1).AddDays(random.Next(3650));
                                DateTime endDate = beginDate.AddYears(1);

                                // Seçim sorgusu
                                string selectQuery = "SELECT SUM(Sales.SalesOrderDetail.OrderQty) " +
                                                     "FROM Sales.SalesOrderDetail " +
                                                     "WHERE UnitPrice > 100 " +
                                                     "AND EXISTS (SELECT * FROM Sales.SalesOrderHeader " +
                                                                 "WHERE Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderDetail.SalesOrderID " +
                                                                 "AND Sales.SalesOrderHeader.OrderDate BETWEEN @BeginDate AND @EndDate " +
                                                                 "AND Sales.SalesOrderHeader.OnlineOrderFlag = 1)";

                                command.CommandText = selectQuery;
                                command.Parameters.AddWithValue("@BeginDate", beginDate);
                                command.Parameters.AddWithValue("@EndDate", endDate);

                                // 50% olasılıkla sorguyu çalıştır
                                if (random.NextDouble() < 0.5)
                                {
                                    object result = command.ExecuteScalar();
                                    Console.WriteLine("Result: " + result);
                                }

                                transaction.Commit();
                            }
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
                            // Rollback the transaction in case of any error
                            transaction.Rollback();
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
