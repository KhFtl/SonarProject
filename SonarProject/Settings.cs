using System.IO.Ports;

namespace SonarProject;

public partial class Settings : Form
{
    public Settings()
    {
        InitializeComponent();
        comboBoxBaudRate.SelectedItem = "9600";
        RefreshPortList();
    }

    private void RefreshPortList()
    {
        comboBoxPorts.Items.Clear();
        string[] ports = SerialPort.GetPortNames();
        if (ports.Length > 0)
        {
            comboBoxPorts.Items.AddRange(ports);
            comboBoxPorts.SelectedIndex = 0;
        }
    }

    private void buttonRefresh_Click(object sender, EventArgs e)
    {
        RefreshPortList();
    }

    private void buttonStart_Click(object sender, EventArgs e)
    {
        string selectedPort = comboBoxPorts.SelectedItem as string;
        if (string.IsNullOrWhiteSpace(selectedPort))
        {
            MessageBox.Show("Будь ласка, виберіть або підключіть COM-порт!", "Попередження",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int baudRate = 9600;
        if (comboBoxBaudRate.SelectedItem != null && int.TryParse(comboBoxBaudRate.SelectedItem.ToString(), out int parsedBaud))
        {
            baudRate = parsedBaud;
        }

        Hide();
        Main mainForm = new Main(selectedPort, baudRate);
        mainForm.FormClosed += (s, args) => Close();
        mainForm.Show();
    }
}