using System;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace WindowsFormsApp1
{

    public partial class Main : Form
    {
        [DllImport("user32.dll")]//winapi 
        [STAThread]
        static extern bool HideCaret(IntPtr hWnd);//для скрытия курсора
        string compiler;//для хранения пути компилятора
        bool gLoad = false;//глобальная переменная как флаг того что в rtb текст изменился только от загрузки туда инфе
        public Main()
        {
            Thread t = new Thread(new ThreadStart(Splash));//поток с загрузочным окном
            t.Start();
            Thread.Sleep(2000);
            t.Abort();
            InitializeComponent();
            richTextBox1.Cursor = Cursors.Arrow; // изменение курсора для элемента на обычный
        }
        private void Splash()
        {
            Application.Run(new SplashScreen());//запуск загрузочного окна
        }
        private void Main_Load(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.Enabled = false;//выключение кнопок в 
            сохранитьКакToolStripMenuItem.Enabled = false;
            отменаДействияToolStripMenuItem.Enabled = false;
            восстановитьToolStripMenuItem.Enabled = false;
            закрытьToolStripMenuItem.Enabled = false;
            скомпилироватьToolStripMenuItem.Enabled = false;
            выполнитьToolStripMenuItem1.Enabled = false;
            СкомпилироватьИВыполнитьToolStripMenuItem.Enabled = false;
            копироватьToolStripMenuItem.Enabled = false;
            вставкаToolStripMenuItem.Enabled = false;
            выделитьВсёToolStripMenuItem.Enabled = false;
            вВерхнийРегистрToolStripMenuItem.Enabled = false;
            вНижнийРегистрToolStripMenuItem.Enabled = false;
            вставитьубратьКомментарииToolStripMenuItem.Enabled = false;
            режимВставкиИЗаменыToolStripMenuItem.Enabled = false;
            убратьТекущуюСтрокуToolStripMenuItem.Enabled = false;
            найтиToolStripMenuItem.Enabled = false;
            заменитьToolStripMenuItem.Enabled = false;
            перейтиКСтрокеToolStripMenuItem.Enabled = false;
            приблизитьToolStripMenuItem.Enabled = false;
            отдалитьToolStripMenuItem.Enabled = false;
            вырезатьToolStripMenuItem.Enabled = false;
            добавитьЗакладкуToolStripMenuItem.Enabled = false;
            убратьЗакладкуToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem1.Enabled = false;
            вставитьГотовыйЭкземплярToolStripMenuItem.Enabled = false;
            классаToolStripMenuItem.Enabled = false;
            СтруктурыКонсольtoolStripMenuItem7.Enabled = false;
            структурыГрафическогоПриложенияToolStripMenuItem.Enabled = false;
            консольноеПриложениеToolStripMenuItem.Enabled = false;
            приложениеСГрафическимИнтерфейсомToolStripMenuItem.Enabled = false;

            toolStripSaveF.Enabled = false;//выключение кнопок на панели инструментов
            toolStripCloseF.Enabled = false;
            toolStripFind.Enabled = false;
            toolStripZoomPlus.Enabled = false;
            toolStripZoomMinus.Enabled = false;
            toolStripCompRum.Enabled = false;
            //выделение буквы системного диска
            string sys2 = Regex.Replace(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)), @":\\", "");
            //Путь к компилятору + логический диск
            if (File.Exists($@"{sys2}:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe") == true)
            {
                compiler = $@"{sys2}:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";
            }
            else
            {
                if (File.Exists($@"{sys2}:\Windows\Microsoft.NET\Framework\v3.5\csc.exe") == true)
                {
                    compiler = $@"{sys2}:\Windows\Microsoft.NET\Framework\v3.5\csc.exe";
                }
                else
                {
                    if (File.Exists($@"{sys2}:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe") == true)
                    {
                        compiler = $@"{sys2}:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe";
                    }
                }
            }
        }
        public void Page()
        {//Добавление страницы с richtexbox'ом,контекстным меню
            TabPage tp = new TabPage();//новая страница
            int tc = (metroTabControl1.TabCount + 1);//счётчик для названия
            tp.Text = "Unnamed " + tc.ToString();//название
            tp.Tag = "";//тег для хранения пути,"обнулён"
            metroTabControl1.TabPages.Add(tp);//добавление страницы
            FastColoredTextBox rtb = new FastColoredTextBox();
            rtb.Dock = DockStyle.Fill;//заполнение места
            rtb.Name = "rtb";
            rtb.Tag = "";//тег для хранения пути,"обнулён"
            rtb.AcceptsTab = true;//tab
            rtb.ContextMenuStrip = metroContextMenu1;//меню для возможности закрытия док-та
            rtb.TextChanged += new EventHandler<TextChangedEventArgs>(rtb_TextChanged);//делегат для события textchanged
            rtb.SelectionChanged += new EventHandler(rtb_SelectionChanged);//делегат для события SelectionChanged
            rtb.UndoRedoStateChanged += new EventHandler<EventArgs>(rtb_UndoRedoStateChanged);//делегат для события UndoRedoStateChanged
            rtb.Language = FastColoredTextBoxNS.Language.CSharp;//выбор языка c#
            rtb.AllowMacroRecording = false;//макросы выключены
            rtb.WordWrap = true;//перенос строк
            rtb.CurrentLineColor = System.Drawing.Color.DeepSkyBlue;//подсветка текущей строки
            rtb.MouseDoubleClick += Rtb_MouseDoubleClick;
            //создание autocomplete popup menu
            AutocompleteMenu popupMenu;//контекстное меню
            popupMenu = new AutocompleteMenu(rtb);
            popupMenu.ForeColor = Color.White;
            popupMenu.BackColor = Color.Black;
            popupMenu.SelectedColor = Color.LightBlue;
            popupMenu.SearchPattern = @"[\w\.]";
            popupMenu.AllowTabKey = true;
            popupMenu.AlwaysShowTooltip = true;
            //assign DynamicCollection as items source
            popupMenu.Items.SetAutocompleteItems(new DynamicCollection(popupMenu, rtb));
            tp.Controls.Add(rtb);//доб-е richtextbox'a в страницу
            metroTabControl1.SelectedTab = tp;//новая страница сразу становится активной(выбранной)
            сохранитьКакToolStripMenuItem.Enabled = true;//сохранить как включается так как есть хотя бы страница
            закрытьToolStripMenuItem.Enabled = true;//закрыть включается так как есть что закрыть
            скомпилироватьToolStripMenuItem.Enabled = true;//включение кнопок
            выполнитьToolStripMenuItem1.Enabled = true;//отвечающих за
            СкомпилироватьИВыполнитьToolStripMenuItem.Enabled = true;//компиляцию,запуск
            вставкаToolStripMenuItem.Enabled = true;//страница есть значит вставка разблокируется
            выделитьВсёToolStripMenuItem.Enabled = true;//включение кнопок на панели инструментов,меню
            вВерхнийРегистрToolStripMenuItem.Enabled = true;
            вНижнийРегистрToolStripMenuItem.Enabled = true;
            вставитьубратьКомментарииToolStripMenuItem.Enabled = true;
            режимВставкиИЗаменыToolStripMenuItem.Enabled = true;
            убратьТекущуюСтрокуToolStripMenuItem.Enabled = true;
            найтиToolStripMenuItem.Enabled = true;
            заменитьToolStripMenuItem.Enabled = true;
            перейтиКСтрокеToolStripMenuItem.Enabled = true;
            приблизитьToolStripMenuItem.Enabled = true;
            отдалитьToolStripMenuItem.Enabled = true;
            добавитьЗакладкуToolStripMenuItem.Enabled = true;
            убратьЗакладкуToolStripMenuItem.Enabled = true;
            перейтиКЗакладкеToolStripMenuItem.Enabled = true;
            перейтиКЗакладкеToolStripMenuItem1.Enabled = true;
            toolStripCloseF.Enabled = true;
            toolStripFind.Enabled = true;
            toolStripZoomPlus.Enabled = true;
            toolStripZoomMinus.Enabled = true;
            toolStripCompRum.Enabled = true;
            вставитьГотовыйЭкземплярToolStripMenuItem.Enabled = true;
            классаToolStripMenuItem.Enabled = true;
            СтруктурыКонсольtoolStripMenuItem7.Enabled = true;
            структурыГрафическогоПриложенияToolStripMenuItem.Enabled = true;
            консольноеПриложениеToolStripMenuItem.Enabled = true;
            приложениеСГрафическимИнтерфейсомToolStripMenuItem.Enabled = true;
        }
        internal class DynamicCollection : IEnumerable<AutocompleteItem>
        {
            private AutocompleteMenu menu;
            private FastColoredTextBox tb;

            public DynamicCollection(AutocompleteMenu menu, FastColoredTextBox tb)
            {
                this.menu = menu;
                this.tb = tb;
            }

            public IEnumerator<AutocompleteItem> GetEnumerator()
            {
                //get current fragment of the text
                var text = menu.Fragment.Text;

                //extract class name (part before dot)
                var parts = text.Split('.');
                if (parts.Length < 2)
                    yield break;
                var className = parts[parts.Length - 2];

                //find type for given className
                var type = FindTypeByName(className);

                if (type == null)
                    yield break;

                //return static methods of the class
                foreach (var methodName in type.GetMethods().AsEnumerable().Select(mi => mi.Name).Distinct())
                    yield return new MethodAutocompleteItem(methodName + "()")
                    {
                        ToolTipTitle = methodName,
                        //ToolTipText = "Description of method " + methodName + " goes here.",
                    };

                //return static properties of the class
                foreach (var pi in type.GetProperties())
                    yield return new MethodAutocompleteItem(pi.Name)
                    {
                        ToolTipTitle = pi.Name,
                        //ToolTipText = "Description of property " + pi.Name + " goes here.",
                    };
            }

            Type FindTypeByName(string name)
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Type type = null;
                foreach (var a in assemblies)
                {
                    foreach (var t in a.GetTypes())
                        if (t.Name == name)
                        {
                            return t;
                        }
                }

                return null;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }//dynamic collection для контекстное меню
        private void Rtb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (e.X < selectedRtb.LeftIndent)//если клик был по левой зоне компонента(до текста,в зоне строк)
                {
                    var place = selectedRtb.PointToPlace(e.Location);//запись места клика
                    if (selectedRtb.Bookmarks.Contains(place.iLine))//если закладка есть убирает,нет - ставит
                        selectedRtb.Bookmarks.Remove(place.iLine);
                    else
                        selectedRtb.Bookmarks.Add(place.iLine);
                }
            }
        }
        void rtb_TextChanged(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (gLoad == true)
                {
                    gLoad = false;//event вызван загрузкой текста из файла,поэтому ничего делать не надо
                    selectedRtb.IsChanged = false;//изменения сброшены
                    return;//просто выход из метода,предварительно инверсировав флаг(для возможности индикации будущих
                           //загрузок 
                }
                if (selectedRtb.IsChanged == true)//если modified true()
                {                                //то кнопки сохранить разблок,если нет то блок кнопок  ;
                    сохранитьToolStripMenuItem.Enabled = true;
                    toolStripSaveF.Enabled = true;
                }
                else
                {
                    сохранитьToolStripMenuItem.Enabled = false;
                    toolStripSaveF.Enabled = false;
                }
            }
        }
        void rtb_SelectionChanged(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.SelectionColor = Color.Blue;//смена цвета на стандартный
                if (selectedRtb.SelectionLength > 0)//если выделеляется больше 0 символов то кнопка копировать разблок
                {
                    копироватьToolStripMenuItem.Enabled = true;
                    вырезатьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    копироватьToolStripMenuItem.Enabled = false;
                    вырезатьToolStripMenuItem.Enabled = false;
                }
            }
        }
        void rtb_UndoRedoStateChanged(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.UndoEnabled == true)//блок,разблок кнопки в зав-ти от готовности undo(отмена)
                {
                    отменаДействияToolStripMenuItem.Enabled = true;
                }
                else
                {
                    отменаДействияToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.RedoEnabled == true)//блок,разблок кнопки в зав-ти от готовности redo(восстановление)
                {
                    восстановитьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    восстановитьToolStripMenuItem.Enabled = false;
                }
            }
        }
        public void Save()
        {
            //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //selt.tag хранит в себе путь сохранения файла
                //selectedRtb хранит в себе путь открытия файла
                TabPage selT = metroTabControl1.SelectedTab;
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
                toolStripSaveF.Enabled = false;//выключение кнопок сохранения
                сохранитьToolStripMenuItem.Enabled = false;//
                selectedRtb.IsChanged = false;//флаг того,что текст модифицирован убран
            }
        }
        public void SaveAs()
        {
            //проверка на существование активной страницы(если её нет то нет страниц вообще и сохранять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;
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
                    toolStripSaveF.Enabled = false;//выключение кнопок сохранения
                    сохранитьToolStripMenuItem.Enabled = false;//
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
                foreach (TabPage page in metroTabControl1.TabPages)
                {
                    if (page.Tag.ToString() == openFileDialog1.FileName)
                    {
                        metroTabControl1.SelectTab(page);
                        return;
                    }
                    if (page.Controls["rtb"].Tag.ToString() == openFileDialog1.FileName)
                    {
                        metroTabControl1.SelectTab(page);
                        return;
                    }
                }
                Page();//создание страницы
                TabPage selT = metroTabControl1.SelectedTab;
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"]; ;
                selectedRtb.Tag = openFileDialog1.FileName.ToString();
                gLoad = true;//флаг показывающий что изменения в тексте вызваны загрузкой текста
                selectedRtb.Text = File.ReadAllText(selectedRtb.Tag.ToString(), Encoding.UTF8);//выгрузка текста в rtb
                selT.Text = Path.GetFileName(openFileDialog1.FileName);//имя вкладки=название файла
            }
        }
        public void CompileWithRun()
        {
            //проверка на существование активной страницы(если её нет то и данAных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
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
                    //модификация переменных для корректной работы с путями содержащими пробелы 
                    path = $@"""{path}""";
                    namex = $@"""{namex}""";
                    //определение типа тип выходных данных
                    string target = "-target:exe";//стандарт это консольное приложение
                    if (консольноеПриложениеToolStripMenuItem.Checked == true)
                    {
                        target = "-target:exe";
                    }
                    else
                    {
                        if (приложениеСГрафическимИнтерфейсомToolStripMenuItem.Checked == true)
                        {
                            target = "-target:winexe";//граф.приложение
                        }
                    }
                    //запуск компилятора
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = $"{compiler}";
                        process.StartInfo.WorkingDirectory = dir;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.Arguments = $@"/out:{namex}.exe {target} /nologo -optimize -warn:0  {path}";
                        process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        process.Start();
                        bool flag = false;//флаг для ошибок
                        string outs;//output для csc.exe
                        outs = process.StandardOutput.ReadToEnd().ToString();//запись output
                        richTextBox1.Text = outs;//вывод output'a
                        if (outs.Length > 0)//если в output'e больше 0 символов значит есть ошибка и флаг компиляции устанвливается false
                        {
                            flag = true;
                        }
                        //ожидание окончания процесса
                        process.WaitForExit();
                        if (flag == true)
                        {
                            for (int i = 0; i < richTextBox1.Lines.Length; i++)
                            {
                                if (Regex.IsMatch(richTextBox1.Lines[i], @"(\d+),(\d+)") == true)
                                {
                                    Match Number;
                                    Number = Regex.Match(richTextBox1.Lines[i], @"(\d+),(\d+)");//поиск 2 значений(номер строки,номер символа)
                                    int NumStr = Convert.ToInt32(Number.Groups[1].ToString()) - 1;//номер строки
                                    int NumChr = Convert.ToInt32(Number.Groups[2].ToString());//номер символа где что-то ожидалось  
                                    selectedRtb.Selection = selectedRtb.GetLine(NumStr);//выделение нужной строки
                                    selectedRtb.SelectionColor = Color.Red;//смена цвета
                                    break;//выход из цикла
                                }
                            }
                        }
                        if (flag == false)//если flag==false значит ошибок не было и можно запускать
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
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
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
                        DialogResult dialogResult = MessageBox.Show($"Исполняемый файл не скомпилирован.{System.Environment.NewLine}Скомпилировать сейчас?", "Потверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
            if (!(metroTabControl1.SelectedTab == null))
            {
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
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
                    //получение пути системы
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
                    //модификация переменных для корректной работы с путями содержащими пробелы 
                    path = $@"""{path}""";
                    namex = $@"""{namex}""";
                    //запуск компилятора
                    //определение типа тип выходных данных
                    string target = "-target:exe";//стандарт это консольное приложение
                    if (консольноеПриложениеToolStripMenuItem.Checked == true)
                    {
                        target = "-target:exe";
                    }
                    else
                    {
                        if (приложениеСГрафическимИнтерфейсомToolStripMenuItem.Checked == true)
                        {
                            target = "-target:winexe";//граф.приложение
                        }
                    }
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = $"{compiler}";
                        process.StartInfo.WorkingDirectory = dir;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.Arguments = $@"/out:{namex}.exe {target} /nologo {path}";
                        process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                        process.Start();
                        bool flag = false;//флаг для ошибок
                        string outs;//output для csc.exe
                        outs = process.StandardOutput.ReadToEnd().ToString();//запись output
                        richTextBox1.Text = outs;//вывод output'a
                        //ожидание окончания процесса
                        process.WaitForExit();
                        richTextBox1.Text = outs;//вывод output'a
                        if (outs.Length > 0)//если в output'e больше 0 символов значит есть ошибка и флаг компиляции устанвливается false
                        {
                            flag = true;
                        }
                        //ожидание окончания процесса
                        process.WaitForExit();
                        if (flag == true)
                        {
                            for (int i = 0; i < richTextBox1.Lines.Length; i++)
                            {
                                if (Regex.IsMatch(richTextBox1.Lines[i], @"(\d+),(\d+)") == true)
                                {
                                    Match Number;
                                    Number = Regex.Match(richTextBox1.Lines[i], @"(\d+),(\d+)");//поиск 2 значений(номер строки,номер символа)
                                    int NumStr = Convert.ToInt32(Number.Groups[1].ToString()) - 1;//номер строки
                                    int NumChr = Convert.ToInt32(Number.Groups[2].ToString());//номер символа где что-то ожидалось  
                                    selectedRtb.Selection = selectedRtb.GetLine(NumStr);//выделение нужной строки
                                    selectedRtb.SelectionColor = Color.Red;//смена цвета
                                    break;//выход из цикла
                                }
                            }
                        }
                    }
                }
                else
                {
                    SaveAs();
                }
            }
        }
        private void MetroTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы (значит она есть)
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                сохранитьКакToolStripMenuItem.Enabled = true;//включение сохранить как(ведь страница есть)
                закрытьToolStripMenuItem.Enabled = true;//страница есть,а значит её можно закрыть
                скомпилироватьToolStripMenuItem.Enabled = true;//отключение кнопок
                выполнитьToolStripMenuItem1.Enabled = true;//отвечающих за
                СкомпилироватьИВыполнитьToolStripMenuItem.Enabled = true;//компиляцию,запуск///и тд
                выделитьВсёToolStripMenuItem.Enabled = true;
                вВерхнийРегистрToolStripMenuItem.Enabled = true;
                вНижнийРегистрToolStripMenuItem.Enabled = true;
                вставитьубратьКомментарииToolStripMenuItem.Enabled = true;
                режимВставкиИЗаменыToolStripMenuItem.Enabled = true;
                убратьТекущуюСтрокуToolStripMenuItem.Enabled = true;
                найтиToolStripMenuItem.Enabled = true;
                заменитьToolStripMenuItem.Enabled = true;
                перейтиКСтрокеToolStripMenuItem.Enabled = true;
                приблизитьToolStripMenuItem.Enabled = true;
                отдалитьToolStripMenuItem.Enabled = true;
                добавитьЗакладкуToolStripMenuItem.Enabled = true;
                убратьЗакладкуToolStripMenuItem.Enabled = true;
                перейтиКЗакладкеToolStripMenuItem.Enabled = true;
                перейтиКЗакладкеToolStripMenuItem1.Enabled = true;
                вставитьГотовыйЭкземплярToolStripMenuItem.Enabled = true;
                классаToolStripMenuItem.Enabled = true;
                СтруктурыКонсольtoolStripMenuItem7.Enabled = true;
                структурыГрафическогоПриложенияToolStripMenuItem.Enabled = true;
                консольноеПриложениеToolStripMenuItem.Enabled = true;
                приложениеСГрафическимИнтерфейсомToolStripMenuItem.Enabled  = true;
                toolStripCloseF.Enabled = true;
                toolStripFind.Enabled = true;
                toolStripZoomPlus.Enabled = true;
                toolStripZoomMinus.Enabled = true;
                toolStripCompRum.Enabled = true;
                if (selectedRtb.IsChanged == true)
                {
                    toolStripSaveF.Enabled = true;
                    сохранитьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    toolStripSaveF.Enabled = false;
                    сохранитьToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.UndoEnabled == true)//блок,разблок кнопки в зав-ти от готовности undo(отмена)
                {
                    отменаДействияToolStripMenuItem.Enabled = true;
                }
                else
                {
                    отменаДействияToolStripMenuItem.Enabled = false;
                }
                if (selectedRtb.RedoEnabled == true)//блок,разблок кнопки в зав-ти от готовности redo(восстановление)
                {
                    восстановитьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    восстановитьToolStripMenuItem.Enabled = false;
                }
                вставкаToolStripMenuItem.Enabled = true;//есть страница значит можно разблокировать кнопку
            }
        }

        private void MetroTabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            сохранитьКакToolStripMenuItem.Enabled = false;//при удалении страницы,выключается кнопка
            сохранитьToolStripMenuItem.Enabled = false;
            отменаДействияToolStripMenuItem.Enabled = false;//выкл undo,redo
            восстановитьToolStripMenuItem.Enabled = false;//
            закрытьToolStripMenuItem.Enabled = false;//страницы нет,значит её закрыть нельзя
            скомпилироватьToolStripMenuItem.Enabled = false;//отключение кнопок
            выполнитьToolStripMenuItem1.Enabled = false;//отвечающих за
            СкомпилироватьИВыполнитьToolStripMenuItem.Enabled = false;//компиляцию,запуск///и тд
            копироватьToolStripMenuItem.Enabled = false;//блок кнопок копирования 
            вставкаToolStripMenuItem.Enabled = false;//и вставки
            вырезатьToolStripMenuItem.Enabled = false;/////
            выделитьВсёToolStripMenuItem.Enabled = false;
            вВерхнийРегистрToolStripMenuItem.Enabled = false;
            вНижнийРегистрToolStripMenuItem.Enabled = false;
            вставитьубратьКомментарииToolStripMenuItem.Enabled = false;
            режимВставкиИЗаменыToolStripMenuItem.Enabled = false;
            убратьТекущуюСтрокуToolStripMenuItem.Enabled = false;
            найтиToolStripMenuItem.Enabled = false;
            заменитьToolStripMenuItem.Enabled = false;
            перейтиКСтрокеToolStripMenuItem.Enabled = false;
            приблизитьToolStripMenuItem.Enabled = false;
            отдалитьToolStripMenuItem.Enabled = false;
            добавитьЗакладкуToolStripMenuItem.Enabled = false;
            убратьЗакладкуToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem.Enabled = false;
            перейтиКЗакладкеToolStripMenuItem1.Enabled = false;
            вставитьГотовыйЭкземплярToolStripMenuItem.Enabled = false;
            СтруктурыКонсольtoolStripMenuItem7.Enabled = false;
            структурыГрафическогоПриложенияToolStripMenuItem.Enabled = false;
            консольноеПриложениеToolStripMenuItem.Enabled = false;
            приложениеСГрафическимИнтерфейсомToolStripMenuItem.Enabled = false;
            классаToolStripMenuItem.Enabled = false;
            toolStripSaveF.Enabled = false;
            toolStripCloseF.Enabled = false;
            toolStripFind.Enabled = false;
            toolStripZoomPlus.Enabled = false;
            toolStripZoomMinus.Enabled = false;
            toolStripCompRum.Enabled = false;
        }
        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void RichTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(richTextBox1.Handle);//скрытие курсора
        }
        private void ToolStripCreateF_Click(object sender, EventArgs e)
        {
            создатьToolStripMenuItem.PerformClick();
        }
        private void ToolStripOpenF_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem.PerformClick();
        }
        private void ToolStripSaveF_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.PerformClick();
        }
        private void ToolStripCloseF_Click(object sender, EventArgs e)
        {
        }
        private void ToolStripFind_Click(object sender, EventArgs e)
        {
            найтиToolStripMenuItem.PerformClick();
        }

        private void ToolStripZoomPlus_Click(object sender, EventArgs e)
        {
            приблизитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripZoomMinus_Click(object sender, EventArgs e)
        {
            отдалитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripCompRum_Click(object sender, EventArgs e)
        {
            СкомпилироватьИВыполнитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripCreateF_Click_1(object sender, EventArgs e)
        {
            создатьToolStripMenuItem.PerformClick();
        }
        private void СоздатьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Page();
        }

        private void ОткрытьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void СохранитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Save();
        }

        private void СохранитьКакToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveAs();
        }
        private void ВыходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ОтменаДействияToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //ctrl+z отмена
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Undo();
            }
        }
        private void ВосстановитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //ctrl+y вперёд
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Redo();
            }
        }
        private void КопироватьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //скопировать
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Copy();
            }
        }
        private void ВставкаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и данных нет)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //вставить 
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Paste();
            }
        }
        private void ВырезатьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Cut();
                вырезатьToolStripMenuItem.Enabled = false;
            }
        }
        private void ВыделитьВсёToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.SelectAll();//вызов диалога выделить всё
                selectedRtb.OnSelectionChanged();//вызов события о изменениях в выделении
            }
        }
        private void ВВерхнийРегистрToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.UpperCase();
            }
        }
        private void ВНижнийРегистрToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.LowerCase();
            }
        }
        private void ВставитьубратьКомментарииToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.CommentSelected();
            }
        }
        private void РежимВставкиИЗаменыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                SendKeys.Send("{Insert}");
            }
        }
        private void УбратьТекущуюСтрокуToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.ClearCurrentLine();
            }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.IsChanged == true)//есть ли изменения которые можно сохранить
                {
                    DialogResult dialogResult = MessageBox.Show($"Сохранить изменения в {selT.Text}", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                        {                                                                                   //из сохранения или открытия(проверка на есть ли )
                            if (File.Exists(selT.Tag.ToString()))                                           //реальный файл или просто вкладка без ничего
                            {
                                Save();//если файл есть то сохранение либо в путь сохранения либо в путь откытия
                            }
                            else
                            {
                                Save();
                            }
                        }
                        else
                        {
                            SaveAs();//файла нет значит сохранить как
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //user нажал нет значит ничего делать не надо
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;//отмена закрытия формы
                    }

                }
            }
        }
        private void ПерейтиКЗакладкеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //при переключении страницы на главную то если есть изменения кнопка не блокируется
            TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
            FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
            selectedRtb.GotoNextBookmark(selectedRtb.Selection.Start.iLine);
        }
        private void ПерейтиКЗакладкеToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            //при переключении страницы на главную то если есть изменения кнопка не блокируется
            TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
            FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
            selectedRtb.GotoPrevBookmark(selectedRtb.Selection.Start.iLine);
        }
        private void НайтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.ShowFindDialog();//вызов диалога поиска
            }
        }
        private void ЗаменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                //проверка на существование активной страницы(если её нет то и удалять нечего)
                if (!(metroTabControl1.SelectedTab == null))
                {
                    //при переключении страницы на главную то если есть изменения кнопка не блокируется
                    TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                    FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                    selectedRtb.ShowReplaceDialog();//вызов диалога поиска + замены
                }
            }
        }

        private void ПерейтиКСтрокеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.ShowGoToDialog();//вызов диалога перейти к
            }
        }

        private void ПриблизитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Zoom = selectedRtb.Zoom + 10;
            }
        }

        private void ОтдалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                selectedRtb.Zoom = selectedRtb.Zoom - 10;
            }
        }

        private void СкомпилироватьИВыполнитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CompileWithRun();
        }

        private void СкомпилироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compile();
        }

        private void ВыполнитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgramm About = new AboutProgramm();
            About.ShowDialog();
        }

        private void ToolStripOpenF_Click_1(object sender, EventArgs e)
        {
            открытьToolStripMenuItem.PerformClick();
        }

        private void ToolStripSaveF_Click_1(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripCloseF_Click_1(object sender, EventArgs e)
        {
            закрытьToolStripMenuItem.PerformClick();
        }

        private void ToolStripFind_Click_1(object sender, EventArgs e)
        {
            найтиToolStripMenuItem.PerformClick();
        }

        private void ToolStripZoomPlus_Click_1(object sender, EventArgs e)
        {
            приблизитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripZoomMinus_Click_1(object sender, EventArgs e)
        {
            отдалитьToolStripMenuItem.PerformClick();
        }

        private void ToolStripCompRum_Click_1(object sender, EventArgs e)
        {
            СкомпилироватьИВыполнитьToolStripMenuItem.PerformClick();
        }

        private void RichTextBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            HideCaret(richTextBox1.Handle);//скрытие курсора
        }

        private void ЗакрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //проверка на существование активной страницы(если её нет то и удалять нечего)
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                if (selectedRtb.IsChanged == true)//есть ли изменения которые можно сохранить
                {

                    DialogResult dialogResult = MessageBox.Show($"Сохранить изменения в {selT.Text}", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if ((File.Exists(selT.Tag.ToString())) || (File.Exists(selectedRtb.Tag.ToString())))//Проверка на существование путей
                        {                                                                                   //из сохранения или открытия(проверка на есть ли )
                            if (File.Exists(selT.Tag.ToString()))                                           //реальный файл или просто вкладка без ничего
                            {
                                Save();//если файл есть то сохранение либо в путь сохранения либо в путь откытия
                            }
                            else
                            {
                                Save();
                            }
                        }
                        else
                        {
                            SaveAs();//файла нет значит сохранить как
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //user нажал нет значит ничего делать не надо
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        return;//user отменил решение
                    }
                }
                metroTabControl1.Controls.Remove(selT);//удаление текущей страницы
            }
        }

        private void ДобавитьЗакладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //selectedRtb.UnbookmarkLine(Place.Empty.iLine);
                selectedRtb.BookmarkLine(selectedRtb.Selection.Start.iLine);
            }
        }

        private void УбратьЗакладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                //selectedRtb.UnbookmarkLine(Place.Empty.iLine);
                selectedRtb.UnbookmarkLine(selectedRtb.Selection.Start.iLine);

            }
        }
        private void ПерейтиКЗакладкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //при переключении страницы на главную то если есть изменения кнопка не блокируется
            TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
            FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
            selectedRtb.GotoNextBookmark(selectedRtb.Selection.Start.iLine);
        }

        private void ПерейтиКЗакладкеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //при переключении страницы на главную то если есть изменения кнопка не блокируется
            TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
            FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
            selectedRtb.GotoPrevBookmark(selectedRtb.Selection.Start.iLine);
        }

        private void ЗакрытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ЗакрытьToolStripMenuItem_Click(sender, e);
        }
        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {

            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                string ExampleProgramm =
@"using System;

class Application
{
    static void Main()
    {
     Console.ReadLine();
    }
}";
                selectedRtb.AppendText(ExampleProgramm);
            }//пример структуры программы
        }

        private void КлассаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                string ExampleClass =
