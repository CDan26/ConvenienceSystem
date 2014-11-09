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
        }

        private ConNetClient cn;        

        public void ChangeState(State state)
        {
            switch (state)
            {
                case State.START:
                    this.button_close.Enabled = false;
                    this.button_connect.Enabled = true;
                    this.button_prices.Enabled = false;
                    this.button_update.Enabled = false;
                    this.button_users.Enabled = false;
                    this.dataGridView1.Enabled = false;

                    this.textLog.AppendText("Changed to State START" + System.Environment.NewLine);
                    break;
                
                case State.CONNECTED:
                    this.button_close.Enabled = true;
                    this.button_connect.Enabled = false;
                    this.button_prices.Enabled = false;
                    this.button_update.Enabled = true;
                    this.button_users.Enabled = false;
                    this.dataGridView1.Enabled = false;

                    this.textLog.AppendText("Changed to State CONNECTED" + System.Environment.NewLine);
                    break;

                case State.PRICES:
                    this.button_close.Enabled = true;
                    this.button_connect.Enabled = false;
                    this.button_prices.Enabled = true;
                    this.button_update.Enabled = true;
                    this.button_users.Enabled = true;
                    this.dataGridView1.Enabled = true;

                    this.textLog.AppendText("Changed to State PRICES" + System.Environment.NewLine);
                    break;

                case State.UPDATED:
                    this.button_close.Enabled = true;
                    this.button_connect.Enabled = false;
                    this.button_prices.Enabled = true;
                    this.button_update.Enabled = true;
                    this.button_users.Enabled = true;
                    this.dataGridView1.Enabled = false;

                    this.textLog.AppendText("Changed to State UPDATED" + System.Environment.NewLine);
                    break;

                case State.USERS:
                    this.button_close.Enabled = true;
                    this.button_connect.Enabled = false;
                    this.button_prices.Enabled = true;
                    this.button_update.Enabled = true;
                    this.button_users.Enabled = true;
                    this.dataGridView1.Enabled = true;

                    this.textLog.AppendText("Changed to State USERS" + System.Environment.NewLine);
                    break;
            }
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            //TODO:Connect
            this.cn.Connect();
            this.ChangeState(State.CONNECTED);
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            this.cn.Update();
            this.ChangeState(State.UPDATED);
        }

        private void button_users_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.USERS);
            textLog.AppendText("#Users: " + this.cn.Users.Count + System.Environment.NewLine);
            //Double d;
            //this.cn.cs.Users.TryGetValue("Gustav Geier",out d);
            //textLog.AppendText("user1: "+ (d+0.1));
            //List<KeyValuePair<String, Double>> list = new List<KeyValuePair<string, double>>();
            //list.AddRange(cn.cs.Users);
            //dataGridView1.DataSource = list;
            dataGridView1.DataSource = this.cn.Users.ToArray();
        }

        private void button_prices_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.PRICES);
            textLog.AppendText("#Products: " + this.cn.Products.Count + System.Environment.NewLine);
            //Double d;
            //this.cn.cs.Users.TryGetValue("Gustav Geier", out d);
            //textLog.AppendText("user1: "+ (d+0.1));
            //List<KeyValuePair<String, Double>> list = new List<KeyValuePair<string, double>>();
            //list.AddRange(cn.cs.Users);
            //dataGridView1.DataSource = list;
            dataGridView1.DataSource = this.cn.Products.ToArray();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.ChangeState(State.START);
            this.cn.Close();
        }
    }
}
