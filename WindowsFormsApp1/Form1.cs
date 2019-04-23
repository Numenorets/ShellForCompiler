using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {    
    public Form1()
        {
            InitializeComponent();
        }
        bool gLoad = false;//глобальная переменная как флаг того что в rtb текст изменился только от загрузки туда инфе
        private void Form1_Load(object sender, EventArgs e)
        {
            сохранитьToolStripButton.Enabled = false;
            сохранитьToolStripMenuItem2.Enabled = false;
            сохранитькакToolStripMenuItem2.Enabled = false;
            отменадействияToolStripMenuItem.Enabled = false;
            восстдействияToolStripMenuItem1.Enabled = false;
            закрытьToolStripMenuItem.Enabled = false;
            toolStripButton1.Enabled = false;
            скомпилироватьToolStripMenuItem1.Enabled = false;
            выполнитьToolStripMenuItem1.Enabled = false;
            скомпилироватьвыпToolStripMenuItem.Enabled = false;
            toolStripButton4.Enabled = false;
            копироватьToolStripMenuItem.Enabled = false;
            вставкаToolStripMenuItem.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void button3_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        public void button2_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            CompileWithRun();
        }
        public void Page()
        {//Добавление страницы с richtexbox'ом,контекстным меню
            TabPage tp = new TabPage();//новая страница
            int tc = (tabControl1.TabCount + 1);//счётчик для названия
            tp.Text = "Unnamed " + tc.ToString();//название
            tp.Tag = "";//тег для хранения пути,"обнулён"
            tabControl1.TabPages.Add(tp);//добавление страницы

            

            

            FastColoredTextBox rtb = new FastColoredTextBox();
            rtb.Dock = DockStyle.Fill;//заполнение места
            rtb.Name = "rtb";
            rtb.Tag = "";//тег для хранения пути,"обнулён"
            rtb.AcceptsTab = true;//tab
            rtb.ContextMenuStrip = contextMenuStrip1;//меню для возможности закрытия док-та
            rtb.TextChanged += new EventHandler<TextChangedEventArgs>(rtb_TextChanged);//делегат для события textchanged
            rtb.SelectionChanged +=new EventHandler(rtb_SelectionChanged);//делегат для события SelectionChanged
            rtb.UndoRedoStateChanged += new EventHandler<EventArgs>(rtb_UndoRedoStateChanged);//делегат для события UndoRedoStateChanged
            rtb.Language = FastColoredTextBoxNS.Language.CSharp;//выбор языка c#
            rtb.AllowMacroRecording = false;//макросы выключены
            rtb.WordWrap = true;//перенос строк
            rtb.CurrentLineColor =System.Drawing.Color.DeepSkyBlue;//подсветка текущей строки
            

            tp.Controls.Add(rtb);//доб-е richtextbox'a в страницу



            tabControl1.SelectedTab=tp;//новая страница сразу становится активной(выбранной)
            сохранитькакToolStripMenuItem2.Enabled = true;//сохранить как включается так как есть хотя бы страница
            закрытьToolStripMenuItem.Enabled = true;//закрыть включается так как есть что закрыть
            toolStripButton1.Enabled = true;//закрыть включается так как есть что закрыть
            скомпилироватьToolStripMenuItem1.Enabled = true;//включение кнопок
            выполнитьToolStripMenuItem1.Enabled = true;//отвечающих за
            скомпилироватьвыпToolStripMenuItem.Enabled = true;//компиляцию,запуск
            toolStripButton4.Enabled = true;///и тд
            вставкаToolStripMenuItem.Enabled = true;//страница есть значит вставка разблокируется
        }

    void rtb_TextChanged(object sender, EventArgs e)
        {
            if (!(tabControl1.SelectedTab == null))
            {
                TabPage selT = tabControl1.SelectedTab;
                FastColoredTextBox  selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (gLoad == true)
                {
                    gLoad = false;//event вызван загрузкой текста из файла,поэтому ничего делать не надо
                    return;//просто выход из метода,предварительно инверсировав флаг(для возможности индикации будущих
                           //загрузок 
                }
                if (selectedRtb.IsChanged == true)//если modified true()
                {                                //то кнопки сохранить разблок,если нет то блок кнопок  
                    сохранитьToolStripButton.Enabled = true;
                    сохранитьToolStripMenuItem2.Enabled = true;
                }
                else
                {
                    сохранитьToolStripButton.Enabled = false;
                    сохранитьToolStripMenuItem.Enabled = false;
                }
            }

        }
        void rtb_SelectionChanged(object sender, EventArgs e)
        {
            if (!(tabControl1.SelectedTab == null))
            {
                TabPage selT = tabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.SelectionLength>0)//если выделеляется больше 0 символов то кнопка копировать разблок
                {
                    копироватьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    копироватьToolStripMenuItem.Enabled = false;
                }
            }
        }
        void rtb_UndoRedoStateChanged(object sender, EventArgs e)
        {
            if (!(tabControl1.SelectedTab == null))
            {
                TabPage selT = tabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.UndoEnabled == true)//блок,разблок кнопки в зав-ти от готовности undo(отмена)
                {
                    отменадействияToolStripMenuItem.Enabled = true;
                }
                else
                {
                    отменадействияToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.RedoEnabled == true)//блок,разблок кнопки в зав-ти от готовности redo(восстановление)
                {
                    восстдействияToolStripMenuItem1.Enabled = true;
                }
                else
                {
                    восстдействияToolStripMenuItem1.Enabled = false;
                }

            }
        }
        public void Save()
        {
            //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
            if (!(tabControl1.SelectedTab==null))
            {
                //selt.tag хранит в себе путь сохранения файла
                //selectedRtb хранит в себе путь открытия файла
                TabPage selT = tabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (File.Exists(selT.Tag.ToString()))
                {
                    File.WriteAllText(selT.Tag.ToString(), selectedRtb.Text, Encoding.UTF8);
                }
                else
                {
                    if (File.Exists(selectedRtb.Tag.ToString()))
                    {
                        File.WriteAllText(selectedRtb.Tag.ToString(), selectedRtb.Text, Encoding.UTF8);
                    }
                    else
                    {
                        SaveAs();//если по предыдущим путям ничего не найдено то предлагается сохранить;
                    }
                }
                сохранитьToolStripButton.Enabled = false;//выключение кнопок сохранения
                сохранитьToolStripMenuItem2.Enabled = false;//
                selectedRtb.IsChanged = false;//флаг того,что текст модифицирован убран
            }

        }
        public void SaveAs()
        {
            //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
            if (!(tabControl1.SelectedTab == null))
            {
                TabPage selT = tabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //показ имени файла без полного пути
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                   {
                        //selt.tag хранит в себе путь сохранения файла
                        //selectedRtb хранит в себе путь открытия файла
                        //создание 2 переменных типа tabpage,richtextbox и заполнение их данными
                        //с текущей  страницы,для того чтобы к элементами на этой странице можно было обратиться
                        //последующая чтение richtextbox'a и запись в file
                        //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
                        //сохранение пути файла(текущей страницы) через tag
                        selT.Tag = saveFileDialog1.FileName.ToString();
                        File.WriteAllText(selT.Tag.ToString(), selectedRtb.Text, Encoding.UTF8);
                        selT.Text = Path.GetFileName(saveFileDialog1.FileName);//имя вкладки=название файла
                        сохранитьToolStripButton.Enabled = false;//выключение кнопок сохранения
                        сохранитьToolStripMenuItem2.Enabled = false;//
                        selectedRtb.IsChanged = false;//флаг того,что текст модифицирован убран
                }
            }
        }
        public void OpenFile()
        {
            //показ имени файла без полного пути
            openFileDialog1.FileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //selt.tag хранит в себе путь сохранения файла
                //selectedRtb хранит в себе путь открытия файла
                //создание 2 переменных типа tabpage,richtextbox и заполнение их данными
                //с текущей  страницы,для того чтобы к элементами на этой странице можно было обратиться
                //последующая чтение file'a и запись в richtexbox
                //сравнение пути открываемого файла и записей путей из сохранения и открытия разных tab'ob и их rtb
                //если совпадают то просто переключает на нужную страницу,нет то создаёт новую
                foreach (TabPage page in tabControl1.TabPages)
                    {
                        if (page.Tag.ToString()==openFileDialog1.FileName)
                        {   
                            tabControl1.SelectTab(page);
                            return;
                        }
                        if (page.Controls["rtb"].Tag.ToString() == openFileDialog1.FileName)
                        {
                            tabControl1.SelectTab(page);
                            return;
                        }
                }
                Page();//создание страницы
                TabPage selT = tabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"]; ;
                selectedRtb.Tag = openFileDialog1.FileName.ToString();
                gLoad = true;//флаг показывающий что изменения в тексте вызваны загрузкой текста
                selectedRtb.Text = File.ReadAllText(selectedRtb.Tag.ToString(), Encoding.UTF8);//выгрузка текста в rtb
                selT.Text =Path.GetFileName(openFileDialog1.FileName);//имя вкладки=название файла
            }
        }
        public void CompileWithRun()
        {
            //проверка на существование активной страницы(если её нет то и данAных нет)
            if (!(tabControl1.SelectedTab==null))
            {
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                {                                                                                   //из сохранения или открытия
                    string path;
                    //путь файла
                    //запись пути из сохранения если нет то из открытия
                    if (File.Exists(selT.Tag.ToString()))
                    {
                        path = selT.Tag.ToString();
                    }
                    else
                    {
                        path = selectedRtb.Tag.ToString();
                    }
                    //получение пути системы,выделение от туда буквы системного диска
                    string sys = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                    string sys2 = Regex.Replace(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), @":\\", "");
                    //Выделение буквы логического диска на котором хранится файл с программным кодом
                    string letter = Regex.Replace((Path.GetPathRoot(path)), @":\\", "");
                    //директория сохранённого файла
                    string dir = Path.GetDirectoryName(path);
                    //имя сохранённого файла
                    string name = Path.GetFileName(path);
                    //имя сохранённого файла без расширения
                    string namex = Path.GetFileNameWithoutExtension(path);
                    //диск на котором программа
                    string prog = Path.GetPathRoot(Application.StartupPath);
                    //Путь к компилятору + имя файла
                    string compiler = $@"C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe";
                    //запуск компилятора
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = $"{compiler}";
                        process.StartInfo.WorkingDirectory = dir;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.Arguments = $@"/out:{namex}.exe  /nologo {path}";
                        process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        process.Start();
                        string outs;//output для csc.exe
                        outs = process.StandardOutput.ReadToEnd().ToString();//запись output
                        richTextBox1.Text = outs;//вывод output'a
                        //ожидание окончания процесса
                        process.WaitForExit();
                        if (outs == "")//если outs "",значит ошибок не было и можно запускать
                        {
                            Run();//метод для запуска exe
                        }
                    }
                }
                else
                {
                    SaveAs();
                }
            }
        }
        public void Run()
        {//проверка на существование активной страницы(если её нет то и данных нет)
            if (!(tabControl1.SelectedTab==null))
                {
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //selt.tag хранит в себе путь сохранения файла
                //selectedRtb хранит в себе путь открытия файла
                string save, open;
                //замена расширения для запуска exe
                save = selT.Tag.ToString().Replace(".cs", ".exe");
                open = selectedRtb.Tag.ToString().Replace(".cs", ".exe");
                //сначала проверка файла из сохранения если нет,то из открытия
                if (File.Exists($@"{save}"))
                {
                    using (Process myProcess = new Process())
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        myProcess.StartInfo.CreateNoWindow = false;
                        myProcess.StartInfo.FileName = $@"{save}";
                        myProcess.Start();
                    }
                }
                else
                {
                    if (File.Exists($@"{open}"))
                    {
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.UseShellExecute = false;
                            myProcess.StartInfo.CreateNoWindow = false;
                            myProcess.StartInfo.FileName = $@"{open}";
                            myProcess.Start();
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show($"Исполняемый файл не скомпилирован.{System.Environment.NewLine}Скомпилировать сейчас?", "Потверждение", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            CompileWithRun();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                        }
                    }
                }
            }     
        }
        public void Compile()
        {
            //проверка на существование активной страницы(если её нет то и данAных нет)
            if (!(tabControl1.SelectedTab == null))
            {
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                {                                                                                   //из сохранения или открытия
                    string path;
                    //путь файла
                    //запись пути из сохранения если нет то из открытия
                    if (File.Exists(selT.Tag.ToString()))
                    {
                        path = selT.Tag.ToString();
                    }
                    else
                    {
                        path = selectedRtb.Tag.ToString();
                    }
                    //получение пути системы,выделение от туда буквы системного диска
                    string sys = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                    string sys2 = Regex.Replace(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), @":\\", "");
                    //Выделение буквы логического диска на котором хранится файл с программным кодом
                    string letter = Regex.Replace((Path.GetPathRoot(path)), @":\\", "");
                    //директория сохранённого файла
                    string dir = Path.GetDirectoryName(path);
                    //имя сохранённого файла
                    string name = Path.GetFileName(path);
                    //имя сохранённого файла без расширения
                    string namex = Path.GetFileNameWithoutExtension(path);
                    //диск на котором программа
                    string prog = Path.GetPathRoot(Application.StartupPath);
                    //Путь к компилятору + имя файла
                    string compiler = $@"C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe";
                    //запуск компилятора
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = $"{compiler}";
                        process.StartInfo.WorkingDirectory = dir;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.Arguments = $@"/out:{namex}.exe  /nologo {path}";
                        process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        process.Start();
                        string outs;//output для csc.exe
                        outs = process.StandardOutput.ReadToEnd().ToString();//запись output
                        richTextBox1.Text = outs;//вывод output'a
                        //ожидание окончания процесса
                        process.WaitForExit();
                    }
                }
                else
                {
                    SaveAs();
                }
            }
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //вызов метода по добавлению страницы
            Page();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Page();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void скомпилироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompileWithRun();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void сохранитьКакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CompileWithRun();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Page();
        }

        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Page();
        }

        private void выполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripButton_Click(object sender, EventArgs e)
        {
            Page();
        }

        private void открытьToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void создатьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Page();
        }

        private void открытьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void сохранитькакToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void отменадействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(tabControl1.SelectedTab==null))
            {
                //ctrl+z отмена
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Undo();
            }
        }
        private void отменадействияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(tabControl1.SelectedTab == null))
            {
                //ctrl+y вперёд
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Redo();
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(tabControl1.SelectedTab==null))
            {
                //скопировать
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Copy();
            }
        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {     
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(tabControl1.SelectedTab==null))
            {
                //вставить 
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Paste();
            }
        }
        private void скомпилироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Compile();
        }

        private void выполнитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(tabControl1.SelectedTab==null))
            {
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                tabControl1.Controls.Remove(selT);//удаление текущей страницы
            }
        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            закрытьToolStripMenuItem_Click(sender, e);
        }

        private void закрытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            закрытьToolStripMenuItem_Click(sender, e);
        }

        private void опрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (!(tabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                сохранитькакToolStripMenuItem2.Enabled = true;//включение сохранить как(ведь страница есть)
                закрытьToolStripMenuItem.Enabled = true;//страница есть,а значит её можно закрыть
                toolStripButton1.Enabled = true;//страница есть,а значит её можно закрыть
                скомпилироватьToolStripMenuItem1.Enabled = true;//отключение кнопок
                выполнитьToolStripMenuItem1.Enabled = true;//отвечающих за
                скомпилироватьвыпToolStripMenuItem.Enabled = true;//компиляцию,запуск
                toolStripButton4.Enabled = true;///и тд
            
                if (selectedRtb.IsChanged == true)
                {
                    сохранитьToolStripButton.Enabled = true;
                    сохранитьToolStripMenuItem2.Enabled = true;
                }
                else
                {
                    сохранитьToolStripButton.Enabled = false;
                    сохранитьToolStripMenuItem2.Enabled = false;
                }
                if (selectedRtb.UndoEnabled == true)//блок,разблок кнопки в зав-ти от готовности undo(отмена)
                {
                    отменадействияToolStripMenuItem.Enabled = true;
                }
                else
                {
                    отменадействияToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.RedoEnabled == true)//блок,разблок кнопки в зав-ти от готовности redo(восстановление)
                {
                    восстдействияToolStripMenuItem1.Enabled = true;
                }
                else
                {
                    восстдействияToolStripMenuItem1.Enabled = false;
                }
                вставкаToolStripMenuItem.Enabled = true;//есть страница значит можно разблокировать кнопку
            }
        }

        private void TabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (!(tabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = tabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (true)
                {

                }
            }
            сохранитькакToolStripMenuItem2.Enabled = false;//при удалении страницы,выключается кнопка
            отменадействияToolStripMenuItem.Enabled = false;//выкл undo,redo
            восстдействияToolStripMenuItem1.Enabled = false;//
            закрытьToolStripMenuItem.Enabled = false;//страницы нет,значит её закрыть нельзя
            toolStripButton1.Enabled = false;//страницы нет,значит её закрыть нельзя
            скомпилироватьToolStripMenuItem1.Enabled = false;//отключение кнопок
            выполнитьToolStripMenuItem1.Enabled = false;//отвечающих за
            скомпилироватьвыпToolStripMenuItem.Enabled = false;//компиляцию,запуск
            toolStripButton4.Enabled = false;///и тд
            копироватьToolStripMenuItem.Enabled = false;//блок кнопок копирования 
            вставкаToolStripMenuItem.Enabled = false;//и вставки
        }

        private void TurnAside_Click(object sender, EventArgs e)
        {
        }
    }
}
//1)ввести где надо проверки try catch для пущей безопаности
//2)подсветка синтаксиса,автодополнение решено(в теории) но осталось грамотно вставить компонент в своей проект
//в нём нет свойства modify,правда есть некоторые новые event'ы как например paste from clipboard(вставить из буфера)
//подумать как и чем заменить modify
//3)вывод ошибок озаглавить,возможно заменить на страницы(page от 2 tabcontrol'a)
//4)в fasttextbox'e есть уже куча функций,надо только грамотно ими воспользоваться,вывести по кнопкам,ненужное убрать
//5)узнать как и какой версией c# пользуется ftb,может быть можно подключить версию поновее
