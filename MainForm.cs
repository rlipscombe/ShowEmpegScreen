using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;

namespace ShowScreen
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Label timeLabel;
		private System.Windows.Forms.PictureBox picture;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
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
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.timeLabel = new System.Windows.Forms.Label();
			this.picture = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Tick += new System.EventHandler(this.MainForm_Tick);
			// 
			// timeLabel
			// 
			this.timeLabel.Location = new System.Drawing.Point(8, 8);
			this.timeLabel.Name = "timeLabel";
			this.timeLabel.TabIndex = 0;
			this.timeLabel.Text = "hh:mm:ss";
			// 
			// picture
			// 
			this.picture.Location = new System.Drawing.Point(8, 40);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(128, 32);
			this.picture.TabIndex = 1;
			this.picture.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(160, 85);
			this.Controls.Add(this.picture);
			this.Controls.Add(this.timeLabel);
			this.Name = "MainForm";
			this.Text = "empeg Screen";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void MainForm_Tick(object sender, System.EventArgs e)
		{
			DateTime now = DateTime.Now;
			timeLabel.Text = now.ToLongTimeString();

			WebRequest req = WebRequest.Create(@"http://crowley/proc/empeg_screen.png");
			WebResponse rsp = req.GetResponse();

			Stream stream = rsp.GetResponseStream();
			Image img = Image.FromStream(stream);
			picture.Image = img;

			rsp.Close();
		}
	}
}
