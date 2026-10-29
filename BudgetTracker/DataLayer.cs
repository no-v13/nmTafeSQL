using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker
{
    internal class DataLayer
    {
        private MySqlConnection connection;
        public DataLayer() { 
            connection = new MySqlConnection($"server=localhost;port=3306;uid=root;pwd=;database=budget_tracker;");
        }

        public List<LineItem> GetLineItems()
        {
            List<LineItem> results =  new List<LineItem>();
            try
            {
                connection.Open();

                string query = "select * from line_items;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new LineItem(
                        reader.GetUInt32("id"),
                        reader.GetString("name"),
                        reader.GetUInt32("credit"),
                        reader.GetUInt32("debit") //-> data invert :  reader.GetDateTime("time")
                        /*
                         * string = "YYYY-MM-DD HH: ~~"
                         * DateTime.Parse(string) _. getting this from C sharp
                         */
                    ));
                }
            }
            
            finally
            {
                connection.Close();
            }          

            return results;
        }
    
        public bool SaveLineItem(LineItem lineItem) {
            if (lineItem.Id != null) { return false; }
            bool result = false;

            try
            {
                connection.Open();               
                string query = $"insert into line_items (`name`, credit, debit) values (@name, @credit, @debit)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", lineItem.Name);
                command.Parameters.AddWithValue("@credit", lineItem.Credit);
                command.Parameters.AddWithValue("@debit", lineItem.Debit);
                command.Prepare();

                int returnValue = command.ExecuteNonQuery(); // -1 is a fail!

                if (returnValue != -1) { result = true;}
            }  finally
            {
                connection.Close(); 
            }

            return result;
        }

        public bool UpdateLineItem(LineItem lineItem)
        {
            if (lineItem.Id == null) { return false; }
            bool result = false;

            try
            {
                connection.Open();
                string query = $"UPDATE line_items SET `name`= @name, credit=@credit, debit=@debit WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", lineItem.Name);
                command.Parameters.AddWithValue("@credit", lineItem.Credit);
                command.Parameters.AddWithValue("@debit", lineItem.Debit);
                command.Parameters.AddWithValue("@id", lineItem.Id);
                command.Prepare();

                int returnValue = command.ExecuteNonQuery(); // -1 is a fail!

                if (returnValue != -1) { result = true; }
            }
            finally
            {
                connection.Close(); 
            }
            return result;
        }

        public bool DeleteLineItem(LineItem lineItem)
        {
            if (lineItem.Id == null) { return false; }
            bool result = false;

            try
            {
                connection.Open();
                string query = $"Delete FROM line_items WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", lineItem.Name);
                command.Parameters.AddWithValue("@credit", lineItem.Credit);
                command.Parameters.AddWithValue("@debit", lineItem.Debit);
                command.Parameters.AddWithValue("@id", lineItem.Id);
                command.Prepare();

                int returnValue = command.ExecuteNonQuery(); // -1 is a fail!

                if (returnValue != -1) { result = true; }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        // 
        public bool SearchLineItem(LineItem lineItem)
        {
            // SearchLineItem 수정하기 
            if (lineItem.Id == null) { return false; }
            bool result = false;

            try
            {
                connection.Open();
                string query = $"UPDATE line_items SET `name`= @name, credit=@credit, debit=@debit WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", lineItem.Name);
                command.Parameters.AddWithValue("@credit", lineItem.Credit);
                command.Parameters.AddWithValue("@debit", lineItem.Debit);
                command.Parameters.AddWithValue("@id", lineItem.Id);
                command.Prepare();

                int returnValue = command.ExecuteNonQuery(); // -1 is a fail!

                if (returnValue != -1) { result = true; }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }


        }
}
