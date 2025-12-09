using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XInputDotNetPure;
using XInputButtonState = XInputDotNetPure.ButtonState;

namespace GamepadTurbo
{
    public partial class Form1 : Form
    {
        private Timer gamepadTimer;
        private bool isTurboActive = false;

        // Local enum to represent selectable gamepad buttons in the UI
        private enum GpButton
        {
            A,
            B,
            X,
            Y,
            Start,
            Back,
            LeftStick,
            RightStick,
            LeftShoulder,
            RightShoulder,
            DPadUp,
            DPadDown,
            DPadLeft,
            DPadRight
        }

        private GpButton input1 = GpButton.A; // Default
        private GpButton input2 = GpButton.B;
        private string output1 = "A"; // SendKeys representation
        private string output2 = "B";
        private int repeatDelay = 50; // ms

        private DateTime lastPress1 = DateTime.MinValue;
        private DateTime lastPress2 = DateTime.MinValue;

        // Mapping dari string ke GpButton
        private readonly Dictionary<string, GpButton> buttonMap = new Dictionary<string, GpButton>
        {
            { "A", GpButton.A },
            { "B", GpButton.B },
            { "X", GpButton.X },
            { "Y", GpButton.Y },
            { "Start", GpButton.Start },
            { "Back", GpButton.Back },
            { "LeftStick", GpButton.LeftStick },
            { "RightStick", GpButton.RightStick },
            { "LeftShoulder", GpButton.LeftShoulder },
            { "RightShoulder", GpButton.RightShoulder },
            { "DPadUp", GpButton.DPadUp },
            { "DPadDown", GpButton.DPadDown },
            { "DPadLeft", GpButton.DPadLeft },
            { "DPadRight", GpButton.DPadRight }
        };

        // Mapping dari string ke SendKeys format
        private readonly Dictionary<string, string> keyMap = new Dictionary<string, string>
        {
            { "KEY_A", "A" },
            { "KEY_B", "B" },
            { "KEY_C", "C" },
            { "KEY_D", "D" },
            { "KEY_E", "E" },
            { "KEY_F", "F" },
            { "KEY_G", "G" },
            { "KEY_H", "H" },
            { "KEY_I", "I" },
            { "KEY_J", "J" },
            { "KEY_K", "K" },
            { "KEY_L", "L" },
            { "KEY_M", "M" },
            { "KEY_N", "N" },
            { "KEY_O", "O" },
            { "KEY_P", "P" },
            { "KEY_Q", "Q" },
            { "KEY_R", "R" },
            { "KEY_S", "S" },
            { "KEY_T", "T" },
            { "KEY_U", "U" },
            { "KEY_V", "V" },
            { "KEY_W", "W" },
            { "KEY_X", "X" },
            { "KEY_Y", "Y" },
            { "KEY_Z", "Z" },
            { "SPACE", "{SPACE}" },
            { "RETURN", "{ENTER}" },
            { "SHIFT", "+" },    // modifier prefix for SendKeys (use with caution)
            { "CONTROL", "^" }   // modifier prefix for SendKeys
        };

        public Form1()
        {
            InitializeComponent();

            // Setup ComboBox untuk Input1 dan Input2 (tombol gamepad)
            var buttons = new[] { "A", "B", "X", "Y", "Start", "Back", "LeftStick", "RightStick", "LeftShoulder", "RightShoulder", "DPadUp", "DPadDown", "DPadLeft", "DPadRight" };
            comboInput1.Items.AddRange(buttons);
            comboInput2.Items.AddRange(buttons);
            comboInput1.SelectedIndex = 0; // Default A
            comboInput2.SelectedIndex = 1; // Default B

            // ComboBox untuk Output1 dan Output2 (key keyboard, contoh sederhana)
            var keys = new[] { "KEY_A", "KEY_B", "KEY_C", "KEY_D", "KEY_E", "KEY_F", "KEY_G", "KEY_H", "KEY_I", "KEY_J", "KEY_K", "KEY_L", "KEY_M", "KEY_N", "KEY_O", "KEY_P", "KEY_Q", "KEY_R", "KEY_S", "KEY_T", "KEY_U", "KEY_V", "KEY_W", "KEY_X", "KEY_Y", "KEY_Z", "SPACE", "RETURN", "SHIFT", "CONTROL" };
            comboOutput1.Items.AddRange(keys);
            comboOutput2.Items.AddRange(keys);
            comboOutput1.SelectedIndex = 0; // Default KEY_A
            comboOutput2.SelectedIndex = 1; // Default KEY_B

            // Default delay
            txtRepeatDelay.Text = "50";

            // Timer untuk polling gamepad (setiap 10ms untuk responsif)
            gamepadTimer = new Timer { Interval = 10 };
            gamepadTimer.Tick += GamepadTimer_Tick;
        }

