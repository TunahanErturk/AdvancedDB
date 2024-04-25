using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedDB
{
    public partial class Form1 : Form
    {
        

        private string connectionString = "Server=2NA\\SQLEXPRESS;Database=AdventureWorks2019;Trusted_Connection=True;"; // Veritabanı bağlantı dizesi
        private int typeAUsersCount = 5; // Tip A kullanıcı sayısı
        private int typeBUsersCount = 8; // Tip B kullanıcı sayısı
        private int transactionsCount = 100; // İşlem sayısı
        private IsolationLevel isolationLevel = IsolationLevel.ReadCommitted; // İzolasyon seviyesi
        private TypeAUser[] typeAUsers;
        private TypeBUser[] typeBUsers;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde simülasyonu başlat
            
        }


        private async Task StartSimulation()
        {
            List<Task> tasks = new List<Task>();

            // Tip A kullanıcıları oluştur ve işlemleri başlat
            typeAUsers = new TypeAUser[typeAUsersCount];
            for (int i = 0; i < typeAUsersCount; i++)
            {
                typeAUsers[i] = new TypeAUser(connectionString, transactionsCount, isolationLevel);
                Task task = Task.Run(() => typeAUsers[i-1].RunTransactions());
                tasks.Add(task);
            }

            // Tip B kullanıcıları oluştur ve işlemleri başlat
            typeBUsers = new TypeBUser[typeBUsersCount];
            for (int i = 0; i < typeBUsersCount; i++)
            {
                typeBUsers[i] = new TypeBUser(connectionString, transactionsCount, isolationLevel);
                Task task = Task.Run(() => typeBUsers[i-1].RunTransactions());
                tasks.Add(task);
            }

            // Tüm görevlerin tamamlanmasını bekleyin
            await Task.WhenAll(tasks);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            StartSimulation();
        }
    }
}

