using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace mysql
{
    
    public partial class Form1 : Form
    {
        private string constring = "data source=localhost;database=bi;" +
            "user id=root;password=123456;pooling=true;charset=utf8;";
        //public ViewForSql()
        //{
        //    InitializeComponent();
        //    Adapt();  //使控件宽度自适应
        //    ConnectView(); //连接数据库操作
        //}


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adapt();
            ConnectView();

        }

        //格式化dataGridView大小
        public void Adapt()
        {
            int width = 0;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                width += this.dataGridView1.Columns[i].Width;
            }
            if (width > this.dataGridView1.Size.Width)
            {
                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

            public void ConnectView()
            {
                MySqlConnection conn = new MySqlConnection(constring); //新建连接
            try
            {
                conn.Open();
                MessageBox.Show("已经建立连接");   //连接成功则显示对话框
                string sql = "select * from course"; //sql语言
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();//格式化数据
                while (reader.Read())//推进到下一条数据
                {
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = reader.GetString("c_id");
                    this.dataGridView1.Rows[index].Cells[1].Value = reader.GetString("c_name");
                    this.dataGridView1.Rows[index].Cells[2].Value = reader.GetString("t_id");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            }
    }
}