        // Handler untuk event Load yang dibuat di Designer
        private void Form1_Load(object sender, EventArgs e)
        {
            // set initial UI state
            btnStart.Text = "Start Turbo";
            btnStart.BackColor = Color.Green;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            isTurboActive = !isTurboActive;
            btnStart.Text = isTurboActive ? "Stop Turbo" : "Start Turbo";
            btnStart.BackColor = isTurboActive ? Color.Red : Color.Green;

            if (isTurboActive)
            {
                // Ambil pilihan user
                if (comboInput1.SelectedItem is string selInput1 && buttonMap.TryGetValue(selInput1, out var btn1))
                    input1 = btn1;
                else
                    MessageBox.Show("Pilih input1 yang valid!");

                if (comboInput2.SelectedItem is string selInput2 && buttonMap.TryGetValue(selInput2, out var btn2))
                    input2 = btn2;
                else
                    MessageBox.Show("Pilih input2 yang valid!");

                if (comboOutput1.SelectedItem is string selOut1 && keyMap.TryGetValue(selOut1, out var k1))
                    output1 = k1;
                else
                    MessageBox.Show("Pilih output1 yang valid!");

                if (comboOutput2.SelectedItem is string selOut2 && keyMap.TryGetValue(selOut2, out var k2))
                    output2 = k2;
                else
                    MessageBox.Show("Pilih output2 yang valid!");

                int.TryParse(txtRepeatDelay.Text, out repeatDelay);
                if (repeatDelay < 10) repeatDelay = 50; // Minimal delay

                gamepadTimer.Start();
            }
            else
            {
                gamepadTimer.Stop();
            }
        }

        private void GamepadTimer_Tick(object sender, EventArgs e)
        {
            // Baca status gamepad (Player One) menggunakan XInputDotNetPure
            GamePadState state = GamePad.GetState(PlayerIndex.One);

            if (state.IsConnected)
            {
                // Cek Input1
                if (IsButtonPressed(state, input1))
                {
                    if ((DateTime.Now - lastPress1).TotalMilliseconds >= repeatDelay)
                    {
                        try { SendKeys.SendWait(output1); } catch { }
                        lastPress1 = DateTime.Now;
                    }
                }

                // Cek Input2
                if (IsButtonPressed(state, input2))
                {
                    if ((DateTime.Now - lastPress2).TotalMilliseconds >= repeatDelay)
                    {
                        try { SendKeys.SendWait(output2); } catch { }
                        lastPress2 = DateTime.Now;
                    }
                }
            }
            else
            {
                MessageBox.Show("Gamepad tidak terhubung!");
                btnStart.PerformClick(); // Stop jika disconnected
            }
        }

        // Helper untuk mengecek apakah tombol tertentu ditekan pada GamePadState
        private bool IsButtonPressed(GamePadState state, GpButton btn)
        {
            switch (btn)
            {
                case GpButton.A: return state.Buttons.A == XInputButtonState.Pressed;
                case GpButton.B: return state.Buttons.B == XInputButtonState.Pressed;
                case GpButton.X: return state.Buttons.X == XInputButtonState.Pressed;
                case GpButton.Y: return state.Buttons.Y == XInputButtonState.Pressed;
                case GpButton.Start: return state.Buttons.Start == XInputButtonState.Pressed;
                case GpButton.Back: return state.Buttons.Back == XInputButtonState.Pressed;
                case GpButton.LeftStick: return state.Buttons.LeftStick == XInputButtonState.Pressed;
                case GpButton.RightStick: return state.Buttons.RightStick == XInputButtonState.Pressed;
                case GpButton.LeftShoulder: return state.Buttons.LeftShoulder == XInputButtonState.Pressed;
                case GpButton.RightShoulder: return state.Buttons.RightShoulder == XInputButtonState.Pressed;
                case GpButton.DPadUp: return state.DPad.Up == XInputButtonState.Pressed;
                case GpButton.DPadDown: return state.DPad.Down == XInputButtonState.Pressed;
                case GpButton.DPadLeft: return state.DPad.Left == XInputButtonState.Pressed;
                case GpButton.DPadRight: return state.DPad.Right == XInputButtonState.Pressed;
                default: return false;
            }
        }
    }
}