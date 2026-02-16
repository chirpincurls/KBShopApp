using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class VehicleForm : Form
    {
        private TextBox txtLicense;
        private ComboBox cmbYear;
        private ComboBox cmbMake;
        private ComboBox cmbModel;
        private TextBox txtUnit;
        private TextBox txtVin;
        private Button btnSave;
        private Button btnCancel;

        public VehicleForm()
        {
            InitializeComponent();
            LoadYears();
        }

        private void InitializeComponent()
        {
            this.Text = "Vehicle Details";
            this.Size = new Size(400, 380);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int xLabel = 20;
            int xInput = 100;
            int yStart = 20;
            int yStep = 40;
            int inputWidth = 240;

            // 1. License
            var lblLicense = new Label { Text = "License:", Location = new Point(xLabel, yStart + 3), AutoSize = true };
            txtLicense = new TextBox { Location = new Point(xInput, yStart), Width = inputWidth };

            // 2. Year
            var lblYear = new Label { Text = "Year:", Location = new Point(xLabel, yStart + yStep + 3), AutoSize = true };
            cmbYear = new ComboBox { Location = new Point(xInput, yStart + yStep), Width = 100, DropDownStyle = ComboBoxStyle.DropDown };
            cmbYear.SelectedIndexChanged += CmbYear_SelectedIndexChanged;

            // 3. Make
            var lblMake = new Label { Text = "Make:", Location = new Point(xLabel, yStart + yStep * 2 + 3), AutoSize = true };
            cmbMake = new ComboBox { Location = new Point(xInput, yStart + yStep * 2), Width = inputWidth, DropDownStyle = ComboBoxStyle.DropDown };
            cmbMake.SelectedIndexChanged += CmbMake_SelectedIndexChanged;

            // 4. Model
            var lblModel = new Label { Text = "Model:", Location = new Point(xLabel, yStart + yStep * 3 + 3), AutoSize = true };
            cmbModel = new ComboBox { Location = new Point(xInput, yStart + yStep * 3), Width = inputWidth, DropDownStyle = ComboBoxStyle.DropDown };

            // 5. Unit
            var lblUnit = new Label { Text = "Unit:", Location = new Point(xLabel, yStart + yStep * 4 + 3), AutoSize = true };
            txtUnit = new TextBox { Location = new Point(xInput, yStart + yStep * 4), Width = inputWidth };

            // 6. VIN
            var lblVin = new Label { Text = "VIN:", Location = new Point(xLabel, yStart + yStep * 5 + 3), AutoSize = true };
            txtVin = new TextBox { Location = new Point(xInput, yStart + yStep * 5), Width = inputWidth };

            // Buttons
            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new Point(160, yStart + yStep * 6 + 10), Width = 80 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(260, yStart + yStep * 6 + 10), Width = 80 };

            this.Controls.AddRange(new Control[] { 
                lblLicense, txtLicense, 
                lblYear, cmbYear, 
                lblMake, cmbMake, 
                lblModel, cmbModel, 
                lblUnit, txtUnit, 
                lblVin, txtVin, 
                btnSave, btnCancel 
            });
        }

        private void LoadYears()
        {
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear + 1; i >= 1950; i--)
            {
                cmbYear.Items.Add(i.ToString());
            }
        }

        private void CmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear subsequent fields
            cmbMake.Items.Clear();
            cmbMake.Text = string.Empty;
            cmbModel.Items.Clear();
            cmbModel.Text = string.Empty;

            // Populate Makes (Placeholder logic - replace with DB/API call)
            cmbMake.Items.AddRange(new string[] { "Ford", "Chevrolet", "Dodge", "Toyota", "Honda", "Nissan" });
        }

        private void CmbMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear subsequent fields
            cmbModel.Items.Clear();
            cmbModel.Text = string.Empty;

            // Populate Models (Placeholder logic - replace with DB/API call)
            string make = cmbMake.SelectedItem?.ToString();
            if (make == "Ford") cmbModel.Items.AddRange(new string[] { "F-150", "F-250", "Mustang", "Explorer", "Escape" });
            else if (make == "Chevrolet") cmbModel.Items.AddRange(new string[] { "Silverado", "Malibu", "Tahoe", "Suburban", "Corvette" });
            else if (make == "Dodge") cmbModel.Items.AddRange(new string[] { "Ram 1500", "Ram 2500", "Charger", "Challenger" });
            else if (make == "Toyota") cmbModel.Items.AddRange(new string[] { "Camry", "Corolla", "Tundra", "Tacoma", "RAV4" });
            else if (make == "Honda") cmbModel.Items.AddRange(new string[] { "Civic", "Accord", "CR-V", "Pilot" });
            else cmbModel.Items.AddRange(new string[] { "Model A", "Model B" });
        }
    }
}