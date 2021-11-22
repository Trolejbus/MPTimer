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

        public GetTextForm()
        {
            InitializeComponent();
            BringToFront();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Value = valueTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Value = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
