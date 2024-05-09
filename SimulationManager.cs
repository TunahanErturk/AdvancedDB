using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using AdvancedDB; // IndexManager, PerformanceAnalyzer, TypeAUser ve TypeBUser sınıflarını içerir

/*
namespace AdvancedDB
{
    public class SimulationManager
    {
        private string connectionString;
        private int typeAUsersCount;
        private int typeBUsersCount;
        private IsolationLevel isolationLevel;
        private bool indexesEnabled;

        public SimulationManager(string connectionString, int typeAUsersCount, int typeBUsersCount, IsolationLevel isolationLevel, bool indexesEnabled)
        {
            this.connectionString = connectionString;
            this.typeAUsersCount = typeAUsersCount;
            this.typeBUsersCount = typeBUsersCount;
            this.isolationLevel = isolationLevel;
            this.indexesEnabled = indexesEnabled;
        }

        public void RunSimulation()
        {
            // 1. İndeksleri kaldır
            RemoveIndexes();

            // 2. Tip A ve Tip B kullanıcılarını oluştur ve işlemleri başlat
            TypeAUser[] typeAUsers = CreateUserArray<TypeAUser>(typeAUsersCount);
            TypeBUser[] typeBUsers = CreateUserArray<TypeBUser>(typeBUsersCount);

            // 3. İşlem tiplerini ve yapısını uygula
            ApplyTransactionStructure(typeAUsers, "Type A");
            ApplyTransactionStructure(typeBUsers, "Type B");

            // 4. İndeksleri oluştur
            CreateIndexes();

            // 5. Ölçümleri yap ve raporla
            MeasureAndReportPerformance(typeAUsers, typeBUsers);
        }


        private void RemoveIndexes()
        {
            // İndeksleri kaldır
            IndexManager indexManager = new IndexManager(connectionString);
            indexManager.RemoveIndexes();
        }

        private void CreateIndexes()
        {
            // İndeksleri oluştur
            IndexManager indexManager = new IndexManager(connectionString);
            indexManager.CreateIndexes();
        }

        private T[] CreateUserArray<T>(int count) where T : IUser, new()
        {
            T[] users = new T[count];
            for (int i = 0; i < count; i++)
            {
                users[i] = new T();
                users[i].ConnectionString = connectionString;
                users[i].IsolationLevel = isolationLevel;
            }
            return users;
        }

        private void ApplyTransactionStructure(IUser[] users, string userType)
        {
            foreach (var user in users)
            {
                user.RunTransactions();
            }
            Console.WriteLine($"{userType} kullanıcı işlemleri tamamlandı.");
        }

        private void MeasureAndReportPerformance(TypeAUser[] typeAUsers, TypeBUser[] typeBUsers)
        {
            PerformanceAnalyzer performanceAnalyzer = new PerformanceAnalyzer();

            // Performans ölçümlerini ve raporlamayı yap
            performanceAnalyzer.MeasurePerformance(typeAUsers, typeBUsers, indexesEnabled);
        }

    }
}
*/

namespace AdvancedDB
{
    public class SimulationManager
    {
        private string connectionString;
        private int typeAUsersCount;
        private int typeBUsersCount;
        private IsolationLevel isolationLevel;
        private bool indexesEnabled;

        public SimulationManager(string connectionString, int typeAUsersCount, int typeBUsersCount, IsolationLevel isolationLevel, bool indexesEnabled)
        {
            this.connectionString = connectionString;
            this.typeAUsersCount = typeAUsersCount;
            this.typeBUsersCount = typeBUsersCount;
            this.isolationLevel = isolationLevel;
            this.indexesEnabled = indexesEnabled;
        }

        public void RunSimulation()
        {
            try
            {
                // 1. İndeksleri kaldır
                RemoveIndexes();

                // 2. Tip A ve Tip B kullanıcılarını oluştur ve işlemleri başlat
                TypeAUser[] typeAUsers = CreateUserArray<TypeAUser>(typeAUsersCount);
                TypeBUser[] typeBUsers = CreateUserArray<TypeBUser>(typeBUsersCount);

                // 3. İşlem tiplerini ve yapısını uygula
                ApplyTransactionStructure(typeAUsers, "Type A");
                ApplyTransactionStructure(typeBUsers, "Type B");

                // 4. İndeksleri oluştur
                CreateIndexes();

                // 5. Ölçümleri yap ve raporla
                MeasureAndReportPerformance(typeAUsers, typeBUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during simulation: " + ex.Message);
            }
        }

        private void RemoveIndexes()
        {
            using (IndexManager indexManager = new IndexManager(connectionString))
            {
                indexManager.RemoveIndexes();
            }
        }

        private void CreateIndexes()
        {
            using (IndexManager indexManager = new IndexManager(connectionString))
            {
                indexManager.CreateIndexes();
            }
        }

        private T[] CreateUserArray<T>(int count) where T : IUser, new()
        {
            T[] users = new T[count];
            for (int i = 0; i < count; i++)
            {
                users[i] = new T();
                users[i].ConnectionString = connectionString;
                users[i].IsolationLevel = isolationLevel;
            }
            return users;
        }

        private void ApplyTransactionStructure(IUser[] users, string userType)
        {
            foreach (var user in users)
            {
                user.RunTransactionsAsync();
            }
            Console.WriteLine($"{userType} user transactions completed.");
        }

        private void MeasureAndReportPerformance(TypeAUser[] typeAUsers, TypeBUser[] typeBUsers)
        {
            PerformanceAnalyzer performanceAnalyzer = new PerformanceAnalyzer();

            // Perform performance measurements and reporting
            performanceAnalyzer.MeasurePerformance(typeAUsers, typeBUsers, indexesEnabled);
        }
    }
}