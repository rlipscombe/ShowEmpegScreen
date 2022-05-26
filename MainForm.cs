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
			this.picture = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// picture
			// 
			this.picture.Location = new System.Drawing.Point(8, 8);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(128, 32);
			this.picture.TabIndex = 1;
			this.picture.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(144, 45);
			this.Controls.Add(this.picture);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "empeg Screen";
			this.Load += new System.EventHandler(this.MainForm_Load);
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

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				// Kick off the first request:
				WebRequest newRequest = WebRequest.Create(empegScreenURL);
				newRequest.BeginGetResponse(new AsyncCallback(MainForm_GetResponse), newRequest);
			}
			catch (Exception ex)
			{
				string message = String.Format("Exception in response callback: {0}", ex.Message);
				MessageBox.Show(message);
			}
		}

		private string empegScreenURL = @"http://crowley/proc/empeg_screen.png";

		private void MainForm_GetResponse(IAsyncResult ar)
		{
			try
			{
				WebRequest currentRequest = (WebRequest)ar.AsyncState;
				WebResponse rsp = currentRequest.EndGetResponse(ar);
				Stream stream = rsp.GetResponseStream();
				Image img = Image.FromStream(stream);
				picture.Image = img;

				rsp.Close();

				// Issue another one:
				WebRequest newRequest = WebRequest.Create(empegScreenURL);
				newRequest.BeginGetResponse(new AsyncCallback(MainForm_GetResponse), newRequest);
			}
			catch (Exception ex)
			{
				string message = String.Format("Exception in response callback: {0}", ex.Message);
				MessageBox.Show(message);
			}
		}
	}
}
