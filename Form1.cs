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


        private async Task StartSimulation(int typeAUsersCount, int typeBUsersCount)
        {
            

            try
            {
                progressBar1.Maximum = typeAUsersCount + typeBUsersCount;
                progressBar1.Value = 0;

                List<Task> tasks = new List<Task>();

                // Tip A kullanıcıları oluştur ve işlemleri başlat
                typeAUsers = new TypeAUser[typeAUsersCount];
                for (int i = 0; i < typeAUsersCount; i++)
                {
                    typeAUsers[i] = new TypeAUser(connectionString, transactionsCount, isolationLevel);
                    Task task = Task.Run(async () =>
                    {
                        try
                        {
                            await typeAUsers[i].RunTransactionsAsync();
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda yapılacak işlemler
                            Console.WriteLine($"Hata oluştu: {ex.Message}");
                            // İsterseniz burada hata ile ilgili kullanıcı arayüzünde bir mesaj gösterebilirsiniz
                        }
                    });
                    tasks.Add(task);
                    progressBar1.Value++;
                }

                // Tip B kullanıcıları oluştur ve işlemleri başlat
                TypeBUser[] typeBUsers = new TypeBUser[typeBUsersCount];

                for (int i = 0; i < typeBUsersCount; i++)
                {
                    typeBUsers[i] = new TypeBUser(connectionString, transactionsCount, isolationLevel);
                    Task task = Task.Run(() => typeBUsers[i].RunTransactionsAsync());
                    tasks.Add(task);
                    progressBar1.Value++;
                }

                // Tüm görevlerin tamamlanmasını bekleyin
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
         {
             // Form yüklendiğinde simülasyonu başlat
             await StartSimulation(typeAUsersCount, typeBUsersCount);
             // Label kontrolüne isimleri atama
             labelTypeAUsersCount.Text = "Type A Users: " + typeAUsersCount.ToString();
             labelTypeBUsersCount.Text = "Type B Users: " + typeBUsersCount.ToString();
         }
        

        private async void btnStartSimulation_Click(object sender, EventArgs e)
        {
            try
            {
                int typeAUsersCount = (int)numTypeAUsersCount.Value;
                int typeBUsersCount = (int)numericUpDown2.Value;

                await StartSimulation(typeAUsersCount, typeBUsersCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            typeAUsersCount = (int)numTypeAUsersCount.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            typeBUsersCount = (int)numericUpDown2.Value;
        }

        private void labelNumTypeA_Click(object sender, EventArgs e)
        {

        }

        private void labelNumTypeB_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}

