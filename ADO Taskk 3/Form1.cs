using System.Data;
using System.Data.SqlClient;

namespace ADO_Taskk_3
{
    public partial class Form1 : Form
    {
        List<string> combobox = new();
        SqlConnection conn;
        DataSet dataSet;
        SqlDataAdapter adapter;
        SqlCommandBuilder cmd;
        DataTable table;
        SqlDataReader reader;
        string cs = @"Server = (localdb)\MSSQLLocalDB; 
Integrated Security = SSPI; 
Database = Library";
        public Form1()
        {
            InitializeComponent();
            conn = new();
            conn.ConnectionString = cs;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataSet = new();
            string? author = comboBox1.SelectedItem as string;
            string? query = null;
            if (author is not null)
            {
                query = @$"SELECT * FROM Books AS B WHERE B.Id_Author = {author[0]}";
                BookGridView1.DataSource = null;
                adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dataSet, "book");

                BookGridView1.DataSource = dataSet.Tables["book"];
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataSet = new();
            comboBox1.DataSource = null;
            string query = @"SELECT * FROM Authors";
            adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dataSet, "author");

            foreach (DataRow item in dataSet.Tables["author"].Rows)
            {
                combobox.Add(item["Id"].ToString()! + " " + item["FirstName"].ToString()! + " " + item["LastName"].ToString()!);

            }
            comboBox1.DataSource = combobox;
        }
    }

}
