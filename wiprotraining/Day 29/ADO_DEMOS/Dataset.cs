using System.Data;

namespace adodemo1
{
    class Dataset
    {
        DataTable t1;
        DataTable t2;
        DataSet ds;

        public Dataset()
        {
            t1 = new DataTable("Customer");
            t1.Columns.Add("Id", typeof(int));
            t1.Columns.Add("Name", typeof(string));
            t1.Columns.Add("Email", typeof(string));
            t1.Columns.Add("Phone", typeof(string));
            t1.Rows.Add(1, "John Doe", "john.doe@example.com", "123-456-7890");
            t1.Rows.Add(2, "Jane Smith", "jane.smith@example.com", "987-654-3210");
            t1.Rows.Add(3, "Alice Johnson", "alice.johnson@example.com", "555-123-4567");
            t1.Rows.Add(4, "Bob Brown", "bob.brown@example.com", "555-987-6543");

            t2 = new DataTable("Orders");
            t2.Columns.Add("OrderId", typeof(int));
            t2.Columns.Add("CustomerId", typeof(int));
            t2.Columns.Add("OrderDate", typeof(DateTime));
            t2.Rows.Add(101, 1, DateTime.Now);
            t2.Rows.Add(201, 2, DateTime.Now);
            t2.Rows.Add(301, 1, DateTime.Now);

            ds = new DataSet();
            ds.Tables.Add(t1);
            ds.Tables.Add(t2);
        }
        public void DisplayDataSet()
        {
            Console.WriteLine("DataSet: " + ds.DataSetName);
            foreach (DataTable table in ds.Tables)
            {
                Console.WriteLine("Table: " + table.TableName);

                //print column names
                foreach (DataColumn column in table.Columns)
                {
                    Console.Write(column.ColumnName + "\t");
                }
                Console.WriteLine();

                //print row values
                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write(item + "\t");
                    }
                    Console.WriteLine();
                }
            }
        }


    }
}