using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace WinFormsApp1
{
    public partial class AdminDashboard : MaterialForm
    {
        private DataAccess Da { get; set; }
        public AdminDashboard()
        {
            InitializeComponent();
            this.Da = new DataAccess();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            // Line Chart
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> {4, 6, 5, 2, 7}
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> {6, 7, 3, 4, 6},
                    PointGeometry = null
                },

            };

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Month",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Sales",
                LabelFormatter = value => value.ToString("C")
            });
            cartesianChart1.LegendLocation = LegendLocation.None;

            //Bar Chart
            cartesianChart2.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            //adding series will update and animate the chart automatically
            cartesianChart2.Series.Add(new ColumnSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42 }
            });

            //also adding values updates and animates the chart automatically
            cartesianChart2.Series[1].Values.Add(48d);

            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "Sales Man",
                Labels = new[] { "Maria", "Susan", "Charles", "Frida" }
            });

            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Sold Apps",
                LabelFormatter = value => value.ToString("N")
            });
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            new AdminDashboard().Show();
            this.Hide();
        }


        private void ShowEmployee(string sql = "SELECT * FROM Users;")
        {
            DataAccess da = new DataAccess();
            DataSet ds = this.Da.ExecuteQuery(sql);
            this.dgvEmp.AutoGenerateColumns = false;
            this.dgvEmp.DataSource = ds.Tables[0];
        }
        private void materialButton3_Click_1(object sender, EventArgs e)
        {
            this.ShowEmployee();
        }

        private bool IsValidToSaveData()
        {
            if (String.IsNullOrEmpty(this.txtEmpId.Text) || String.IsNullOrEmpty(this.txtEmpUsrName.Text) || String.IsNullOrEmpty(this.txtEmpName.Text) ||
                String.IsNullOrEmpty(this.txtEmpEmail.Text) ||  String.IsNullOrEmpty(this.txtEmpPass.Text) || String.IsNullOrEmpty(this.txtEmpRole.Text) || String.IsNullOrEmpty(this.txtEmpAddr.Text)
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ClearContent()
        {
            this.txtEmpId.Clear();
            this.txtEmpUsrName.Clear();
            this.txtEmpName.Clear();
            this.txtEmpEmail.Clear();
            this.txtEmpPass.Clear();
            this.txtEmpRole.Clear();
            this.txtEmpAddr.Clear();
        }

        private void mbtnEmpAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsValidToSaveData())
                {
                    MessageBox.Show("Invalid opration. Please fill up all the information");
                    return;
                }
                // Insert
                var sql = @"INSERT INTO Users(id,username,fullname,email,password,role,address) VALUES ('" + this.txtEmpId.Text + "','" + this.txtEmpUsrName.Text + "','" + this.txtEmpName.Text + "','" + this.txtEmpEmail.Text + "','"
                + this.txtEmpPass.Text + "','" + this.txtEmpRole.Text + "','" + this.txtEmpAddr.Text + "'); ";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data insertion successfull");
                }
                else
                {
                    MessageBox.Show("Data insertion failed");
                }
                this.ShowEmployee();
                this.ClearContent();

            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured: " + exc.Message);
            }
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Users Where username = '" + this.txtEmpSearch.Text + "';";
            this.ShowEmployee(sql);
        }

       private void UpdateEmployee()
        {
            try
            {
                if (!this.IsValidToSaveData())
                {
                    MessageBox.Show("Invalid opration. Please fill up all the information");
                    return;
                }
                var sql = @"UPDATE Users 
                            SET username = '" + this.txtEmpUsrName.Text + @"',
                            fullname = '" + this.txtEmpName.Text + @"',
                            email = '" + this.txtEmpEmail.Text + @"',
                            password = '" + this.txtEmpPass.Text + @"',
                            role = '" + this.txtEmpRole.Text + @"',
                            address = '" + this.txtEmpAddr.Text + @"'
                            WHERE id = '" + this.txtEmpId.Text + "';";
                                    
                int count = this.Da.ExecuteDMLQuery(sql);
                if (count == 1)
                {
                    MessageBox.Show("Data update complete");
                }
                else
                {
                    MessageBox.Show("Data update failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured: " + ex.Message);
            }
        }
        private void DeleteEmployee()
        {
            try
            {
                var id = this.txtEmpId.Text;
                var name = this.txtEmpName.Text;
                var sql = "DELETE FROM Users WHERE id = '" + id + "';";
                int count = this.Da.ExecuteDMLQuery(sql);
                if (count == 1)
                {
                    MessageBox.Show(name + " has been deleted successfully");
                }
                else
                {
                    MessageBox.Show("Data deletion failed");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error has occured: " + ex.Message);
            }
        }

        private void dgvEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtEmpId.Text = dgvEmp.SelectedRows[0].Cells[0].Value.ToString();
            this.txtEmpId.Enabled = false;
            this.txtEmpUsrName.Text = dgvEmp.SelectedRows[0].Cells[1].Value.ToString();
            this.txtEmpName.Text   =  dgvEmp.SelectedRows[0].Cells[2].Value.ToString();
            this.txtEmpEmail.Text = dgvEmp.SelectedRows[0].Cells[3].Value.ToString();
            this.txtEmpPass.Text = dgvEmp.SelectedRows[0].Cells[4].Value.ToString();
            this.txtEmpRole.Text = dgvEmp.SelectedRows[0].Cells[5].Value.ToString();
            this.txtEmpAddr.Text = dgvEmp.SelectedRows[0].Cells[6].Value.ToString();
        }
        private void dgvEmp_Load(object sender, EventArgs e)
        {
            dgvEmp.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            var query = "SELECT * FROM Users WHERE id = '" + this.txtEmpId.Text + "';";
            var ds = this.Da.ExecuteQuery(query);
            if(ds.Tables[0].Rows.Count == 1)
            {
                this.UpdateEmployee();
            }
            this.ShowEmployee();
            this.ClearContent();
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            var query = "SELECT * FROM Users WHERE id = '" + this.txtEmpId.Text + "';";
            var ds = this.Da.ExecuteQuery(query);
            if (ds.Tables[0].Rows.Count == 1)
            {
                DeleteEmployee();
            }
            this.ShowEmployee();
            this.ClearContent();
        }

        private void dgvEmp_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.ForeColor = Color.Black;
        }
        // Customers
        private void ShowCustomers(string sql = "SELECT * FROM Customers;")
        {
            DataAccess da = new DataAccess();
            DataSet ds = this.Da.ExecuteQuery(sql);
            this.dgvCustomers.AutoGenerateColumns = false;
            this.dgvCustomers.DataSource = ds.Tables[0];
        }
        private void txtCustomerShow_Click(object sender, EventArgs e)
        {
            this.ShowCustomers();
        }

        private void dgvCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.ForeColor = Color.Black;
        }
        // FOOD Items
        private bool IsValidToSaveDataFoods()
        {
            if (String.IsNullOrEmpty(this.txtFOODId.Text) || String.IsNullOrEmpty(this.txtFoodTItle.Text) || String.IsNullOrEmpty(this.txtFoodType.Text) ||
                String.IsNullOrEmpty(this.txtFoodStatus.Text) || String.IsNullOrEmpty(this.txtFoodPrice.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // Show Food Items
        private void ShowFoodItems(string sql = "SELECT * FROM Items;")
        {
            DataAccess da = new DataAccess();
            DataSet ds = this.Da.ExecuteQuery(sql);
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.DataSource = ds.Tables[0];
        }
        // Insert Food Items
        private void AddFoodItems()
        {
            try
            {
                if (!this.IsValidToSaveDataFoods())
                {
                    MessageBox.Show("Invalid opration. Please fill up all the information");
                    return;
                }
                
                var sql = @"INSERT INTO Items(id,title,type,status,price) VALUES ('" + this.txtFOODId.Text + "','" + this.txtFoodTItle.Text + "','" + this.txtFoodType.Text + "','" + this.txtFoodStatus.Text + "','"
                + this.txtFoodPrice.Text + "'); ";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show("Data insertion successfull");
                }
                else
                {
                    MessageBox.Show("Data insertion failed");
                }
                this.ShowEmployee();
                this.ClearContent();

            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured: " + exc.Message);
            }
        }
        // Update Food Items
        private void UpdateFoodItems()
        {
            try
            {
                if (!this.IsValidToSaveDataFoods())
                {
                    MessageBox.Show("Invalid opration. Please fill up all the information");
                    return;
                }
                var sql = @"UPDATE Items 
                            SET title = '" + this.txtFoodTItle.Text + @"',
                            type = '" + this.txtFoodType.Text + @"',
                            status = '" + this.txtFoodStatus.Text + @"',
                            price = '" + this.txtFoodPrice.Text + @"'
                            WHERE id = '" + this.txtFOODId.Text + "';";

                int count = this.Da.ExecuteDMLQuery(sql);
                if (count == 1)
                {
                    MessageBox.Show("Data update complete");
                }
                else
                {
                    MessageBox.Show("Data update failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured: " + ex.Message);
            }
        }
        // Delete Items
        private void DeleteFoodItems()
        {
            try
            {
                var id = this.txtFOODId.Text;
                var name = this.txtFoodTItle.Text;
                var sql = "DELETE FROM Items WHERE id = '" + id + "';";
                int count = this.Da.ExecuteDMLQuery(sql);
                if (count == 1)
                {
                    MessageBox.Show(name + " has been deleted successfully");
                }
                else
                {
                    MessageBox.Show("Data deletion failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured: " + ex.Message);
            }
        }

        private void btnFoodShow_Click(object sender, EventArgs e)
        {
            this.ShowFoodItems();
        }
        private void dgvItems_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.ForeColor = Color.Black;
        }

        private void btnFoodSearch_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Items Where title = '" + this.txtFoodTItle.Text + "';";
            this.ShowFoodItems(sql);
        }

        private void btnFoodAdd_Click(object sender, EventArgs e)
        {
            this.AddFoodItems();
            this.ShowFoodItems();
            this.ClearContent();
        }

        private void btnFoodUpdate_Click(object sender, EventArgs e)
        {
            var query = "SELECT * FROM Items WHERE id = '" + this.txtFOODId.Text + "';";
            var ds = this.Da.ExecuteQuery(query);
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.UpdateFoodItems();
            }
            this.ShowEmployee();
            this.ClearContent();
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtFOODId.Text = dgvItems.SelectedRows[0].Cells[0].ToString();
            this.txtFoodTItle.Text = dgvItems.SelectedRows[0].Cells[1].ToString();
            this.txtFoodType.Text = dgvItems.SelectedRows[0].Cells[2].ToString();
            this.txtFoodStatus.Text = dgvItems.SelectedRows[0].Cells[3].ToString();
            this.txtFoodPrice.Text = dgvItems.SelectedRows[0].Cells[4].ToString();
        }

        private void btnFoodDelete_Click(object sender, EventArgs e)
        {
            var query = "SELECT * FROM Items WHERE id = '" + this.txtFOODId.Text + "';";
            var ds = this.Da.ExecuteQuery(query);
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.DeleteFoodItems();
            }
            this.ShowEmployee();
            this.ClearContent();
        }
    }
}
