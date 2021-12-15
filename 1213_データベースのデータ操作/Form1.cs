using System;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data;

namespace _1213_データベースのデータ操作
{
    public partial class Form1 : Form
    {
        //DataSet クラスの新しいインスタンスを初期化
        DataSet dtSet = new DataSet();

        //OdbcCommand cmd;

        OdbcDataAdapter dataAdapter;

        //接続文字列の作成
        OdbcConnection conn = new OdbcConnection(
            "Driver={PostgreSQL ANSI};database=postgres;Server=192.168.1.45;Port=5432;" +
            "Uid=postgres;Pwd=12345;CommandTimeOut=20;TimeOut=5");

        public Form1()
        {
            //コンポーネント初期化
            InitializeComponent();
        }

        //データの抽出
        private void btn_Select_Click_1(object sender, EventArgs e)
        {
            //テーブル内のすべての行を削除
            dtSet.Clear();

            if (dtSet.Tables.Count == 1)
            {
                dtSet.Tables.Remove(dtSet.Tables[0]);
            }


            try
            {
                //conn.Open();

                //クラスの新しいインスタンスを初期化
                dataAdapter = new OdbcDataAdapter(@"Select * from " + textBox1.Text, conn);

                //DataTable dt = new DataTable();
                //dataAdapter.Fill(dt);
                //dataGridView1.DataSource = dt;

                OdbcCommandBuilder builder = new OdbcCommandBuilder(dataAdapter);

                // データ取り出し
                dataAdapter.Fill(dtSet);
                // データ表示
                dataGridView1.DataSource = dtSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //conn.Close();
            }
        }

        //データベースに反映
        private void button1_Click(object sender, EventArgs e)
        {
            dataAdapter.Update(dtSet);

            //出力メッセージ
            MessageBox.Show("データベースに反映済み");
        }

        //データ削除
        private void btn_Delete_Click_1(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection src = dataGridView1.SelectedRows;

            //行選択されない場合は、削除しない
            //src.Count:選択された行数
            for (int i = src.Count - 1; i >= 0; i--)
            {
                dataGridView1.Rows.RemoveAt(src[i].Index);
            }

            //foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            //{
            //    if (!item.IsNewRow)
            //    {
            //        cmd = new OdbcCommand("Delete from " + textBox1.Text +
            //            " where " + dataGridView1.Columns[0].Name + " = '" + dataGridView1.CurrentRow.Cells[0].Value + "'", conn);

            //        conn.Open();
            //        cmd.ExecuteNonQuery();

            //        conn.Close();
            //    }
            //}
        }

    }
}