using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class PartsForm : Form
    {
        public TextBox txtPartNo;
        public TextBox txtDescription;
        public TextBox txtQuantity;
        public TextBox txtUnitCost;
        public TextBox txtUnitRetail;
        private Button btnSave;
        private Button btnCancel;

        public PartsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Part Details";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int xLabel = 20;
            int xInput = 120;
            int yStart = 20;
            int yStep = 40;
            int inputWidth = 200;

            // 1. Part No
            var lblPartNo = new Label { Text = "Part No:", Location = new Point(xLabel, yStart + 3), AutoSize = true };
            txtPartNo = new TextBox { Location = new Point(xInput, yStart), Width = inputWidth };

            // 2. Description
            var lblDesc = new Label { Text = "Description:", Location = new Point(xLabel, yStart + yStep + 3), AutoSize = true };
            txtDescription = new TextBox { Location = new Point(xInput, yStart + yStep), Width = inputWidth };

            // 3. Quantity
            var lblQty = new Label { Text = "Quantity:", Location = new Point(xLabel, yStart + yStep * 2 + 3), AutoSize = true };
            txtQuantity = new TextBox { Location = new Point(xInput, yStart + yStep * 2), Width = 80 };

            // 4. Unit Cost
            var lblCost = new Label { Text = "Unit Cost $:", Location = new Point(xLabel, yStart + yStep * 3 + 3), AutoSize = true };
            txtUnitCost = new TextBox { Location = new Point(xInput, yStart + yStep * 3), Width = 80 };

            // 5. Unit Retail
            var lblRetail = new Label { Text = "Unit Retail $:", Location = new Point(xLabel, yStart + yStep * 4 + 3), AutoSize = true };
            txtUnitRetail = new TextBox { Location = new Point(xInput, yStart + yStep * 4), Width = 80 };

            // Buttons
            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new Point(100, yStart + yStep * 5 + 20), Width = 80 };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(200, yStart + yStep * 5 + 20), Width = 80 };

            this.Controls.AddRange(new Control[] { 
                lblPartNo, txtPartNo,
                lblDesc, txtDescription,
                lblQty, txtQuantity,
                lblCost, txtUnitCost,
                lblRetail, txtUnitRetail,
                btnSave, btnCancel
            });
        }
    }
}