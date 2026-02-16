using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class StartJobForm : Form
    {
        private TextBox txtName;
        private TextBox txtPhone;
        private TextBox txtLicense;
        private TextBox txtVin;
        private DataGridView gridCustomers;
        private Button btnNewCustomer;
        private Button btnCancel;
        private Button btnOK;

        public StartJobForm()
        {
            InitializeComponent();
            LoadPlaceholderData();
        }

        private void InitializeComponent()
        {
            this.Text = "Start New Job";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // --- 1. Top Search Section ---
            var pnlSearch = new GroupBox();
            pnlSearch.Text = "Search Customer / Vehicle";
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Height = 80;

            var lblName = new Label { Text = "Name:", Location = new Point(20, 30), AutoSize = true };
            txtName = new TextBox { Location = new Point(65, 27), Width = 150 };

            var lblPhone = new Label { Text = "Phone:", Location = new Point(230, 30), AutoSize = true };
            txtPhone = new TextBox { Location = new Point(280, 27), Width = 100 };

            var lblLicense = new Label { Text = "License:", Location = new Point(400, 30), AutoSize = true };
            txtLicense = new TextBox { Location = new Point(455, 27), Width = 100 };

            var lblVin = new Label { Text = "VIN:", Location = new Point(570, 30), AutoSize = true };
            txtVin = new TextBox { Location = new Point(605, 27), Width = 150 };

            pnlSearch.Controls.AddRange(new Control[] { lblName, txtName, lblPhone, txtPhone, lblLicense, txtLicense, lblVin, txtVin });

            // --- 2. Bottom Button Section ---
            var pnlButtons = new Panel();
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Height = 60;

            btnNewCustomer = new Button { Text = "New Customer", Location = new Point(20, 15), Width = 120, Height = 30 };
            btnNewCustomer.Click += (s, e) => { this.DialogResult = DialogResult.Retry; this.Close(); };
            btnOK = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(580, 15), Width = 90, Height = 30 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(680, 15), Width = 90, Height = 30 };

            pnlButtons.Controls.AddRange(new Control[] { btnNewCustomer, btnOK, btnCancel });

            // --- 3. Grid Section ---
            gridCustomers = new DataGridView();
            gridCustomers.Dock = DockStyle.Fill;
            gridCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCustomers.AllowUserToAddRows = false;
            gridCustomers.RowHeadersVisible = false;
            gridCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCustomers.BackgroundColor = SystemColors.Window;
            gridCustomers.BorderStyle = BorderStyle.Fixed3D;

            gridCustomers.Columns.Add("LastName", "Last Name");
            gridCustomers.Columns.Add("FirstName", "First Name");
            
            // The ComboBox column for vehicles
            var vehicleCol = new DataGridViewComboBoxColumn();
            vehicleCol.HeaderText = "Select Vehicle";
            vehicleCol.Name = "Vehicles";
            vehicleCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            vehicleCol.FlatStyle = FlatStyle.Flat;
            gridCustomers.Columns.Add(vehicleCol);

            // Add controls (Order matters for Docking: Add Fill first, then Top/Bottom)
            this.Controls.Add(gridCustomers); 
            this.Controls.Add(pnlButtons);
            this.Controls.Add(pnlSearch);
        }

        private void LoadPlaceholderData()
        {
            // Example: Adding a customer with specific vehicles in the dropdown
            int rowIndex = gridCustomers.Rows.Add("Doe", "John");
            var row = gridCustomers.Rows[rowIndex];
            var comboCell = (DataGridViewComboBoxCell)row.Cells["Vehicles"];
            comboCell.Items.AddRange("2015 Ford F-150", "2012 Toyota Camry");
            comboCell.Value = "2015 Ford F-150"; // Default selection

            rowIndex = gridCustomers.Rows.Add("Smith", "Jane");
            row = gridCustomers.Rows[rowIndex];
            comboCell = (DataGridViewComboBoxCell)row.Cells["Vehicles"];
            comboCell.Items.AddRange("2018 Honda Civic");
            comboCell.Value = "2018 Honda Civic";
        }
    }
}