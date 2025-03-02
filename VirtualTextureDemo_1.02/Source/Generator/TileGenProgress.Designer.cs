namespace VirtualTextureDemo
{
	partial class TileGenProgress
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.progress = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.b_close = new System.Windows.Forms.Button();
			this.b_cancel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.l_pagevalue = new System.Windows.Forms.Label();
			this.l_elapsed = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progress
			// 
			this.progress.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.progress.Location = new System.Drawing.Point( 12, 34 );
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size( 585, 23 );
			this.progress.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 12, 16 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 35, 13 );
			this.label1.TabIndex = 1;
			this.label1.Text = "Page:";
			// 
			// b_close
			// 
			this.b_close.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.b_close.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.b_close.Enabled = false;
			this.b_close.Location = new System.Drawing.Point( 522, 69 );
			this.b_close.Name = "b_close";
			this.b_close.Size = new System.Drawing.Size( 75, 23 );
			this.b_close.TabIndex = 3;
			this.b_close.Text = "Close";
			this.b_close.UseVisualStyleBackColor = true;
			// 
			// b_cancel
			// 
			this.b_cancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.b_cancel.Location = new System.Drawing.Point( 441, 69 );
			this.b_cancel.Name = "b_cancel";
			this.b_cancel.Size = new System.Drawing.Size( 75, 23 );
			this.b_cancel.TabIndex = 3;
			this.b_cancel.Text = "Cancel";
			this.b_cancel.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point( 12, 75 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size( 74, 13 );
			this.label3.TabIndex = 4;
			this.label3.Text = "Elapsed Time:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// l_pagevalue
			// 
			this.l_pagevalue.AutoSize = true;
			this.l_pagevalue.Location = new System.Drawing.Point( 45, 16 );
			this.l_pagevalue.Name = "l_pagevalue";
			this.l_pagevalue.Size = new System.Drawing.Size( 75, 13 );
			this.l_pagevalue.TabIndex = 5;
			this.l_pagevalue.Text = "0 of 1000 (0%)";
			// 
			// l_elapsed
			// 
			this.l_elapsed.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.l_elapsed.AutoSize = true;
			this.l_elapsed.Location = new System.Drawing.Point( 107, 75 );
			this.l_elapsed.Name = "l_elapsed";
			this.l_elapsed.Size = new System.Drawing.Size( 39, 13 );
			this.l_elapsed.TabIndex = 7;
			this.l_elapsed.Text = "1 Hour";
			this.l_elapsed.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// PageGenProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 609, 104 );
			this.Controls.Add( this.l_elapsed );
			this.Controls.Add( this.l_pagevalue );
			this.Controls.Add( this.label3 );
			this.Controls.Add( this.b_cancel );
			this.Controls.Add( this.b_close );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.progress );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "PageGenProgress";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Generating Pages...";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button b_close;
		private System.Windows.Forms.Button b_cancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label l_pagevalue;
		private System.Windows.Forms.Label l_elapsed;
	}
}