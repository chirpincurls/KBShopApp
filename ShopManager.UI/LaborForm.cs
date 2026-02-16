using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class LaborForm : Form
    {
        public TextBox txtWorkRequested;
        public TextBox txtWorkPerformed;
        public TextBox txtHours;
        public TextBox txtTotal;
        private Button btnSave;
        private Button btnCancel;

        public LaborForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Labor Details";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int xLabel = 20;
            int xInput = 20;
            int yStart = 15;
            int inputWidth = 440;

            // 1. Work Requested
            var lblRequested = new Label { Text = "Work Requested:", Location = new Point(xLabel, yStart), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            txtWorkRequested = new TextBox { Location = new Point(xInput, yStart + 25), Width = inputWidth, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical };

            // 2. Work Performed
            int yPerformed = yStart + 120;
            var lblPerformed = new Label { Text = "Work Performed:", Location = new Point(xLabel, yPerformed), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            txtWorkPerformed = new TextBox { Location = new Point(xInput, yPerformed + 25), Width = inputWidth, Height = 120, Multiline = true, ScrollBars = ScrollBars.Vertical };

            // 3. Hours and Total
            int yStats = yPerformed + 160;
            
            var lblHours = new Label { Text = "Charged Hrs:", Location = new Point(xLabel, yStats + 3), AutoSize = true };
            txtHours = new TextBox { Location = new Point(xLabel + 80, yStats), Width = 80 };

            var lblTotal = new Label { Text = "Labor Total $:", Location = new Point(xLabel + 200, yStats + 3), AutoSize = true };
            txtTotal = new TextBox { Location = new Point(xLabel + 290, yStats), Width = 100 };

            // Buttons
            int yButtons = yStats + 50;
            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new Point(260, yButtons), Width = 90 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(370, yButtons), Width = 90 };

            this.Controls.AddRange(new Control[] { 
                lblRequested, txtWorkRequested,
                lblPerformed, txtWorkPerformed,
                lblHours, txtHours,
                lblTotal, txtTotal,
                btnSave, btnCancel
            });
        }
    }
}