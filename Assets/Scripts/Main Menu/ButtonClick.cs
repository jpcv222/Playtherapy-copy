using UnityEngine;
using System.Collections;
using Npgsql;

public class ButtonClick : MonoBehaviour
{
	public void Click()
	{
        if (DBConnection.dbconn != null)
        {
            NpgsqlCommand dbcmd = DBConnection.dbconn.CreateCommand();

            string sql =
                "SELECT id, name " +
                "FROM users";
            dbcmd.CommandText = sql;

            NpgsqlDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                int FirstName = (int)reader["id"];
                string LastName = (string)reader["name"];

                Debug.Log("Name: " + FirstName + " " + LastName);
            }

            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
        }
        else
        {
            Debug.Log("Database connection not established");
        }
    }
}
