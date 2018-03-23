



          
            ///////////
            var stringquery ="";
            var C=new SqlConnection(kdjdjd);
            var adapter=new SqlDataAdapter(stringquery,c)

            var commandBuilder=new SqlCommandBuilder(adapter)
            var ds=new DataSet()
            adapter.Fill(ds)
              dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dg.selectedrows[0].Cells[].value.tostring();

