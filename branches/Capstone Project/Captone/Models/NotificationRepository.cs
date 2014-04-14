﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Captone.Hubs;

namespace Captone.Models
{
    public class NotificationRepository
    {
        private iDeliverEntities _db = new iDeliverEntities();
        public int GetData(string username)
        {
            var count = 0;
            SqlDataReader reader = null;
            var getStaff = _db.Accounts.SingleOrDefault(p => p.Username == username);
            if (getStaff == null) return count;
            var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
   
            string str = "SELECT * FROM Notification WHERE StationID = @stationId AND isView = 'false'";
            SqlCommand command = new SqlCommand(str, connection);
            command.Parameters.AddWithValue("@stationId", getStaff.StationID);
            SqlDependency dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
            command.ExecuteReaderAsync();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            command.CommandTimeout = 0;
            using (reader = command.ExecuteReader())
            {
             count =   reader.Cast<IDataRecord>().Select(x => new Notification()
                {
                    NotificationID = x.GetInt32(0),
                    StationID = x.GetInt32(1),
                    isView = x.GetBoolean(2)
                }).Count();
                connection.Close();
            }

            return count;

        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ChatHub.Show();
        }
    }
}