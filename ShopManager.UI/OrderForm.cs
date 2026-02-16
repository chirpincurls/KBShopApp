using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class OrderForm : Form
    {
        private DataGridView gridItems;
        private Label lblPartsTotalVal;
        private Label lblLaborTotalVal;
        private Label lblTaxVal;
        private Label lblGrandTotalVal;

        public OrderForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Order Form";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- 1. Top Sub Menu Buttons ---
            var pnlMenu = new FlowLayoutPanel();
            pnlMenu.Dock = DockStyle.Top;
            pnlMenu.Height = 60;
            pnlMenu.Padding = new Padding(10);
            pnlMenu.BackColor = SystemColors.ControlLight;

            var btnLabor = CreateMenuButton("Labor");
            btnLabor.Click += (s, e) => new LaborForm().ShowDialog();

            var btnParts = CreateMenuButton("Parts");
            btnParts.Click += (s, e) => new PartsForm().ShowDialog();

            var btnNotes = CreateMenuButton("Notes");
            var btnCanned = CreateMenuButton("Canned Jobs");

            pnlMenu.Controls.AddRange(new Control[] { btnLabor, btnParts, btnNotes, btnCanned });

            // --- 2. Bottom Totals Section ---
            var pnlBottom = new Panel();
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Height = 120;
            pnlBottom.BackColor = SystemColors.ControlLight;

            // Layout for totals (Aligned to the right)
            int labelX = 850;
            int valueX = 950;
            int startY = 15;
            int stepY = 25;

            var lblParts = new Label { Text = "Parts:", Location = new Point(labelX, startY), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblPartsTotalVal = new Label { Text = "$0.00", Location = new Point(valueX, startY), AutoSize = true };

            var lblLabor = new Label { Text = "Labor:", Location = new Point(labelX, startY + stepY), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblLaborTotalVal = new Label { Text = "$0.00", Location = new Point(valueX, startY + stepY), AutoSize = true };

            var lblTax = new Label { Text = "Tax:", Location = new Point(labelX, startY + stepY * 2), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblTaxVal = new Label { Text = "$0.00", Location = new Point(valueX, startY + stepY * 2), AutoSize = true };

            var lblTotal = new Label { Text = "Total:", Location = new Point(labelX, startY + stepY * 3), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblGrandTotalVal = new Label { Text = "$0.00", Location = new Point(valueX, startY + stepY * 3), AutoSize = true };

            pnlBottom.Controls.AddRange(new Control[] { 
                lblParts, lblPartsTotalVal,
                lblLabor, lblLaborTotalVal,
                lblTax, lblTaxVal,
                lblTotal, lblGrandTotalVal
            });

            // --- 3. Grid Section ---
            gridItems = new DataGridView();
            gridItems.Dock = DockStyle.Fill;
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridItems.BackgroundColor = SystemColors.Window;
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.RowHeadersVisible = false;
            gridItems.AllowUserToAddRows = true;

            // Columns: Time, Description, Quantity, Sale, Extended, Rate, Part No, Cost, Price
            gridItems.Columns.Add("Time", "Time");
            gridItems.Columns.Add("Description", "Description");
            gridItems.Columns.Add("Quantity", "Quantity");
            gridItems.Columns.Add("Sale", "Sale");
            gridItems.Columns.Add("Extended", "Extended");
            gridItems.Columns.Add("Rate", "Rate");
            gridItems.Columns.Add("PartNo", "Part No.");
            gridItems.Columns.Add("Cost", "Cost");
            gridItems.Columns.Add("Price", "Price");

            // Add Controls (Dock order: Fill first, then Top/Bottom)
            this.Controls.Add(gridItems);
            this.Controls.Add(pnlMenu);
            this.Controls.Add(pnlBottom);
        }

        private Button CreateMenuButton(string text)
        {
            return new Button 
            { 
                Text = text, 
                Width = 120, 
                Height = 40, 
                Margin = new Padding(5) 
            };
        }
    }
}