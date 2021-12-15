using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

namespace _1213_データベースのデータ操作
{
    public partial class Form1 : Form
    {
        //DataSet クラスの新しいインスタンスを初期化
        DataSet dtSet = new DataSet();

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
            //DataSetからデータテーブルを削除
            if (dtSet.Tables.Count == 1)
            {
                dtSet.Tables.Remove(dtSet.Tables[0]);
            }

            try
            {
                //クラスの新しいインスタンスを初期化
                dataAdapter = new OdbcDataAdapter(@"Select * from " + textBox1.Text, conn);

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
        }
    }
}