@"
class Application
{

}";
                selectedRtb.AppendText(ExampleClass);
            }//пример класса
        }
        private void КонсольноеПриложениеToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (консольноеПриложениеToolStripMenuItem.Checked==true)//если стал true,то true в другом item'e убираем
            {
                if (приложениеСГрафическимИнтерфейсомToolStripMenuItem.Checked == true)
                {
                    приложениеСГрафическимИнтерфейсомToolStripMenuItem.Checked = false;
                }
            }

        }
        private void ПриложениеСГрафическимИнтерфейсомToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (приложениеСГрафическимИнтерфейсомToolStripMenuItem.Checked==true)//если стал true,то true в другом item'e убираем
            {
                if (консольноеПриложениеToolStripMenuItem.Checked == true)
                {
                    консольноеПриложениеToolStripMenuItem.Checked = false;
                }
            }
        }

        private void КонсольноеПриложениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (консольноеПриложениеToolStripMenuItem.Checked == true)//если был true,оставляем старое 
            {
            }
            else                                                      //если бы false то меняем на true
            {
                консольноеПриложениеToolStripMenuItem.Checked = true;
            }
        }

        private void ПриложениеСГрафическимИнтерфейсомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (приложениеСГрафическимИнтерфейсомToolStripMenuItem.Checked == true)//если был true,оставляем старое 
            {

            }
            else                                                      //если бы false то меняем на true
            {
                приложениеСГрафическимИнтерфейсомToolStripMenuItem.Checked = true;
            }
        }

        private void СтруктурыГрафическогоПриложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!(metroTabControl1.SelectedTab == null))
            {
                //при переключении страницы на главную то если есть изменения кнопка не блокируется
                TabPage selT = metroTabControl1.SelectedTab;//активная страница(selected) с её rtb,и собственными данными
                FastColoredTextBox selectedRtb = (FastColoredTextBox)selT.Controls["rtb"];
                string ExampleGraphicProgramm =@"using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormWithButton
{
    public class Form1 : Form
    {
        public Button button1;
        public Form1()
        {
            button1 = new Button();
            button1.Size = new Size(40, 40);
            button1.Location = new Point(30, 30);
            button1.Text = ""Click me"";
            this.Controls.Add(button1);
                button1.Click += new EventHandler(button1_Click);
            }
            private void button1_Click(object sender, EventArgs e)
            {
                MessageBox.Show(""Hello World"");
            }
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}";
                selectedRtb.AppendText(ExampleGraphicProgramm);
            }//пример структуры графического приложения

        }

        private void Main_Shown(object sender, EventArgs e)
        {
        }
    }
}
//1)ввести где надо проверки try catch для пущей безопаности
//вызов подсветки строки из-за клика по  ошибке