using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StayFit___Proto_
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }
        

        SqlConnection sqlCon = new SqlConnection();             // 초기화를 안하면 밑에서 오류가 납니다.
        SqlCommand sqlCmd = new SqlCommand();                   // 클래스이기 때문에 생성자를 만들어 줍니다.

        string sConn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=;Integrated Security = True; Connect Timeout = 30"; //Con

        private void btnOpenDB_Click(object sender, EventArgs e)
        {

            
            try
            {

                DialogResult ret = openFileDialog1.ShowDialog();  // db file
                if (ret != DialogResult.OK) return;               // 
                string nFile = openFileDialog1.FileName;          //
                string[] ss = sConn.Split(';');  //세미콜론으로 나누는 이유는 세미콜론으로 나누어져있기 때문이다..;;
                                                 //    string s1 = $"{ss[1]}{nFile}";   //AttachDbFilename
                                                 // DB를 어떻게 처리할까?  //
                                                 // @ = 백슬래시 기호를 무시하시오.    하나씩 \\을 붙히기 너무 귀찮으니까 이렇게 만들었구나!

                sqlCmd.Connection = sqlCon;     // sqlcmd는 sqlCon에 종속되어야합니다. (이유? 왜?)
                // sqlCon.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Fixme\source\repos\DBManager\TestDB.mdf;Integrated Security = True; Connect Timeout = 30"; //Con
                sqlCon.ConnectionString = $"{ss[0]};{ss[1]}{nFile};{ss[2]};{ss[3]}"; // 이걸 해주면 DB를 성공적으로 띄우는 것이다.
                sqlCon.Open();


              //  sbPanel1.Text = openFileDialog1.SafeFileName; // safeFilename이 뭐지? // DB파일 자체의 이름을 나타내는구나.
              //  sbPanel2.Text = " DB open Success";
              //  sbPanel1.BackColor = Color.Green;

                DataTable dt = sqlCon.GetSchema("Tables");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = dt.Rows[i].ItemArray[2].ToString();  // 배열, 스트링 형태로 가져옴.
              //      sbButton1.DropDownItems.Add(s);                 // 이걸 수행하면 stripStatus에 현재 테이블에
                                                                    //대한 DB 리스트 항목이 자동으로 나옵니다!
                    comboBox1.Items.Add(s);
                }

                //  string sample = "column1,column2";
                //  string[] sa = sample.Split(',');
                //  string buf = "";

                //  foreach (string col in sa)
                //  {
                //     buf += $"{col,30}";
                //      //Console.Write("{0,-" + 30 + " }", col);
                //  }
                //  sbPanel2.Text = buf;
            }
            catch (SqlException e1)
            {
                MessageBox.Show(e1.Message);
            //    sbPanel2.Text = " DB open failed";
            //    sbPanel2.BackColor = Color.Red;
            }
        }

        string GetToken(int index, char deli, string str)
        {
            string[] Strs = str.Split(deli);
            //           int n = Strs.Length;
            string ret = Strs[index];
            return ret;
        }

        string TableName;

        public int RunSql(string sql)
        {
            try
            {
                // ex) select * from fStatus : select id, fname, fdesc from __
                string s1 = sql.Trim();  //white space : space, 줄바꿈\r\n, Tab 등등..
                sqlCmd.CommandText = sql;
                if (GetToken(0, ' ', sql).ToUpper() == "SELECT") // 이걸 대문자로 해야지..
                {
                    // sql이라는 문자열중, 제일 첫 번째
                    //문자열에서 대문자로 변환한 것이 "select" 이면...
                    SqlDataReader sr = sqlCmd.ExecuteReader(); // sr 이라는 것은 하나의 객체이다.

                    TableName = GetToken(3, ' ', sql);   //string str = sr.GetString(0);
                    //sbPanel3.Text = TableName;
                    dataGrid.Rows.Clear();  // 모든 로우가 클리어되어 초기화
                    dataGrid.Columns.Clear(); // 모든 컬럼이 클리어됨. 초기화.
                    for (int i = 0; i < sr.FieldCount; i++)  // Header 처리.
                    {
                        string ss = sr.GetName(i);
                        dataGrid.Columns.Add(ss, ss);
                    }


                    for (int i = 0; sr.Read(); i++)  // 1 record read : 1줄을 읽어서옵니다.
                    {
                        int ridx = dataGrid.Rows.Add();           // 버퍼 초기화.
                        for (int j = 0; j < sr.FieldCount; j++)
                        {
                            object str = sr.GetValue(j); // GetValue는 오브젝트이다. 따라서 object 선언.
                            dataGrid.Rows[ridx].Cells[j].Value = str;
                        }

                    }
                    sr.Close();
                    /*
                    sr.Close(); for (int i = 0; sr.Read(); i++)  // 1 record read : 1줄을 읽어서옵니다.
                    {
                        string buf = "";           // 버퍼 초기화.
                        for (int j = 0; j < sr.FieldCount; j++)
                        {
                            object str = sr.GetValue(j); // GetValue는 오브젝트이다. 따라서 object 선언.
                            buf += $" {str} ";
                        }
                        tbsql.Text += $"\r\n{buf}\r\n";
                    }
                    sr.Close();
                    */
                }
                else
                {
                    sqlCmd.ExecuteNonQuery();
                }


                //               sqlCmd.CommandText = sql;           // insert into TestTable values(1,2,3,4,5)
                //               sqlCmd.ExecuteNonQuery();
                //      sqlCmd.ExecuteReader();
                //      update, insert, delete, create, alt


                //sbPanel2.Text = " Success ";
                //sbPanel2.BackColor = Color.AliceBlue;
                // select문 제외 -> 리턴 값이 없다. No return value.
                // 오류가 발생할 때에는 예외처리를 해줘서 에러 메시지를 띄워주어야 합니다.
            }

            catch (SqlException e1)
            {
                MessageBox.Show(e1.Message);
                //sbPanel2.Text = " Error ";
                //sbPanel2.BackColor = Color.Red;

            }
            catch (InvalidOperationException e2)
            {
                MessageBox.Show(e2.Message);
            }


            
            

            return 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlCon.Close();

            string s = comboBox1.Text;       // 사용할 테이블 명
            string sql = $"select * from {s}";   // 

            comboBox1.Text = s;

            //   SqlConnection sqlCon = new SqlConnection();             // 초기화를 안하면 밑에서 오류가 납니다.
            //   SqlCommand sqlCmd = new SqlCommand();                   // 클래스이기 때문에 생성자를 만들어 줍니다.

            //   string sConn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=;Integrated Security = True; Connect Timeout = 30"; //Con
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlCon);
            DataSet ds = new DataSet();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(ds);
            chart1.DataSource = ds.Tables[0];

          

            RunSql(sql);
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            sqlCon.Close();

            string s = comboBox1.Text;       // 사용할 테이블 명
            string sql = $"select * from {s}";   // 

            comboBox1.Text = s;

            //   SqlConnection sqlCon = new SqlConnection();             // 초기화를 안하면 밑에서 오류가 납니다.
            //   SqlCommand sqlCmd = new SqlCommand();                   // 클래스이기 때문에 생성자를 만들어 줍니다.

            //   string sConn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=;Integrated Security = True; Connect Timeout = 30"; //Con
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlCon);
            DataSet ds = new DataSet();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(ds);
            chart1.DataSource = ds.Tables[0];



            RunSql(sql);
            if (CbBox1.Text == "CaloriesBurned")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "CaloriesBurned";
                chart1.Series[0].LegendText = "[Kcal]";
                chart1.DataBind();
            }

            if (CbBox1.Text == "Steps")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "Steps";
                chart1.Series[0].LegendText = "걸음 수";
                chart1.DataBind();
            }

            if (CbBox1.Text == "Distance")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "Distance";
                chart1.Series[0].LegendText = "이동 거리";

                chart1.DataBind();
            }

            if (CbBox1.Text == "Floors")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "Floors";
                chart1.Series[0].LegendText = "오른 계단 수";

                chart1.DataBind();
            }

            if (CbBox1.Text == "MinutesSedentary")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "MinutesSedentary";
                chart1.Series[0].LegendText = "앉아 있는 시간 [분]";

                chart1.DataBind();
            }

            if (CbBox1.Text == "MinutesLightlyActive")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "MinutesLightlyActive";
                chart1.Series[0].LegendText = "가벼운 활동 시간[분]";

                chart1.DataBind();
            }

            if (CbBox1.Text == "MinutesFairlyActive")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "MinutesFairlyActive";
                chart1.Series[0].LegendText = "활동 시간 [분]";

                chart1.DataBind();
            }

            if (CbBox1.Text == "MinutesVeryActive")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "MinutesVeryActive";
                chart1.Series[0].LegendText = "격렬한 운동 시간 [분]";
                chart1.DataBind();
            }

            if (CbBox1.Text == "ActivityCalories")
            {
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "ActivityCalories";
                chart1.Series[0].LegendText = "활동으로 소모한 칼로리 [Kcal]";
                chart1.DataBind();
            }

            
        }

        private void btnFileNew_Click(object sender, EventArgs e)
        {
            sqlCon.Close();
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();      // 로우/컬럼 클리어
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            //chart1.Series.Clear();
            //sbPanel1.Text = "DB File Name";
            //sbButton1.Text = "Table List";
            //sbButton1.DropDownItems.Clear(); //클리어를 시켜줍시다.
            //sbPanel2.Text = "Initialized";

            //====================== DB 초기화==============================
            sqlCon.Close();
        }

        private void btnAverage_Click(object sender, EventArgs e)
        {
            sqlCon.Close();
            sqlCon.Open();
            
            string workouttype = CbBox1.Text;
            string TableName = comboBox1.Text;
            string s = $"select AVG ({workouttype}) from {TableName}";

           

            if (CbBox1.Text == "CaloriesBurned")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 2500 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "일 평균 소모 Kcal";
                chart2.Series[0].LegendText = "Kcal";
                chart2.Series[0].Points.DataBindXY(x, y);
            }


            if (CbBox1.Text == "Steps")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 7000 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "걸음 수";
                chart2.Series[0].LegendText = "걸음 수";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            if (CbBox1.Text == "Distance")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 4 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "Km";
                chart2.Series[0].LegendText = "도보 이동 거리";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            if (CbBox1.Text == "Floors")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 50 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "층 수";
                chart2.Series[0].LegendText = "오른 계단 수";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            if (CbBox1.Text == "MinutesSedentary")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 800 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "앉아 있었던 시간[분]";
                chart2.Series[0].LegendText = "평균 앉은 시간 [분]";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            if (CbBox1.Text == "MinutesLightlyActive")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 120 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "활동량 [분]";
                chart2.Series[0].LegendText = "평균 활동량 [분]";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            if (CbBox1.Text == "MinutesFairlyActive")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 20 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "활동량 [분]";
                chart2.Series[0].LegendText = "저강도 활동량 [분]";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            if (CbBox1.Text == "MinutesVeryActive")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 5 };// 임의값
                chart2.Series[0].LegendText = "고강도 활동량 [분]";
                chart2.ChartAreas[0].AxisY.Title = "활동량 [분]";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            if (CbBox1.Text == "ActivityCalories")
            {
                List<string> x = new List<string> { "나의 평균", "권장 평균" };
                List<double> y = new List<double> { GetValue(s), 700 };// 임의값
                chart2.ChartAreas[0].AxisY.Title = "Kcal";
                chart2.Series[0].LegendText = "운동 칼로리 소모량 [Kcal]";
                chart2.Series[0].Points.DataBindXY(x, y);
            }

            //List<string> x = new List<string> { "철수", "영희", "길동", "재동", "민희" };
            //List<double> y = new List<double> { 80, 90, 85, 70, 95 };
        }

        public double GetValue(string sql)
        {
            try
            {
                // ex) select * from fStatus : select id, fname, fdesc from __
                string s1 = sql.Trim();  //white space : space, 줄바꿈\r\n, Tab 등등..
                sqlCmd.CommandText = sql;
                if (GetToken(0, ' ', sql).ToUpper() == "SELECT") // 이걸 대문자로 해야지..
                {
                    // sql이라는 문자열중, 제일 첫 번째
                    //문자열에서 대문자로 변환한 것이 "select" 이면...
                    SqlDataReader sr = sqlCmd.ExecuteReader(); // sr 이라는 것은 하나의 객체이다.

                    TableName = GetToken(3, ' ', sql);   //string str = sr.GetString(0);
                    //sbPanel3.Text = TableName;
                    dataGridView1.Rows.Clear();  // 모든 로우가 클리어되어 초기화
                    dataGridView1.Columns.Clear(); // 모든 컬럼이 클리어됨. 초기화.
                    for (int i = 0; i < sr.FieldCount; i++)  // Header 처리.
                    {
                        string ss = sr.GetName(i);
                        dataGridView1.Columns.Add(ss, ss);
                    }


                    for (int i = 0; sr.Read(); i++)  // 1 record read : 1줄을 읽어서옵니다.
                    {
                        int ridx = dataGridView1.Rows.Add();           // 버퍼 초기화.
                        for (int j = 0; j < sr.FieldCount; j++)   // 접근할 수 없는 코드.. 아마 j가 한번만 사용되고 Return 되기 때문에?
                        {
                            object str = sr.GetValue(j); // GetValue는 오브젝트이다. 따라서 object 선언.
                            dataGridView1.Rows[ridx].Cells[j].Value = str;  // 원본코드는 여기까지


                            double ReturnValue;  // 리턴값 설정
                            string TempString = dataGridView1.Rows[ridx].Cells[j].Value.ToString();    // 데이터그리드 값을 임시로 저장할 TempString 생성
                            ReturnValue = double.Parse(TempString);                               // double로 파싱 후 리턴.

                            return ReturnValue;
                        }

                    }
                   

                    sr.Close();
                    /*
                    sr.Close(); for (int i = 0; sr.Read(); i++)  // 1 record read : 1줄을 읽어서옵니다.
                    {
                        string buf = "";           // 버퍼 초기화.
                        for (int j = 0; j < sr.FieldCount; j++)
                        {
                            object str = sr.GetValue(j); // GetValue는 오브젝트이다. 따라서 object 선언.
                            buf += $" {str} ";
                        }
                        tbsql.Text += $"\r\n{buf}\r\n";
                    }
                    sr.Close();
                    */
                }
                else
                {
                    sqlCmd.ExecuteNonQuery();
                }


                //               sqlCmd.CommandText = sql;           // insert into TestTable values(1,2,3,4,5)
                //               sqlCmd.ExecuteNonQuery();
                //      sqlCmd.ExecuteReader();
                //      update, insert, delete, create, alt


                //sbPanel2.Text = " Success ";
                //sbPanel2.BackColor = Color.AliceBlue;
                // select문 제외 -> 리턴 값이 없다. No return value.
                // 오류가 발생할 때에는 예외처리를 해줘서 에러 메시지를 띄워주어야 합니다.
            }

            catch (SqlException e1)
            {
                MessageBox.Show(e1.Message);
                //sbPanel2.Text = " Error ";
                //sbPanel2.BackColor = Color.Red;

            }
            catch (InvalidOperationException e2)
            {
                MessageBox.Show(e2.Message);
            }

            
            

            return 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.Yes)

                Application.Exit();
        }

        private void btnNewTable_Click(object sender, EventArgs e)
        {
            AddColumns dlg = new AddColumns("새로운 테이블 이름을 입력해주세요");            //


            if (dlg.ShowDialog() != DialogResult.OK) return;
            string tableName = dlg.sInput;

            string sql = $"Create table {tableName} (";


            for (int i = 0; i < dataGrid.ColumnCount; i++)
            {
                if (dataGrid.Columns[i].HeaderText == "Date")
                {
                    sql += $"{dataGrid.Columns[i].HeaderText} nchar(30)";
                    if (i < dataGrid.ColumnCount - 1) sql += ",";
                }
                else if (dataGrid.Columns[i].HeaderText == "Distance")
                {
                    sql += $"{dataGrid.Columns[i].HeaderText} float ";
                    if (i < dataGrid.ColumnCount - 1) sql += ",";
                }
                else
                {
                    sql += $"{dataGrid.Columns[i].HeaderText} int ";
                    if (i < dataGrid.ColumnCount - 1) sql += ",";
                }
            }
            sql += ")";
            RunSql(sql);

            // insert into[TableName] values (
            // [col_val_1], [col_val_2], ...
            // ) 

            for (int j = 0; j < dataGrid.Rows.Count; j++)
            {
                sql = $"insert into {tableName} values (";
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    sql += $"'{dataGrid.Rows[j].Cells[i].Value}'"; // 오브젝트
                    if (i < dataGrid.ColumnCount - 1) sql += ",";
                }
                sql += ")";
                RunSql(sql);

            }
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            OleDbConnection excel_con = null;     // excol_con을 Ole Db 연결 변수로 쓸것입니다. 하지만 아직은 어떻게 쓸지몰라요
            string xls_filename;                  // xls_filename 선언, 딱 봐도 엑셀파일 이름이죠?

            try
            {
                //엑셀 불러오기
                FileDialog file_dlg = new OpenFileDialog();    // file_dlg 라는 파일오픈 변수를 선언하고 초기화해줍니다.

                //엑셀문서를 보여주기
                if (file_dlg.ShowDialog() == DialogResult.OK) // 파일오픈을 했을때 정상적으로 선택됐다면
                {
                    xls_filename = file_dlg.FileName;         // 선택한 파일의 '이름'만 xls_filename이라는 변수에 저장해줍니다.

                    string str_con = "Provider = Microsoft.ACE.OLEDB.12.0.0;Data Source=" + xls_filename + ";Extended Properties='Excel 12.0;HDR=YES'";
                    excel_con = new OleDbConnection(str_con);
                    excel_con.Open();
                    string excel_sql = @"select * from[Sheet1$]";

                    OleDbDataAdapter excel_adapter = new OleDbDataAdapter(excel_sql, excel_con);
                    DataSet excel_DS = new DataSet();
                    excel_adapter.Fill(excel_DS);

                    DataTable excel_table = excel_DS.Tables[0];

                    dataGrid.DataSource = excel_table;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일 가져오기 실패 : " + ex.Message);
            }
        }

        public int RunSql2(string sql)
        {
            try
            {
                // ex) select * from fStatus : select id, fname, fdesc from __
                string s1 = sql.Trim();  //white space : space, 줄바꿈\r\n, Tab 등등..
                sqlCmd.CommandText = sql;
                if (GetToken(0, ' ', sql).ToUpper() == "SELECT") // 이걸 대문자로 해야지..
                {
                    // sql이라는 문자열중, 제일 첫 번째
                    //문자열에서 대문자로 변환한 것이 "select" 이면...
                    SqlDataReader sr = sqlCmd.ExecuteReader(); // sr 이라는 것은 하나의 객체이다.

                    TableName = GetToken(3, ' ', sql);   //string str = sr.GetString(0);
                    //sbPanel3.Text = TableName;
                    dataGridView1.Rows.Clear();  // 모든 로우가 클리어되어 초기화
                    dataGridView1.Columns.Clear(); // 모든 컬럼이 클리어됨. 초기화.
                    for (int i = 0; i < sr.FieldCount; i++)  // Header 처리.
                    {
                        string ss = sr.GetName(i);
                        dataGridView1.Columns.Add(ss, ss);
                    }


                    for (int i = 0; sr.Read(); i++)  // 1 record read : 1줄을 읽어서옵니다.
                    {
                        int ridx = dataGridView1.Rows.Add();           // 버퍼 초기화.
                        for (int j = 0; j < sr.FieldCount; j++)
                        {
                            object str = sr.GetValue(j); // GetValue는 오브젝트이다. 따라서 object 선언.
                            dataGridView1.Rows[ridx].Cells[j].Value = str;
                        }

                    }
                    sr.Close();
                    /*
                    sr.Close(); for (int i = 0; sr.Read(); i++)  // 1 record read : 1줄을 읽어서옵니다.
                    {
                        string buf = "";           // 버퍼 초기화.
                        for (int j = 0; j < sr.FieldCount; j++)
                        {
                            object str = sr.GetValue(j); // GetValue는 오브젝트이다. 따라서 object 선언.
                            buf += $" {str} ";
                        }
                        tbsql.Text += $"\r\n{buf}\r\n";
                    }
                    sr.Close();
                    */
                }
                else
                {
                    sqlCmd.ExecuteNonQuery();
                }


                //               sqlCmd.CommandText = sql;           // insert into TestTable values(1,2,3,4,5)
                //               sqlCmd.ExecuteNonQuery();
                //      sqlCmd.ExecuteReader();
                //      update, insert, delete, create, alt


                //sbPanel2.Text = " Success ";
                //sbPanel2.BackColor = Color.AliceBlue;
                // select문 제외 -> 리턴 값이 없다. No return value.
                // 오류가 발생할 때에는 예외처리를 해줘서 에러 메시지를 띄워주어야 합니다.
            }

            catch (SqlException e1)
            {
                MessageBox.Show(e1.Message);
                //sbPanel2.Text = " Error ";
                //sbPanel2.BackColor = Color.Red;

            }
            catch (InvalidOperationException e2)
            {
                MessageBox.Show(e2.Message);
            }

            return 0;
        }

        private void CbBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlCon.Close();
            string workouttype = CbBox1.Text;
            string TableName = comboBox1.Text;
            string s = $"select AVG ({workouttype}) from {TableName}";

            //   SqlConnection sqlCon = new SqlConnection();             // 초기화를 안하면 밑에서 오류가 납니다.
            //   SqlCommand sqlCmd = new SqlCommand();                   // 클래스이기 때문에 생성자를 만들어 줍니다.

            //   string sConn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=;Integrated Security = True; Connect Timeout = 30"; //Con
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(s, sqlCon);
            DataSet ds = new DataSet();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(ds);
            chart1.DataSource = ds.Tables[0];



            RunSql2(s);
        }
    }
 }

    
