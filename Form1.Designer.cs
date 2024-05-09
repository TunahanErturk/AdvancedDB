namespace AdvancedDB
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.numTypeAUsersCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.labelTypeAUsersCount = new System.Windows.Forms.Label();
            this.labelTypeBUsersCount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeAUsersCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(406, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnStartSimulation_Click);
            // 
            // numTypeAUsersCount
            // 
            this.numTypeAUsersCount.AccessibleName = "";
            this.numTypeAUsersCount.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.numTypeAUsersCount.Location = new System.Drawing.Point(216, 159);
            this.numTypeAUsersCount.Name = "numTypeAUsersCount";
            this.numTypeAUsersCount.Size = new System.Drawing.Size(120, 21);
            this.numTypeAUsersCount.TabIndex = 1;
            this.numTypeAUsersCount.Tag = "";
            this.numTypeAUsersCount.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(556, 159);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // labelTypeAUsersCount
            // 
            this.labelTypeAUsersCount.AutoSize = true;
            this.labelTypeAUsersCount.Location = new System.Drawing.Point(216, 140);
            this.labelTypeAUsersCount.Name = "labelTypeAUsersCount";
            this.labelTypeAUsersCount.Size = new System.Drawing.Size(38, 13);
            this.labelTypeAUsersCount.TabIndex = 3;
            this.labelTypeAUsersCount.Text = "TypeA";
            this.labelTypeAUsersCount.Click += new System.EventHandler(this.labelNumTypeA_Click);
            // 
            // labelTypeBUsersCount
            // 
            this.labelTypeBUsersCount.AutoSize = true;
            this.labelTypeBUsersCount.Location = new System.Drawing.Point(556, 140);
            this.labelTypeBUsersCount.Name = "labelTypeBUsersCount";
            this.labelTypeBUsersCount.Size = new System.Drawing.Size(38, 13);
            this.labelTypeBUsersCount.TabIndex = 4;
            this.labelTypeBUsersCount.Text = "TypeB";
            this.labelTypeBUsersCount.Click += new System.EventHandler(this.labelNumTypeB_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(-1, 1);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(255, 298);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(100, 23);
            this.progressBar2.TabIndex = 6;
            // 
            // Form1
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelTypeBUsersCount);
            this.Controls.Add(this.labelTypeAUsersCount);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numTypeAUsersCount);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numTypeAUsersCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numTypeAUsersCount;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label labelTypeAUsersCount;
        private System.Windows.Forms.Label labelTypeBUsersCount;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}

