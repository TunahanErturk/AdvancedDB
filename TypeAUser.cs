using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AdvancedDB
{
    public class TypeAUser : IUser
    {
        private string connectionString;
        private int transactionsCount;
        private IsolationLevel isolationLevel;

        public TypeAUser() { }

        public TypeAUser(string connectionString, int transactionsCount, IsolationLevel isolationLevel)
        {
            this.connectionString = connectionString;
            this.transactionsCount = transactionsCount;
            this.isolationLevel = isolationLevel;
        }

        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public IsolationLevel IsolationLevel { get => isolationLevel; set => isolationLevel = value; }

        // IUser arabirimini uygulamak için RunTransactions metodu eklenmeli
        public async Task RunTransactionsAsync()
        {
            for (int i = 0; i < transactionsCount; i++)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlTransaction transaction = null;
                    try
                    {
                        connection.Open();

                        // İşlemi başlat
                        transaction = connection.BeginTransaction(isolationLevel);

                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;

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
                                await command.ExecuteNonQueryAsync();
                            }

                            // İşlemi tamamla
                            transaction.Commit();
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Hata durumunda işlemi geri al
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }

                        // Hata mesajını yazdır
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    finally
                    {
                        // Bağlantıyı kapat
                        connection.Close();
                    }
                }
            }
        }
    }
}
