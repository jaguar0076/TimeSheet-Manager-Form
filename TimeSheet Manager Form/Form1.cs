using System.Collections.Generic;
using System.Windows.Forms;

namespace TimeSheet_Manager_Form
{
    public partial class Form1 : Form
    {
        private bool allowshowdisplay = false;

        private List<string> Categories, Activities, Clients, Project_Ref;

        private Excel_Utils XUtils;

        public Form1()
        {
            InitializeComponent();
            InitializeExcelFile();
            LoadDatasToLists();
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.allowshowdisplay = true;
                this.Visible = !this.Visible;
            }
        }

        private void InitializeExcelFile()
        {
            XUtils = new Excel_Utils("D:\\TestRFI.xlsx", "Time Sheet");
        }

        private void LoadDatasToLists()
        {
            Categories = XUtils.ReadRange("D4", "D15");
            Activities = XUtils.ReadRange("E4", "E14");
            Clients = XUtils.ReadRange("M3", "M23");
            Project_Ref = XUtils.ReadRange("S3", "S70");

            BindExcelDatasToComboBox(comboBox1, Categories);
            BindExcelDatasToComboBox(comboBox2, Activities);
            BindExcelDatasToComboBox(comboBox3, Clients);
            BindExcelDatasToComboBox(comboBox4, Project_Ref);
        }

        private void BindExcelDatasToComboBox(ComboBox CbB, List<string> SList)
        {
            CbB.DataSource = SList;
        }
    }
}