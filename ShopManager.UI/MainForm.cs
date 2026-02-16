using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class MainForm : Form
    {
        private ToolStrip toolStrip;
        private ToolStripButton btnWIP;
        private ToolStripButton btnStart;
        private DataGridView gridWIP;

        public MainForm()
        {
            InitializeComponent();
            LoadPlaceholderData();
        }

        private void InitializeComponent()
        {
            this.Text = "Shop Manager";
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- 1. ToolStrip Setup ---
            toolStrip = new ToolStrip();
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            
            // Button: Work In Progress
            btnWIP = new ToolStripButton("Work In Progress");
            btnWIP.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            // Note: You can assign actual icons here later using Properties.Resources
            btnWIP.Image = SystemIcons.Application.ToBitmap(); 
            
            // Button: Start
            btnStart = new ToolStripButton("Start");
            btnStart.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            btnStart.Image = SystemIcons.Shield.ToBitmap();
            btnStart.Click += (s, e) => 
            {
                if (new StartJobForm().ShowDialog() == DialogResult.Retry)
                {
                    this.Controls.Remove(gridWIP);
                    var form = new NewCustomerForm();
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;
                    this.Controls.Add(form);
                    toolStrip.BringToFront();
                    form.Show();
                }
            };

            toolStrip.Items.Add(btnWIP);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(btnStart);

            // --- 2. DataGridView Setup ---
            gridWIP = new DataGridView();
            gridWIP.Dock = DockStyle.Fill;
            gridWIP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridWIP.AllowUserToAddRows = false; // Prevent the empty row at bottom
            gridWIP.ReadOnly = true;            // Read-only for the list view
            gridWIP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridWIP.RowHeadersVisible = false;  // Hide the row selector column on the far left
            gridWIP.BackgroundColor = SystemColors.Control;
            gridWIP.BorderStyle = BorderStyle.None;

            // Define Columns
            gridWIP.Columns.Add("Number", "RO #");
            gridWIP.Columns.Add("Customer", "Customer");
            gridWIP.Columns.Add("License", "License");
            gridWIP.Columns.Add("Vehicle", "Vehicle");

            // --- 3. Add Controls to Form ---
            // Order matters for Docking: Add the Fill control first, or ensure ToolStrip is added last if using Dock.Top
            this.Controls.Add(gridWIP);
            this.Controls.Add(toolStrip);
        }

        private void LoadPlaceholderData()
        {
            // This is where we will eventually call _sqliteRepo.GetWorkOrders()
            gridWIP.Rows.Add("1001", "John Doe", "ABC-123", "2015 Ford F-150");
            gridWIP.Rows.Add("1002", "Jane Smith", "XYZ-987", "2018 Honda Civic");
            gridWIP.Rows.Add("1003", "Bob Wilson", "LMN-456", "2020 Chevy Silverado");
        }
    }
}