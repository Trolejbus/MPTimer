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
            BringToFront();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BringToFront();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Accept(valueTextBox.Text);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Value = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CreateHintsButtons()
        {
            hintsFlowLayoutPanel.Controls.Clear();
            foreach (var hint in hints)
            {
                var button = new Button()
                {
                    Text = hint,
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
    }
}
