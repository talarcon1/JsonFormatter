using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace JsonFormatter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblJson;
		private System.Windows.Forms.Button btnFormat;
		private System.Windows.Forms.TextBox txtJson;
		private System.Windows.Forms.TextBox txtFormatted;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnCopy;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
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
			this.lblJson = new System.Windows.Forms.Label();
			this.btnFormat = new System.Windows.Forms.Button();
			this.txtJson = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.txtFormatted = new System.Windows.Forms.TextBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblJson
			// 
			this.lblJson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblJson.Location = new System.Drawing.Point(80, 24);
			this.lblJson.Name = "lblJson";
			this.lblJson.Size = new System.Drawing.Size(368, 23);
			this.lblJson.TabIndex = 1;
			this.lblJson.Text = "Enter unformatted Json and Press Format";
			// 
			// btnFormat
			// 
			this.btnFormat.Location = new System.Drawing.Point(232, 176);
			this.btnFormat.Name = "btnFormat";
			this.btnFormat.TabIndex = 1;
			this.btnFormat.Text = "Format";
			this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
			// 
			// txtJson
			// 
			this.txtJson.Location = new System.Drawing.Point(72, 56);
			this.txtJson.Multiline = true;
			this.txtJson.Name = "txtJson";
			this.txtJson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtJson.Size = new System.Drawing.Size(520, 104);
			this.txtJson.TabIndex = 0;
			this.txtJson.Text = "";
			this.txtJson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJson_KeyDown);
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(264, 432);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(112, 23);
			this.btnCopy.TabIndex = 4;
			this.btnCopy.Text = "Copy to Clipboard";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// txtFormatted
			// 
			this.txtFormatted.Location = new System.Drawing.Point(72, 216);
			this.txtFormatted.Multiline = true;
			this.txtFormatted.Name = "txtFormatted";
			this.txtFormatted.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFormatted.Size = new System.Drawing.Size(520, 208);
			this.txtFormatted.TabIndex = 3;
			this.txtFormatted.Text = "";
			this.txtFormatted.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFormatted_KeyDown);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(352, 176);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 485);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.txtFormatted);
			this.Controls.Add(this.txtJson);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnFormat);
			this.Controls.Add(this.lblJson);
			this.Name = "Form1";
			this.Text = "Json Formatter";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		#region CONSTANTS
			private const short SUCCESS = 0;
			private const short ERROR_FAILURE = 1;
			private const short ERROR_COMMA = 2;
			private const short ERROR_SQUIGGLY = 3;
			private const short ERROR_BRACKET = 4;
			
		#endregion
		#region METHODS

		public void clear()
		{
			txtJson.Text = "";
			txtFormatted.Text = "";
			txtJson.Focus();
		}


		public string format(string json)
		{
			Stack stack = new Stack();
			int level = 0;
			string formattedJson = "";
			bool inString = false;
			int iteration = 0;
			
			foreach(char c in json)
			{
				if(inString)
				{
					if(c == '"')
					{
						inString = !inString;
					}
					formattedJson += c;
				}
				else
				{
					switch(c)
					{
						case '{':
							stack.Push(c);
							//level++;
							//formattedJson += "\r\n";
							for(int x = 0; x < level; x++)
							{
								formattedJson += "\t";
							}
							formattedJson += c;
							
							level++;
							formattedJson += "\r\n";
							for(int x = 0; x < level; x++)
							{
								formattedJson += "\t";
							}
							break;

						case '[':
							stack.Push(c);
//							formattedJson += "\r\n";
//							for(int x = 0; x < level; x++)
//							{
//								formattedJson += "\t";
//							}
							formattedJson += c;
							
							level++;
							formattedJson += "\r\n";		
							break;

						case '}':
							
							short error = ErrorCheck(c,stack);
							if(error != SUCCESS)
							{
								btnCopy.Enabled = false;
								txtFormatted.ForeColor = Color.Red;
								return createErrorMessage(error,formattedJson);
							}
						
							
							formattedJson += "\r\n";
							level -= 1;
							for(int x = 0; x < level; x++)
							{
								formattedJson += "\t";
							}				
							
							formattedJson += c;
							formattedJson += "\r\n";
				
							break;
						case ']':
						
							short error1 = ErrorCheck(c,stack);
							if(error1 != SUCCESS)
							{
								btnCopy.Enabled = false;
								txtFormatted.ForeColor = Color.Red;
								return createErrorMessage(error1,formattedJson);
							}
						
							
							formattedJson += "\r\n";
							level -= 1;
							for(int x = 0; x < level; x++)
							{
								formattedJson += "\t";
							}				
							
							formattedJson += c;
							//formattedJson += "\r\n";
				
							break;
						case ',':
							
							stack.Push(c);
							formattedJson += c;
							formattedJson += "\r\n";
							for(int x = 0; x < level; x++)
							{
								formattedJson += "\t";
							}
							break;
		
						default:
							stack.Push(c);
							formattedJson += c;
							break;
					}
 
				}
				iteration++;
			}
			return formattedJson;
		}

		//Method for creating error message to show in txtFormatted
		public string createErrorMessage(short error, string currentJson)
		{
			string message = "";
			if(error == ERROR_COMMA)
			{
				message = "ERROR: UNNECESSARY COMMA.";
			}
			else if(error == ERROR_SQUIGGLY)
			{	
				message = "ERROR: MISSING CORRESPONDING OPEN AND CLOSING SQUIGGLY BRACKETS.";
			}
			else if(error == ERROR_BRACKET)
			{
				message = "ERROR: MISSING CORRESPONDING OPEN AND CLOSING SQUARE BRACKETS.";
			}
			else
			{
				message = "An ERROR has occured.";
			}
			return "\r\n" + message + "\r\n" + "\r\n" +  currentJson;	
		}
		//Method for checking json validity
		public short ErrorCheck(char target, Stack stack)
		{
			short error;
			if(target == '}')
			{
				target = '{';
				error = ERROR_SQUIGGLY;
			}
			else if(target == ']')
			{
				target = '[';
				error = ERROR_BRACKET;
			}
			else
			{
				return ERROR_FAILURE;
			}
			char popped = '\0';
			bool targetFound = false;
			try
			{
				
						
				if(stack.Count > 0)
				{
					popped = Convert.ToChar(stack.Pop());
					if(popped == ',')
					{
						return ERROR_COMMA;
					}
				}
				while(!targetFound && stack.Count >= 0)
				{	
					if(popped == target)
					{
						return SUCCESS;				
					}
									
					if(stack.Count > 0)
					{
						popped = Convert.ToChar(stack.Pop());
					}
									
				}
				
			}
			catch
			{

			}
			return error;
		
		}


#endregion

		#region EVENT HANDLERS

		private void btnFormat_Click(object sender, System.EventArgs e)
		{
			//string formattedJson = format("{  \"ProfileId\":null,   \"CardNumber\":null, }");
			btnCopy.Enabled = true;
			txtFormatted.ForeColor = Color.Black;
			string finalJson = format(txtJson.Text);//.Substring(2);
			txtFormatted.Text = finalJson;			
		}

		private void txtFormatted_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Control)
			{
				if(e.KeyCode == Keys.A)
				{
					txtFormatted.SelectionStart = 0;
					txtFormatted.SelectionLength = txtFormatted.Text.Length;
				}

			}					
			
		}

		private void txtJson_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Control)
			{
				if(e.KeyCode == Keys.A)
				{
					txtJson.SelectionStart = 0;
					txtJson.SelectionLength = txtJson.Text.Length;
				}

			}
		}

		private void btnClear_Click_1(object sender, System.EventArgs e)
		{
			btnCopy.Enabled = true;
			txtFormatted.ForeColor = Color.Black;
			clear();
		}

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(txtFormatted.Text);
			MessageBox.Show("Copied to your clipboard");
		}


		#endregion





	






	}
}
