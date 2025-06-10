using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace WebAPI
{
    public partial class User_Form : Form
    {
        public User_Form()
        {
            InitializeComponent();
            Load += User_Form_Load;
        }

        private async void User_Form_Load(object sender, EventArgs e)
        {
            string url = "https://jsonplaceholder.typicode.com/users";

            var client = new HttpClient();
            string json = await client.GetStringAsync(url);

            JArray array = JArray.Parse(json);

            var userList = array.Select(user => new
            {
                Name = (string)user["name"],
                Username = (string)user["username"],
                Email = (string)user["email"]
            }).ToList();

            DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Username");
            table.Columns.Add("Email");

            foreach (var user in userList)
            {
                table.Rows.Add(user.Name,user.Username, user.Email);
            }

            DGVUsers.DataSource = table;
        }
    }

}
