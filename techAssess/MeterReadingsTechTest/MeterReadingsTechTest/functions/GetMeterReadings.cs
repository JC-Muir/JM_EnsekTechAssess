using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MeterReadingsTechTest.objects;

namespace MeterReadingsTechTest.functions
{
    public class GetMeterReadings
    {
        public static List<PBOM_MeterReadings> GetReadings()
        {
            // Retrieve List of Records:-
            var path = @"D:\Coding\Tehnical Assessments\Ensek\techAssess\files\Meter_Reading.csv";
            var rows = System.IO.File.ReadAllLines(path);
            var readOnly_meterDetails = new List<DBOM_MeterReadings>(){ };
            //var meterDetails = new List<IBOM_MeterReadings>() { };

            Console.WriteLine();
            Console.WriteLine("Get Meter Reading Details Service successfully triggered.");

            foreach (var row in rows.Skip(1)) // First row is the column labels. Skip over it.
            {
                var record = row.Split(',');
                var reading = new DBOM_MeterReadings();

                reading.accountId = record[0];
                reading.meterReading_tms = record[1];
                reading.meterReading_value = record[2];
                readOnly_meterDetails.Add(reading);

            }

            //Retreive List of Account Details:-
            var conn = new SqlConnection(@"Data Source=DESKTOP-4A93H7F;Initial Catalog=master;Integrated Security=True");
            var sql = new SqlDataAdapter("SELECT * FROM JM_EMR.CUSTOMER_ACCOUNTS", conn);
            var dtbl = new DataTable();
            sql.Fill(dtbl);

            var accDetails = new List<IBOM_AccountDetails>() { };

            foreach (DataRow row in dtbl.Rows)
            {
                var record = new IBOM_AccountDetails();
                record.accountId = Convert.ToInt32(row["ACCOUNT_ID"]);
                record.firstName = row["FIRST_NAME"].ToString();
                record.lastName = row["LAST_NAME"].ToString();

                accDetails.Add(record);

            }

            var processData = validateReadings.meterReadingValidation(readOnly_meterDetails, accDetails);
            Console.WriteLine();
            Console.WriteLine("Retrieve Meter Details Service completed.");
            return processData;
        }

    }
}
