using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConvenienceBackend;

namespace ConvenienceFormClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.cn = new ConNetClient();
            InitializeComponent();
            this.ChangeState(State.START);
        }

        private ConNetClient cn;
        private State state;

        public void ChangeState(State state)
        {
            switch (state)
            {
                case State.START:
                    this.showToolStripMenuItem.Enabled = true;
                    this.editToolStripMenuItem.Enabled = false;
                    this.dataGridView1.Enabled = false;

                    this.textLog.AppendText("Changed to State START" + System.Environment.NewLine);
                    break;
                
                case State.DATA:
                    this.showToolStripMenuItem.Enabled = true;
                    this.editToolStripMenuItem.Enabled = true;
                    this.dataGridView1.Enabled = true;
                    //this.dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                    //this.dataGridView1.ReadOnly = false;

                    this.textLog.AppendText("Changed to State DATA" + System.Environment.NewLine);
                    break;

            }
            this.state = state;
        }



        DataGridAdapter dg;

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cn.Update();
            this.ChangeState(State.DATA);
            textLog.AppendText("#Users: " + this.cn.Users.Count + System.Environment.NewLine);

            dg = new DataGridAdapter();
            dg.ImportUserData(this.cn.Users);
            dataGridView1.DataSource = dg.Table;

            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView1.ReadOnly = false;
            dataGridView1.Enabled = true;
        }

        public DataTable data;

        private void pricesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cn.Update();
            this.ChangeState(State.DATA);
            textLog.AppendText("#Products: " + this.cn.Products.Count + System.Environment.NewLine);

            DataGridAdapter dg = new DataGridAdapter();
            dg.ImportPricingData(this.cn.Products);
            dataGridView1.DataSource = dg.Table;

            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView1.Enabled = true;
            dataGridView1.ReadOnly = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bin = ConvenienceBackend.ConNetClient.isBinaryBackend();
            
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string backVersion = typeof(ConvenienceBackend.ConNetClient).Assembly.GetName().Version.ToString();
            if (bin)
                backVersion = backVersion + " (Binary Version)";
            else
                backVersion = backVersion + " (Plain Version)";
            string message = "This is the Convenience Admin Client."
                + System.Environment.NewLine
                + "Version "
                + version
                + System.Environment.NewLine
                + "Using backend Version "
                + backVersion
                + System.Environment.NewLine
                + System.Environment.NewLine
                + "(Part of the ConvenienceSystem by auxua/Arno Schmetz - Source: "
                + "https://github.com/auxua/ConvenienceSystem" + " )";

            string caption = "About The Application";
            MessageBox.Show(text: message, caption: caption);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save changes?
            
            //really wanna quit?
        }

        private void keydatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.DATA);

            this.cn.UpdateKeydates();
            DataGridAdapter da = new DataGridAdapter();
            da.ImportKeydateData(cn.Keydates);
            dataGridView1.DataSource = da.Table;

            //textLog.AppendText("#Keydates: " + this.cn.Keydates.Count + System.Environment.NewLine);
            //dataGridView1.DataSource = this.cn.Users.ToArray();

            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }


        private void userscompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.DATA);

            List<Tuple<int, string, double, string, string>> list = this.cn.GetFullUsers();

            DataGridAdapter da = new DataGridAdapter();
            da.ImportUserData(list);

			/*Dictionary<string,double> dict = cn.GetDebtSinceKeydate();
			DataGridAdapter da = new DataGridAdapter();
			da.ImportDebtData(dict);*/

            dataGridView1.DataSource = da.Table;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.DataSource is DataGridAdapter)
            {
                ((DataGridAdapter)dataGridView1.DataSource).MakeDirty();
            }
        }

        private void last10EntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.DATA);

            List<Tuple<string, string, double, string>> list = cn.GetActivity();
            
            DataGridAdapter da = new DataGridAdapter();
            da.ImportActivityData(list);

            dataGridView1.DataSource = da.Table;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.ReadOnly = true;
        }

        private void sinceLastKeydateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.DATA);

            //List<Tuple<string, string, double, string>> list = cn.GetActivity();

            Dictionary<string,double> dict = cn.GetDebtSinceKeydate();
			DataGridAdapter da = new DataGridAdapter();
            da.ImportDebtData(dict);

            dataGridView1.DataSource = da.Table;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.ReadOnly = true;

        }

        private void allTakesTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get all accounting activities
        }

        private void applyChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //apply cahnges made to data set
        }

        private void addKeydateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //add a new keydate...
        }

        private void pricescompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.DATA);

            List<Tuple<int, string, double, string>> list = this.cn.GetFullProducts();
            
            DataGridAdapter da = new DataGridAdapter();
            da.ImportPricingData(list);


            dataGridView1.DataSource = da.Table;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }
    }
}
