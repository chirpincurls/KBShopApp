using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class NewCustomerForm : Form
    {
        private TextBox txtCompany;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtPhone1;
        private TextBox txtPhone2;
        private TextBox txtPhone3;
        private TextBox txtEmail;
        private DataGridView gridVehicles;
        private Button btnSave;
        private Button btnCancel;

        public NewCustomerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "New Customer";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // --- 1. Customer Details Section ---
            var pnlDetails = new GroupBox();
            pnlDetails.Text = "Customer Information";
            pnlDetails.Dock = DockStyle.Top;
            pnlDetails.Height = 200;

            // Row 1: Company
            var lblCompany = new Label { Text = "Company:", Location = new Point(20, 30), AutoSize = true };
            txtCompany = new TextBox { Location = new Point(100, 27), Width = 450 };

            // Row 2: Names
            var lblLastName = new Label { Text = "Last Name:", Location = new Point(20, 65), AutoSize = true };
            txtLastName = new TextBox { Location = new Point(100, 62), Width = 180 };

            var lblFirstName = new Label { Text = "First Name:", Location = new Point(290, 65), AutoSize = true };
            txtFirstName = new TextBox { Location = new Point(370, 62), Width = 180 };

            // Row 3: Phones
            var lblPhone1 = new Label { Text = "Phone 1:", Location = new Point(20, 100), AutoSize = true };
            txtPhone1 = new TextBox { Location = new Point(100, 97), Width = 100 };

            var lblPhone2 = new Label { Text = "Phone 2:", Location = new Point(210, 100), AutoSize = true };
            txtPhone2 = new TextBox { Location = new Point(270, 97), Width = 100 };

            var lblPhone3 = new Label { Text = "Phone 3:", Location = new Point(380, 100), AutoSize = true };
            txtPhone3 = new TextBox { Location = new Point(440, 97), Width = 100 };

            // Row 4: Email
            var lblEmail = new Label { Text = "Email:", Location = new Point(20, 135), AutoSize = true };
            txtEmail = new TextBox { Location = new Point(100, 132), Width = 450 };

            pnlDetails.Controls.AddRange(new Control[] { 
                lblCompany, txtCompany, 
                lblLastName, txtLastName, lblFirstName, txtFirstName,
                lblPhone1, txtPhone1, lblPhone2, txtPhone2, lblPhone3, txtPhone3,
                lblEmail, txtEmail 
            });

            // --- 2. Buttons Section ---
            var pnlButtons = new Panel();
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Height = 50;

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new Point(380, 10), Width = 90, Height = 30 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(480, 10), Width = 90, Height = 30 };

            pnlButtons.Controls.AddRange(new Control[] { btnSave, btnCancel });

            // --- 3. Vehicle Grid Section ---
            var pnlGrid = new GroupBox();
            pnlGrid.Text = "Vehicles";
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Padding = new Padding(10, 20, 10, 10); 

            gridVehicles = new DataGridView();
            gridVehicles.Dock = DockStyle.Fill;
            gridVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridVehicles.BackgroundColor = SystemColors.Window;
            gridVehicles.RowHeadersVisible = false;
            gridVehicles.AllowUserToAddRows = true; 

            gridVehicles.Columns.Add("Year", "Year");
            gridVehicles.Columns.Add("Make", "Make");
            gridVehicles.Columns.Add("Model", "Model");
            gridVehicles.Columns.Add("License", "License");
            gridVehicles.Columns.Add("VIN", "VIN");

            pnlGrid.Controls.Add(gridVehicles);

            // Add controls (Order matters for Docking: Add Fill first, then Top/Bottom)
            this.Controls.Add(pnlGrid);
            this.Controls.Add(pnlButtons);
            this.Controls.Add(pnlDetails);
        }
    }
}