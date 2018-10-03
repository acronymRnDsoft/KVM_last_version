namespace keyMouSerial
{
    partial class view
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ChooseVideoDevice = new System.Windows.Forms.ComboBox();
            this.Display_1 = new System.Windows.Forms.Button();
            this.Display_2 = new System.Windows.Forms.Button();
            this.Display_3 = new System.Windows.Forms.Button();
            this.Display_4 = new System.Windows.Forms.Button();
            this.Display_5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(118, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(491, 426);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // ChooseVideoDevice
            // 
            this.ChooseVideoDevice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ChooseVideoDevice.Location = new System.Drawing.Point(0, 169);
            this.ChooseVideoDevice.Margin = new System.Windows.Forms.Padding(2);
            this.ChooseVideoDevice.Name = "ChooseVideoDevice";
            this.ChooseVideoDevice.Size = new System.Drawing.Size(105, 21);
            this.ChooseVideoDevice.Sorted = true;
            this.ChooseVideoDevice.TabIndex = 100;
            this.ChooseVideoDevice.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Display_1
            // 
            this.Display_1.Location = new System.Drawing.Point(13, 196);
            this.Display_1.Name = "Display_1";
            this.Display_1.Size = new System.Drawing.Size(75, 23);
            this.Display_1.TabIndex = 1;
            this.Display_1.Text = "Display 1";
            this.Display_1.UseVisualStyleBackColor = true;
            this.Display_1.Click += new System.EventHandler(this.Display_1_Click);
            // 
            // Display_2
            // 
            this.Display_2.Location = new System.Drawing.Point(13, 225);
            this.Display_2.Name = "Display_2";
            this.Display_2.Size = new System.Drawing.Size(75, 23);
            this.Display_2.TabIndex = 2;
            this.Display_2.Text = "Display 2";
            this.Display_2.UseVisualStyleBackColor = true;
            this.Display_2.Click += new System.EventHandler(this.Display_2_Click);
            // 
            // Display_3
            // 
            this.Display_3.Location = new System.Drawing.Point(13, 254);
            this.Display_3.Name = "Display_3";
            this.Display_3.Size = new System.Drawing.Size(75, 23);
            this.Display_3.TabIndex = 3;
            this.Display_3.Text = "Display 3";
            this.Display_3.UseVisualStyleBackColor = true;
            this.Display_3.Click += new System.EventHandler(this.Display_3_Click);
            // 
            // Display_4
            // 
            this.Display_4.Location = new System.Drawing.Point(13, 283);
            this.Display_4.Name = "Display_4";
            this.Display_4.Size = new System.Drawing.Size(75, 23);
            this.Display_4.TabIndex = 4;
            this.Display_4.Text = "Display 4";
            this.Display_4.UseVisualStyleBackColor = true;
            this.Display_4.Click += new System.EventHandler(this.Display_4_Click);
            // 
            // Display_5
            // 
            this.Display_5.Location = new System.Drawing.Point(13, 312);
            this.Display_5.Name = "Display_5";
            this.Display_5.Size = new System.Drawing.Size(75, 23);
            this.Display_5.TabIndex = 5;
            this.Display_5.Text = "Display 5";
            this.Display_5.UseVisualStyleBackColor = true;
            this.Display_5.Click += new System.EventHandler(this.Display_5_Click);
            // 
            // view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 426);
            this.Controls.Add(this.Display_5);
            this.Controls.Add(this.Display_4);
            this.Controls.Add(this.Display_3);
            this.Controls.Add(this.Display_2);
            this.Controls.Add(this.Display_1);
            this.Controls.Add(this.ChooseVideoDevice);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "view";
            this.Text = "view";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.view_FormClosed);
            this.Load += new System.EventHandler(this.view_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.view_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ComboBox ChooseVideoDevice;
        private System.Windows.Forms.Button Display_1;
        private System.Windows.Forms.Button Display_2;
        private System.Windows.Forms.Button Display_3;
        private System.Windows.Forms.Button Display_4;
        private System.Windows.Forms.Button Display_5;
    }
}