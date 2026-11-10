
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OOP_WishList
{
    public partial class Form1 : Form
    {
        string connectionString = "server=localhost;database=wishlist_db;uid=root;pwd=;";

        public Form1()
        {
            InitializeComponent();
            LoadWishlist();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
       
        private void LoadWishlist()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT
                                    id,
                                    name,
                                    item1,
                                    item1_price,
                                    item2,
                                    item2_price,
                                    (item1_price + item2_price) AS total
                                    FROM wishlist";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            decimal price1;
            decimal price2;

            if (!decimal.TryParse(textBox3.Text, out price1) ||
                !decimal.TryParse(textBox5.Text, out price2) ||
                price1 < 0 || price2 < 0)
            {
                MessageBox.Show("Please enter valid prices.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO wishlist
                                    (name, item1, item1_price, item2, item2_price)
                                    VALUES
                                    (@name, @item1, @price1, @item2, @price2)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@item1", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@price1", price1);
                        cmd.Parameters.AddWithValue("@item2", textBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@price2", price2);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Wishlist added successfully!");

                LoadWishlist();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
