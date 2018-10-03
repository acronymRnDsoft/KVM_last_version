using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Net.Sockets;

namespace keyMouSerial
{
    public partial class view : Form
    {
		private FilterInfoCollection VideoDevices; //List of source video devices
		private VideoCaptureDevice WebCamSourceDevice; //Choosen source video device
        private static Socket Device;

		public view()
        {
            InitializeComponent();
            _hookID = SetHook(_proc);
        }

        private void view_Load(object sender, EventArgs e)
        {

            Device = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            System.Net.IPAddress VideoServer = System.Net.IPAddress.Parse("192.168.43.56");
            try
            {
                Device.Connect(new System.Net.IPEndPoint(VideoServer, 8000));
            }
            catch(SocketException exep)
            {
                Console.WriteLine(exep);
            }
            Device.NoDelay = true;
			WindowState = FormWindowState.Maximized;
            VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            ChooseVideoDevice.DropDownStyle = ComboBoxStyle.DropDownList;






            foreach (FilterInfo device in VideoDevices)
			{
				ChooseVideoDevice.Items.Add(device.Name); //Fill the combo box with video devices
			}
			//ChooseVideoDevice.SelectedIndex = 0;

			// WebCamSourceDevice = new VideoCaptureDevice(VideoDevices[ChooseVideoDevice.SelectedIndex].MonikerString);
			WebCamSourceDevice = new VideoCaptureDevice();
            ChooseVideoDevice.SelectedIndex = 0;
            
            pictureBox1.Focus();
           
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
			if (EnterPressed)
            {
				Globals.lastXValue = Cursor.Position.X;
				Globals.lastYValue = Cursor.Position.Y;
				Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2,
					Screen.PrimaryScreen.Bounds.Height / 2);
			}


        }

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
            WebCamSourceDevice.Stop();
			WebCamSourceDevice = new VideoCaptureDevice(VideoDevices[ChooseVideoDevice.SelectedIndex].MonikerString);
			WebCamSourceDevice.NewFrame += new NewFrameEventHandler(WebCamSourceDevice_NewFrame);
			WebCamSourceDevice.Start();
            pictureBox1.Focus();
            //ChooseVideoDevice.Enabled = false;
        }
		private void WebCamSourceDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
		{
            try
            {
                Bitmap picture = (Bitmap)eventArgs.Frame.Clone();
                pictureBox1.Image = picture;
            }
            catch
            {

            }
		}

		private void BlackBox_FormClosing(object sender, FormClosingEventArgs e)
		{
			WebCamSourceDevice.Stop();
		}
        public static bool EnterPressed = false;
       
        private void view_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

        private void view_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            Application.Exit();
        }

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = HookCallback;

        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelKeyboardProc proc)

        {

            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)

            {

                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,

                    GetModuleHandle(curModule.ModuleName), 0);

            }

        }


        private delegate IntPtr LowLevelKeyboardProc(

            int nCode, IntPtr wParam, IntPtr lParam);


        private static IntPtr HookCallback(

            int nCode, IntPtr wParam, IntPtr lParam)

        {

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)

            {

                int vkCode = Marshal.ReadInt32(lParam);

                if (vkCode == 163)
                    EnterPressed = !EnterPressed;

            }
            if (EnterPressed)
            {
                Cursor.Hide();
            }
            else
            {
                Cursor.Show();
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr SetWindowsHookEx(int idHook,

            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,

            IntPtr wParam, IntPtr lParam);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);
        byte DeviceNumber = 0;

        private void Display_1_Click(object sender, EventArgs e)
        {
            DeviceNumber = 1;
            byte[] h = System.Text.Encoding.ASCII.GetBytes(@"h");
            SendToDevice(h);
            byte[] Device = new byte[] { (byte)'1' };
            SendToDevice(Device);
        }

        private void Display_2_Click(object sender, EventArgs e)
        {
            byte[] h = System.Text.Encoding.ASCII.GetBytes(@"h");
            SendToDevice(h);
            byte[] Device = new byte[] { (byte)'2' };
            SendToDevice(Device);
        }

        private void Display_3_Click(object sender, EventArgs e)
        {
            byte[] h = System.Text.Encoding.ASCII.GetBytes(@"h");
            SendToDevice(h);
            byte[] Device = new byte[] { (byte)'3' };
            SendToDevice(Device);
        }

        private void Display_4_Click(object sender, EventArgs e)
        {
            byte[] h = System.Text.Encoding.ASCII.GetBytes(@"h");
            SendToDevice(h);
            byte[] Device = new byte[] { (byte)'4' };
            SendToDevice(Device);
        }

        private void Display_5_Click(object sender, EventArgs e)
        {
            byte[] h = System.Text.Encoding.ASCII.GetBytes(@"h");
            SendToDevice(h);
            byte[] Device = new byte[] { (byte)'5' };
            SendToDevice(Device);
        }

        static void SendToDevice(byte[] data)
        {
            try
            {
                Device.Send(data);
            }
            catch (Exception e)
            {


            }
        }
    }
}
