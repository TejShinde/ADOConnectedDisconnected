using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;

namespace ADOConnectionProduct
{
    public partial class StudentFormDisconnected : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public StudentFormDisconnected()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }
        private DataSet GetStudents()
        {
            da = new SqlDataAdapter("select * from student",con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "student");
            return ds;
        }
        private void ClearFormFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtBranch.Clear();
            txtEmail.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetStudents();

                DataRow row = ds.Tables["student"].NewRow();
                row["name"] = txtName.Text;
                row["branch"] = txtBranch.Text;
                row["email"] = txtEmail.Text;

                ds.Tables["student"].Rows.Add(row);

                int result = da.Update(ds.Tables["student"]);
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted");
                    ClearFormFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetStudents();
                DataRow row = ds.Tables["student"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtBranch.Text = row["branch"].ToString();
                    txtEmail.Text = row["email"].ToString();

                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetStudents();
                DataRow row = ds.Tables["student"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["branch"] = txtBranch.Text;
                    row["email"] = txtEmail.Text;

                    int result = da.Update(ds.Tables["student"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record inserted");
                        ClearFormFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found for id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetStudents();
                DataRow row = ds.Tables["student"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();

                    int result = da.Update(ds.Tables["student"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record deleted");
                        ClearFormFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found for id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ds = GetStudents();
            dataGridView1.DataSource = ds.Tables["student"];
        }
    }
}
