
namespace FSM
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAnt = new System.Windows.Forms.Button();
            this.chLeaf = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rtbHome = new System.Windows.Forms.RichTextBox();
            this.btnAddLeaf = new System.Windows.Forms.Button();
            this.btnAddAnt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAnt
            // 
            this.btnAnt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAnt.Location = new System.Drawing.Point(679, 475);
            this.btnAnt.Name = "btnAnt";
            this.btnAnt.Size = new System.Drawing.Size(23, 23);
            this.btnAnt.TabIndex = 4;
            this.btnAnt.Text = "button1";
            this.btnAnt.UseVisualStyleBackColor = true;
            this.btnAnt.Click += new System.EventHandler(this.BtnAnt_Click);
            // 
            // chLeaf
            // 
            this.chLeaf.AutoSize = true;
            this.chLeaf.Location = new System.Drawing.Point(1488, 371);
            this.chLeaf.Name = "chLeaf";
            this.chLeaf.Size = new System.Drawing.Size(15, 14);
            this.chLeaf.TabIndex = 1;
            this.chLeaf.UseVisualStyleBackColor = true;
            this.chLeaf.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChLeaf_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rtbHome
            // 
            this.rtbHome.Location = new System.Drawing.Point(1101, 531);
            this.rtbHome.Name = "rtbHome";
            this.rtbHome.Size = new System.Drawing.Size(100, 96);
            this.rtbHome.TabIndex = 2;
            this.rtbHome.Text = "";
            // 
            // btnAddLeaf
            // 
            this.btnAddLeaf.Location = new System.Drawing.Point(12, 12);
            this.btnAddLeaf.Name = "btnAddLeaf";
            this.btnAddLeaf.Size = new System.Drawing.Size(75, 23);
            this.btnAddLeaf.TabIndex = 15;
            this.btnAddLeaf.Text = "Add Leaf";
            this.btnAddLeaf.UseVisualStyleBackColor = true;
            this.btnAddLeaf.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddAnt
            // 
            this.btnAddAnt.Location = new System.Drawing.Point(93, 12);
            this.btnAddAnt.Name = "btnAddAnt";
            this.btnAddAnt.Size = new System.Drawing.Size(75, 23);
            this.btnAddAnt.TabIndex = 16;
            this.btnAddAnt.Text = "Add Ant";
            this.btnAddAnt.UseVisualStyleBackColor = true;
            this.btnAddAnt.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1810, 1059);
            this.Controls.Add(this.btnAddAnt);
            this.Controls.Add(this.btnAddLeaf);
            this.Controls.Add(this.chLeaf);
            this.Controls.Add(this.btnAnt);
            this.Controls.Add(this.rtbHome);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnt;
        private System.Windows.Forms.CheckBox chLeaf;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rtbHome;
        private System.Windows.Forms.Button btnAddLeaf;
        private System.Windows.Forms.Button btnAddAnt;
    }
}

