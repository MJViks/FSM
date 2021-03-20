
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
            this.SuspendLayout();
            // 
            // btnAnt
            // 
            this.btnAnt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAnt.Location = new System.Drawing.Point(222, 175);
            this.btnAnt.Name = "btnAnt";
            this.btnAnt.Size = new System.Drawing.Size(23, 23);
            this.btnAnt.TabIndex = 0;
            this.btnAnt.Text = "button1";
            this.btnAnt.UseVisualStyleBackColor = true;
            this.btnAnt.Click += new System.EventHandler(this.btnAnt_Click);
            // 
            // chLeaf
            // 
            this.chLeaf.AutoSize = true;
            this.chLeaf.Location = new System.Drawing.Point(500, 196);
            this.chLeaf.Name = "chLeaf";
            this.chLeaf.Size = new System.Drawing.Size(15, 14);
            this.chLeaf.TabIndex = 1;
            this.chLeaf.UseVisualStyleBackColor = true;
            this.chLeaf.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chLeaf_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chLeaf);
            this.Controls.Add(this.btnAnt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnt;
        private System.Windows.Forms.CheckBox chLeaf;
        private System.Windows.Forms.Timer timer1;
    }
}

