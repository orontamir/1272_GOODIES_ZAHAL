namespace _1272_GOODIES_ZAHAL
{
    partial class Form1
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
            this.LogtextBox = new System.Windows.Forms.TextBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.OpenLogBtn = new System.Windows.Forms.Button();
            this.OpenLogFolderBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LogtextBox
            // 
            this.LogtextBox.Location = new System.Drawing.Point(13, 13);
            this.LogtextBox.Multiline = true;
            this.LogtextBox.Name = "LogtextBox";
            this.LogtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogtextBox.Size = new System.Drawing.Size(775, 381);
            this.LogtextBox.TabIndex = 0;
            // 
            // StartBtn
            // 
            this.StartBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.StartBtn.Location = new System.Drawing.Point(681, 412);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(106, 45);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.StopBtn.Location = new System.Drawing.Point(556, 412);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(104, 45);
            this.StopBtn.TabIndex = 2;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // OpenLogBtn
            // 
            this.OpenLogBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OpenLogBtn.Location = new System.Drawing.Point(423, 412);
            this.OpenLogBtn.Name = "OpenLogBtn";
            this.OpenLogBtn.Size = new System.Drawing.Size(108, 45);
            this.OpenLogBtn.TabIndex = 3;
            this.OpenLogBtn.Text = "Open Log";
            this.OpenLogBtn.UseVisualStyleBackColor = true;
            this.OpenLogBtn.Click += new System.EventHandler(this.OpenLogBtn_Click);
            // 
            // OpenLogFolderBtn
            // 
            this.OpenLogFolderBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OpenLogFolderBtn.Location = new System.Drawing.Point(247, 412);
            this.OpenLogFolderBtn.Name = "OpenLogFolderBtn";
            this.OpenLogFolderBtn.Size = new System.Drawing.Size(156, 45);
            this.OpenLogFolderBtn.TabIndex = 4;
            this.OpenLogFolderBtn.Text = "Open Log Folder";
            this.OpenLogFolderBtn.UseVisualStyleBackColor = true;
            this.OpenLogFolderBtn.Click += new System.EventHandler(this.OpenLogFolderBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 479);
            this.Controls.Add(this.OpenLogFolderBtn);
            this.Controls.Add(this.OpenLogBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.LogtextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogtextBox;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button OpenLogBtn;
        private System.Windows.Forms.Button OpenLogFolderBtn;
    }
}

