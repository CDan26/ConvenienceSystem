using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ConvenienceFormClient
{
    class DataGridAdapter
    {
        public DataTable Table;

        //public DataTable getData() { return this.Table; }

        /// <summary>
        /// a flag for describing whether the current data set has been edited (is dirty)
        /// </summary>
        public Boolean IsDirty { get; private set; }

        public void MakeDirty()
        {
            this.IsDirty = true;
        }


        public void ImportUserData(List<Tuple<int,string,double,string,string>> list)
        {
            //Basically for full user information
            Table = new DataTable("User-Table");

            //Create the table structure

            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ID";
            column.Caption = "ID";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the Column
            Table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "username";
            column.Caption = "Username";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "debt";
            column.Caption = "Debt of the user";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "state";
            column.Caption = "Account State";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "comment";
            column.Caption = "comment";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            //now import data into this structure
            foreach (Tuple<int,string,double,string,string> tup in list)
            {
                DataRow row;
                row = Table.NewRow();
                row["ID"] = tup.Item1;
                row["username"] = tup.Item2;
                row["debt"] = tup.Item3;
                row["state"] = tup.Item4;
                row["comment"] = tup.Item5;
                Table.Rows.Add(row);
            }

            //Now, the data is not corrupted...
            this.IsDirty = false;
        }

        public void ImportUserData(Dictionary<string,double> list)
        {
            //Basically for small user information
            Table = new DataTable("User-Table");

            //Create the table structure

            DataColumn column;
            
            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "username";
            column.Caption = "Username";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "debt";
            column.Caption = "Debt of the user";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            //now import data into this structure
            foreach (KeyValuePair<string,double> tup in list)
            {
                DataRow row;
                row = Table.NewRow();
                row["username"] = tup.Key;
                row["debt"] = tup.Value;
                Table.Rows.Add(row);
            }

            //Now, the data is not corrupted...
            this.IsDirty = false;
        }

        public void ImportPricingData(List<Tuple<int, string, double, string>> list)
        {
            //Basically for full user information
            Table = new DataTable("Pricing-Table");

            //Create the table structure

            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ID";
            column.Caption = "ID";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the Column
            Table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "product";
            column.Caption = "Product";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "price";
            column.Caption = "Price of the Product";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "comment";
            column.Caption = "Comment";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);


            //now import data into this structure
            foreach (Tuple<int, string, double, string> tup in list)
            {
                DataRow row;
                row = Table.NewRow();
                row["ID"] = tup.Item1;
                row["product"] = tup.Item2;
                row["price"] = tup.Item3;
                row["comment"] = tup.Item4;
                Table.Rows.Add(row);
            }

            //Now, the data is not corrupted...
            this.IsDirty = false;
        }

		public void ImportDebtData(Dictionary<string, double> list)
		{
			//Basically for small user information
			Table = new DataTable("Debt-Table");

			//Create the table structure

			DataColumn column;

			// Create second column.
			column = new DataColumn();
			column.DataType = System.Type.GetType("System.String");
			column.ColumnName = "username";
			column.Caption = "User";
			column.ReadOnly = true;
			column.Unique = false;
			// Add the column to the table.
			Table.Columns.Add(column);

			column = new DataColumn();
			column.DataType = System.Type.GetType("System.Double");
			column.ColumnName = "debt";
			column.Caption = "SUM(price)";
			column.ReadOnly = true;
			column.Unique = false;
			// Add the column to the table.
			Table.Columns.Add(column);

			//now import data into this structure
			foreach (KeyValuePair<string, double> tup in list)
			{
				DataRow row;
				row = Table.NewRow();
				row["username"] = tup.Key;
				row["debt"] = tup.Value;
				Table.Rows.Add(row);
			}

			//Now, the data is not corrupted...
			this.IsDirty = false;
		}

        public void ImportPricingData(Dictionary<string, double> list)
        {
            //Basically for small user information
            Table = new DataTable("Pricing-Table");

            //Create the table structure

            DataColumn column;

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "product";
            column.Caption = "Product";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "price";
            column.Caption = "Price";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            //now import data into this structure
            foreach (KeyValuePair<string, double> tup in list)
            {
                DataRow row;
                row = Table.NewRow();
                row["product"] = tup.Key;
                row["price"] = tup.Value;
                Table.Rows.Add(row);
            }

            //Now, the data is not corrupted...
            this.IsDirty = false;
        }

        public void ImportKeydateData(List<Tuple<string,string>> list)
        {
            //Basically for small user information
            Table = new DataTable("Keydates-Table");

            //Create the table structure

            DataColumn column;

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "keydate";
            column.Caption = "Ksername";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "comment";
            column.Caption = "Comment";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            //now import data into this structure
            foreach (Tuple<string, string> tup in list)
            {
                DataRow row;
                row = Table.NewRow();
                row["keydate"] = tup.Item1;
                row["comment"] = tup.Item2;
                Table.Rows.Add(row);
            }

            //Now, the data is not corrupted...
            this.IsDirty = false;
        }

        internal void ImportActivityData(List<Tuple<string, string, double, string>> list)
        {
            //Basically for full user information
            Table = new DataTable("Activity-Table");

            //Create the table structure

            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "date";
            column.Caption = "Date";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the Column
            Table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "user";
            column.Caption = "User";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "price";
            column.Caption = "Price";
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "comment";
            column.Caption = "Comment";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            Table.Columns.Add(column);


            //now import data into this structure
            foreach (Tuple<string, string, double, string> tup in list)
            {
                DataRow row;
                row = Table.NewRow();
                row["date"] = tup.Item1;
                row["user"] = tup.Item2;
                row["price"] = tup.Item3;
                row["comment"] = tup.Item4;
                Table.Rows.Add(row);
            }

            //Now, the data is not corrupted...
            this.IsDirty = false;
        }

    }
}
