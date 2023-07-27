using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdonetMovie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection ("Server=DESKTOP-NOMRM5V\\SQLEXPRESS;initial catalog=InspimoMovieDb;integrated security=true;");
        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCategory", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command); //Arka tarafta bizim için data table oluşturur.Data table hafızaya alma işlemi için kullanılır.
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dtgCategory.DataSource = dt;

            sqlConnection.Close();
        }

        private void btnCategorySave_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert Into TblCategory (CategoryName) values (@p1)", sqlConnection);
            command.Parameters.AddWithValue("@p1",txtCategoryName.Text);
            command.ExecuteNonQuery(); //Sorguyu çalıştırıp kaydediyor. SaveChanges gibi
            MessageBox.Show("Kategori başarılı bir şekilde eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sqlConnection.Close();
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCategory Where CategoryID=@p1", sqlConnection);
            command.Parameters.AddWithValue("@p1",txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Silinme işlemi başarılı","Sil",MessageBoxButtons.OK, MessageBoxIcon.Error);
            sqlConnection.Close();
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update TblCategory Set CategoryName=@p1 Where CategoryID=@p2", sqlConnection);
            command.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command.Parameters.AddWithValue("@p2", txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Güncellem işlemi başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            sqlConnection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCategory", sqlConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dt);
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.DataSource = dt;
            sqlConnection.Close();
            #endregion

            #region
            sqlConnection.Open();
            SqlCommand command2 = new SqlCommand("Select Count(*) From TblCategory", sqlConnection);
            SqlDataReader dr2 = command2.ExecuteReader();

            while (dr2.Read())
            {
                lblCategoryCount.Text = dr2[0].ToString();
            }
            sqlConnection.Close();
            #endregion
        }

        private void btnMovieList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select MovieName,MovieDuration,MovieImdb,CategoryName From TblMovie inner join TblCategory on TblMovie.MovieCategory=TblCategory.CategoryID", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dtgMovie.DataSource = dt;
            sqlConnection.Close();
        }

        private void btnMovieSave_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert Into TblMovie (MovieName,MovieImdb,MovieDuration,MovieCategory) values (@p1,@p2,@p3,@p4)",sqlConnection);
            command.Parameters.AddWithValue("@p1",txtMovieName.Text);           
            command.Parameters.AddWithValue("@p2",txtMovieImdb.Text);           
            command.Parameters.AddWithValue("@p3",txtMovieDuration.Text);           
            command.Parameters.AddWithValue("@p4",cmbCategory.SelectedValue);
            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme işlemi başarılı" , "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            sqlConnection.Close();
        }

        private void btnMovieProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Exec MovieListWithCategory", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dtgMovie.DataSource = dt;
            sqlConnection.Close();
        }

        private void btnMovieDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From TblMovie Where MovieID=@p1", sqlConnection);
            command.Parameters.AddWithValue("@p1", txtMovieID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Silme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            sqlConnection.Close();
        }

        private void btnMovieUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update TblMovie Set MovieName=@p1,MovieImdb=@p2,MovieDuration=@p3,MovieCategory=@p4 where MovieID=@p5", sqlConnection);
            command.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command.Parameters.AddWithValue("@p2", txtMovieImdb.Text);
            command.Parameters.AddWithValue("@p3", txtMovieDuration.Text);
            command.Parameters.AddWithValue("@p4", cmbCategory.SelectedValue);
            command.Parameters.AddWithValue("@p5", txtMovieID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Güncelle işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            sqlConnection.Close();
        }
    }
}
