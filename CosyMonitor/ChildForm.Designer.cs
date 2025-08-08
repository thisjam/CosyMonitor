namespace CosyMonitor
{
    partial class ChildForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbMemoryT = new Label();
            lbMemoryTips = new Label();
            lbMemoryM = new Label();
            lbGpuT = new Label();
            lbGpuTips = new Label();
            lbGpuM = new Label();
            lbCpuT = new Label();
            lbCpuTips = new Label();
            lbCpuM = new Label();
            lbGpuRam = new Label();
            lbGpuRamTotal = new Label();
            SuspendLayout();
            // 
            // lbMemoryT
            // 
            lbMemoryT.AutoSize = true;
            lbMemoryT.Location = new Point(446, 19);
            lbMemoryT.Name = "lbMemoryT";
            lbMemoryT.Size = new Size(43, 17);
            lbMemoryT.TabIndex = 17;
            lbMemoryT.Text = "label1";
            // 
            // lbMemoryTips
            // 
            lbMemoryTips.AutoSize = true;
            lbMemoryTips.Location = new Point(347, 19);
            lbMemoryTips.Name = "lbMemoryTips";
            lbMemoryTips.Size = new Size(44, 17);
            lbMemoryTips.TabIndex = 16;
            lbMemoryTips.Text = "内存：";
            // 
            // lbMemoryM
            // 
            lbMemoryM.AutoSize = true;
            lbMemoryM.Location = new Point(397, 19);
            lbMemoryM.Name = "lbMemoryM";
            lbMemoryM.Size = new Size(43, 17);
            lbMemoryM.TabIndex = 15;
            lbMemoryM.Text = "label1";
            // 
            // lbGpuT
            // 
            lbGpuT.AutoSize = true;
            lbGpuT.Location = new Point(297, 19);
            lbGpuT.Name = "lbGpuT";
            lbGpuT.Size = new Size(43, 17);
            lbGpuT.TabIndex = 14;
            lbGpuT.Text = "label1";
            // 
            // lbGpuTips
            // 
            lbGpuTips.AutoSize = true;
            lbGpuTips.Location = new Point(167, 19);
            lbGpuTips.Name = "lbGpuTips";
            lbGpuTips.Size = new Size(45, 17);
            lbGpuTips.TabIndex = 13;
            lbGpuTips.Text = "GPU：";
            // 
            // lbGpuM
            // 
            lbGpuM.AutoSize = true;
            lbGpuM.Location = new Point(206, 154);
            lbGpuM.Name = "lbGpuM";
            lbGpuM.Size = new Size(43, 17);
            lbGpuM.TabIndex = 12;
            lbGpuM.Text = "label1";
            // 
            // lbCpuT
            // 
            lbCpuT.AutoSize = true;
            lbCpuT.Location = new Point(107, 19);
            lbCpuT.Name = "lbCpuT";
            lbCpuT.Size = new Size(43, 17);
            lbCpuT.TabIndex = 11;
            lbCpuT.Text = "label1";
            // 
            // lbCpuTips
            // 
            lbCpuTips.AutoSize = true;
            lbCpuTips.ForeColor = Color.IndianRed;
            lbCpuTips.Location = new Point(20, 19);
            lbCpuTips.Name = "lbCpuTips";
            lbCpuTips.Size = new Size(44, 17);
            lbCpuTips.TabIndex = 10;
            lbCpuTips.Text = "CPU：";
            // 
            // lbCpuM
            // 
            lbCpuM.AutoSize = true;
            lbCpuM.Location = new Point(70, 19);
            lbCpuM.Name = "lbCpuM";
            lbCpuM.Size = new Size(43, 17);
            lbCpuM.TabIndex = 9;
            lbCpuM.Text = "label1";
            // 
            // lbGpuRam
            // 
            lbGpuRam.AutoSize = true;
            lbGpuRam.Location = new Point(199, 19);
            lbGpuRam.Name = "lbGpuRam";
            lbGpuRam.Size = new Size(22, 17);
            lbGpuRam.TabIndex = 18;
            lbGpuRam.Text = "10";
            // 
            // lbGpuRamTotal
            // 
            lbGpuRamTotal.AutoSize = true;
            lbGpuRamTotal.Location = new Point(227, 19);
            lbGpuRamTotal.Name = "lbGpuRamTotal";
            lbGpuRamTotal.Size = new Size(22, 17);
            lbGpuRamTotal.TabIndex = 19;
            lbGpuRamTotal.Text = "24";
            // 
            // ChildForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(697, 214);
            Controls.Add(lbGpuRamTotal);
            Controls.Add(lbGpuRam);
            Controls.Add(lbMemoryT);
            Controls.Add(lbMemoryTips);
            Controls.Add(lbMemoryM);
            Controls.Add(lbGpuT);
            Controls.Add(lbGpuTips);
            Controls.Add(lbGpuM);
            Controls.Add(lbCpuT);
            Controls.Add(lbCpuTips);
            Controls.Add(lbCpuM);
            Name = "ChildForm";
            Text = "ChildForm";
            Load += ChildForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbMemoryT;
        private Label lbMemoryTips;
        private Label lbMemoryM;
        private Label lbGpuT;
        private Label lbGpuTips;
        private Label lbGpuM;
        private Label lbCpuT;
        private Label lbCpuTips;
        private Label lbCpuM;
        private Label lbGpuRam;
        private Label lbGpuRamTotal;
    }
}