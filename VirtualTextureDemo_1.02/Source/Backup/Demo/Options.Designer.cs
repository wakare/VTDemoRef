namespace VirtualTextureDemo
{
	partial class Options
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
			this.components = new System.ComponentModel.Container();
			this.gb_scene = new System.Windows.Forms.GroupBox();
			this.b_browse_geometry = new System.Windows.Forms.Button();
			this.l_geometry = new System.Windows.Forms.Label();
			this.tb_geometry = new System.Windows.Forms.TextBox();
			this.b_browse_texture = new System.Windows.Forms.Button();
			this.l_texture = new System.Windows.Forms.Label();
			this.tb_texture = new System.Windows.Forms.TextBox();
			this.b_go = new System.Windows.Forms.Button();
			this.gb_vtexconfig = new System.Windows.Forms.GroupBox();
			this.cb_vt_size = new System.Windows.Forms.ComboBox();
			this.l_vt_size = new System.Windows.Forms.Label();
			this.cb_vt_bordersize = new System.Windows.Forms.ComboBox();
			this.l_vt_bordersize = new System.Windows.Forms.Label();
			this.cb_vt_tilesize = new System.Windows.Forms.ComboBox();
			this.l_vt_tilesize = new System.Windows.Forms.Label();
			this.cb_atlassize = new System.Windows.Forms.ComboBox();
			this.l_atlassize = new System.Windows.Forms.Label();
			this.gb_config = new System.Windows.Forms.GroupBox();
			this.tb_uploads = new System.Windows.Forms.TextBox();
			this.l_uploads = new System.Windows.Forms.Label();
			this.cb_feedbacksize = new System.Windows.Forms.ComboBox();
			this.l_feedbacksize = new System.Windows.Forms.Label();
			this.cb_preset = new System.Windows.Forms.ComboBox();
			this.l_preset = new System.Windows.Forms.Label();
			this.b_delete = new System.Windows.Forms.Button();
			this.gb_windowconfig = new System.Windows.Forms.GroupBox();
			this.l_ms = new System.Windows.Forms.Label();
			this.cb_ms = new System.Windows.Forms.ComboBox();
			this.l_res = new System.Windows.Forms.Label();
			this.cb_res = new System.Windows.Forms.ComboBox();
			this.tb_info = new System.Windows.Forms.TextBox();
			this.ll_url = new System.Windows.Forms.LinkLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip( this.components );
			this.gb_scene.SuspendLayout();
			this.gb_vtexconfig.SuspendLayout();
			this.gb_config.SuspendLayout();
			this.gb_windowconfig.SuspendLayout();
			this.SuspendLayout();
			// 
			// gb_scene
			// 
			this.gb_scene.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) 
            | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.gb_scene.Controls.Add( this.b_browse_geometry );
			this.gb_scene.Controls.Add( this.l_geometry );
			this.gb_scene.Controls.Add( this.tb_geometry );
			this.gb_scene.Controls.Add( this.b_browse_texture );
			this.gb_scene.Controls.Add( this.l_texture );
			this.gb_scene.Controls.Add( this.tb_texture );
			this.gb_scene.Location = new System.Drawing.Point( 12, 12 );
			this.gb_scene.Name = "gb_scene";
			this.gb_scene.Size = new System.Drawing.Size( 459, 80 );
			this.gb_scene.TabIndex = 0;
			this.gb_scene.TabStop = false;
			this.gb_scene.Text = "Scene";
			// 
			// b_browse_geometry
			// 
			this.b_browse_geometry.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.b_browse_geometry.Location = new System.Drawing.Point( 378, 43 );
			this.b_browse_geometry.Name = "b_browse_geometry";
			this.b_browse_geometry.Size = new System.Drawing.Size( 75, 23 );
			this.b_browse_geometry.TabIndex = 5;
			this.b_browse_geometry.Text = "Browse...";
			this.b_browse_geometry.UseVisualStyleBackColor = true;
			// 
			// l_geometry
			// 
			this.l_geometry.AutoSize = true;
			this.l_geometry.Location = new System.Drawing.Point( 8, 48 );
			this.l_geometry.Name = "l_geometry";
			this.l_geometry.Size = new System.Drawing.Size( 52, 13 );
			this.l_geometry.TabIndex = 4;
			this.l_geometry.Text = "Geometry";
			// 
			// tb_geometry
			// 
			this.tb_geometry.AllowDrop = true;
			this.tb_geometry.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) 
            | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.tb_geometry.Location = new System.Drawing.Point( 66, 45 );
			this.tb_geometry.Name = "tb_geometry";
			this.tb_geometry.Size = new System.Drawing.Size( 306, 20 );
			this.tb_geometry.TabIndex = 3;
			this.tb_geometry.WordWrap = false;
			// 
			// b_browse_texture
			// 
			this.b_browse_texture.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.b_browse_texture.Location = new System.Drawing.Point( 378, 17 );
			this.b_browse_texture.Name = "b_browse_texture";
			this.b_browse_texture.Size = new System.Drawing.Size( 75, 23 );
			this.b_browse_texture.TabIndex = 2;
			this.b_browse_texture.Text = "Browse...";
			this.b_browse_texture.UseVisualStyleBackColor = true;
			// 
			// l_texture
			// 
			this.l_texture.AutoSize = true;
			this.l_texture.Location = new System.Drawing.Point( 17, 22 );
			this.l_texture.Name = "l_texture";
			this.l_texture.Size = new System.Drawing.Size( 43, 13 );
			this.l_texture.TabIndex = 1;
			this.l_texture.Text = "Texture";
			// 
			// tb_texture
			// 
			this.tb_texture.AllowDrop = true;
			this.tb_texture.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left ) 
            | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.tb_texture.Location = new System.Drawing.Point( 66, 19 );
			this.tb_texture.Name = "tb_texture";
			this.tb_texture.Size = new System.Drawing.Size( 306, 20 );
			this.tb_texture.TabIndex = 0;
			this.tb_texture.WordWrap = false;
			// 
			// b_go
			// 
			this.b_go.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.b_go.Enabled = false;
			this.b_go.Location = new System.Drawing.Point( 371, 301 );
			this.b_go.Name = "b_go";
			this.b_go.Size = new System.Drawing.Size( 100, 23 );
			this.b_go.TabIndex = 1;
			this.b_go.Text = "Generate Pages";
			this.b_go.UseVisualStyleBackColor = true;
			this.b_go.Click += new System.EventHandler( this.b_go_Click );
			// 
			// gb_vtexconfig
			// 
			this.gb_vtexconfig.Controls.Add( this.cb_vt_size );
			this.gb_vtexconfig.Controls.Add( this.l_vt_size );
			this.gb_vtexconfig.Controls.Add( this.cb_vt_bordersize );
			this.gb_vtexconfig.Controls.Add( this.l_vt_bordersize );
			this.gb_vtexconfig.Controls.Add( this.cb_vt_tilesize );
			this.gb_vtexconfig.Controls.Add( this.l_vt_tilesize );
			this.gb_vtexconfig.Location = new System.Drawing.Point( 12, 98 );
			this.gb_vtexconfig.Name = "gb_vtexconfig";
			this.gb_vtexconfig.Size = new System.Drawing.Size( 220, 111 );
			this.gb_vtexconfig.TabIndex = 3;
			this.gb_vtexconfig.TabStop = false;
			this.gb_vtexconfig.Text = "Virtual Texture Config";
			// 
			// cb_vt_size
			// 
			this.cb_vt_size.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cb_vt_size.FormattingEnabled = true;
			this.cb_vt_size.Items.AddRange( new object[] {
            "4096",
            "8192",
            "16384",
            "32768",
            "65536",
            "131072",
            "262144"} );
			this.cb_vt_size.Location = new System.Drawing.Point( 125, 23 );
			this.cb_vt_size.Name = "cb_vt_size";
			this.cb_vt_size.Size = new System.Drawing.Size( 89, 21 );
			this.cb_vt_size.TabIndex = 7;
			// 
			// l_vt_size
			// 
			this.l_vt_size.AutoSize = true;
			this.l_vt_size.Location = new System.Drawing.Point( 6, 26 );
			this.l_vt_size.Name = "l_vt_size";
			this.l_vt_size.Size = new System.Drawing.Size( 101, 13 );
			this.l_vt_size.TabIndex = 6;
			this.l_vt_size.Text = "Virtual Texture Size:";
			// 
			// cb_vt_bordersize
			// 
			this.cb_vt_bordersize.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cb_vt_bordersize.FormattingEnabled = true;
			this.cb_vt_bordersize.Items.AddRange( new object[] {
            "0",
            "1",
            "2",
            "4",
            "8",
            "16"} );
			this.cb_vt_bordersize.Location = new System.Drawing.Point( 125, 77 );
			this.cb_vt_bordersize.Name = "cb_vt_bordersize";
			this.cb_vt_bordersize.Size = new System.Drawing.Size( 89, 21 );
			this.cb_vt_bordersize.TabIndex = 3;
			// 
			// l_vt_bordersize
			// 
			this.l_vt_bordersize.AutoSize = true;
			this.l_vt_bordersize.Location = new System.Drawing.Point( 6, 80 );
			this.l_vt_bordersize.Name = "l_vt_bordersize";
			this.l_vt_bordersize.Size = new System.Drawing.Size( 64, 13 );
			this.l_vt_bordersize.TabIndex = 2;
			this.l_vt_bordersize.Text = "Border Size:";
			// 
			// cb_vt_tilesize
			// 
			this.cb_vt_tilesize.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cb_vt_tilesize.FormattingEnabled = true;
			this.cb_vt_tilesize.Items.AddRange( new object[] {
            "8",
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"} );
			this.cb_vt_tilesize.Location = new System.Drawing.Point( 125, 50 );
			this.cb_vt_tilesize.Name = "cb_vt_tilesize";
			this.cb_vt_tilesize.Size = new System.Drawing.Size( 89, 21 );
			this.cb_vt_tilesize.TabIndex = 1;
			// 
			// l_vt_tilesize
			// 
			this.l_vt_tilesize.AutoSize = true;
			this.l_vt_tilesize.Location = new System.Drawing.Point( 6, 53 );
			this.l_vt_tilesize.Name = "l_vt_tilesize";
			this.l_vt_tilesize.Size = new System.Drawing.Size( 50, 13 );
			this.l_vt_tilesize.TabIndex = 0;
			this.l_vt_tilesize.Text = "Tile Size:";
			// 
			// cb_atlassize
			// 
			this.cb_atlassize.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cb_atlassize.FormattingEnabled = true;
			this.cb_atlassize.Items.AddRange( new object[] {
            "1024",
            "2048",
            "4096",
            "8192"} );
			this.cb_atlassize.Location = new System.Drawing.Point( 138, 50 );
			this.cb_atlassize.Name = "cb_atlassize";
			this.cb_atlassize.Size = new System.Drawing.Size( 89, 21 );
			this.cb_atlassize.TabIndex = 5;
			// 
			// l_atlassize
			// 
			this.l_atlassize.AutoSize = true;
			this.l_atlassize.Location = new System.Drawing.Point( 6, 53 );
			this.l_atlassize.Name = "l_atlassize";
			this.l_atlassize.Size = new System.Drawing.Size( 56, 13 );
			this.l_atlassize.TabIndex = 4;
			this.l_atlassize.Text = "Atlas Size:";
			// 
			// gb_config
			// 
			this.gb_config.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.gb_config.Controls.Add( this.tb_uploads );
			this.gb_config.Controls.Add( this.l_uploads );
			this.gb_config.Controls.Add( this.cb_feedbacksize );
			this.gb_config.Controls.Add( this.l_feedbacksize );
			this.gb_config.Controls.Add( this.cb_atlassize );
			this.gb_config.Controls.Add( this.l_atlassize );
			this.gb_config.Location = new System.Drawing.Point( 238, 98 );
			this.gb_config.Name = "gb_config";
			this.gb_config.Size = new System.Drawing.Size( 233, 111 );
			this.gb_config.TabIndex = 4;
			this.gb_config.TabStop = false;
			this.gb_config.Text = "Memory/Performance";
			// 
			// tb_uploads
			// 
			this.tb_uploads.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.tb_uploads.Location = new System.Drawing.Point( 138, 80 );
			this.tb_uploads.Name = "tb_uploads";
			this.tb_uploads.Size = new System.Drawing.Size( 89, 20 );
			this.tb_uploads.TabIndex = 11;
			// 
			// l_uploads
			// 
			this.l_uploads.AutoSize = true;
			this.l_uploads.Location = new System.Drawing.Point( 6, 80 );
			this.l_uploads.Name = "l_uploads";
			this.l_uploads.Size = new System.Drawing.Size( 100, 13 );
			this.l_uploads.TabIndex = 10;
			this.l_uploads.Text = "Uploads Per Frame:";
			// 
			// cb_feedbacksize
			// 
			this.cb_feedbacksize.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cb_feedbacksize.FormattingEnabled = true;
			this.cb_feedbacksize.Items.AddRange( new object[] {
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"} );
			this.cb_feedbacksize.Location = new System.Drawing.Point( 138, 23 );
			this.cb_feedbacksize.Name = "cb_feedbacksize";
			this.cb_feedbacksize.Size = new System.Drawing.Size( 89, 21 );
			this.cb_feedbacksize.TabIndex = 9;
			// 
			// l_feedbacksize
			// 
			this.l_feedbacksize.AutoSize = true;
			this.l_feedbacksize.Location = new System.Drawing.Point( 6, 26 );
			this.l_feedbacksize.Name = "l_feedbacksize";
			this.l_feedbacksize.Size = new System.Drawing.Size( 112, 13 );
			this.l_feedbacksize.TabIndex = 8;
			this.l_feedbacksize.Text = "Feedback Buffer Size:";
			// 
			// cb_preset
			// 
			this.cb_preset.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.cb_preset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cb_preset.FormattingEnabled = true;
			this.cb_preset.Location = new System.Drawing.Point( 55, 303 );
			this.cb_preset.Name = "cb_preset";
			this.cb_preset.Size = new System.Drawing.Size( 124, 21 );
			this.cb_preset.TabIndex = 5;
			this.cb_preset.SelectedIndexChanged += new System.EventHandler( this.cb_preset_SelectedIndexChanged );
			// 
			// l_preset
			// 
			this.l_preset.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.l_preset.AutoSize = true;
			this.l_preset.Location = new System.Drawing.Point( 9, 306 );
			this.l_preset.Name = "l_preset";
			this.l_preset.Size = new System.Drawing.Size( 40, 13 );
			this.l_preset.TabIndex = 6;
			this.l_preset.Text = "Preset:";
			// 
			// b_delete
			// 
			this.b_delete.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.b_delete.Enabled = false;
			this.b_delete.Location = new System.Drawing.Point( 261, 301 );
			this.b_delete.Name = "b_delete";
			this.b_delete.Size = new System.Drawing.Size( 104, 23 );
			this.b_delete.TabIndex = 7;
			this.b_delete.Text = "Delete Pages";
			this.b_delete.UseVisualStyleBackColor = true;
			this.b_delete.Click += new System.EventHandler( this.b_delete_Click );
			// 
			// gb_windowconfig
			// 
			this.gb_windowconfig.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) 
            | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.gb_windowconfig.Controls.Add( this.l_ms );
			this.gb_windowconfig.Controls.Add( this.cb_ms );
			this.gb_windowconfig.Controls.Add( this.l_res );
			this.gb_windowconfig.Controls.Add( this.cb_res );
			this.gb_windowconfig.Location = new System.Drawing.Point( 12, 215 );
			this.gb_windowconfig.Name = "gb_windowconfig";
			this.gb_windowconfig.Size = new System.Drawing.Size( 220, 82 );
			this.gb_windowconfig.TabIndex = 8;
			this.gb_windowconfig.TabStop = false;
			this.gb_windowconfig.Text = "Window";
			// 
			// l_ms
			// 
			this.l_ms.AutoSize = true;
			this.l_ms.Location = new System.Drawing.Point( 6, 49 );
			this.l_ms.Name = "l_ms";
			this.l_ms.Size = new System.Drawing.Size( 50, 13 );
			this.l_ms.TabIndex = 10;
			this.l_ms.Text = "Samples:";
			// 
			// cb_ms
			// 
			this.cb_ms.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cb_ms.FormattingEnabled = true;
			this.cb_ms.Items.AddRange( new object[] {
            "1",
            "2",
            "4",
            "8"} );
			this.cb_ms.Location = new System.Drawing.Point( 94, 46 );
			this.cb_ms.Name = "cb_ms";
			this.cb_ms.Size = new System.Drawing.Size( 120, 21 );
			this.cb_ms.TabIndex = 9;
			// 
			// l_res
			// 
			this.l_res.AutoSize = true;
			this.l_res.Location = new System.Drawing.Point( 6, 22 );
			this.l_res.Name = "l_res";
			this.l_res.Size = new System.Drawing.Size( 60, 13 );
			this.l_res.TabIndex = 8;
			this.l_res.Text = "Resolution:";
			// 
			// cb_res
			// 
			this.cb_res.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.cb_res.FormattingEnabled = true;
			this.cb_res.Items.AddRange( new object[] {
            "640x480 (4:3)",
            "800x600 (4:3)",
            "1024x768 (4:3)",
            "1280x768 (16:10)",
            "1440x900 (16:10)",
            "1920x1200 (16:10)"} );
			this.cb_res.Location = new System.Drawing.Point( 94, 19 );
			this.cb_res.Name = "cb_res";
			this.cb_res.Size = new System.Drawing.Size( 120, 21 );
			this.cb_res.TabIndex = 8;
			// 
			// tb_info
			// 
			this.tb_info.BackColor = System.Drawing.SystemColors.Control;
			this.tb_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tb_info.Location = new System.Drawing.Point( 238, 232 );
			this.tb_info.Multiline = true;
			this.tb_info.Name = "tb_info";
			this.tb_info.ReadOnly = true;
			this.tb_info.Size = new System.Drawing.Size( 233, 27 );
			this.tb_info.TabIndex = 0;
			this.tb_info.Text = "Virtual Texture Demo\r\nCopyright © 2009 Brad Blanchard";
			this.tb_info.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// ll_url
			// 
			this.ll_url.AutoSize = true;
			this.ll_url.Location = new System.Drawing.Point( 311, 258 );
			this.ll_url.Name = "ll_url";
			this.ll_url.Size = new System.Drawing.Size( 88, 13 );
			this.ll_url.TabIndex = 9;
			this.ll_url.TabStop = true;
			this.ll_url.Text = "www.linedef.com";
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 20000;
			this.toolTip1.InitialDelay = 250;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 100;
			this.toolTip1.ShowAlways = true;
			// 
			// Options
			// 
			this.AcceptButton = this.b_go;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 483, 336 );
			this.Controls.Add( this.ll_url );
			this.Controls.Add( this.tb_info );
			this.Controls.Add( this.gb_windowconfig );
			this.Controls.Add( this.b_delete );
			this.Controls.Add( this.l_preset );
			this.Controls.Add( this.cb_preset );
			this.Controls.Add( this.gb_config );
			this.Controls.Add( this.gb_vtexconfig );
			this.Controls.Add( this.b_go );
			this.Controls.Add( this.gb_scene );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Options";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Virtual Texture Demo Options";
			this.gb_scene.ResumeLayout( false );
			this.gb_scene.PerformLayout();
			this.gb_vtexconfig.ResumeLayout( false );
			this.gb_vtexconfig.PerformLayout();
			this.gb_config.ResumeLayout( false );
			this.gb_config.PerformLayout();
			this.gb_windowconfig.ResumeLayout( false );
			this.gb_windowconfig.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gb_scene;
		private System.Windows.Forms.TextBox tb_texture;
		private System.Windows.Forms.Button b_browse_geometry;
		private System.Windows.Forms.Label l_geometry;
		private System.Windows.Forms.TextBox tb_geometry;
		private System.Windows.Forms.Button b_browse_texture;
		private System.Windows.Forms.Label l_texture;
		private System.Windows.Forms.Button b_go;
		private System.Windows.Forms.GroupBox gb_vtexconfig;
		private System.Windows.Forms.ComboBox cb_vt_tilesize;
		private System.Windows.Forms.Label l_vt_tilesize;
		private System.Windows.Forms.ComboBox cb_vt_bordersize;
		private System.Windows.Forms.Label l_vt_bordersize;
		private System.Windows.Forms.GroupBox gb_config;
		private System.Windows.Forms.ComboBox cb_atlassize;
		private System.Windows.Forms.Label l_atlassize;
		private System.Windows.Forms.ComboBox cb_vt_size;
		private System.Windows.Forms.Label l_vt_size;
		private System.Windows.Forms.ComboBox cb_feedbacksize;
		private System.Windows.Forms.Label l_feedbacksize;
		private System.Windows.Forms.TextBox tb_uploads;
		private System.Windows.Forms.Label l_uploads;
		private System.Windows.Forms.ComboBox cb_preset;
		private System.Windows.Forms.Label l_preset;
		private System.Windows.Forms.Button b_delete;
		private System.Windows.Forms.GroupBox gb_windowconfig;
		private System.Windows.Forms.Label l_res;
		private System.Windows.Forms.ComboBox cb_res;
		private System.Windows.Forms.Label l_ms;
		private System.Windows.Forms.ComboBox cb_ms;
		private System.Windows.Forms.TextBox tb_info;
		private System.Windows.Forms.LinkLabel ll_url;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}