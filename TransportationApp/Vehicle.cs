using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TransportationApp
{
    public partial class Vehicle : Form
    {
        public Vehicle()
        {
            InitializeComponent();
            ShowVehicle();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Tech Louisville\Documents\transApp.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowVehicle()
        {
            Con.Open();
            string Query = "select * from VehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VehicleDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(LPlateTb.Text == "" || MakeCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MileageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {  
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into VehicleTbl (VLp,VMake,VModel,VYear,VEngType,VColor,VMileage,VType,Booked) values(@VLp,@VMa,@VMo,@VY,@VEng,@VCo,@VMi,@VTy,@VB)", Con);
                    cmd.Parameters.AddWithValue("@VLp", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@VMa", MakeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VMo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MileageTb.Text);
                    cmd.Parameters.AddWithValue("@VTy", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Recorded");

                    Con.Close();
                    ShowVehicle();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "")
            {
                MessageBox.Show("Select a Vehicle");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from VehicleTbl where VLP=@VPlate", Con);
                    cmd.Parameters.AddWithValue("@VPlate", LPlateTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Deleted");

                    Con.Close();
                    ShowVehicle();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void VehicleDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LPlateTb.Text = VehicleDGV.SelectedRows[0].Cells[1].Value.ToString();
            MakeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[2].Value.ToString();
            ModelTb.Text = VehicleDGV.SelectedRows[0].Cells[3].Value.ToString();
            VYearCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[4].Value.ToString();
            EngTypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[5].Value.ToString();
            ColorTb.Text = VehicleDGV.SelectedRows[0].Cells[6].Value.ToString();
            MileageTb.Text = VehicleDGV.SelectedRows[0].Cells[7].Value.ToString();
            TypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[8].Value.ToString();
            BookedCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[9].Value.ToString();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
