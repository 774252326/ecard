using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using SLAB_HID_TO_UART;
//using CP2110_DLL;

using Phychips.Rcp;
using Phychips.Helper;



namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        //private int a;
        public int b;

        public const int CB_ERR = -1;
        public uint m_baudRate = 115200;

        public ushort VID = 0;
        public ushort PID = 0;

        public System.IntPtr m_hidUart;
        public byte m_partNumber;
        public byte m_version;

        public uint READ_TIMEOUT = 0;
        public uint WRITE_TIMEOUT = 2000;
        public const int TRUE = 1;
        public const int FALSE = 0;
        public uint READ_TIMER_ELAPSE = 50;
        public uint READ_SIZE = 1000;


        public Form1()
        {
            InitializeComponent();
            //CP2110_DLL.HidUart_GetPinConfig();

            InitializeDialog();
        }


        void InitializeDialog()
        {
            //InitStatusBar();
            InitUartSettings();
            InitTransferSettings();
            //InitHexEditCtrl();

            UpdateDeviceList();

            //RegisterDeviceChange();

            //button1.Text = "&Connect";

            //groupBox1.Text = "Connection";

            rename();
        }


        void rename()
        {
            

            groupBox1.Text = "Connection";
            button1.Text = "&Connect";

            groupBox2.Text = "Configuration";
            label1.Text = "Baud Rate:";
            label2.Text = "Data Bits:";
            label3.Text = "Parity:";
            label4.Text = "Stop Bits:";
            label5.Text = "Flow Control:";

            groupBox3.Text = "Pin Control";
            checkBox1.Text = "GPIO.0/CLK";
            checkBox2.Text = "GPIO.1/RTS";
            checkBox3.Text = "GPIO.2/CTS";
            checkBox4.Text = "GPIO.3/RS485";
            checkBox5.Text = "GPIO.4/TX Toggle";
            checkBox6.Text = "GPIO.5/RX Toggle";
            checkBox7.Text = "GPIO.6";
            checkBox8.Text = "GPIO.7";
            checkBox9.Text = "GPIO.8";
            checkBox10.Text = "GPIO.9";
            button2.Text = "Set";
            button3.Text = "Get";

            groupBox4.Text = "Device Information";
            label6.Text = "VID:";
            label7.Text = "PID:";
            label8.Text = "Serial:";
            label9.Text = "Path:";
            label10.Text = "Part Number:";
            label11.Text = "Version:";
            label12.Text = "Manufacturer:";
            label13.Text = "Product:";

            groupBox5.Text = "Data Transfer";
            label14.Text = "Transmit:";
            label15.Text = "Receive:";
            button4.Text = "Transmit";
            button5.Text = "Clear";
            radioButton1.Text = "ASCII";
            radioButton2.Text = "HEX";





        }

        //        // Register for device change notification for USB HID devices
        //// OnDeviceChange() will handle device arrival and removal
        //void RegisterDeviceChange()
        //{
        //    DEV_BROADCAST_DEVICEINTERFACE devIF = {0};

        //    devIF.dbcc_size			= sizeof(devIF);    
        //    devIF.dbcc_devicetype	= DBT_DEVTYP_DEVICEINTERFACE;

        //    CP2110_DLL.HidUart_GetHidGuid(&devIF.dbcc_classguid);

        //    m_hNotifyDevNode = RegisterDeviceNotification(GetSafeHwnd(), &devIF, DEVICE_NOTIFY_WINDOW_HANDLE);
        //}




        // Set the default transfer data settings
        // - Set transmit format to ASCII
        void InitTransferSettings()
        {
            // Select ASCII format
            //CheckRadioButton(IDC_RADIO_ASCII, IDC_RADIO_HEX, IDC_RADIO_ASCII);
            radioButton1.Checked = true;
            //radioButton2.Checked = true;
        }

        // Set the default UART control values
        // - Set default baud rate to 115200
        // - Set default UART combo settings to 8N1 (No flow control)
        void InitUartSettings()
        {
            //UpdateData(TRUE);

            // Set default baud rate
            m_baudRate = 115200;
            comboBox2.Items.Add(m_baudRate);
            comboBox2.SelectedIndex = 0;

            // Set default UART settings
            //m_comboDataBits.SelectString(-1, _T("8"));

            comboBox3.Items.Add(8);
            comboBox3.SelectedIndex = 0;
            //m_comboParity.SelectString(-1, _T("N"));
            comboBox4.Items.Add("N");
            comboBox4.SelectedIndex = 0;

            //m_comboStopBits.SelectString(-1, _T("1"));
            comboBox5.Items.Add("1");
            comboBox5.SelectedIndex = 0;
            //m_comboFlowControl.SelectString(-1, _T("No"));
            comboBox6.Items.Add("No");
            comboBox6.SelectedIndex = 0;


            //UpdateData(FALSE);
        }


        // Return a string description for the specified status code
        String GetHidUartStatusStr(int status)
        {
            String statusStr = ("Unknown status");

            switch (status)
            {
                case CP2110_DLL.HID_UART_SUCCESS: statusStr = ("Success"); break;
                case CP2110_DLL.HID_UART_DEVICE_NOT_FOUND: statusStr = ("Device not found"); break;
                case CP2110_DLL.HID_UART_INVALID_HANDLE: statusStr = ("Invalid handle"); break;
                case CP2110_DLL.HID_UART_INVALID_DEVICE_OBJECT: statusStr = ("Invalid device object"); break;
                case CP2110_DLL.HID_UART_INVALID_PARAMETER: statusStr = ("Invalid parameter"); break;
                case CP2110_DLL.HID_UART_INVALID_REQUEST_LENGTH: statusStr = ("Invalid request length"); break;

                case CP2110_DLL.HID_UART_READ_ERROR: statusStr = ("Read error"); break;
                case CP2110_DLL.HID_UART_WRITE_ERROR: statusStr = ("Write error"); break;
                case CP2110_DLL.HID_UART_READ_TIMED_OUT: statusStr = ("Read timed out"); break;
                case CP2110_DLL.HID_UART_WRITE_TIMED_OUT: statusStr = ("Write timed out"); break;
                case CP2110_DLL.HID_UART_DEVICE_IO_FAILED: statusStr = ("Device I/O failed"); break;
                case CP2110_DLL.HID_UART_DEVICE_ACCESS_ERROR: statusStr = ("Device access error"); break;
                case CP2110_DLL.HID_UART_DEVICE_NOT_SUPPORTED: statusStr = ("Device not supported"); break;

                case CP2110_DLL.HID_UART_UNKNOWN_ERROR: statusStr = ("Unknown error"); break;
            }

            return statusStr;
        }

        // Return the UART control settings
        // - Get UART setting combo selections
        // - Get device list serial string
        public int GetConnectSettings(ref String serial, ref uint baudRate, ref byte dataBits, ref byte parity, ref byte stopBits, ref byte flowControl)
        {
            //if (UpdateData(TRUE))
            //{
            // Get UART settings combo box selections
            //int dataBitsSel		= m_comboDataBits.GetCurSel();
            //int paritySel		= m_comboParity.GetCurSel();
            //int stopBitsSel		= m_comboStopBits.GetCurSel();
            //int flowControlSel	= m_comboFlowControl.GetCurSel();
            int dataBitsSel = (comboBox3.SelectedIndex);
            int paritySel = (comboBox4.SelectedIndex);
            int stopBitsSel = (comboBox5.SelectedIndex);
            int flowControlSel = (comboBox6.SelectedIndex);

            // Make sure selections are valid
            if (dataBitsSel != CB_ERR &&
                paritySel != CB_ERR &&
                stopBitsSel != CB_ERR &&
                flowControlSel != CB_ERR)
            {
                baudRate = m_baudRate;
                dataBits = Convert.ToByte(dataBitsSel + CP2110_DLL.HID_UART_EIGHT_DATA_BITS);
                parity = Convert.ToByte(paritySel + CP2110_DLL.HID_UART_NO_PARITY);
                stopBits = Convert.ToByte(stopBitsSel + CP2110_DLL.HID_UART_SHORT_STOP_BIT);
                flowControl = Convert.ToByte(flowControlSel + CP2110_DLL.HID_UART_NO_FLOW_CONTROL);

                //m_comboDeviceList.GetWindowText(serial);
                if (comboBox1.SelectedItem != null)
                    serial = comboBox1.SelectedItem.ToString();
                //serial = "00086338";
                else
                    serial = "";




                if (serial.Length > 0)
                {
                    return 1;
                }
            }
            //}

            return 0;
        }


        // Connect to the device with the specified serial string and configure the UART
        // - Search for device with matching serial string
        // - Open matching device
        // - Get the device part number and version
        // - Configure device UART settings
        // - Output any error messages
        // - Display "Not Connected" or "Connected to ..." in the status bar
        int Connect(String serial, uint baudRate, byte dataBits, byte parity, byte stopBits, byte flowControl)
        {
            int status = CP2110_DLL.HID_UART_DEVICE_NOT_FOUND;
            uint numDevices = 0;
            //status = CP2110_DLL.HidUart_GetNumDevices(ref numDevices, VID, PID);
            if (CP2110_DLL.HidUart_GetNumDevices(ref numDevices, VID, PID) == CP2110_DLL.HID_UART_SUCCESS)
            {
                for (uint i = 0; i < numDevices; i++)
                {
                    StringBuilder deviceString = new StringBuilder(260);


                    // Search through all HID devices for a matching serial string
                    status = CP2110_DLL.HidUart_GetString(i, VID, PID, deviceString, CP2110_DLL.HID_UART_GET_SERIAL_STR);
                    if (status == CP2110_DLL.HID_UART_SUCCESS)
                    {
                        // Found a matching device
                        if (serial == deviceString.ToString())
                        {
                            // Open the device
                            status = CP2110_DLL.HidUart_Open(ref m_hidUart, i, VID, PID);
                            break;
                        }
                    }
                }
            }

            // Found and opened the device
            if (status == CP2110_DLL.HID_UART_SUCCESS)
            {
                // Get part number and version
                status = CP2110_DLL.HidUart_GetPartNumber(m_hidUart, ref m_partNumber, ref m_version);
            }

            // Got part number
            if (status == CP2110_DLL.HID_UART_SUCCESS)
            {
                // Configure the UART
                status = CP2110_DLL.HidUart_SetUartConfig(m_hidUart, baudRate, dataBits, parity, stopBits, flowControl);
            }

            // Confirm UART settings
            if (status == CP2110_DLL.HID_UART_SUCCESS)
            {
                uint vBaudRate = 0;
                byte vDataBits = 0;
                byte vParity = 0;
                byte vStopBits = 0;
                byte vFlowControl = 0;

                status = CP2110_DLL.HidUart_GetUartConfig(m_hidUart, ref vBaudRate, ref vDataBits, ref vParity, ref vStopBits, ref vFlowControl);

                if (status == CP2110_DLL.HID_UART_SUCCESS)
                {
                    if (vBaudRate != baudRate ||
                        vDataBits != dataBits ||
                        vParity != parity ||
                        vStopBits != stopBits ||
                        vFlowControl != flowControl)
                    {
                        status = CP2110_DLL.HID_UART_INVALID_PARAMETER;
                    }
                }
            }

            // Configured the UART
            if (status == CP2110_DLL.HID_UART_SUCCESS)
            {
                // Set short read timeouts for periodic read timer
                // Set longer write timeouts for user transmits
                status = CP2110_DLL.HidUart_SetTimeouts(m_hidUart, READ_TIMEOUT, WRITE_TIMEOUT);
            }

            // Fully connected to the device
            if (status == CP2110_DLL.HID_UART_SUCCESS)
            {
                // Output the connection status to the status bar
                String statusMsg;
                statusMsg = "Connected to " + serial;
                //SetStatusText(statusMsg);
            }
            // Connect failed
            else
            {
                // Disconnect
                CP2110_DLL.HidUart_Close(m_hidUart);

                // Notify the user that an error occurred
                String msg;
                msg = "Failed to connect to " + serial + ": " + GetHidUartStatusStr(status);
                MessageBox.Show(msg);

                // Update status
                //SetStatusText(_T("Not Connected"));
            }

            // Return TRUE if the device was opened successfully
            return ((status == CP2110_DLL.HID_UART_SUCCESS) ? 1 : 0);
        }


        // Show/hide the CLK Output controls:
        // - CLK Output labels
        // - CLK Output edit box
        void ShowClkOutputCtrls(int show)
        {
            // Show/hide the CLK Output labels
            //GetDlgItem(IDC_STATIC_CLK_OUTPUT)->ShowWindow(show);
            //GetDlgItem(IDC_STATIC_CLK_OUTPUT_SUFFIX)->ShowWindow(show);

            // Show hide the CLK Output edit box
            //GetDlgItem(IDC_EDIT_CLK_OUTPUT)->ShowWindow(show);
        }



        // Calculate and display the CLK output speed in Hz
        // CLK = 24000000 Hz / (2 x clkDiv)
        // If clkDiv = 0, then CLK = 24000000 Hz
        void UpdateClkOutputSpeed(byte clkDiv)
        {
            String clk;

            if (clkDiv == 0)
            {
                clk = "24000000";
            }
            else
            {
                clk = Convert.ToString(24000000 / (2 * clkDiv));
            }

            //SetDlgItemText(IDC_EDIT_CLK_OUTPUT, clk);
        }


        // Get the GPIO configuration from the device (input, output, function mode)
        // Set GPIO checkbox properites:
        //   Input		- Tri-state = False,	Disabled = True
        //   Output		- Tri-state = False,	Disabled = False
        //   Function	- Tri-state = True,		Disabled = True
        int UpdateGpioButtonProperties()
        {
            int status;

            byte[] pinConfig = new byte[13];
            int useSuspendValues = 0;
            ushort suspendValue = 0;
            ushort suspendMode = 0;
            byte rs485Level = 0;
            byte clkDiv = 0;

            if ((status = CP2110_DLL.HidUart_GetPinConfig(m_hidUart, pinConfig, ref useSuspendValues, ref suspendValue, ref suspendMode, ref rs485Level, ref clkDiv)) == CP2110_DLL.HID_UART_SUCCESS)
            {
                //for (int i = 0; i < 10; i++)
                //{
                //    //CButton* pBtn		= (CButton*)GetDlgItem(i + IDC_CHECK_GPIO0);
                //    //CStatic* pCaption	= (CStatic*)GetDlgItem(i + IDC_STATIC_GPIO0);

                //    // Input
                //    if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                //    {
                //        //pBtn->SetButtonStyle(BS_AUTOCHECKBOX);
                //        //pBtn->SetCheck(BST_UNCHECKED);
                //        //pBtn->EnableWindow(FALSE);
                //        //pCaption->EnableWindow(TRUE);
                //    }
                //    // Output
                //    else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                //             pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                //    {
                //        //pBtn->SetButtonStyle(BS_AUTOCHECKBOX);
                //        //pBtn->SetCheck(BST_UNCHECKED);
                //        //pBtn->EnableWindow(TRUE);
                //        //pCaption->EnableWindow(TRUE);
                //    }
                //    // Function
                //    else
                //    {
                //        //pBtn->SetButtonStyle(BS_AUTO3STATE);
                //        //pBtn->SetCheck(BST_INDETERMINATE);
                //        //pBtn->EnableWindow(FALSE);
                //        //pCaption->EnableWindow(FALSE);
                //    }

                //    // Redraw the button with the new button style
                //    //pBtn->Invalidate();
                //}

                int i;
                i = 0;
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    //pBtn->SetButtonStyle(BS_AUTOCHECKBOX);
                    //pBtn->SetCheck(BST_UNCHECKED);
                    //pBtn->EnableWindow(FALSE);
                    //pCaption->EnableWindow(TRUE);
                    //checkBox1.Checked = false;
                    checkBox1.Enabled = false;
                    checkBox1.CheckState = CheckState.Unchecked;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    //pBtn->SetButtonStyle(BS_AUTOCHECKBOX);
                    //pBtn->SetCheck(BST_UNCHECKED);
                    //pBtn->EnableWindow(TRUE);
                    //pCaption->EnableWindow(TRUE);
                    //checkBox1.Checked = true;
                    checkBox1.Enabled = true;
                    checkBox1.CheckState = CheckState.Checked;
                }
                // Function
                else
                {
                    //pBtn->SetButtonStyle(BS_AUTO3STATE);
                    //pBtn->SetCheck(BST_INDETERMINATE);
                    //pBtn->EnableWindow(FALSE);
                    //pCaption->EnableWindow(FALSE);
                    checkBox1.CheckState = CheckState.Indeterminate;
                    checkBox1.Enabled = false;

                }

                i = 1;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox2.CheckState = CheckState.Unchecked;
                    checkBox2.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox2.CheckState = CheckState.Checked;
                    checkBox2.Enabled = true;
                }
                // Function
                else
                {
                    checkBox2.CheckState = CheckState.Indeterminate;
                    checkBox2.Enabled = false;
                }


                i = 2;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox3.CheckState = CheckState.Unchecked;
                    checkBox3.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox3.CheckState = CheckState.Checked;
                    checkBox3.Enabled = true;
                }
                // Function
                else
                {
                    checkBox3.CheckState = CheckState.Indeterminate;
                    checkBox3.Enabled = false;
                }


                i = 3;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox4.CheckState = CheckState.Unchecked;
                    checkBox4.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox4.CheckState = CheckState.Checked;
                    checkBox4.Enabled = true;
                }
                // Function
                else
                {
                    checkBox4.CheckState = CheckState.Indeterminate;
                    checkBox4.Enabled = false;
                }


                i = 4;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox5.CheckState = CheckState.Unchecked;
                    checkBox5.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox5.CheckState = CheckState.Checked;
                    checkBox5.Enabled = true;
                }
                // Function
                else
                {
                    checkBox5.CheckState = CheckState.Indeterminate;
                    checkBox5.Enabled = false;
                }


                i = 5;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox6.CheckState = CheckState.Unchecked;
                    checkBox6.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox6.CheckState = CheckState.Checked;
                    checkBox6.Enabled = true;
                }
                // Function
                else
                {
                    checkBox6.CheckState = CheckState.Indeterminate;
                    checkBox6.Enabled = false;
                }


                i = 6;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox7.CheckState = CheckState.Unchecked;
                    checkBox7.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox7.CheckState = CheckState.Checked;
                    checkBox7.Enabled = true;
                }
                // Function
                else
                {
                    checkBox7.CheckState = CheckState.Indeterminate;
                    checkBox7.Enabled = false;
                }


                i = 7;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox8.CheckState = CheckState.Unchecked;
                    checkBox8.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox8.CheckState = CheckState.Checked;
                    checkBox8.Enabled = true;
                }
                // Function
                else
                {
                    checkBox8.CheckState = CheckState.Indeterminate;
                    checkBox8.Enabled = false;
                }


                i = 8;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox9.CheckState = CheckState.Unchecked;
                    checkBox9.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox9.CheckState = CheckState.Checked;
                    checkBox9.Enabled = true;
                }
                // Function
                else
                {
                    checkBox9.CheckState = CheckState.Indeterminate;
                    checkBox9.Enabled = false;
                }


                i = 9;
                // Input
                if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_INPUT)
                {
                    checkBox10.CheckState = CheckState.Unchecked;
                    checkBox10.Enabled = false;
                }
                // Output
                else if (pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_OD ||
                         pinConfig[i] == CP2110_DLL.HID_UART_GPIO_MODE_OUTPUT_PP)
                {
                    checkBox10.CheckState = CheckState.Checked;
                    checkBox10.Enabled = true;
                }
                // Function
                else
                {
                    checkBox10.CheckState = CheckState.Indeterminate;
                    checkBox10.Enabled = false;
                }











                // GPIO.0 is configured as CLK output
                if (pinConfig[CP2110_DLL.HID_UART_INDEX_GPIO_0_CLK] == CP2110_DLL.HID_UART_GPIO_MODE_FUNCTION)
                {
                    // Show CLK Output controls
                    // (controls are disabled by default in Disconnect())
                    ShowClkOutputCtrls(1);
                }

                // Calculate and display the CLK output speed in Hz
                UpdateClkOutputSpeed(clkDiv);
            }

            return status;
        }


        // Set the GPIO checkbox check state
        void UpdateGpioState(int nID, ushort bit)
        {
            //CButton* pBtn = (CButton*)GetDlgItem(nID);

            // If the pin is in function mode, then the
            // checkbox state is indeterminate
            //if (pBtn->GetButtonStyle() == BS_AUTO3STATE)
            {
                //pBtn->SetCheck(BST_INDETERMINATE);
            }
            //else
            {
                // Latch bit value is 1
                //if (bit)
                {
                    //pBtn->SetCheck(BST_CHECKED);
                }
                // Latch bit value is 0
                //else
                {
                    //pBtn->SetCheck(BST_UNCHECKED);
                }
            }
        }


        void GetLatch(int silent)
        {
            int status;
            ushort latchValue = 0x0000;
            int opened = 0;

            // Check if the device is opened
            if (CP2110_DLL.HidUart_IsOpened(m_hidUart, ref opened) == CP2110_DLL.HID_UART_SUCCESS && opened != 0)
            {
                // Retrieve the GPIO latch values
                status = CP2110_DLL.HidUart_ReadLatch(m_hidUart, ref latchValue);

                if (status == CP2110_DLL.HID_UART_SUCCESS)
                {
                    // Update GPIO checkboxes
                    //UpdateGpioState(IDC_CHECK_GPIO0, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_0_CLK);
                    checkBox1.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_0_CLK);
                    //UpdateGpioState(IDC_CHECK_GPIO1, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_1_RTS);
                    checkBox2.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_1_RTS);
                    //UpdateGpioState(IDC_CHECK_GPIO2, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_2_CTS);
                    checkBox3.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_2_CTS);
                    //UpdateGpioState(IDC_CHECK_GPIO3, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_3_RS485);
                    checkBox4.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_3_RS485);
                    //UpdateGpioState(IDC_CHECK_GPIO4, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_4_TX_TOGGLE);
                    checkBox5.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_4_TX_TOGGLE);
                    //UpdateGpioState(IDC_CHECK_GPIO5, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_5_RX_TOGGLE);
                    checkBox6.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_5_RX_TOGGLE);
                    //UpdateGpioState(IDC_CHECK_GPIO6, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_6);
                    checkBox7.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_6);
                    //UpdateGpioState(IDC_CHECK_GPIO7, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_7);
                    checkBox8.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_7);
                    //UpdateGpioState(IDC_CHECK_GPIO8, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_8);
                    checkBox9.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_8);
                    //UpdateGpioState(IDC_CHECK_GPIO9, latchValue & CP2110_DLL.HID_UART_MASK_GPIO_9);
                    checkBox10.Checked = Convert.ToBoolean(latchValue & CP2110_DLL.HID_UART_MASK_GPIO_9);
                }
                else
                {
                    if (silent != 0)
                    {
                        // Notify the user that an error occurred
                        String msg;
                        msg = "Failed to read latch values: " + GetHidUartStatusStr(status);
                        //MessageBox(msg, 0, MB_ICONWARNING);
                        MessageBox.Show(msg);
                    }
                }
            }
        }



        // Set latch values based on dialog selections
        void SetLatch()
        {
            int status;
            ushort latchValue = 0x0000;
            ushort latchMask = 0xFFFF;
            int opened = 0;

            // Check if the device is opened
            if (CP2110_DLL.HidUart_IsOpened(m_hidUart, ref opened) == CP2110_DLL.HID_UART_SUCCESS && opened != 0)
            {
                // Set latch values based on dialog selection
                if (checkBox1.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_0_CLK;
                if (checkBox2.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_1_RTS;
                if (checkBox3.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_2_CTS;
                if (checkBox4.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_3_RS485;
                if (checkBox5.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_4_TX_TOGGLE;
                if (checkBox6.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_5_RX_TOGGLE;
                if (checkBox7.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_6;
                if (checkBox8.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_7;
                if (checkBox9.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_8;
                if (checkBox10.Checked) latchValue |= CP2110_DLL.HID_UART_MASK_GPIO_9;

                // Set latch values
                status = CP2110_DLL.HidUart_WriteLatch(m_hidUart, latchValue, latchMask);

                if (status != CP2110_DLL.HID_UART_SUCCESS)
                {
                    // Notify the user that an error occurred
                    String msg;
                    msg = "Failed to write latch values: " + GetHidUartStatusStr(status);
                    MessageBox.Show(msg);
                }
            }
        }




        // Retrieve device information strings and display on the dialog
        void UpdateDeviceInformation()
        {
            StringBuilder deviceString = new StringBuilder(260);
            int opened = 0;

            // Clear all strings
            //SetDlgItemText(IDC_EDIT_VID, _T(""));
            //SetDlgItemText(IDC_EDIT_PID, _T(""));
            //SetDlgItemText(IDC_EDIT_SERIAL, _T(""));
            //SetDlgItemText(IDC_EDIT_PATH, _T(""));
            //SetDlgItemText(IDC_EDIT_PART_NUMBER, _T(""));
            //SetDlgItemText(IDC_EDIT_VERSION, _T(""));
            //SetDlgItemText(IDC_EDIT_MFR, _T(""));
            //SetDlgItemText(IDC_EDIT_PRODUCT, _T(""));
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

            // Update VID string
            if (CP2110_DLL.HidUart_GetOpenedString(m_hidUart, deviceString, CP2110_DLL.HID_UART_GET_VID_STR) == CP2110_DLL.HID_UART_SUCCESS)
            {
                //SetDlgItemText(IDC_EDIT_VID, CString(deviceString));
                textBox1.Text = deviceString.ToString();
            }

            // Update PID string
            if (CP2110_DLL.HidUart_GetOpenedString(m_hidUart, deviceString, CP2110_DLL.HID_UART_GET_PID_STR) == CP2110_DLL.HID_UART_SUCCESS)
            {
                //SetDlgItemText(IDC_EDIT_PID, CString(deviceString));
                textBox2.Text = deviceString.ToString();
            }

            // Update serial string
            if (CP2110_DLL.HidUart_GetOpenedString(m_hidUart, deviceString, CP2110_DLL.HID_UART_GET_SERIAL_STR) == CP2110_DLL.HID_UART_SUCCESS)
            {
                //SetDlgItemText(IDC_EDIT_SERIAL, CString(deviceString));
                textBox3.Text = deviceString.ToString();
            }

            // Update device path string
            if (CP2110_DLL.HidUart_GetOpenedString(m_hidUart, deviceString, CP2110_DLL.HID_UART_GET_PATH_STR) == CP2110_DLL.HID_UART_SUCCESS)
            {
                //SetDlgItemText(IDC_EDIT_PATH, CString(deviceString));
                textBox4.Text = deviceString.ToString();
            }

            // Update manufacturer string
            if (CP2110_DLL.HidUart_GetOpenedString(m_hidUart, deviceString, CP2110_DLL.HID_UART_GET_MANUFACTURER_STR) == CP2110_DLL.HID_UART_SUCCESS)
            {
                //SetDlgItemText(IDC_EDIT_MFR, CString(deviceString));
                textBox7.Text = deviceString.ToString();
            }

            // Update product string
            if (CP2110_DLL.HidUart_GetOpenedString(m_hidUart, deviceString, CP2110_DLL.HID_UART_GET_PRODUCT_STR) == CP2110_DLL.HID_UART_SUCCESS)
            {
                //SetDlgItemText(IDC_EDIT_PRODUCT, CString(deviceString));
                textBox8.Text = deviceString.ToString();
            }

            // Update part number and version
            if (CP2110_DLL.HidUart_IsOpened(m_hidUart, ref opened) == CP2110_DLL.HID_UART_SUCCESS && opened != 0)
            {
                // Update part number and version
                String partNumStr;
                String versionStr;

                partNumStr = Convert.ToString(m_partNumber);
                versionStr = Convert.ToString(m_version);

                //SetDlgItemText(IDC_EDIT_PART_NUMBER, partNumStr);
                textBox5.Text = partNumStr;
                //SetDlgItemText(IDC_EDIT_VERSION, versionStr);
                textBox6.Text = versionStr;
            }
        }


        // Connect to the device with the serial string selected
        // in the device list using the device settings specified
        // on the dialog
        // - Read settings from the dialog controls
        // - Connect to the device specified in the device list
        // - Disable device settings controls
        // - Update the device information text boxes
        // - Set Connect checkbox/button caption and pressed state
        // - Start the read timer
        int Connect()
        {

            String serial = "";
            uint baudRate = 0;
            byte dataBits = 0;
            byte parity = 0;
            byte stopBits = 0;
            byte flowControl = 0;

            //int i1=GetConnectSettings(out serial, out baudRate, out dataBits, out parity, out stopBits, out flowControl);
            // Retrieve UART settings from the dialog
            if (GetConnectSettings(ref serial, ref baudRate, ref dataBits, ref parity, ref stopBits, ref flowControl) != 0)
            {

                // Connect to the device and configure the UART
                if (Connect(serial, baudRate, dataBits, parity, stopBits, flowControl) != 0)
                {
                    // Disable the device list, update button, and UART settings
                    EnableSettingsCtrls(FALSE);

                    // Get pin configuration and set checkbox properties
                    // accordingly
                    UpdateGpioButtonProperties();

                    // Update the latch values
                    GetLatch(TRUE);

                    // Update device information strings
                    UpdateDeviceInformation();

                    // Check the connect checkbox/button
                    //CheckDlgButton(IDC_CHECK_CONNECT, TRUE);

                    // Next button press should disconnect
                    //SetDlgItemText(IDC_CHECK_CONNECT, _T("&Disconnect"));
                    button1.Text = "&Disconnect";
                    // Start read timer to display received data in the receive window
                    StartReadTimer();

                    return TRUE;
                }
            }

            // Connect failed, uncheck the button
            //CheckDlgButton(IDC_CHECK_CONNECT, FALSE);

            // Next button press should connect
            //SetDlgItemText(IDC_CHECK_CONNECT, _T("&Connect"));
            button1.Text = "&Connect";
            return FALSE;
        }



        // Populate the device list combo box with connected device serial strings
        // - Save previous device serial string selection
        // - Fill the device list with connected device serial strings
        // - Restore previous device selection
        void UpdateDeviceList()
        {
            // Only update the combo list when the drop down list is closed
            //if (!m_comboDeviceList.GetDroppedState())
            if (!comboBox1.DroppedDown)
            {
                //vector<CString> oldDeviceList;
                //vector<CString> newDeviceList;
                ArrayList oldDeviceList = new ArrayList();
                ArrayList newDeviceList = new ArrayList();

                String itemText;
                String selText;
                uint numDevices = 0;
                StringBuilder deviceString = new StringBuilder(260);

                // Populate old device serial string list
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    //m_comboDeviceList.GetLBText(i, itemText);
                    itemText = comboBox1.Items[i].ToString();
                    //oldDeviceList.push_back(itemText);
                    oldDeviceList.Add(itemText);

                }

                // Populate new device serial string list
                if (CP2110_DLL.HidUart_GetNumDevices(ref numDevices, VID, PID) == CP2110_DLL.HID_UART_SUCCESS)
                {
                    for (uint i = 0; i < numDevices; i++)
                    {
                        if (CP2110_DLL.HidUart_GetString(i, VID, PID, deviceString, CP2110_DLL.HID_UART_GET_SERIAL_STR) == CP2110_DLL.HID_UART_SUCCESS)
                        {
                            //newDeviceList.push_back(CString(deviceString));
                            newDeviceList.Add(deviceString.ToString());
                        }
                    }
                }

                // Check if the device list needs to be updated
                //if ( !((object)oldDeviceList).Equals((object)newDeviceList) )
                if (!ArrayListEquals(oldDeviceList, newDeviceList))
                {
                    // Save old serial string selection
                    //m_comboDeviceList.GetWindowText(selText);
                    if (comboBox1.SelectedItem != null)
                    {
                        selText = comboBox1.SelectedItem.ToString();
                        //m_comboDeviceList.ResetContent();
                        comboBox1.Items.Clear();

                        // Update the device list
                        for (uint i = 0; i < newDeviceList.Count; i++)
                        {
                            //m_comboDeviceList.AddString(newDeviceList[i]);
                            comboBox1.Items.Add(newDeviceList[Convert.ToInt32(i)].ToString());
                        }

                        // Restore serial string selection
                        //if (m_comboDeviceList.SelectString(-1, selText) == CB_ERR)
                        if ((comboBox1.SelectedIndex = comboBox1.Items.IndexOf(selText)) == CB_ERR)
                        {
                            //m_comboDeviceList.SetCurSel(0);
                            comboBox1.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        comboBox1.Items.Clear();

                        // Update the device list
                        for (uint i = 0; i < newDeviceList.Count; i++)
                        {
                            //m_comboDeviceList.AddString(newDeviceList[i]);
                            comboBox1.Items.Add(newDeviceList[Convert.ToInt32(i)].ToString());
                        }

                        comboBox1.SelectedIndex = 0;
                    }

                }
            }
        }


        bool ArrayListEquals(ArrayList list1, ArrayList list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (!(list1[i].Equals(list2[i])))
                    return false;
            }
            return true;
        }


        // Toggle the device connection (Connect/Disconnect)
        private void button1_Click(object sender, EventArgs e)
        {

            // Connecting
            //if (IsDlgButtonChecked(IDC_CHECK_CONNECT))
            if (button1.Text == "&Connect")
            {
                Connect();
            }
            // Disconnecting
            else
            {
                Disconnect();
            }




        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        // Remove whitespace and invalid characters from a string
        // and return
        String CleanHexString(ref String editStr)
        {
            String cleanStr = editStr;

            // Remove spaces
            cleanStr.Replace((" "), (""));

            // Remove commas
            cleanStr.Replace((","), (""));

            // Remove carriage returns and newlines
            cleanStr.Replace(("\r"), (""));
            cleanStr.Replace(("\n"), (""));

            // Only parse text including valid hex characters
            // (stop at the first invalid character)
            //cleanStr = cleanStr.SpanIncluding(("0123456789abcdefABCDEF"));


            // Odd number of characters
            if (cleanStr.Length % 2 == 1)
            {
                // Insert a "0" before the last character to make a complete set of hex bytes (two characters each)
                //cleanStr = cleanStr.Left(cleanStr.GetLength() - 1) + _T("0") + cleanStr[cleanStr.GetLength() -1];
                cleanStr.Remove(cleanStr.Length - 1);
            }

            return cleanStr;
        }


        // Transmit entered text using ASCII or hex format
        private void button4_Click(object sender, EventArgs e)
        {
            int opened = 0;

            // Check if the device is opened
            if (CP2110_DLL.HidUart_IsOpened(m_hidUart, ref opened) == CP2110_DLL.HID_UART_SUCCESS && opened != 0)
            {
                String transmitStr;

                // Get transmit edit text
                //GetDlgItemText(IDC_EDIT_TRANSMIT, transmitStr);
                transmitStr = textBox9.Text;

                // String is empty
                if (String.IsNullOrEmpty(transmitStr))
                {
                    // Do nothing
                    return;
                }

                // Interpret edit text as ASCII
                if (radioButton1.Checked)
                {
                    int status;
                    uint numBytesWritten = 0;
                    uint numBytesToWrite = Convert.ToUInt32(transmitStr.Length);
                    byte[] buffer = new byte[numBytesToWrite];

                    // Copy ASCII values to the new array
                    for (uint i = 0; i < numBytesToWrite; i++)
                    {
                        // Use the LSB of the 2-byte unicode value
                        buffer[i] = (byte)transmitStr[(int)i];
                    }

                    // Send the UART data to the device to transmit
                    status = CP2110_DLL.HidUart_Write(m_hidUart, buffer, numBytesToWrite, ref numBytesWritten);

                    // Notify the user that an error occurred
                    if (status != CP2110_DLL.HID_UART_SUCCESS)
                    {
                        String msg;
                        msg = "Failed to transmit : " + GetHidUartStatusStr(status);
                        MessageBox.Show(msg);
                    }

                    //delete[] buffer;
                    //buffer.

                }
                // Interpret edit text as hex
                else
                {
                    // //Remove invalid hex characters
                    //transmitStr = CleanHexString(ref transmitStr);

                    //if (!String.IsNullOrEmpty(transmitStr))
                    //{
                    //    int status;
                    //    uint numBytesWritten = 0;
                    //    uint numBytesToWrite = Convert.ToUInt32(transmitStr.Length / 2);
                    //    byte[] buffer = new byte[numBytesToWrite];

                    //    // Convert each hex byte string to a BYTE value
                    //    for (uint i = 0; i < numBytesToWrite; i++)
                    //    {
                    //        String hexByte = transmitStr.Mid(i * 2, 2);
                    //        buffer[i] = (byte)_tcstol(hexByte, NULL, 16);
                    //    }

                    //    // Separate each hex pair with a space
                    //    for (int i = transmitStr.GetLength() - 2; i >= 2; i -= 2)
                    //    {
                    //        transmitStr.Insert(i, _T(" "));
                    //    }

                    //    // Set transmit window text to the cleaned up string
                    //    SetDlgItemText(IDC_EDIT_TRANSMIT, transmitStr);

                    //    // Send the UART data to the device to transmit
                    //    status = CP2110_DLL.HidUart_Write(m_hidUart, buffer, numBytesToWrite, &numBytesWritten);

                    //    // Notify the user that an error occurred
                    //    if (status != CP2110_DLL.HID_UART_SUCCESS)
                    //    {
                    //        CString msg;
                    //        msg.Format(_T("Failed to transmit : %s"), GetHidUartStatusStr(status));
                    //        MessageBox(msg, 0, MB_ICONWARNING);
                    //    }

                    //    delete[] buffer;
                    //}


                    int discard;
                    byte[] RCPpkt;

                    ByteBuilder bb = new ByteBuilder();

                    RCPpkt = HexEncoding.GetBytes(transmitStr, out discard);
                    bb.Append(RCPpkt);
                    bb.Append(CRCCalculator.Cal_CRC16(RCPpkt));
                    //tb_debug_CRC.Text = String.Format("{0:X}", CRCCalculator.Cal_CRC16(RCPpkt));

                    uint numBytesWritten = 0;
                    uint numBytesToWrite = Convert.ToUInt32(bb.Length);

                    int status;
                    //uint numBytesWritten = 0;
                    //uint numBytesToWrite = Convert.ToUInt32(transmitStr.Length);
                    byte[] buffer = bb.GetByteArray();

                    //// Copy ASCII values to the new array
                    //for (uint i = 0; i < numBytesToWrite; i++)
                    //{
                    //    // Use the LSB of the 2-byte unicode value
                    //    buffer[i] = (byte)transmitStr[(int)i];
                    //}

                    // Send the UART data to the device to transmit
                    status = CP2110_DLL.HidUart_Write(m_hidUart, buffer, numBytesToWrite, ref numBytesWritten);

                    // Notify the user that an error occurred
                    if (status != CP2110_DLL.HID_UART_SUCCESS)
                    {
                        String msg;
                        msg = "Failed to transmit : " + GetHidUartStatusStr(status);
                        MessageBox.Show(msg);
                    }



                }
            }

        }















        // Start the read timer which displays received UART
        // data in the receive window
        void StartReadTimer()
        {
            //SetTimer(READ_TIMER_ID, READ_TIMER_ELAPSE, NULL);
            timer1.Interval = Convert.ToInt32(READ_TIMER_ELAPSE);
            timer1.Start();
        }

        // Stop the read timer which displays received UART
        // data in the receive window
        void StopReadTimer()
        {
            //KillTimer(READ_TIMER_ID);
            timer1.Stop();
        }


        // Periodically call read and append to receive window
        private void timer1_Tick(object sender, EventArgs e)
        {
            ReceiveData();

        }


        // Receive UART data from the device and output to the receive window
        void ReceiveData()
        {
            int status;
            uint numBytesRead = 0;
            uint numBytesToRead = READ_SIZE;
            byte[] buffer = new byte[numBytesToRead];

            // Receive UART data from the device (up to 1000 bytes)
            status = CP2110_DLL.HidUart_Read(m_hidUart, buffer, numBytesToRead, ref numBytesRead);

            // HidUart_Read returns HID_UART_SUCCESS if numBytesRead == numBytesToRead
            // and returns HID_UART_READ_TIMED_OUT if numBytesRead < numBytesToRead
            if (status == CP2110_DLL.HID_UART_SUCCESS || status == CP2110_DLL.HID_UART_READ_TIMED_OUT)
            {
                // Output received data to the receive window
                if (numBytesRead > 0)
                {
                    //m_hexEditReceive.AppendWindowHex(buffer, numBytesRead);

                    //for(uint i=0;i<numBytesRead;i++)
                    //textBox10.AppendText(buffer[i].ToString()+" ");
                    String str;

                    if (radioButton1.Checked)
                    {
                        str = System.Text.Encoding.ASCII.GetString(buffer);
                        //str = System.Text.Encoding.Default.GetString(buffer);
                    }
                    else
                    {
                        ByteBuilder bb=new ByteBuilder(buffer);
                        str = HexEncoding.ToString(bb.GetByteArray(0,(int)numBytesRead));
                    }

                    textBox10.AppendText(str + "\n");

                }
            }

            //delete [] buffer;
        }

        // Clear the receive window
        private void button5_Click(object sender, EventArgs e)
        {
            textBox10.Clear();
        }




        // Disconnect from the currently connected device
        // - Stop the read timer
        // - Disconnect from the current device
        // - Output any error messages
        // - Display "Not Connected" in the status bar
        // - Re-enable all device settings controls
        // - Update the device information text boxes (clear text)
        // - Set Connect checkbox/button caption and pressed state
        int Disconnect()
        {
            // Stop read timer
            StopReadTimer();

            // Close the device
            int status = CP2110_DLL.HidUart_Close(m_hidUart);

            // Disconnect failed
            if (status != CP2110_DLL.HID_UART_SUCCESS)
            {
                // Notify the user that an error occurred
                String msg;
                msg = "Failed to disconnect: " + GetHidUartStatusStr(status);
                MessageBox.Show(msg);
            }

            // Output the disconnect status to the status bar
            //SetStatusText(_T("Not Connected"));

            // Re-enable the device list, update button, and UART settings
            EnableSettingsCtrls(TRUE);

            // Hide CLK Output controls
            ShowClkOutputCtrls(FALSE);

            // Update device information strings
            UpdateDeviceInformation();

            // Uncheck the connect checkbox/button
            //CheckDlgButton(IDC_CHECK_CONNECT, FALSE);

            // Next button press should connect
            //SetDlgItemText(IDC_CHECK_CONNECT, _T("&Connect"));
            button1.Text = "&Connect";

            // Return TRUE if the device was closed successfully
            return ((status == CP2110_DLL.HID_UART_SUCCESS) ? 1 : 0);
        }


        // Enable/disable the device settings controls:
        // - Device list
        // - Update button
        // - Baud rate edit box
        // - Data bits, parity, stop bits, and flow control combo boxes
        void EnableSettingsCtrls(int enable)
        {
            // Enable/disable the device list, update button, and UART settings
            //m_comboDeviceList.EnableWindow(enable);
            comboBox1.Enabled = Convert.ToBoolean(enable);
            //GetDlgItem(IDC_EDIT_BAUD_RATE)->EnableWindow(enable);
            comboBox2.Enabled = Convert.ToBoolean(enable);
            //GetDlgItem(IDC_COMBO_DATA_BITS)->EnableWindow(enable);
            comboBox3.Enabled = Convert.ToBoolean(enable);
            //GetDlgItem(IDC_COMBO_PARITY)->EnableWindow(enable);
            comboBox4.Enabled = Convert.ToBoolean(enable);
            //GetDlgItem(IDC_COMBO_STOP_BITS)->EnableWindow(enable);
            comboBox5.Enabled = Convert.ToBoolean(enable);
            //GetDlgItem(IDC_COMBO_FLOW_CONTROL)->EnableWindow(enable);
            comboBox6.Enabled = Convert.ToBoolean(enable);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetLatch(FALSE);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SetLatch();
        }









    }
}
