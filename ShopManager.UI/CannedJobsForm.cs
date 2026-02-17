using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class CannedJobItem
    {
        public string PartNo { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public bool IsLabor { get; set; }
    }

    public class CannedJobsForm : Form
    {
        private ListBox lstJobs;
        private Button btnSelect;
        private Button btnCancel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        
        public List<CannedJobItem> SelectedItems { get; private set; }

        private static Dictionary<string, List<CannedJobItem>> _jobs;

        public CannedJobsForm()
        {
            InitializeComponent();
            LoadJobs();
        }

        private void InitializeComponent()
        {
            this.Text = "Select Canned Job";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblTitle = new Label { Text = "Available Jobs:", Location = new Point(10, 10), AutoSize = true };
            
            lstJobs = new ListBox { Location = new Point(10, 30), Size = new Size(300, 280) };
            lstJobs.DoubleClick += (s, e) => SelectJob();

            btnSelect = new Button { Text = "Select", DialogResult = DialogResult.OK, Location = new Point(50, 320), Width = 80 };
            btnSelect.Click += (s, e) => SelectJob();

            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(150, 320), Width = 80 };

            // Management Buttons
            btnAdd = new Button { Text = "Add New", Location = new Point(320, 30), Width = 100 };
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button { Text = "Edit", Location = new Point(320, 70), Width = 100 };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button { Text = "Delete", Location = new Point(320, 110), Width = 100 };
            btnDelete.Click += BtnDelete_Click;

            this.Controls.AddRange(new Control[] { lblTitle, lstJobs, btnSelect, btnCancel, btnAdd, btnEdit, btnDelete });
        }

        private void LoadJobs()
        {
            if (_jobs == null)
            {
                _jobs = new Dictionary<string, List<CannedJobItem>>();

                // Mock Data - Replace with DB calls later
                _jobs.Add("Oil Change (Standard)", new List<CannedJobItem>
                {
                    new CannedJobItem { IsLabor = true, Description = "Lube, Oil, Filter", Quantity = 0.5m, Price = 60.00m },
                    new CannedJobItem { IsLabor = false, PartNo = "OF-123", Description = "Oil Filter", Quantity = 1, Price = 8.50m, Cost = 3.00m },
                    new CannedJobItem { IsLabor = false, PartNo = "OIL-5W30", Description = "5W30 Synthetic Blend", Quantity = 5, Price = 4.50m, Cost = 2.10m }
                });

                _jobs.Add("Brake Job (Front)", new List<CannedJobItem>
                {
                    new CannedJobItem { IsLabor = true, Description = "Replace Front Pads and Rotors", Quantity = 1.5m, Price = 150.00m },
                    new CannedJobItem { IsLabor = false, PartNo = "BP-F-Ceramic", Description = "Ceramic Brake Pads (Front)", Quantity = 1, Price = 55.00m, Cost = 25.00m },
                    new CannedJobItem { IsLabor = false, PartNo = "RT-F-Std", Description = "Standard Rotor (Front)", Quantity = 2, Price = 45.00m, Cost = 18.00m }
                });

                _jobs.Add("Tire Rotation", new List<CannedJobItem>
                {
                    new CannedJobItem { IsLabor = true, Description = "Rotate Tires & Check Pressure", Quantity = 0.4m, Price = 29.95m }
                });
            }

            lstJobs.Items.Clear();
            foreach (var jobName in _jobs.Keys)
            {
                lstJobs.Items.Add(jobName);
            }
        }

        private void SelectJob()
        {
            if (lstJobs.SelectedItem == null) return;

            string jobName = lstJobs.SelectedItem.ToString();
            if (_jobs.ContainsKey(jobName))
            {
                SelectedItems = _jobs[jobName];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new CannedJobEditorForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(form.JobName) && !_jobs.ContainsKey(form.JobName))
                {
                    _jobs.Add(form.JobName, form.JobItems);
                    LoadJobs();
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (lstJobs.SelectedItem == null) return;
            string jobName = lstJobs.SelectedItem.ToString();
            
            if (_jobs.ContainsKey(jobName))
            {
                var form = new CannedJobEditorForm(jobName, _jobs[jobName]);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _jobs[jobName] = form.JobItems; // Update items
                    // If we supported renaming, we'd handle dictionary key change here
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstJobs.SelectedItem == null) return;
            string jobName = lstJobs.SelectedItem.ToString();
            
            _jobs.Remove(jobName);
            LoadJobs();
        }
    }
}