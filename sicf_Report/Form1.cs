namespace sicf_Report
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.ReportEmbeddedResource = "sicf_Report.Report2.rdlc";
            reportViewer1.RefreshReport();
        }
    }
}