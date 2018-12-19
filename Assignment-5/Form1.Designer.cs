namespace Assignment_5
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
			this.selectLevelCB = new System.Windows.Forms.ComboBox();
			this.newGameBtn = new System.Windows.Forms.Button();
			this.progressBtn = new System.Windows.Forms.Button();
			this.SaveBtn = new System.Windows.Forms.Button();
			this.loadBtn = new System.Windows.Forms.Button();
			this.pauseBtn = new System.Windows.Forms.Button();
			this.resetBtn = new System.Windows.Forms.Button();
			this.messagesRTB = new System.Windows.Forms.RichTextBox();
			this.panel = new System.Windows.Forms.Panel();
			this.statusLB = new System.Windows.Forms.Label();
			this.helpBtn = new System.Windows.Forms.Button();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// selectLevelCB
			// 
			this.selectLevelCB.FormattingEnabled = true;
			this.selectLevelCB.Items.AddRange(new object[] {
            "Easy",
            "Medium",
            "Hard"});
			this.selectLevelCB.Location = new System.Drawing.Point(12, 306);
			this.selectLevelCB.Name = "selectLevelCB";
			this.selectLevelCB.Size = new System.Drawing.Size(107, 21);
			this.selectLevelCB.TabIndex = 1;
			this.selectLevelCB.Text = "Select Level";
			this.selectLevelCB.SelectedIndexChanged += new System.EventHandler(this.selectLevelCB_SelectedIndexChanged);
			// 
			// newGameBtn
			// 
			this.newGameBtn.Location = new System.Drawing.Point(129, 306);
			this.newGameBtn.Name = "newGameBtn";
			this.newGameBtn.Size = new System.Drawing.Size(75, 23);
			this.newGameBtn.TabIndex = 2;
			this.newGameBtn.Text = "New Game";
			this.newGameBtn.UseVisualStyleBackColor = true;
			this.newGameBtn.Click += new System.EventHandler(this.newGameBtn_Click);
			// 
			// progressBtn
			// 
			this.progressBtn.Location = new System.Drawing.Point(13, 335);
			this.progressBtn.Name = "progressBtn";
			this.progressBtn.Size = new System.Drawing.Size(106, 23);
			this.progressBtn.TabIndex = 3;
			this.progressBtn.Text = "Verify Progress";
			this.progressBtn.UseVisualStyleBackColor = true;
			this.progressBtn.Click += new System.EventHandler(this.progressBtn_Click);
			// 
			// SaveBtn
			// 
			this.SaveBtn.Location = new System.Drawing.Point(291, 306);
			this.SaveBtn.Name = "SaveBtn";
			this.SaveBtn.Size = new System.Drawing.Size(75, 23);
			this.SaveBtn.TabIndex = 4;
			this.SaveBtn.Text = "Save";
			this.SaveBtn.UseVisualStyleBackColor = true;
			this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
			// 
			// loadBtn
			// 
			this.loadBtn.Location = new System.Drawing.Point(210, 306);
			this.loadBtn.Name = "loadBtn";
			this.loadBtn.Size = new System.Drawing.Size(75, 23);
			this.loadBtn.TabIndex = 5;
			this.loadBtn.Text = "Load";
			this.loadBtn.UseVisualStyleBackColor = true;
			this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
			// 
			// pauseBtn
			// 
			this.pauseBtn.Location = new System.Drawing.Point(210, 335);
			this.pauseBtn.Name = "pauseBtn";
			this.pauseBtn.Size = new System.Drawing.Size(75, 23);
			this.pauseBtn.TabIndex = 7;
			this.pauseBtn.Text = "Pause";
			this.pauseBtn.UseVisualStyleBackColor = true;
			this.pauseBtn.Click += new System.EventHandler(this.pauseBtn_Click);
			// 
			// resetBtn
			// 
			this.resetBtn.Location = new System.Drawing.Point(291, 336);
			this.resetBtn.Name = "resetBtn";
			this.resetBtn.Size = new System.Drawing.Size(75, 23);
			this.resetBtn.TabIndex = 8;
			this.resetBtn.Text = "Reset";
			this.resetBtn.UseVisualStyleBackColor = true;
			this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
			// 
			// messagesRTB
			// 
			this.messagesRTB.Location = new System.Drawing.Point(13, 365);
			this.messagesRTB.Name = "messagesRTB";
			this.messagesRTB.Size = new System.Drawing.Size(353, 35);
			this.messagesRTB.TabIndex = 10;
			this.messagesRTB.Text = "";
			// 
			// panel
			// 
			this.panel.Controls.Add(this.statusLB);
			this.panel.Location = new System.Drawing.Point(12, 12);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(358, 278);
			this.panel.TabIndex = 11;
			// 
			// statusLB
			// 
			this.statusLB.AutoSize = true;
			this.statusLB.Location = new System.Drawing.Point(157, 117);
			this.statusLB.Name = "statusLB";
			this.statusLB.Size = new System.Drawing.Size(0, 13);
			this.statusLB.TabIndex = 0;
			// 
			// helpBtn
			// 
			this.helpBtn.Location = new System.Drawing.Point(129, 335);
			this.helpBtn.Name = "helpBtn";
			this.helpBtn.Size = new System.Drawing.Size(75, 23);
			this.helpBtn.TabIndex = 12;
			this.helpBtn.Text = "Help";
			this.helpBtn.UseVisualStyleBackColor = true;
			this.helpBtn.Click += new System.EventHandler(this.helpBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(376, 408);
			this.Controls.Add(this.helpBtn);
			this.Controls.Add(this.panel);
			this.Controls.Add(this.messagesRTB);
			this.Controls.Add(this.resetBtn);
			this.Controls.Add(this.pauseBtn);
			this.Controls.Add(this.loadBtn);
			this.Controls.Add(this.SaveBtn);
			this.Controls.Add(this.progressBtn);
			this.Controls.Add(this.newGameBtn);
			this.Controls.Add(this.selectLevelCB);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel.ResumeLayout(false);
			this.panel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox selectLevelCB;
		private System.Windows.Forms.Button newGameBtn;
		private System.Windows.Forms.Button progressBtn;
		private System.Windows.Forms.Button SaveBtn;
		private System.Windows.Forms.Button loadBtn;
		private System.Windows.Forms.Button pauseBtn;
		private System.Windows.Forms.Button resetBtn;
		private System.Windows.Forms.RichTextBox messagesRTB;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Button helpBtn;
		private System.Windows.Forms.Label statusLB;
	}
}

