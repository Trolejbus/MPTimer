namespace MPTimer.Controls
{
    public partial class GetTextForm : Form
    {
        public string Label
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string? Value { get; set; }

        private IEnumerable<string> hints = new List<string>();
        private OpacityForm opacityForm;

        public IEnumerable<string> Hints
        {
            get => hints;
            set {
                hints = value;
                CreateHintsButtons();
            }
        }

        public GetTextForm()
        {
            InitializeComponent();
            opacityForm = new OpacityForm();
            BackColor = Color.LimeGreen;
            TransparencyKey = Color.LimeGreen;
            valueTextBox.Focus();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Cursor.Clip = Bounds;
            BringToFront();
            opacityForm.Show();
            opacityForm.BringToFront();
            BringToFront();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Cursor.Clip = Rectangle.Empty;
            opacityForm.Close();
        }

        private void CreateHintsButtons()
        {
            hintsFlowLayoutPanel.Controls.Clear();
            foreach (var hint in hints)
            {
                var button = new Button()
                {
                    Text = hint,
                    BackColor = Color.White,
                    Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point),
                    AutoSize = true,
                };
                button.Click += (s, e) => Accept(hint);
                hintsFlowLayoutPanel.Controls.Add(button);
            }
        }

        private void Accept(string text)
        {
            Value = text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void valueTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (valueTextBox.Text == "")
                {
                    return;
                }

                Accept(valueTextBox.Text);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Value = null;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
